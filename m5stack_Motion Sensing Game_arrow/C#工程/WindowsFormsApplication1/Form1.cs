using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Runtime.InteropServices;


namespace WindowsFormsApplication1
{




    public partial class Form1 : Form
    {


        #region bVk参数 常量定义

        public const byte vbKeyLButton = 0x1;    // 鼠标左键
        public const byte vbKeyRButton = 0x2;    // 鼠标右键
        public const byte vbKeyCancel = 0x3;     // CANCEL 键
        public const byte vbKeyMButton = 0x4;    // 鼠标中键
        public const byte vbKeyBack = 0x8;       // BACKSPACE 键
        public const byte vbKeyTab = 0x9;        // TAB 键
        public const byte vbKeyClear = 0xC;      // CLEAR 键
        public const byte vbKeyReturn = 0xD;     // ENTER 键
        public const byte vbKeyShift = 0x10;     // SHIFT 键
        public const byte vbKeyControl = 0x11;   // CTRL 键
        public const byte vbKeyAlt = 18;         // Alt 键  (键码18)
        public const byte vbKeyMenu = 0x12;      // MENU 键
        public const byte vbKeyPause = 0x13;     // PAUSE 键
        public const byte vbKeyCapital = 0x14;   // CAPS LOCK 键
        public const byte vbKeyEscape = 0x1B;    // ESC 键
        public const byte vbKeySpace = 0x20;     // SPACEBAR 键
        public const byte vbKeyPageUp = 0x21;    // PAGE UP 键
        public const byte vbKeyEnd = 0x23;       // End 键
        public const byte vbKeyHome = 0x24;      // HOME 键
        public const byte vbKeyLeft = 0x25;      // LEFT ARROW 键
        public const byte vbKeyUp = 0x26;        // UP ARROW 键
        public const byte vbKeyRight = 0x27;     // RIGHT ARROW 键
        public const byte vbKeyDown = 0x28;      // DOWN ARROW 键
        public const byte vbKeySelect = 0x29;    // Select 键
        public const byte vbKeyPrint = 0x2A;     // PRINT SCREEN 键
        public const byte vbKeyExecute = 0x2B;   // EXECUTE 键
        public const byte vbKeySnapshot = 0x2C;  // SNAPSHOT 键
        public const byte vbKeyDelete = 0x2E;    // Delete 键
        public const byte vbKeyHelp = 0x2F;      // HELP 键
        public const byte vbKeyNumlock = 0x90;   // NUM LOCK 键

        //常用键 字母键A到Z
        public const byte vbKeyA = 65;
        public const byte vbKeyB = 66;
        public const byte vbKeyC = 67;
        public const byte vbKeyD = 68;
        public const byte vbKeyE = 69;
        public const byte vbKeyF = 70;
        public const byte vbKeyG = 71;
        public const byte vbKeyH = 72;
        public const byte vbKeyI = 73;
        public const byte vbKeyJ = 74;
        public const byte vbKeyK = 75;
        public const byte vbKeyL = 76;
        public const byte vbKeyM = 77;
        public const byte vbKeyN = 78;
        public const byte vbKeyO = 79;
        public const byte vbKeyP = 80;
        public const byte vbKeyQ = 81;
        public const byte vbKeyR = 82;
        public const byte vbKeyS = 83;
        public const byte vbKeyT = 84;
        public const byte vbKeyU = 85;
        public const byte vbKeyV = 86;
        public const byte vbKeyW = 87;
        public const byte vbKeyX = 88;
        public const byte vbKeyY = 89;
        public const byte vbKeyZ = 90;

        //数字键盘0到9
        public const byte vbKey0 = 48;    // 0 键
        public const byte vbKey1 = 49;    // 1 键
        public const byte vbKey2 = 50;    // 2 键
        public const byte vbKey3 = 51;    // 3 键
        public const byte vbKey4 = 52;    // 4 键
        public const byte vbKey5 = 53;    // 5 键
        public const byte vbKey6 = 54;    // 6 键
        public const byte vbKey7 = 55;    // 7 键
        public const byte vbKey8 = 56;    // 8 键
        public const byte vbKey9 = 57;    // 9 键


        public const byte vbKeyNumpad0 = 0x60;    //0 键
        public const byte vbKeyNumpad1 = 0x61;    //1 键
        public const byte vbKeyNumpad2 = 0x62;    //2 键
        public const byte vbKeyNumpad3 = 0x63;    //3 键
        public const byte vbKeyNumpad4 = 0x64;    //4 键
        public const byte vbKeyNumpad5 = 0x65;    //5 键
        public const byte vbKeyNumpad6 = 0x66;    //6 键
        public const byte vbKeyNumpad7 = 0x67;    //7 键
        public const byte vbKeyNumpad8 = 0x68;    //8 键
        public const byte vbKeyNumpad9 = 0x69;    //9 键
        public const byte vbKeyMultiply = 0x6A;   // MULTIPLICATIONSIGN(*)键
        public const byte vbKeyAdd = 0x6B;        // PLUS SIGN(+) 键
        public const byte vbKeySeparator = 0x6C;  // ENTER 键
        public const byte vbKeySubtract = 0x6D;   // MINUS SIGN(-) 键
        public const byte vbKeyDecimal = 0x6E;    // DECIMAL POINT(.) 键
        public const byte vbKeyDivide = 0x6F;     // DIVISION SIGN(/) 键


        //F1到F12按键
        public const byte vbKeyF1 = 0x70;   //F1 键
        public const byte vbKeyF2 = 0x71;   //F2 键
        public const byte vbKeyF3 = 0x72;   //F3 键
        public const byte vbKeyF4 = 0x73;   //F4 键
        public const byte vbKeyF5 = 0x74;   //F5 键
        public const byte vbKeyF6 = 0x75;   //F6 键
        public const byte vbKeyF7 = 0x76;   //F7 键
        public const byte vbKeyF8 = 0x77;   //F8 键
        public const byte vbKeyF9 = 0x78;   //F9 键
        public const byte vbKeyF10 = 0x79;  //F10 键
        public const byte vbKeyF11 = 0x7A;  //F11 键
        public const byte vbKeyF12 = 0x7B;  //F12 键

        #endregion

        #region 引用win32api方法

        /// <summary>
        /// 导入模拟键盘的方法
        /// </summary>
        /// <param name="bVk" >按键的虚拟键值</param>
        /// <param name= "bScan" >扫描码，一般不用设置，用0代替就行</param>
        /// <param name= "dwFlags" >选项标志：0：表示按下，2：表示松开</param>
        /// <param name= "dwExtraInfo">一般设置为0</param>
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        #endregion

        IDataObject iData = Clipboard.GetDataObject();

        int count = 0;
        int num_blue = 10;
        bool red_time = false;
        bool red_is = false;
        /// <summary>
        /// 获取指定窗口的设备场景
        /// </summary>
        /// <param name="hwnd">将获取其设备场景的窗口的句柄。若为0，则要获取整个屏幕的DC</param>
        /// <returns>指定窗口的设备场景句柄，出错则为0</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        /// <summary>
        /// 释放由调用GetDC函数获取的指定设备场景
        /// </summary>
        /// <param name="hwnd">要释放的设备场景相关的窗口句柄</param>
        /// <param name="hdc">要释放的设备场景句柄</param>
        /// <returns>执行成功为1，否则为0</returns>
        [DllImport("user32.dll")]
        public static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        /// <summary>
        /// 在指定的设备场景中取得一个像素的RGB值
        /// </summary>
        /// <param name="hdc">一个设备场景的句柄</param>
        /// <param name="nXPos">逻辑坐标中要检查的横坐标</param>
        /// <param name="nYPos">逻辑坐标中要检查的纵坐标</param>
        /// <returns>指定点的颜色</returns>
        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);
        public Color GetColor(int x, int y)
        {
            IntPtr hdc = GetDC(IntPtr.Zero); uint pixel = GetPixel(hdc, x, y);
            ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF), (int)(pixel & 0x0000FF00) >> 8, (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }


        public class MouseFlag
        {
            enum MouseEventFlag : uint
            {
                Move = 0x0001,
                LeftDown = 0x0002,
                LeftUp = 0x0004,
                RightDown = 0x0008,
                RightUp = 0x0010,
                MiddleDown = 0x0020,
                MiddleUp = 0x0040,
                XDown = 0x0080,
                XUp = 0x0100,
                Wheel = 0x0800,
                VirtualDesk = 0x4000,
                Absolute = 0x8000
            }

            [DllImport("user32.dll")]
            static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);

            [DllImport("user32.dll")]
            public static extern int SetCursorPos(int x, int y);

            public static void Move(int dx, int dy, uint data)
            {
                Point screenPoint = Control.MousePosition;
                SetCursorPos(screenPoint.X+dx, screenPoint.Y+dy);

            }

            public static void MouseLeftClickEvent(int dx, int dy, uint data)
            {

                Move(dx, dy,0);
                System.Threading.Thread.Sleep(2 * 1000);
                mouse_event(MouseEventFlag.LeftDown, dx, dy, data, UIntPtr.Zero);
                mouse_event(MouseEventFlag.LeftUp, dx, dy, data, UIntPtr.Zero);
            }

            public static void MouseRightClickEvent(int dx, int dy, uint data)
            {
                Move(dx, dy, 0);
                System.Threading.Thread.Sleep(2 * 1000);
                mouse_event(MouseEventFlag.RightDown, dx, dy, data, UIntPtr.Zero);
                mouse_event(MouseEventFlag.RightUp, dx, dy, data, UIntPtr.Zero);
            }
        }

       



        public Form1()
        {
            InitializeComponent();
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived); //串口数据接收事件
            serialPort1.Encoding = Encoding.GetEncoding("GB2312"); //串口接收编码
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }



        //------------------------------------------------------------------------------------------

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e) //串口接收事件
        {
            textBox1.Clear();
            textBox2.Clear();
            string str = " "; 
            if (!radioButton3.Checked)
            {
            textBox1.AppendText(serialPort1.ReadExisting()); //串口类会自动处理汉字，所以不需要特别转换
            }
            else
            {
                byte[] data = new byte[serialPort1.BytesToRead]; //定义缓冲区，因为串口事件触发时有可能收到不止一个字节
                serialPort1.Read(data, 0, data.Length);
                foreach (byte Member in data) //遍历用法
                {
                    str = Convert.ToString(Member, 16).ToUpper();
                    textBox1.AppendText("0x" + (str.Length == 1 ? "0" + str : str) + " ");
                }
            }
            

            string control = textBox1.Text;
            if (control.Length >= 8)
            {
                string y_num = control.Substring(0, 3);
                string anim = control.Substring(3, 1);
                string shoot = control.Substring(4, 1);
                string z_num = control.Substring(5, 3);
                int Y = int.Parse(y_num);
                int Z = int.Parse(z_num);
                MouseFlag.Move(((Y-500)) /2, (Z - 500) / 30, 0);


                if (anim == "A")
                {
                    keybd_event(vbKeyA, 0, 0, 0);
                }

                if (anim == "a")
                {
                    keybd_event(vbKeyA, 0, 2, 0);
                }

                if (anim == "S")
                {
                    keybd_event(vbKeyS, 0, 0, 0);
                }

                if (anim == "s")
                {
                    keybd_event(vbKeyS, 0, 2, 0);
                }


                if (anim == "D")
                {
                    keybd_event(vbKeyD, 0, 0, 0);
                }

                if (anim == "d")
                {
                    keybd_event(vbKeyD, 0, 2, 0);
                }

                if (anim == "W")
                {
                    keybd_event(vbKeyW, 0, 0, 0);

                }
                if (anim == "w")
                {
                    keybd_event(vbKeyW, 0, 2, 0);
                }

                if (shoot == "H")
                {
                    keybd_event(vbKeyH, 0, 0, 0);

                }

                if (shoot == "h")
                {
                    keybd_event(vbKeyH, 0, 2, 0);

                }

                if (anim == "J")
                {
                    keybd_event(vbKeySpace, 0, 0, 0);
                    keybd_event(vbKeySpace, 0, 2, 0);

                }

            }

           
            textBox1.AppendText(str + '\t');
           
           var path = "0";

           try
           {
               path = (String)iData.GetData(DataFormats.Text);
           }
           catch (Exception ex)
           {
               Application.DoEvents();
               path = (String)iData.GetData(DataFormats.Text);
           }

           textBox2.AppendText(path);
           num_blue--;
           if (num_blue == 0)
           {
               num_blue = 10;
               blue_send();
           }
           
        }




        private void SearchAndAddSerialToComboBox(SerialPort MyPort, ComboBox MyBox)
        { //将可用端口号添加到ComboBox
            string[] MyString = new string[20]; //最多容纳20个，太多会影响调试效率
            string Buffer; //缓存
            MyBox.Items.Clear(); //清空ComboBox内容
            for (int i = 1; i < 20; i++) //循环
            {
                try //核心原理是依靠try和catch完成遍历
                {
                    Buffer = "COM" + i.ToString();
                    MyPort.PortName = Buffer;
                    MyPort.Open(); //如果失败，后面的代码不会执行
                    MyString[i - 1] = Buffer;
                    MyBox.Items.Add(Buffer); //打开成功，添加至下俩列表
                    MyPort.Close(); //关闭
                    MyBox.Text = MyString[i - 1]; //显示最后面扫描成功的那个串口
                }
                catch
                {
                }
            }



        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SearchAndAddSerialToComboBox(serialPort1, comboBox1);
            comboBox2.Text = "9600";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SearchAndAddSerialToComboBox(serialPort1, comboBox1);
        }






        //------------------------------------------------------------------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox1.Text; //端口号
                serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text); //波特率
                serialPort1.Open(); //打开串口
                button2.Enabled = false;
                button3.Enabled = true;
            }
            catch
            {
                MessageBox.Show("端口错误", "错误");
            }

            textBox2.Clear();

            
            var path="0";

            try
            {
               path = (String)iData.GetData(DataFormats.Text);
            }
                        catch (Exception ex){
                                 Application.DoEvents();
               path = (String)iData.GetData(DataFormats.Text);
            }

            textBox2.AppendText(path);

 

        }



            private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close(); //关闭串口
                button2.Enabled = true;
                button3.Enabled = false;
            }
            catch
            {
            }


        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.S)
            if (e.KeyCode == Keys.S && e.Control)
            {
                try
                {
                    serialPort1.Close(); //关闭串口
                    button2.Enabled = true;
                    button3.Enabled = false;
                }
                catch
                {
                }
            }
        }

        private void blue_send()
        {
            byte[] Data = new byte[1]; //单字节发数据
            if (serialPort1.IsOpen) //串口打开了才能发送数据
            {
                if (textBox2.Text != "") //串口那个输入发送控件不是空的才发送
                {
                    if (!radioButton1.Checked) //那个单选发送的是数据还是字符
                    {
                        try
                        {
                            serialPort1.Write(textBox2.Text);
                            //serialPort1.WriteLine(); //字符串写入
                        }
                        catch
                        {
                            MessageBox.Show("串口数据写入错误", "错误");
                        }
                    }
                    else //数据模式
                    {
                        try //如果此时用户输入字符串中含有非法字符（字母，汉字，符号等等，try，catch块可以捕捉并提示）
                        {
                            for (int i = 0; i < (textBox2.Text.Length - textBox2.Text.Length % 2) / 2; i++)//转换偶数个
                            {
                                Data[0] = Convert.ToByte(textBox2.Text.Substring(i * 2, 2), 16); //转换
                                serialPort1.Write(Data, 0, 1);
                            }
                            if (textBox2.Text.Length % 2 != 0)
                            {
                                Data[0] = Convert.ToByte(textBox2.Text.Substring(textBox2.Text.Length - 1, 1), 16);//单独处理最后一个字符
                                serialPort1.Write(Data, 0, 1); //写入
                            }
                            //Data = Convert.ToByte(textBox2.Text.Substring(textBox2.Text.Length - 1, 1), 16);
                            // }
                        }
                        catch
                        {
                            MessageBox.Show("数据转换错误，请输入数字。", "错误");
                        }
                    }
                }
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            byte[] Data = new byte[1]; //单字节发数据
            if (serialPort1.IsOpen) //串口打开了才能发送数据
            {
                if (textBox2.Text != "") //串口那个输入发送控件不是空的才发送
                {
                    if (!radioButton1.Checked) //那个单选发送的是数据还是字符
                    {
                        try
                        {
                        serialPort1.Write(textBox2.Text);
                        //serialPort1.WriteLine(); //字符串写入
                        }
                        catch
                        {
                        MessageBox.Show("串口数据写入错误", "错误");
                        }
                    }
                    else //数据模式
                    {
                        try //如果此时用户输入字符串中含有非法字符（字母，汉字，符号等等，try，catch块可以捕捉并提示）
                        {
                            for (int i = 0; i < (textBox2.Text.Length - textBox2.Text.Length % 2) / 2; i++)//转换偶数个
                            {
                                Data[0] = Convert.ToByte(textBox2.Text.Substring(i * 2, 2), 16); //转换
                                serialPort1.Write(Data, 0, 1);
                            }
                            if (textBox2.Text.Length % 2 != 0)
                            {
                                Data[0] = Convert.ToByte(textBox2.Text.Substring(textBox2.Text.Length - 1, 1), 16);//单独处理最后一个字符
                                serialPort1.Write(Data, 0, 1); //写入
                            }
                        //Data = Convert.ToByte(textBox2.Text.Substring(textBox2.Text.Length - 1, 1), 16);
                        // }
                        }
                        catch
                        {
                        MessageBox.Show("数据转换错误，请输入数字。", "错误");
                        }
                    }
                }
            }
        }
    }
}
