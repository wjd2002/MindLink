using Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.Threading;
using System.IO;
using sulfur_nitrogen_ac.lib;
using System.Runtime.InteropServices;

namespace 灵犀_串口助手
{
    public partial class FrmMain : Form
    {


        public ManualResetEvent mre = new ManualResetEvent(false);//控制线程启动、暂停、恢复，目前没用上
        EllipseStyle ellipseStyle = new EllipseStyle();
        EllipseStyle2 ellipseStyle2 = new EllipseStyle2();
        public Style timeStyle;
        public Style findStyle;
        EllipseStyle valueStyle = new EllipseStyle();


        CmdStyle cmdStyle = new CmdStyle();
        DataStyle dataStyle = new DataStyle();
        public SerialPort sp = new SerialPort();
        FileStream fs;//日志保存
        FileStream fs_filter;//过滤命令保存

        class CmdStyle : Style
        {
            public override void Draw(Graphics gr, Point position, Range range)
            {
                //get size of rectangle
                Size size = GetSizeOfRange(range);
                //create rectangle
                Rectangle rect = new Rectangle(position, size);
                //inflate it
                rect.Inflate(0, 0);
                //get rounded rectangle
                var path = GetRoundedRectangle(rect, 7);
                //draw rounded rectangle
                //gr.DrawPath(Pens.Red, path);
                Color customColor = Color.FromArgb(105, Color.Blue);
                SolidBrush shadowBrush = new SolidBrush(customColor);
                gr.FillPath(shadowBrush, path);
            }
        }
        class DataStyle : Style
        {
            public override void Draw(Graphics gr, Point position, Range range)
            {
                //get size of rectangle
                Size size = GetSizeOfRange(range);
                //create rectangle
                Rectangle rect = new Rectangle(position, size);
                //inflate it
                rect.Inflate(0, 0);
                //get rounded rectangle
                var path = GetRoundedRectangle(rect, 7);
                //draw rounded rectangle
                //gr.DrawPath(Pens.Red, path);
                Color customColor = Color.FromArgb(215, Color.Orange);
                SolidBrush shadowBrush = new SolidBrush(customColor);
                gr.FillPath(shadowBrush, path);
            }
        }
        /// <summary>
        /// This style will drawing ellipse around of the word
        /// </summary>
        class EllipseStyle : Style
        {
            public override void Draw(Graphics gr, Point position, Range range)
            {
                //get size of rectangle
                Size size = GetSizeOfRange(range);
                //create rectangle
                Rectangle rect = new Rectangle(position, size);
                //inflate it
                rect.Inflate(0, 0);
                //get rounded rectangle
                var path = GetRoundedRectangle(rect, 7);
                //draw rounded rectangle
                //gr.DrawPath(Pens.Red, path);
                Color customColor = Color.FromArgb(105, Color.Purple);
                SolidBrush shadowBrush = new SolidBrush(customColor);
                gr.FillPath(shadowBrush, path);
            }
        }
        class EllipseStyle2 : Style
        {
            public override void Draw(Graphics gr, Point position, Range range)
            {
                //get size of rectangle
                Size size = GetSizeOfRange(range);
                //create rectangle
                Rectangle rect = new Rectangle(position, size);
                //inflate it
                rect.Inflate(2, 2);
                //get rounded rectangle
                var path = GetRoundedRectangle(rect, 7);
                //draw rounded rectangle
                gr.DrawPath(Pens.Blue, path);
            }
        }
        public FrmMain()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;//去掉线程访问主线程UI控件的安全检查，使用
            timeStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(55, Color.Green)));

            //TextStyle errorStyle = new TextStyle(Brushes.Red, null, FontStyle.Regular);
            //Brush yellow = new SolidBrush(Color.FromArgb(50, Color.Red));
            Color customColor = Color.FromArgb(55, Color.White);
            SolidBrush shadowBrush = new SolidBrush(customColor);
            Color customColor2 = Color.FromArgb(55, Color.Purple);
            SolidBrush shadowBrush2 = new SolidBrush(customColor2);
            findStyle = new TextStyle(Brushes.Black, shadowBrush, FontStyle.Regular); //new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Yellow)));
            //valueStyle = new TextStyle(Brushes.Black, shadowBrush2, FontStyle.Regular); //new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Yellow)));

        }
        /// <summary>
        ///初始化串口组件 
        /// </summary>
        public SerialPort initSerialPort()
        {
            if (sp.IsOpen)//如果打开状态，则先关闭一下
            {
                sp.Close();
            }

            sp.PortName = Common.SerialPortInfo.PortName;//设置串口名
            sp.BaudRate = Common.SerialPortInfo.BaudRate;  //波特率
            sp.DataBits = Common.SerialPortInfo.DataBits;  //数据位
            sp.StopBits = Common.SerialPortInfo.StopBits;//停止位
            sp.DtrEnable = Common.SerialPortInfo.DtrEnable;//该值在串行通信过程中启用数据终端就绪 (DTR) 信号  
            sp.RtsEnable = Common.SerialPortInfo.RtsEnable;//串行通信中是否启用请求发送 (RTS) 信号
            sp.ReadTimeout = Common.SerialPortInfo.ReadTimeout;//设置数据读取超时为1秒
            sp.ReceivedBytesThreshold = Common.SerialPortInfo.ReceivedBytesThreshold;//DataReceived 事件发生前内部输入缓冲区中的字节数
            sp.Parity = Common.SerialPortInfo.Parity; //校验位
            sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);//数据接收事件
            try
            {
                sp.Open();
                return sp;
            }
            catch (UnauthorizedAccessException)
            {//System.UnauthorizedAccessException 端口占用异常
                //RuntimeParameters.AnomalySignal.online = true;
                //sp_connected = false;
                UnauthorizedAccessException err = new UnauthorizedAccessException();
                rtbinfo.AppendText(err.Message);
                rtbinfo.SelectionStart = rtbinfo.Text.Length;
                rtbinfo.SelectionLength = 0;
                //fctb.ScrollToCaret();
                //frmMain.sbtnstart.Enabled = true;
                throw err;
                //log.Error(err.Message);
                //MessageBox.Show("异常信息：" + err.Message.ToString(), "错误提示");
            }
            catch (Exception err)
            {
                //RuntimeParameters.AnomalySignal.online = true;
                //sp_connected = false;
                rtbinfo.AppendText(err.Message);
                rtbinfo.SelectionStart = rtbinfo.Text.Length;
                rtbinfo.SelectionLength = 0;
                //frmMain.sbtnstart.Enabled = true;
                //log.Error(err.Message);
                //MessageBox.Show("异常信息：" + err.Message.ToString(), "错误提示");
                throw err;

            }
            return sp;

        }
        int total_rows = 0;
        public void showData(byte[] data)
        {

            string hex = BitConverter.ToString(data, 0);
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff: ");
            hex = hex.Replace("-", " ");
            #region 获取下位机参数值
            if (lstcmds.Items.Count > 0)
            {
                int item_index = 0;
                foreach (ListViewItem item in lstcmds.Items)
                {

                    float fv = getMachineValue(hex, "AA 55 05 " + item.SubItems[1].Text);

                    if (fv > 0)
                    {
                        lstcmds.Items[item_index].SubItems[3].Text = fv.ToString("0.00");//> 0 ? fv.ToString("0.00"): "";
                    }
                    item_index++;
                }

            }
            #endregion
            if (chksave.Checked)
            {
                StreamWriter sw = new StreamWriter(fs);
                //sw.WriteLine("\r\n===" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "串口接收数据===\r\n");
                sw.WriteLine(now + hex + "\r\n");
                sw.Flush();
            }
            if (chkautoclear.Checked)
            {
                if (total_rows > 1000)
                {
                    fctb.Clear();
                    total_rows = 0;
                }
            }
            data_begin_strings.Clear();
            if (!chkfilter.Checked)
            {
                int start = 0;
                string find = hex;
                while (find.IndexOf("AA 55 ", start, find.Length) > -1)
                {
                    int pos = find.IndexOf("AA 55 ");
                    if ((pos + 12) > find.Length)
                    {
                        break;
                    }
                    string temp = find.Substring(pos, 12);
                    if (temp != null)
                    {
                        data_begin_strings.Add(temp);
                        //data_begin_strings[index] = temp;
                        if (find.Length < 38)
                            break;
                        if ((pos + 42) > find.Length)
                        {
                            break;
                        }
                        find = find.Substring(pos + 42);
                        if (find == null) break;
                    }
                    else
                    {
                        break;
                    }

                    // index++;

                }




                if (!chkblank.Checked)
                {

                    fctb.AppendText(now, timeStyle);
                    fctb.AppendText(hex + "\n");
                    total_rows = total_rows + 4;
                }
            }
            else
            {
                if (chkblank.Checked) return;

                string[] findResult;// = UtilsEx.Getunit(hex, "(AA-55)[.\\s\\S]*?(CC-33-C3-3C)");
                                    //if (findResult == null) return;
                #region 数据过滤，只有符合条件的数据才会显示
                foreach (string item in tbfilter.Lines)
                {
                    if (item.Equals("")) continue;
                    if (item.Substring(0, 2).Equals("--") || item.Substring(1, 2).Equals("--")) continue;

                    string[] arr = item.Split('*');
                    if (arr.Length > 1)
                        findResult = UtilsEx.Getunit(hex, "(" + arr[0] + ")[.\\s\\S]*?(" + arr[1] + ")"); // "(?<=(AA-55))[.\\s\\S]*?(?=(CC-33-C3-3C))"
                    else
                        findResult = UtilsEx.Getunit(hex, "(" + arr[0] + ")[.\\s\\S]*?"); // "(?<=(AA-55))[.\\s\\S]*?(?=(CC-33-C3-3C))"
                    if (findResult == null) continue;
                    foreach (string value in findResult)
                    {
                        string[] rxd = value.Split(' ');

                        string cmd_name = "";
                        if (rxd.Length > 3)
                        {
                            cmd_name = getCmdName(rxd[3]);

                        }

                        float fv = processBuffer(value);
                        fctb.AppendText(now, timeStyle);
                        int start = 0;
                        string find = value;

                        while (find.IndexOf("AA 55 ", start, find.Length) > -1)
                        {
                            int pos = find.IndexOf("AA 55 ");
                            string temp = find.Substring(pos, 12);
                            if (temp != null)
                            {
                                data_begin_strings.Add(temp);
                                find = find.Substring(pos + 41);
                                if (find == null) break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        //data_begin_string = value.Substring(value.IndexOf("AA 55 "), 12);
                        fctb.AppendText(value, findStyle);
                        fctb.AppendText(" {" + cmd_name + "：" + fv.ToString("0.00") + "}\n");//valueStyle
                        total_rows++;
                        if (chksave.Checked)
                        {
                            StreamWriter sw = new StreamWriter(fs);
                            //sw.WriteLine("\r\n===" + DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss ") + "串口接收数据===\r\n");
                            sw.WriteLine(now + value + " { " + cmd_name + "：" + fv.ToString("0.00") + "}\r\n");
                            sw.Flush();
                        }
                    }
                }
            }
            #endregion
            if (!chkblank.Checked)
            {
                //fctb.AppendText(Environment.NewLine);
                fctb.GoEnd();
            }

        }
        /// <summary>
        /// 来自下位机的信息
        /// </summary>
        public struct FromLowerComputer
        {
            public static byte headA = 0xAA;
            //数据头特征0xAA，0x55
            public static byte headB = 0x55;
            /// <summary>
            /// 下位机指令+数据总长度，一般为5，特殊的20（上传的是设备型号信息）
            /// </summary>
            public static byte len;
            /// <summary>
            /// 下位机上传指令
            /// </summary>
            public static byte cmd;
            /// <summary>
            /// 下位机上传的数据
            /// </summary>
            public static byte[] data;//4 或多字节数据区
            /// <summary>
            /// 下位机上传CRC校验码
            /// </summary>
            public static byte c1;
            /// <summary>
            /// 下位机上传CRC校验码
            /// </summary>
            public static byte c2;
            public static byte[][] arr_lower_computer_data = new byte[1024][];//下位机数据保存
            public static byte endA = 0xcc;
            public static byte endB = 0x33;
            public static byte endC = 0xc3;
            public static byte endD = 0x3c;

        }
        private int mpkg_framenum = 1;//数据包内数据帧数
        List<byte> mlst_bt = new List<byte>();//串口数据封包使用，动态
        private int mpkg_cnt = 1;//现定每mpkg_framenum个数据合并一个包，进行解析
        public byte[] g_machine;// = new byte[len];//下位机上传数据
        public delegate void Invoke_machine_data(byte[] data);//代理函数更新状态栏中的在线状态
        public string g_newStr = "";//串口每次获取的字符串
        public int data_length = 0;
        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            mre.WaitOne();//根据是否收到信号判断阻塞当前线程
            if (!sp.IsOpen)//安全措施
            {
                //Exception error = new Exception();
                //throw error;
                return;
            }
            //生成随机数，判断下位机是否联线或者usb已联但电源未联
            int len = sp.BytesToRead;//数据的长度
            byte[] lowerData = new byte[len];//下位机上传数据
            sp.Read(lowerData, 0, len);

            Invoke_machine_data invoke = new Invoke_machine_data(showData);
            BeginInvoke(invoke, new object[] { lowerData });

            string hex2 = BitConverter.ToString(lowerData, 0);
            //mpkg_framenum条数据合并一个数据
            if (mpkg_cnt - mpkg_framenum < 1)
            {
                mlst_bt.AddRange(lowerData.ToList());

                //读包mpkg_framenum次进行处理
                if (mpkg_cnt == mpkg_framenum)
                {
                    //processBuffer(mlst_bt);
                    mlst_bt.Clear();
                    mpkg_cnt = 0;
                }
                mpkg_cnt++;
            }
            return;



        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            btnsend.Left = this.Width - 100;
            cbrtbsend.Left = 100;
            cbrtbsend.Width = btnsend.Left - 120;
            label7.Left = cbrtbsend.Left - 70;
            fctb.AutoScroll = true;
            fctb.IsChanged = false;
            fctb.ClearUndo();
            //set delay interval (10 ms)
            fctb.DelayedEventsInterval = 10;
            //初始化各个cbb
            cbbname.Items.Clear();
            string[] spnames = UtilsEx.MulGetsSrialPortInfo("Name");
            foreach (string val in spnames)
            {
                cbbname.Items.Add(val);
            }
            //for (int i = 1; i < 30; i++)
            //{
            //    cbbname.Items.Add("COM" + i);
            //}
            if (cbbname.Items.Count > 0)
                cbbname.SelectedIndex = 0;
            //cbbname.Text = cbbname.SelectedIndex;
            cbbbr.Text = "57600";
            cbbbit.Text = "8";
            cbbstop.Text = "1";
            cbbpb.Text = "None";
            fctb.Text = "";
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            if (chksave.Checked)
            {

                if (!File.Exists(appPath + "RXD.log"))
                {
                    fs = new FileStream(appPath + "RXD.log", FileMode.Create, FileAccess.Write);
                }
                else
                {
                    fs = new FileStream(appPath + "RXD.log", FileMode.Open, FileAccess.Write);
                }
            }
            //
            string inifile = "setting.ini";
            if (!File.Exists(appPath + inifile))
            {
                fs_filter = new FileStream(appPath + inifile, FileMode.Create, FileAccess.Write);
            }
            else
            {
                fs_filter = new FileStream(appPath + inifile, FileMode.Open, FileAccess.Write);
            }
            fs_filter.Close();


            //读取ini

            string fc = UtilsEx.Read("filters", "count", "0", appPath + inifile);
            int filters_count = int.Parse(fc);
            tbfilter.Clear();
            for (int i = 0; i < filters_count; i++)
            {
                string item = UtilsEx.Read("filters", "Q" + (i + 1), "", appPath + inifile);
                //tbfilter.Lines.Append(item);
                tbfilter.Text += item + "\r\n";
            }
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("这是Mr.Wang的小软件，能用颜色区分串口接收的数据，心有灵犀.", "串口小助手", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 连接串口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Common.setForm(this);
            //Common.frmMain = this;
            Common.SerialPortInfo.PortName = cbbname.Text;
            Common.SerialPortInfo.BaudRate = int.Parse(cbbbr.Text);
            Common.SerialPortInfo.DataBits = int.Parse(cbbbit.Text);
            Common.SerialPortInfo.StopBits = StopBits.One;// cbbname.Text;
            Common.SerialPortInfo.Parity = Parity.None;// cbbname.Text;

            Common.initSerialPort();

        }

        private void rtbSP_TextChanged(object sender, EventArgs e)
        {
            fctb.SelectionStart = fctb.Text.Length;
            fctb.SelectionLength = 0;
            //fctb.ScrollToCaret();
            //ColourRrbText(fctb);
            // HighlightPhrase(fctb, "AA-55", Color.Red, "");
            // HighlightPhrase(fctb, "CC-33-C3-3C", Color.Green, "");
            //Regex regExp = new Regex("\b(AA-55|Next|If|Then)\b");

            //foreach (Match match in regExp.Matches(fctb.Text))
            //{
            //    fctb.Select(match.Index, match.Length);
            //    fctb.SelectionColor = Color.Blue;
            //}
        }

        private void 关闭连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Common.sp.Close();
        }
        void HighlightPhrase(RichTextBox box, string phrase, Color color, string txt)
        {
            int pos = box.SelectionStart;
            string s = box.Text;//Common.g_newStr ;// 
            for (int ix = 0; ;)
            {
                int jx = s.IndexOf(phrase, ix, StringComparison.CurrentCultureIgnoreCase);
                if (jx < 0) break;
                box.SelectionStart = jx;
                box.SelectionLength = phrase.Length;
                box.SelectionColor = Color.White;
                box.SelectionBackColor = color;// Color.Blue;
                ix = jx + 1;
            }
            box.SelectionStart = pos;
            box.SelectionLength = 0;
        }
        private void ColourRrbText(RichTextBox rtb)
        {
            RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled;
            Regex regExp = new Regex(rtb.Text, options); //new Regex("\b(AA|Next|If|Then)\b");

            foreach (Match match in regExp.Matches("AA"))
            {
                rtb.Select(match.Index, match.Length);
                rtb.SelectionColor = Color.Blue;
            }


            //Regex regex = new Regex(rtb.Text, options);
            //Match matches = regex.Match("AA-55");
            //while (matches.Success)
            //{
            //    rtb.Select(matches.Index, matches.Length);
            //    rtb.SelectionColor = Color.Red;
            //    rtb.SelectionBackColor = Color.Black;
            //}
        }
        string data_begin_string = "";
        List<string> data_begin_strings = new List<string>();
        private void fctb_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {

            //string len_str = @"\w{2}(?=[\w\s]{33}$)";
            ////string data_str = @"\s\S...........(?=[\s\S]{14}$)";
            //StringBuilder sb_data = new StringBuilder();
            //StringBuilder sb_cmd = new StringBuilder();
            //foreach (string str in data_begin_strings)
            //{
            //    if (str == null) break;
            //    sb_data.Append(str);
            //    sb_data.Append("|");
            //    sb_cmd.Append(str.Substring(0, str.Length - 3));
            //    sb_cmd.Append("|");

            //}
            //if (sb_data.Length > 1)
            //    sb_data.Remove(sb_data.Length - 1, 1);
            //if (sb_cmd.Length > 1)
            //    sb_cmd.Remove(sb_cmd.Length - 1, 1);
            //string data_str = @"(?<=" + sb_data.ToString() + ")([\\s\\S]..........)";
            //string cmd_str = @"(?<=" + sb_cmd.ToString() + ")([\\s\\S].)"; //@"\w{2}(?=[\w\s]{30}$)";

            //e.ChangedRange.SetStyle(valueStyle, len_str, RegexOptions.Singleline);// (?<= ({))[.\\s\\S] *? (?= (}))
            //e.ChangedRange.SetStyle(cmdStyle, cmd_str, RegexOptions.Singleline);// (?<= ({))[.\\s\\S] *? (?= (}))
            //e.ChangedRange.SetStyle(dataStyle, data_str, RegexOptions.Singleline);// (?<= ({))[.\\s\\S] *? (?= (}))
            fctb.GoEnd();
            //(sender as FastColoredTextBoxNS.FastColoredTextBox).GoEnd();

        }
        List<String> lst_cmds = new List<string>();
        Dictionary<string, string> dict_cmds = new Dictionary<string, string>();
        private void DataSyntaxHighlight(Range range)
        {
            string len_str = @"\w{2}(?=[\w\s]{33}$)";
            //string data_str = @"\s\S...........(?=[\s\S]{14}$)";
            StringBuilder sb_data = new StringBuilder();
            StringBuilder sb_cmd = new StringBuilder();
            bool state = false;
            foreach (string str in data_begin_strings)
            {
                if (str == null) break;
                sb_data.Append(str);
                sb_data.Append("|");
                sb_cmd.Append(str.Substring(0, str.Length - 3));
                string[] temp = str.Split(' ');
                //dict_cmds.Add(temp[temp.Length - 2], temp[temp.Length - 2]);

                foreach (string s in lst_cmds)
                {
                    if (s.Equals(temp[temp.Length - 2]))
                    {
                        state = true;
                        break;
                    }
                }
                if (!state)
                {
                    lst_cmds.Add(temp[temp.Length - 2]);
                    if (chkfindcmd.Checked)
                    {
                        if (rtbcmds.Lines.Length < 2)
                        {
                            rtbcmds.Clear();
                            lstcmds.Items.Clear();
                            rtbcmds.Text = "命令列表：\n";
                            int index = 0;
                            foreach (string s in lst_cmds)
                            {
                                rtbcmds.Text += s + ", ";
                                ListViewItem lvi = new ListViewItem();
                                lvi.SubItems[0].Text = (index + 1).ToString();
                                //lvi.SubItems.Add((index + 1).ToString());
                                lvi.SubItems.Add(s);
                                string cmd_code = s;
                                lvi.SubItems.Add(getCmdName(cmd_code));
                                lvi.SubItems.Add("");
                                lstcmds.Items.Add(lvi);
                                index++;
                            }
                        }
                        else
                        {
                            rtbcmds.Text += temp[temp.Length - 2] + ", ";
                            int index = lstcmds.Items.Count;
                            ListViewItem lvi = new ListViewItem();
                            lvi.SubItems[0].Text = (index + 1).ToString();
                            string cmd_code = temp[temp.Length - 2];

                            lvi.SubItems.Add(cmd_code);
                            lvi.SubItems.Add(getCmdName(cmd_code));
                            lvi.SubItems.Add("");
                            lstcmds.Items.Add(lvi);
                        }

                    }

                }
                sb_cmd.Append("|");

            }



            if (sb_data.Length > 1)
                sb_data.Remove(sb_data.Length - 1, 1);
            if (sb_cmd.Length > 1)
                sb_cmd.Remove(sb_cmd.Length - 1, 1);
            string data_str = @"(?<=" + sb_data.ToString() + ")([\\s\\S]..........)";
            string cmd_str = @"(?<=" + sb_cmd.ToString() + ")([\\s\\S].)"; //@"\w{2}(?=[\w\s]{30}$)";

            //range.SetStyle(valueStyle, len_str, RegexOptions.Singleline);// (?<= ({))[.\\s\\S] *? (?= (}))
            //range.SetStyle(cmdStyle, cmd_str, RegexOptions.Singleline);// (?<= ({))[.\\s\\S] *? (?= (}))
            //range.SetStyle(dataStyle, data_str, RegexOptions.Singleline);// (?<= ({))[.\\s\\S] *? (?= (}))

        }
        private string getCmdName(string cmdCode)
        {
            string cmd_code = cmdCode;

            string cmd_name = "";
            switch (cmd_code)
            {
                case "50"://大气压
                    cmd_name = "大气压";
                    break;
                case "80"://程序是否通信
                    cmd_name = "上下位机接通标志";
                    break;
                case "51"://左蒸馏管回收室的温度
                    cmd_name = "左蒸馏管回收室的温度";
                    break;
                case "52"://左蒸馏管循环水冷却液的温度
                    cmd_name = "左蒸馏管循环水冷却液的温度";
                    break;
                case "53"://左蒸馏管跟踪模块实时检测ADC的值地址
                    cmd_name = "左蒸馏管跟踪模块实时检测ADC的值地址";
                    break;
                case "54"://左蒸馏管样品回收体积
                    cmd_name = "左蒸馏管样品回收体积";
                    break;
                case "55"://左蒸馏管初馏点温度地址
                    cmd_name = "左蒸馏管初馏点温度地址";
                    break;
                case "56"://左蒸馏管终馏点温度地址
                    cmd_name = "左蒸馏管终馏点温度地址";
                    break;
                case "57"://左蒸馏管从开始加热到初馏点流出的计时时间地址
                    cmd_name = "左蒸馏管从开始加热到初馏点流出的计时时间地址";
                    break;
                case "58"://左蒸馏管里蒸汽的温度
                    cmd_name = "左蒸馏管回收室的温度";
                    break;
                case "59"://左蒸馏管馏出物的速度
                    cmd_name = "左蒸馏管馏出物的速度";
                    break;
                case "5A"://左蒸馏管电炉子的温度
                    cmd_name = "左蒸馏管电炉子的温度";
                    break;
                case "5B"://左蒸馏管液滴的滴数地址
                    cmd_name = "左蒸馏管液滴的滴数地址";
                    break;
                case "5C"://左蒸馏管蒸馏的状态
                    cmd_name = "左蒸馏管状态";
                    break;
                case "67"://右蒸馏管循环水冷却液的温度
                    cmd_name = "右蒸馏管循环水冷却液的温度";
                    break;
                case "68"://右蒸馏管跟踪模块实时检测ADC的值地址     
                    cmd_name = "右蒸馏管跟踪模块实时检测ADC的值地址     ";
                    break;
                case "69"://右蒸馏管样品回收体积
                    cmd_name = "右蒸馏管样品回收体积";
                    break;
                case "6A"://右蒸馏管初馏点温度地址
                    cmd_name = "右蒸馏管初馏点温度地址";
                    break;
                case "6B"://右蒸馏管终馏点温度地址
                    cmd_name = "右蒸馏管终馏点温度地址";
                    break;
                case "6C"://右蒸馏管从开始加热到初馏点流出的计时时间地址
                    cmd_name = "右蒸馏管从开始加热到初馏点流出的计时时间地址";
                    break;
                case "6D"://右蒸馏管里蒸汽的温度
                    cmd_name = "右蒸馏管里蒸汽的温度";
                    break;
                case "6E"://右蒸馏管馏出物的速度
                    cmd_name = "右蒸馏管馏出物的速度";
                    break;
                case "6F"://右蒸馏管电炉子的温度
                    cmd_name = "右蒸馏管电炉子的温度";
                    break;
                case "70"://右蒸馏管液滴的滴数地址
                    cmd_name = "右蒸馏管液滴的滴数地址";
                    break;
                case "71"://右蒸馏管蒸馏的状态
                    cmd_name = "右蒸馏管状态";
                    break;
                case "72"://右蒸馏管回收室的温度
                    cmd_name = "右蒸馏管回收室的温度";
                    break;
                case "98"://
                    cmd_name = "左管功率";
                    break;
                case "99"://
                    cmd_name = "右管功率";
                    break;
                default:
                    break;
            }
            return cmd_name;
        }


        float fv_cmds = 0;
        /// <summary>
        /// 获取下位机发送到参数对应值
        /// </summary>
        /// <returns></returns>
        private float getMachineValue(string hex, string paramaters)
        {
            //AA 55 05 99
            string[] findResult = UtilsEx.Getunit(hex, "(" + paramaters + ")[.\\s\\S]*?(CC 33 C3 3C)"); // "(?<=(AA-55))[.\\s\\S]*?(?=(CC-33-
            #region 获取下位机发送到参数数值
            //if (arr.Length > 1)
            //    findResult = UtilsEx.Getunit(hex, "(" + arr[0] + ")[.\\s\\S]*?(" + arr[1] + ")"); // "(?<=(AA-55))[.\\s\\S]*?(?=(CC-33-C3-3C))"
            //else
            //    findResult = UtilsEx.Getunit(hex, "(" + arr[0] + ")[.\\s\\S]*?"); // "(?<=(AA-55))[.\\s\\S]*?(?=(CC-33-C3-3C))"
            if (findResult == null) return 0;

            foreach (string value in findResult)
            {
                fv_cmds = processBuffer(value);

            }
            #endregion
            return fv_cmds;
        }
        private void skinButton1_Click(object sender, EventArgs e)
        {
            //Common.frmMain = this;
            string comName = cbbname.Text;
            int pos1 = comName.IndexOf("(");
            int pos2 = comName.IndexOf(")");
            comName = comName.Substring(pos1+1, pos2 - pos1 -1);
            Common.SerialPortInfo.PortName = comName;// cbbname.Text;
            Common.SerialPortInfo.BaudRate = int.Parse(cbbbr.Text);
            Common.SerialPortInfo.DataBits = int.Parse(cbbbit.Text);
            Common.SerialPortInfo.StopBits = StopBits.One;// cbbname.Text;
            Common.SerialPortInfo.Parity = Parity.None;// cbbname.Text;
            try
            {

                if (sp.IsOpen)
                {
                    sp.Close();
                }
                initSerialPort();
                mre.Set();
                //if (!Common.sp.IsOpen)
                //{

                //    Common.initSerialPort();
                //    mre.Set();
                //}
                //else
                //{

                //    mre.Set();
                //}
                sbtnstart.Enabled = false;
                rtbinfo.AppendText("串口已连接.\n");
                rtbinfo.ScrollToCaret();
            }
            catch (Exception err)
            {
                sbtnstart.Enabled = true;
            }

        }
        /// <summary>
        /// 从数据包中截取完整数据包，AA 55开头，CC-33-C3-3C结束，但没有结尾则存入缓存变量
        /// 提高数据包的识别率
        /// </summary>
        /// <param name="bt">mpkg_framenum个数据包组合在一起传入</param>
        public float processBuffer(string data)
        {
            string hex = data;// BitConverter.ToString(data, 0);
            string[] databuffer = UtilsEx.Getunit(hex, "(?<=(AA 55))[.\\s\\S]*?(?=(CC 33 C3 3C))");
            if (databuffer == null) return 0;
            byte dframe_len = 0x0;//每条指令中包含的数据长度
            float fvalue = 0;
            //for (int i = 1; i <= databuffer.Length; i++)
            //{
            databuffer[0] = databuffer[0].Substring(1, databuffer[0].Length - 2);
            string[] stemp = databuffer[0].Split(' ');
            byte[] bt_temp = new byte[stemp.Length];
            for (int k = 0; k <= stemp.Length - 1; k++)
            {
                bt_temp[k] = Convert.ToByte(stemp[k], 16);
            }

            dframe_len = bt_temp[0];
            FromLowerComputer.cmd = bt_temp[1];//公共变量赋值
            FromLowerComputer.data = new byte[dframe_len - 1];
            //将下位机上传的数据内容赋值公共变量，供其他模块使用 
            Buffer.BlockCopy(bt_temp, 2, FromLowerComputer.data, 0, dframe_len - 1);

            FromLowerComputer.c1 = bt_temp[bt_temp.Length - 2];
            FromLowerComputer.c2 = bt_temp[bt_temp.Length - 1];
            //
            fvalue = BitConverter.ToSingle(FromLowerComputer.data, 0);
            return fvalue;
            //}
            // return fvalue;

        }
        private void sbtnstop_Click(object sender, EventArgs e)
        {
            sbtnstart.Enabled = true;
            rtbinfo.AppendText("串口已关闭.\n");
            rtbinfo.ScrollToCaret();
            sp.Close();
            // mre.Reset();//暂停对应的线程thread7
            //Thread thread1 = new Thread(new ThreadStart(check_online))
        }

        private void chksave_CheckedChanged(object sender, EventArgs e)
        {
            if (chksave.Checked)
            {
                string appPath = AppDomain.CurrentDomain.BaseDirectory;

                if (!File.Exists(appPath + "RXD.log"))
                {
                    fs = new FileStream(appPath + "RXD.log", FileMode.Create, FileAccess.Write);
                }
                else
                {
                    fs = new FileStream(appPath + "RXD.log", FileMode.Open, FileAccess.Write);
                }
            }
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (fs != null)
                fs.Close();
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string inifile = "setting.ini";
            int filter_cnt = 0;

            for (int i = 0; i < tbfilter.Lines.Length; i++)
            {
                string item = tbfilter.Lines[i];
                if (item.Equals("")) continue;
                UtilsEx.Write("filters", "Q" + (i + 1), item, appPath + inifile);
                filter_cnt++;
            }
            UtilsEx.Write("filters", "count", filter_cnt.ToString(), appPath + inifile);
            if (fs_filter != null)
                fs_filter.Close();
        }

        private void btnselall_Click(object sender, EventArgs e)
        {

            string ss = tbfilter.Lines[3];
            if (tbfilter.SelectionLength > 0)
            {
                string[] lines = new string[tbfilter.Lines.Length];
                lines = tbfilter.Lines;
                for (int i = 0; i < tbfilter.Lines.Length; i++)
                {
                    string item = tbfilter.Lines[i];
                    if (item.Equals("")) continue;
                    if (item.Substring(0, 3).Equals("✘--")) item = item.Substring(3, item.Length - 3);
                    lines[i] = item;

                }
                tbfilter.Clear();
                tbfilter.Lines = lines;
            }
            else
            {

                string[] lines = new string[tbfilter.Lines.Length];
                lines = tbfilter.Lines;
                for (int i = 0; i < tbfilter.Lines.Length; i++)
                {
                    string item = tbfilter.Lines[i];
                    if (item.Equals("")) continue;
                    if (item.Substring(0, 3).Equals("✘--")) item = item.Substring(3, item.Length - 3);
                    lines[i] = item;

                }
                tbfilter.Clear();
                tbfilter.Lines = lines;
            }
            chkfilter.Checked = true;
        }

        private void btnselcancel_Click(object sender, EventArgs e)
        {
            string[] lines = new string[tbfilter.Lines.Length];
            lines = tbfilter.Lines;
            //tbfilter.Lines.CopyTo(lines, 0);

            for (int i = 0; i < tbfilter.Lines.Length; i++)
            {
                string item = tbfilter.Lines[i];
                if (item.Equals("")) continue;
                if (!item.Substring(0, 3).Equals("✘--")) item = "✘--" + item;
                lines[i] = item;
            }
            tbfilter.Clear();
            tbfilter.Lines = lines;
        }

        private void fctb_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 发往下位机的命令数据
        /// </summary>
        public struct ToLowerComputer
        {
            public static byte headA = 0xAA;
            //数据头特征0xAA，0x55
            public static byte headB = 0x55;
            /// <summary>
            /// 指令+数据总长度，一般为5，特殊的20（上传的是设备型号信息）
            /// </summary>
            public byte len;
            /// <summary>
            /// 指令
            /// </summary>
            public byte cmd;
            /// <summary>
            /// 数据
            /// </summary>
            public byte[] data_arry;//4 或多字节数据区
            public float data_value;
            /// <summary>
            /// CRC校验码
            /// </summary>
            public byte c1;
            /// <summary>
            /// CRC校验码
            /// </summary>
            public byte c2;
            public static byte endA = 0xcc;
            public static byte endB = 0x33;
            public static byte endC = 0xc3;
            public static byte endD = 0x3c;

            private byte[] arr_senddata;//拼接命令数据帧,目前是14个字节
            /// <summary>
            /// 转为byte[]返回，完整发送数据
            /// </summary>
            /// <returns></returns>
            public byte[] toByteArry()
            {
                arr_senddata = new byte[14];//拼接命令数据帧
                arr_senddata[0] = headA;
                arr_senddata[1] = headB;
                arr_senddata[2] = 0x05;//命令+数据长度
                arr_senddata[3] = this.cmd;//命令
                //数据内容
                data_arry = BitConverter.GetBytes(data_value);
                data_arry.CopyTo(arr_senddata, 4);
                //CRC校验码2字节
                byte[] crc_src = new byte[5];
                crc_src[0] = this.cmd;
                data_arry.CopyTo(crc_src, 1);
                //此处生成crc校验与原软件不同，原软件使用GenerateCRC与当前算法不符,
                //故使用generateCRC16使上传和下发统一算法

                byte[] crc_result = CRC16.generateCRC16(crc_src, 5);   //Utils.GenerateCRC(crc_src, 0, 4);

                crc_result.CopyTo(arr_senddata, 8);

                byte[] arr_end = new byte[] { endA, endB, endC, endD };
                arr_end.CopyTo(arr_senddata, 10);

                return arr_senddata;
            }
        }
        /// <summary>
        /// 给下位机下发参数设置，1或多参数
        /// </summary>
        public struct ToLowerComputer_parameters
        {
            public static byte headA = 0xAA;
            //数据头特征0xAA，0x55
            public static byte headB = 0x55;
            /// <summary>
            /// 指令+数据总长度，一般为5，特殊的20（上传的是设备型号信息）
            /// </summary>
            public byte len;
            /// <summary>
            /// 指令
            /// </summary>
            public byte[] cmds;
            /// <summary>
            /// 数据
            /// </summary>
            public float[] datas;//
            /// <summary>
            /// CRC校验码
            /// </summary>
            public byte c1;
            /// <summary>
            /// CRC校验码
            /// </summary>
            public byte c2;
            public static byte endA = 0xcc;
            public static byte endB = 0x33;
            public static byte endC = 0xc3;
            public static byte endD = 0x3c;

            private byte[] arr_senddata;//拼接命令数据帧,目前是14个字节
            /// <summary>
            /// 转为byte[]返回，完整发送数据
            /// </summary>
            /// <returns></returns>
            public byte[] toByteArry()
            {
                int len = cmds.Length + datas.Length * 4;
                int arrlength = 9 + len;
                arr_senddata = new byte[arrlength];//拼接命令数据帧
                arr_senddata[0] = headA;
                arr_senddata[1] = headB;
                arr_senddata[2] = (byte)len;//命令+数据长度
                byte[] data_arry = new byte[4];

                //命令+数据、命令+数据...此为参数下发的格式
                for (int i = 0; i < cmds.Length; i++)
                {
                    arr_senddata[3 + i * 5] = cmds[i];
                    data_arry = BitConverter.GetBytes(datas[i]);
                    Buffer.BlockCopy(data_arry, 0, arr_senddata, 4 + i * 5, 4);
                    //arr_senddata[4 + i] = (byte);
                }
                //数据内容
                //CRC校验码2字节
                byte[] crc_src = new byte[len];//crc计算用的源数组
                //arr_senddata.CopyTo(crc_src, 0);
                Buffer.BlockCopy(arr_senddata, 3, crc_src, 0, len);

                byte[] crc_result = CRC16.generateCRC16(crc_src, len);   //Utils.GenerateCRC(crc_src, 0, 4);

                crc_result.CopyTo(arr_senddata, 3 + len);

                byte[] arr_end = new byte[] { endA, endB, endC, endD };
                arr_end.CopyTo(arr_senddata, 5 + len);

                return arr_senddata;
            }
        }
        public void sendONOFFcmd(float p_cmd, byte p_cmd_type)
        {
            ToLowerComputer cmd = new ToLowerComputer();
            cmd.len = 5;
            cmd.cmd = p_cmd_type;
            cmd.data_value = p_cmd;
            sendInfo(cmd.toByteArry());
        }
        /// <summary>
        /// 给下位机发送一个或多个参数
        /// </summary>
        /// <param name="p_cmds">命令数组</param>
        /// <param name="p_values">值数组</param>
        public void sendPARAcmd(byte[] p_cmds, float[] p_values)
        {
            ToLowerComputer_parameters cmd = new ToLowerComputer_parameters();
            cmd.cmds = p_cmds;
            cmd.datas = p_values;
            sendInfo(cmd.toByteArry());
        }
        private void sendInfo(byte[] cmdCode)
        {
            /**
             * AA 55 05 53 00 00 00 00 0C 84 CC 33 C3 3C 
			 * 0xaa 0x55 len cmd D0 D1 D2 D3 C1 C2 0xcc 0x33 0xc3 0x3c
			 * 0    1    2   3   4  5  6  7  8  9  10   11   12   13
			 */
            try
            {
                sp.Write(cmdCode, 0, cmdCode.Length);
            }
            catch (Exception e)
            {
                //log.Error(e.Message);
                //e.printStackTrace();
            }

        }

        private void btnsend_Click(object sender, EventArgs e)
        {

            if (cbrtbsend.Text.Equals("")) return;
            string sendvalue = cbrtbsend.Text.Split('-')[1];// cbrtbsend.Text.Trim();
            string[] send = sendvalue.Split(' ');
            byte[] bt = new byte[send.Length];
            int i = 0;
            foreach (string item in send)
            {
                if (item.Equals("")) continue;
                bt[i] = Convert.ToByte(item, 16);
                i++;
            }
            sendInfo(bt);

        }

        private void rtbsend_KeyDown(object sender, KeyEventArgs e)
        {


        }
        //int AApos = 0;
        private void fctb_TextChanging(object sender, TextChangingEventArgs e)
        {
            //if (e.InsertingText != null)
            //{
            //    string str = e.InsertingText;
            //    if (str.Length > 10)
            //    {
            //        AApos = str.IndexOf("AA 55");
            //    }

            //    //e.InsertingText = str.Substring(5, str.Length - 5);
            //}
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            fctb.Clear();
        }

        private void fctb_VisibleRangeChangedDelayed(object sender, EventArgs e)
        {
            DataSyntaxHighlight(fctb.VisibleRange);
        }

        private void cbrtbsend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsend_Click(sender, e);
            }
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            btnsend.Left = this.Width - 100;
            cbrtbsend.Left = 100;
            cbrtbsend.Width = btnsend.Left - 120;
            label7.Left = cbrtbsend.Left - 70;
        }
    }
}
