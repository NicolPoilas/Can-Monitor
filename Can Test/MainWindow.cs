using canTransport;
using Dongzr.MidiLite;
using SecurityAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Uds;

namespace Can_Test
{
    public partial class Form1 : Form
    {
        can_driver driver = new can_driver();
        canTrans driverTrans = new canTrans();
        SecurityKey securityDriver = new SecurityKey();

        List<cmd> cmd_list = new List<cmd>();
        cmd cmd_shortkey = new cmd(new string[18], "");
        bool shortkey_en = false;
        int cmd_string_count = 0;
        int i = 0;
        byte[] RxMsgs = new byte[0];

        public enum UnhandledExceptionMode
        {
            Automatic,         //将所有异常都传送到 ThreadException 处理程序，除非应用程序的配置文件指定了其他位置。 
            ThrowException,     //从不将异常传送到 ThreadException 处理程序。忽略应用程序配置文件。
            CatchException         //始终将异常传送到 ThreadException 处理程序。忽略应用程序配置文件。
        }

        public Form1()
        {
            InitializeComponent();
            BusParamsInit();
            mmTime_init();
            trans_init();
            auto_load_file();
        }
        #region BusSetting
        private void BusParamsInit()
        {
            string[] channel = new string[0];
            channel = driver.GetChannel();
            comboBoxCanDevice.Items.Clear();
            comboBoxCanDevice.Items.AddRange(channel);//add items for comboBox
            comboBoxCanDevice.SelectedIndex = 0;//default select the first , physical driver always come first
            comboBoxCanBaudRate.SelectedIndex = 4;//default select 500K                                   
        }
        //取消占用总线
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (BusButtontoolstrip.Text == "Off-Line")//bus on和 bus off 对整个应用程序（包括子窗体）有绝对的控制功能
            {
                if (driver.OpenChannel(comboBoxCanDevice.SelectedIndex, comboBoxCanBaudRate.Text) == true)
                {
                    BusButtontoolstrip.Text = "On-Line";
                    tabControl1.SelectedIndex = 1;//自动跳入诊断界面
                    driverTrans.Start();
                    mmTimer.Start();
                    groupBox2.Enabled = true;
                    //新建记事本命名为当前时间转换的日期，存储文件记录
                    //使能状态栏信息可视
                    //产生一个事件通知其他所有需要用总线的控件成为可用
                }
                else
                {
                    MessageBox.Show("打开" + comboBoxCanDevice.Text + "通道失败!");  //最好能把原因定位出来 给故障编码写入帮助文件
                }
            }
            else
            {
                driver.CloseChannel();
                BusButtontoolstrip.Text = "Off-Line";
                groupBox2.Enabled = false;
                driverTrans.Stop();
                mmTimer.Stop();
            }
        }
        #endregion

        #region Trans
        private void DisplayTxFarm(object sender, EventArgs e)
        {
            canTrans.FarmsEventArgs args = (canTrans.FarmsEventArgs)e;//实例化一个参数类
            EventHandler TextBoxDisplayUpdate = delegate //richTextBoxDisplay 主线程创建控件  访问此方法是trans线程 设计跨线程调用 必须用委托 
            {
                richTextBoxDisplay.AppendText(args.ToString() + "\r\n");
            };
            try { Invoke(TextBoxDisplayUpdate); } catch { };
        }
        private void DisplayRxFarm(object sender, EventArgs e)
        {
            canTrans.FarmsEventArgs args = (canTrans.FarmsEventArgs)e;
            EventHandler TextBoxDisplayUpdate = delegate  //EventHandler是系统自定义的委托
            {
                richTextBoxDisplay.AppendText(args.ToString() + "\r\n");
            };
            try { Invoke(TextBoxDisplayUpdate); } catch { };
        }
        #region RxMessage 处理
        private void DisplayRxMessage(object sender, EventArgs e)
        {
            canTrans.RxMsgsEventArgs RxMsgs = (canTrans.RxMsgsEventArgs)e;
            byte[] Rxdata =StringToHex(RxMsgs.ToString());
            autoResponse(Rxdata);
            {
                EventHandler TextBoxDisplayUpdate = delegate
                {

                };
                try { Invoke(TextBoxDisplayUpdate); } catch { };
            }
        }

        private void DisplayError(object sender, EventArgs e)
        {
            canTrans.ErrorEventArgs args = (canTrans.ErrorEventArgs)e;
            EventHandler TextBoxDisplayUpdate = delegate
            {
                richTextBoxDisplay.AppendText(args.ToString() + "\r\n");
            };
            try { Invoke(TextBoxDisplayUpdate); } catch { };
        }

        endofline_config endofline = new endofline_config();

        void trans_init()
        {
            driverTrans.EventTxFarms += new EventHandler(DisplayTxFarm);
            driverTrans.EventRxFarms += new EventHandler(DisplayRxFarm);
            driverTrans.EventRxMsgs += new EventHandler(DisplayRxMessage);
            driverTrans.EventError += new EventHandler(DisplayError);
            driverTrans.CanRead += driver.ReadData;
            driverTrans.CanWrite += driver.WriteData;
        }
        #endregion     

        public List<endofline_config> endofline_list = new List<endofline_config>();

        private void ShortKey_Click(object sender, EventArgs e)
        {
            if (shortkeycomboBox.Items.Count != 0)
            {
                shortkey_en = true;
                i = 0;
                cmd_string_count = cmd_list[shortkeycomboBox.SelectedIndex].cmd_string.Length;
            }
        }

        void ShortKeyCmd(string cmd_string)
        {
            driverTrans.CanSendString(cmd_string);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveudsFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void autoResponse(byte[] data)
        {
            if (data[0] == 0x67)
            {
                uint seed = 0;
                byte level;
                uint key = 0;
                if (data.Length == 4)
                {
                    seed = (uint)data[2] << 8
                        | (uint)data[3];
                }
                else if (data.Length == 6)
                {
                    seed = (uint)data[2] << 24
                        | (uint)data[3] << 16
                        | (uint)data[4] << 8
                        | (uint)data[5];
                }
                level = data[1];
                if (seed != 0 && level % 2 != 0)
                {
                    key = securityDriver.UdsCallback_CalcKey(seed, level);
                    if (data.Length == 4)
                    {
                        key &= 0xFFFF;
                        driverTrans.CanSendString("27" + (level + 1).ToString("x2") + key.ToString("x4"));
                    }
                    else if (data.Length == 6)
                    {
                        driverTrans.CanSendString("27" + (level + 1).ToString("x2") + key.ToString("x8"));
                    }
                }
            }
        }

        #region file load
        //bootloader file/uds file/configration eeprom file/dbc file 
        readonly string Project_Name = "$Project";
        readonly string Tx_ID = "$Tx";
        readonly string Rx_ID = "$Rx";
        readonly string Cmd_ID = "$Cmd";
        //readonly string Can_Message = "$Message";//can消息的开头
        readonly string ENDOFLINE = "$ENDOFLINE";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamReader myStream;//
            openFileDialog.Title = "打开文件";
            openFileDialog.Filter = " (*.ini)|*.ini|(*.dbc)|*.dbc";//ini格式 或者dbc文本
            openFileDialog.FilterIndex = 0;//选择第一个
            openFileDialog.RestoreDirectory = true;//存储路径
            if (openFileDialog.ShowDialog() == DialogResult.OK)//选择打开该文件
            {
                myStream = new StreamReader(openFileDialog.FileName);//初始化myStream并将打开的文件指针传给
                if (System.IO.Path.GetExtension(openFileDialog.FileName) == ".ini")//加载的是ini文件
                {
                    cmd_list.Clear();
                    endofline_list.Clear();
                    ftp.Server = openFileDialog.FileName;
                    ftp.Save();
                    string strline;//取每行的字符串            
                    while ((strline = myStream.ReadLine()) != null)//只要不是空的继续读
                    {
                        try
                        {
                            Match match0 = Regex.Match(strline, @"(^[$].*)[:]([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?");//将所有需要的数据匹配出来存放到相应空间中去-以$开头 最多分20组
                            if (match0.Success)
                            {
                                if (match0.Groups[1].Value == Project_Name)
                                {
                                    this.Text = match0.Groups[2].Value;
                                }
                                else if (match0.Groups[1].Value == Tx_ID)
                                {
                                    //driverTrans.tx_id_load = int.Parse(match0.Groups[2].Value.Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
                                }
                                else if (match0.Groups[1].Value == Rx_ID)
                                {
                                    //driverTrans.rx_id_load = int.Parse(match0.Groups[2].Value.Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
                                }
                                else if (match0.Groups[1].Value == Cmd_ID)
                                {
                                    int stringlength = 0;
                                    for (int i = 0; i < 20; i++)
                                    {
                                        if (match0.Groups[i + 2].Value == "")
                                        {
                                            stringlength = i - 1;
                                            break;
                                        }
                                    }
                                    string[] cmd = new string[stringlength];
                                    string cmd_name = "";
                                    for (int i = 0; i < 20; i++)
                                    {
                                        Match match_cmd = Regex.Match(match0.Groups[i + 2].Value, @"^#");
                                        if (match_cmd.Success)
                                        {
                                            cmd_name = match0.Groups[i + 2].Value;
                                            break;
                                        }
                                        else
                                        {
                                            cmd[i] = match0.Groups[i + 2].Value;
                                        }
                                    }
                                    cmd_shortkey = new cmd(cmd, cmd_name);
                                    cmd_list.Add(cmd_shortkey);
                                }
                                else if (match0.Groups[1].Value == ENDOFLINE)
                                {
                                    endofline_config endofline = new endofline_config();
                                    endofline_list.Add(endofline);
                                    int canid = int.Parse(match0.Groups[2].Value, System.Globalization.NumberStyles.HexNumber);
                                    string name = match0.Groups[3].Value;
                                    endofline.id = canid;
                                    endofline.name = name;
                                    while (true)
                                    {
                                        strline = myStream.ReadLine();
                                        Match match2 = Regex.Match(strline, @"(^[@].*)[:]([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?");
                                        if (match2.Success && match2.Groups[1].Value == "@Signal")
                                        {
                                            endofline_config.signal_config signal = new endofline_config.signal_config("", 0, 0);
                                            byte startbitpos = byte.Parse(DecimalStringToHexString(match2.Groups[2].Value), System.Globalization.NumberStyles.HexNumber);
                                            string sname = match2.Groups[3].Value;
                                            byte value = byte.Parse(match2.Groups[4].Value, System.Globalization.NumberStyles.HexNumber);
                                            signal.start_bitpos = startbitpos;
                                            signal.name = sname;
                                            signal.value = value;
                                            signal.signal_open = false;                                         
                                            endofline.signal_list.Add(signal);
                                        }
                                        else
                                        {
                                            break;
                                        }                                            
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                }
                myStream.Close();
                //重新更新service pid 
                if (cmd_list.Count != 0)
                {
                    shortkeycomboBox.Items.Clear();
                    foreach (cmd s in cmd_list)
                    {
                        shortkeycomboBox.Items.Add(s.cmd_name);
                    }
                    shortkeycomboBox.SelectedIndex = 0;
                }
            }
        }

        void auto_load_file()
        {
            if (ftp.Server == "" || !(File.Exists(ftp.Server)))//之前没有加载过文件或者加载文件但文件找不到了
                return;
            StreamReader myStream;
            cmd_list.Clear();
            myStream = new StreamReader(ftp.Server);//初始化myStream并将打开的文件指针传给
            string strline;//取每行的字符串              
            while ((strline = myStream.ReadLine()) != null)//只要不是空的继续读
            {
                try
                {
                    Match match0 = Regex.Match(strline, @"(^[$].*)[:]([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?");//将所有需要的数据匹配出来存放到相应空间中去-以$开头 最多分20组
                    if (match0.Success)
                    {
                        if (match0.Groups[1].Value == Project_Name)
                        {
                            this.Text = match0.Groups[2].Value;
                        }
                        else if (match0.Groups[1].Value == Tx_ID)
                        {
                            //driverTrans.tx_id_load = int.Parse(match0.Groups[2].Value.Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
                        }
                        else if (match0.Groups[1].Value == Rx_ID)
                        {
                            //driverTrans.rx_id_load = int.Parse(match0.Groups[2].Value.Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
                        }
                        else if (match0.Groups[1].Value == Cmd_ID)
                        {
                            int stringlength = 0;
                            for (int i = 0; i < 20; i++)
                            {
                                if (match0.Groups[i + 2].Value == "")
                                {
                                    stringlength = i - 1;
                                    break;
                                }
                            }
                            string[] cmd = new string[stringlength];
                            string cmd_name = "";
                            for (int i = 0; i < 20; i++)
                            {
                                Match match_cmd = Regex.Match(match0.Groups[i + 2].Value, @"^#");
                                if (match_cmd.Success)
                                {
                                    cmd_name = match0.Groups[i + 2].Value;
                                    break;
                                }
                                else
                                {
                                    cmd[i] = match0.Groups[i + 2].Value;
                                }
                            }
                            cmd_shortkey = new cmd(cmd, cmd_name);
                            cmd_list.Add(cmd_shortkey);
                        }
                        else if (match0.Groups[1].Value == ENDOFLINE)
                        {
                            endofline_config endofline = new endofline_config();
                            endofline_list.Add(endofline);
                            int canid = int.Parse(match0.Groups[2].Value, System.Globalization.NumberStyles.HexNumber);
                            string name = match0.Groups[3].Value;
                            endofline.id = canid;
                            endofline.name = name;
                            while (true)
                            {
                                strline = myStream.ReadLine();
                                Match match2 = Regex.Match(strline, @"(^[@].*)[:]([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?([^,]*)[,]?");
                                if (match2.Success && match2.Groups[1].Value == "@Signal")
                                {
                                    endofline_config.signal_config signal = new endofline_config.signal_config("", 0, 0);
                                    byte startbitpos = byte.Parse(DecimalStringToHexString(match2.Groups[2].Value), System.Globalization.NumberStyles.HexNumber);
                                    string sname = match2.Groups[3].Value;
                                    byte value = byte.Parse(match2.Groups[4].Value, System.Globalization.NumberStyles.HexNumber);
                                    signal.start_bitpos = startbitpos;
                                    signal.name = sname;
                                    signal.value = value;
                                    signal.signal_open = false;
                                    endofline.signal_list.Add(signal);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                catch { }
            }
            myStream.Close();           
            if (cmd_list.Count != 0)
            {
                shortkeycomboBox.Items.Clear();
                foreach (cmd s in cmd_list)
                {
                    shortkeycomboBox.Items.Add(s.cmd_name);
                }
                shortkeycomboBox.SelectedIndex = 0;
            }
        }
        #endregion

        public  byte[] StringToHex(string strings)
        {
            byte[] hex = new byte[0];
            try
            {
                strings = strings.Replace("0x", "");
                strings = strings.Replace("0X", "");
                strings = strings.Replace(" ", "");
                strings = Regex.Replace(strings, @"(?i)[^a-f\d\s]+", "");
                if (strings.Length % 2 != 0)
                {
                    strings += "0";
                }
                hex = new byte[strings.Length / 2];
                for (int i = 0; i < hex.Length; i++)
                {
                    hex[i] = Convert.ToByte(strings.Substring(i * 2, 2), 16);
                }
                return hex;
            }
            catch
            {
                return hex;
            }
        }

        public static string DecimalStringToHexString(string strings)
        {
            UInt32 returnNum = UInt32.Parse(strings, System.Globalization.NumberStyles.HexNumber);
            byte highbyte = (byte)(returnNum / 0x10000);
            int dechigbyte = (highbyte - 6 * (highbyte / 0x10)) * 10000;
            byte Midbyte = (byte)(returnNum / 0x100);
            int decMidbyte = (Midbyte - 6 * (Midbyte / 0x10)) * 100;
            byte lowbyte = (byte)(returnNum % 0x100);
            int declowbyte = (lowbyte - 6 * (lowbyte / 0x10));
            returnNum = (UInt32)(dechigbyte + decMidbyte + declowbyte);
            if (returnNum < 0x10)
            {
                return returnNum.ToString("X1");
            }
            else if (returnNum < 0x100)
            {
                return returnNum.ToString("X2");
            }
            else if (returnNum < 0x1000)
            {
                return returnNum.ToString("X3");
            }
            else if (returnNum < 0x10000)
            {
                return returnNum.ToString("X4");
            }
            else if (returnNum < 0x100000)
            {
                return returnNum.ToString("X5");
            }
            else
            {
                return returnNum.ToString("X6");
            }
        }

        public class endofline_config
        {
            public string name;
            public int id;
            public signal_config signal = new signal_config("", 0, 0);
            public List<signal_config> signal_list = new List<signal_config>();
            public class signal_config
            {
                public string name;
                public bool signal_open = false;
                public byte start_bitpos;
                public bool signal_true = true;
                public bool record_signal_true = false;//第一次都是要刷新的
                public string component1_name;
                public string component2_name;
                public byte value;
                public signal_config(string name, byte start_bitpos, byte value)
                {
                    this.name = name;
                    this.start_bitpos = start_bitpos;
                    this.value = value;
                }
            }
        }

        /// <summary>
        /// 快捷指令类 这个类存储指令集合
        /// </summary>
        public class cmd
        {
            public string[] cmd_string;
            public string cmd_name;
            public cmd(string[] cmd_string, string cmd_name)
            {
                this.cmd_string = cmd_string;
                this.cmd_name = cmd_name;
            }
        }

        #region Timer
        public delegate void Tick_10ms();
        public delegate void Tick_50ms();
        public delegate void Tick_100ms();
        public delegate void Tick_1s();
        public Tick_10ms mmtimer_tick_10ms;
        public Tick_10ms mmtimer_tick_50ms;
        public Tick_100ms mmtimer_tick_100ms;
        public Tick_1s mmtimer_tick_1s;
        public MmTimer mmTimer;
        const int timer_interval = 10;
        int timer_10ms_counter = 0;
        int timer_50ms_counter = 0;
        int timer_100ms_counter = 0;
        int timer_1s_counter = 0;

        private void mmTime_init()
        {
            mmTimer = new MmTimer();
            mmTimer.Mode = MmTimerMode.Periodic;
            mmTimer.Interval = timer_interval;
            mmTimer.Tick += mmTimer_tick;

            mmtimer_tick_10ms += delegate
            {

            };

            mmtimer_tick_50ms += delegate
            {
                EventHandler ShortKeySend = delegate
                {
                    if (shortkey_en)
                    {
                        ShortKeyCmd(cmd_list[shortkeycomboBox.SelectedIndex].cmd_string[i]);
                        i++;
                        if (cmd_string_count == i)
                        {
                            shortkey_en = false;
                        }
                    }
                };
                try { Invoke(ShortKeySend); } catch { };
            };

            mmtimer_tick_100ms += delegate
            {

            };

            mmtimer_tick_1s += delegate
            {
                EventHandler BusLoadUpdate = delegate
                {
                    toolStripStatusLabel4.Text = "Bus Load：" + driver.BusLoad().ToString() + "% ";
                };
                try { Invoke(BusLoadUpdate); } catch { };
            };
        }

        void mmTimer_tick(object sender, EventArgs e)
        {
            timer_10ms_counter += timer_interval;
            if (timer_10ms_counter >= 10)
            {
                timer_10ms_counter = 0;
                if (mmtimer_tick_10ms != null)
                {
                    mmtimer_tick_10ms();
                }
            }

            timer_50ms_counter += timer_interval;
            if (timer_50ms_counter >= 50)
            {
                timer_50ms_counter = 0;
                if (mmtimer_tick_10ms != null)
                {
                    mmtimer_tick_50ms();
                }
            }

            timer_100ms_counter += timer_interval;
            if (timer_100ms_counter >= 100)
            {
                timer_100ms_counter = 0;
                if (mmtimer_tick_100ms != null)
                {
                    mmtimer_tick_100ms();
                }
            }

            timer_1s_counter += timer_interval;
            if (timer_1s_counter >= 1000)
            {
                timer_1s_counter = 0;
                if (mmtimer_tick_1s != null)
                {
                    mmtimer_tick_1s();
                }
            }
        }
        #endregion

        #region ini
        sealed class FtpSettings : ApplicationSettingsBase
        {
            [UserScopedSetting]
            [DefaultSettingValue("")]
            public string Server
            {
                get { return (string)this["Server"]; }
                set { this["Server"] = value; }
            }
            [UserScopedSetting]
            [DefaultSettingValue("")]
            public int Port
            {
                get { return (int)this["Port"]; }
                set { this["Port"] = value; }
            }
        }
        FtpSettings ftp = new FtpSettings();
        #endregion     

        private void Panel_Click(object sender, EventArgs e)
        {
            MMX.PanelWindow Panel = new MMX.PanelWindow(endofline_list);//将主窗体的list传递到子窗体
            Panel.Show();
            PanelButtontoolstrip.Enabled = true;
            Panel.Text = "Panel";
        }
    }
    #endregion
}