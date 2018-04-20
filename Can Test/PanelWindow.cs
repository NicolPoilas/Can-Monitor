using System;
using Dongzr.MidiLite;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using Uds;
using System.Drawing;
using System.Windows.Forms;
using Can_Test;
using System.IO;

namespace MMX
{
    public partial class PanelWindow : Form
    {
        List<Form1.endofline_config> list = new List<Form1.endofline_config>();
        can_driver driver = new can_driver();

        FileStream fs;
        StreamWriter sw;

        public int id;
        byte[] data = new byte[8];
        byte[] load_open_all = new byte [8] ;
        
        
        public int dlc;
        long timestamp = 0;
        bool rx_success = false;

        bool Receieved_CMD;
        bool LOAD_ALL_EN;
        bool LOAD_ALL_UnEN;

        int counter =1000;//10s

        Brush brush_green = new SolidBrush(Color.Green);//填充的颜色
        Brush brush_red = new SolidBrush(Color.Red);
        Brush brush_Black = new SolidBrush(Color.Black);
        Pen pen = new Pen(Color.Black);
        Font font = new Font("楷体",8);
        Font font1 = new Font("华文隶书",40);
        int paint_counter=0;
        float angle = Convert.ToSingle(360);
        bool startdrawlabel = true;
        Graphics g;

        public PanelWindow(List<Form1.endofline_config> endofline_list)
        {
            list = endofline_list;          
            InitializeComponent();
            mmTime_init();
            mmTimer.Start();          
            g= this.CreateGraphics();
            load_open_all[0] = 0x10;
            load_open_all[1] = 0x0D;
            load_open_all[2] = 0x6F;
            load_open_all[3] = 0xFD;
            load_open_all[4] = 0xA0;
            load_open_all[5] = 0x03;
            load_open_all[6] = 0xFF;
            load_open_all[7] = 0xFF;
            LogFile_Generate();      
        }

        #region LogFile 此功能暂时预留
        void LogFile_Generate()
        {
            fs = new FileStream("log" + DateTime.Now.ToString(" yyyy MM dd HH_mm") + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            sw = new StreamWriter(fs); // 创建写入流
            sw.WriteLine("Monitor log file");
        }
        #endregion

        void adjust_position(int x, int y,out int z,out int k)
        {
            x = x + 100;
            if (x > 1300)
            {
                x = 60;
                y = y + 80;
            }
            z = x;k = y;
        }

        public string HexToStrings(byte[] hex, string space)
        {
            string strings = "";
            for (int i = 0; i < hex.Length; i++)//逐字节变为16进制字符，并以space隔开
            {
                strings += hex[i].ToString("X2") + space;
            }
            return strings;
        }

        void auto_Window()
        {
            int ptx = 60;
            int pty = 30;
            int width = 30;
            int height = 30;
            int labelptx_adjust = 0;
            startdrawlabel = true;            
            foreach (Form1.endofline_config endofline in list)
            {
                foreach (Form1.endofline_config.signal_config signal in endofline.signal_list)
                {
                    if (signal.signal_true)
                    {
                        g.FillPie(brush_green, ptx, pty, width, height, 0, angle);
                    }
                    else
                    {
                        g.FillPie(brush_red, ptx, pty, width, height, 0, angle);

                    }
                    if (startdrawlabel)
                    {
                        labelptx_adjust = (int)(signal.name.Length * 2);
                        g.DrawString(signal.name, font, brush_Black, ptx - labelptx_adjust, pty + 50);
                    }
                    adjust_position(ptx, pty, out ptx, out pty);
                }
            }
            if (startdrawlabel)
            {
                //g.DrawString("Borgward Monitor", font1, brush_Black, 900, 600);
                g.DrawRectangle(pen,20,10,1310,720);
                startdrawlabel = false;
            }
        }

        void Update_Window()
        {
            int ptx = 60;
            int pty = 30;
            int width = 30;
            int height = 30;
            int labelptx_adjust = 0;
            foreach (Form1.endofline_config endofline in list) 
            {
                foreach (Form1.endofline_config.signal_config signal in endofline.signal_list)
                {
                    if (signal.signal_true)
                    {
                        if (signal.record_signal_true == false||true)
                        {
                            g.FillPie(brush_green, ptx, pty, width, height, 0, angle);
                            signal.record_signal_true = true;
                        }
                    }
                    else
                    {
                        if (signal.record_signal_true == true || true)
                        {
                            sw.WriteLine(signal.name+"：NOT CORRECT");
                            g.FillPie(brush_red, ptx, pty, width, height, 0, angle);
                            signal.record_signal_true = false;
                        }
                    }
                    if (startdrawlabel)
                    {
                        g.FillPie(brush_green, ptx, pty, width, height, 0, angle);
                        labelptx_adjust = (int)(signal.name.Length * 2);
                        g.DrawString(signal.name, font, brush_Black, ptx-labelptx_adjust, pty + 50);                       
                    }                   
                    adjust_position(ptx, pty, out ptx, out pty);
                }
            }
            if (startdrawlabel)
            {
                //g.DrawString("Borgward Monitor",font1,brush_Black,900,600);
                g.DrawRectangle(pen, 20, 10, 1310,720);
                t_Start();
                startdrawlabel = false;
            }                      
        }

        private void Form6_FormClosed(object sender, FormClosedEventArgs e)
        {
            sw.Close(); //在form关掉后，将文件关掉 
            mmTimer.Stop();
            t_Stop();
        }

        #region thread t_Receive
        Thread t_Receive;
        private void t_Receive_Thread()
        {
            while (true)
            {
                int i = 0;
                while (i < 50)
                {
                    CycleRecieve();
                    i++;
                }
                t_Sleep(20);//休息10ms
            }
        }

        public int GetBitMotorola(byte[] bytes, int start, int length)
        {
            int value = 0;
            int posByte = start / 8;
            int posBit = start % 8;

            for (int i = 0; i < length; i++)
            {
                value |= (((int)bytes[posByte] >> posBit) & 0x01) << i;

                if (i >= length)
                {
                    break;
                }
                if (++posBit >= 8)
                {
                    posBit = 0;
                    posByte--;
                }
            }
            return value;
        }

        public  bool sameArray(byte[] a, byte[] b)
        {
            bool c = true;
            if (a.Length != b.Length)
            {
                c = false;
            }
            else
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] != b[i])
                    {
                        c = false;
                        break;
                    }
                }
            }
            return c;
        }

        private void CycleRecieve()
        {
            rx_success = driver.ReadData(out id, ref data, out dlc, out timestamp);//接收一帧数据
            if (id != 0x7B8&& id != 0x220 && id != 0x221 && id != 0x222)
            {
                return;
            }
            if (rx_success)
            {
                sw.WriteLine("$" + id.ToString("X3") + " " + dlc.ToString() + " " + HexToStrings(data, " ") + (timestamp / 1000).ToString() + "." + (timestamp % 1000).ToString());
                if (id == 0x7B8)
                {
                    bool bools = sameArray(data, load_open_all);
                    if (bools)
                    {
                        Receieved_CMD = true;
                        LOAD_ALL_EN = false;
                        LOAD_ALL_UnEN = false;
                        counter = 1000;
                    }
                }
            }
            if (!Receieved_CMD) //静态
            {
                foreach (Form1.endofline_config endofline in list)
                {
                    if (endofline.id == id)
                    {
                        foreach (Form1.endofline_config.signal_config signal in endofline.signal_list)
                        {
                            byte bytes = (byte)GetBitMotorola(data, signal.start_bitpos, 1);
                            if (bytes == signal.value)
                            {
                                signal.signal_true = true;
                            }
                            else
                            {
                                signal.signal_true = false;
                            }
                            Match match = Regex.Match(signal.name, @"CAN|LIN");//CAN LIN单独处理
                            if (match.Success)//对于CAN LIN需要将结果反过来
                            {
                                signal.signal_true = !signal.signal_true;
                            }
                        }
                        break;
                    }                    
                }
            }
            else//动态
            {
                if (LOAD_ALL_EN)
                {
                    foreach (Form1.endofline_config endofline in list)
                    {
                        if (endofline.id == id)
                        {
                            foreach (Form1.endofline_config.signal_config signal in endofline.signal_list)
                            {
                                byte bytes = (byte)GetBitMotorola(data, signal.start_bitpos, 1);
                                 if (bytes == signal.value)
                                {
                                    signal.signal_true = false;
                                }
                                else
                                {
                                    signal.signal_true = true;
                                }
                            }
                            break;
                        }
                    }
                }
                if (LOAD_ALL_UnEN)
                {
                    foreach (Form1.endofline_config endofline in list)
                    {
                        if (endofline.id == id)//负载此时应该全部关闭
                        {
                            foreach (Form1.endofline_config.signal_config signal in endofline.signal_list)
                            {
                                byte bytes = (byte)GetBitMotorola(data, signal.start_bitpos, 1);
                                if (bytes == signal.value)
                                {
                                    signal.signal_true = true;
                                }
                                else
                                {
                                    signal.signal_true = false;
                                }
                                Match match = Regex.Match(signal.name, @"CAN|LIN");//CAN LIN单独处理
                                if (match.Success)//对于CAN LIN需要将结果反过来
                                {
                                    signal.signal_true = !signal.signal_true;
                                }
                            }
                            break;
                        }
                    }
                }        
            }
        }
        public void t_Start()
        {
            t_Receive = new Thread(new ThreadStart(t_Receive_Thread));
            t_Receive.IsBackground = true;
            t_Receive.Priority = ThreadPriority.Lowest;
            t_Receive.Start();
        }
        public void t_Stop()
        {
            if (t_Receive != null && t_Receive.IsAlive)
            {
                t_Receive.Abort();
            }
        }
        public void t_Sleep(int timespan)
        {
            if (t_Receive != null && t_Receive.IsAlive)
            {
                Thread.Sleep(timespan);
            }
        }
        #endregion

        #region Timer
        public delegate void Tick_10ms();
        public delegate void Tick_50ms();
        public delegate void Tick_100ms();
        public delegate void Tick_1s();
        public delegate void Tick_2s();
        public delegate void Tick_10s();
        public Tick_10ms mmtimer_tick_10ms;
        public Tick_10ms mmtimer_tick_50ms;
        public Tick_100ms mmtimer_tick_100ms;
        public Tick_1s mmtimer_tick_1s;
        public Tick_2s mmtimer_tick_2s;
        public Tick_10s mmtimer_tick_10s;
        public MmTimer mmTimer;
        const int timer_interval = 10;
        int timer_10ms_counter = 0;
        int timer_50ms_counter = 0;
        int timer_100ms_counter = 0;
        int timer_1s_counter = 0;
        int timer_2s_counter = 0;
        int timer_10s_counter = 0;

        private void mmTime_init()
        {
            mmTimer = new MmTimer();
            mmTimer.Mode = MmTimerMode.Periodic;
            mmTimer.Interval = timer_interval;
            mmTimer.Tick += mmTimer_tick;
            mmtimer_tick_10ms += delegate
            {
                if (Receieved_CMD)//接收到指令
                {
                    counter--;
                    if (counter<900&& counter > 600) 
                    {
                        LOAD_ALL_EN = true;
                    }
                    if (counter < 600 && counter > 500)
                    {
                        LOAD_ALL_EN = false;
                    }
                    if (counter < 300 && counter > 200)
                    {
                        LOAD_ALL_UnEN = true;
                    }
                    if (counter < 200)
                    {
                        Receieved_CMD = false;
                    }                      
                }
            };

            mmtimer_tick_50ms += delegate
            {
                
            };

            mmtimer_tick_100ms += delegate
            {
            };

            mmtimer_tick_1s += delegate
            {
                EventHandler BusLoadUpdate = delegate
                {
                    Update_Window();                   
                };
                try { Invoke(BusLoadUpdate); } catch { };
            };
            mmtimer_tick_2s += delegate
            {

            };

            mmtimer_tick_10s += delegate
            {
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

            timer_2s_counter += timer_interval;
            if (timer_2s_counter >= 2000)
            {
                timer_2s_counter = 0;
                if (mmtimer_tick_2s != null)
                {
                    mmtimer_tick_2s();
                }
            }

            timer_10s_counter += timer_interval;
            if (timer_10s_counter > 10000)
            {
                timer_10s_counter = 0;
                if (mmtimer_tick_10s != null)
                {
                    mmtimer_tick_10s();
                }
            }
        }
        #endregion

        private void PanelWindow_LocationChanged(object sender, EventArgs e)
        {
            paint_counter++;
            if (paint_counter == 30)//经过测试发现，30这个值 最好，既不会卡顿闪烁，也不会导致窗体信息不能恢复
            {
                paint_counter = 0;
                g.Clear(Color.WhiteSmoke);
                auto_Window();
            }
        }
    }
}
