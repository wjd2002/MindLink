using sulfur_nitrogen_ac.lib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using 灵犀_串口助手;

/// <summary>
/// 公共代码类
/// 2020-11-17
/// </summary>
namespace Lib
{
    class Common
    {
        

        public static string state_hasData = ""; //缓冲区有新数据到达标识,内容为随机guid
        public static string check_state_hasData = ""; //校验缓冲数据标识
        private static byte[] mbuffer = new byte[10240];//串口接收缓存
        private static List<byte> mlst_bt = new List<byte>();//串口数据封包使用，动态
        private static List<byte> checkSP_mlst_bt = new List<byte>();//测试串口数据封包使用，动态
                                                                     // private static int mrec_length = 0;//每次收到的数据包大小
        private static int mpkg_framenum = 1;//数据包内数据帧数

        private static int mpkg_cnt = 1;//现定每mpkg_framenum个数据合并一个包，进行解析
        public static int checkSP_mpkg_cnt = 1;//测试串口连接时用到的计数器
        public static bool checkSP_passTest = true;//测试串口连接是否有对应下位机，首选判断数据库配置，假设可以联机true

        public static byte[] g_machine;// = new byte[len];//下位机上传数据
        public static FrmMain frmMain;// = new byte[len];//下位机上传数据
        public delegate void Invoke_machine_data(byte[] data);//代理函数更新状态栏中的在线状态
        public static string g_newStr = "";//串口每次获取的字符串
        public static int data_length = 0;
        //static class X
        //{
        //    //public static FrmMain frmMain2;
        //    public static void showData(byte[] data)
        //    {
        //        string hex = BitConverter.ToString(data, 0);
                


        //        frmMain.fctb.AppendText(DateTime.Now.ToString("HH:mm:ss:fff"), frmMain.timeStyle);
        //        frmMain.fctb.AppendText(": " + hex + "\n");
                    
        //        #region 数据过滤，只有符号条件的数据才会显示
        //        foreach (string item in frmMain.tbfilter.Lines)
        //        {
        //            //string[] findResult = UtilsEx.Getunit(hex, "(?<=(AA-55))[.\\s\\S]*?(?=(CC-33-C3-3C))");
        //            string[] findResult = UtilsEx.Getunit(hex, "(?<=(AA-55))[.\\s\\S]*?(?=(CC-33-C3-3C))");
        //            foreach (string value in findResult)
        //            {

        //            }
        //        }


        //        #endregion



        //        //string hex = BitConverter.ToString(bt.ToArray(), 0);
        //        // fctb1.AppendText(line.line + Environment.NewLine, redStyle);
               


        //        frmMain.fctb.AppendText(Environment.NewLine);
        //        frmMain.fctb.GoEnd();
        //    }
           
        //}


        //状态栏显示内容
        public static float mval_o3 = 0;//臭氧值
        public static float mval_ljy = 0;//裂解氧值
        public static float mval_jky = 0;//进口氧值
        public static float mval_yq = 0;//氩气值




        public static string malarm = "";//状态栏异常字符串如：1100000
        public static string mon_off = "";//下位机开关状态， HardSW_ST_CMD = 0x10, 0：紫外、1：臭氧、2：炉子、3：载气
        public static float m_status_jyq = 0;//进样器执行状态 JYQ_OP_CMD 0停止 1 作样 2 复位
        public static float m_status_ksfx = 0;//开始分析
        public static float m_status_qxfx = 0;//取消分析
        public static float m_status_ztfx = 0;//暂停分析
        public static float m_status_zygc = 0;//作样过程状态


        public static float mval_nv_max = 0;//N电压数最大值,来自每次比较后得出
        public static float mval_sv_max = 0;//S电压数最大值,来自每次比较后得出
        public static float mval_sv_min = 0;//S电压数最小值,来自每次比较后得出
        public static float mval_nv_min = 0;//N电压数最小值,来自每次比较后得出
        public static float mval_nv = 0;//N电压数值,来自下位机
        public static float mval_sv = 0;//S电压数值
        public static float mval_njx = 0;//N基线
        public static float mval_sjx = 0;//S基线
        public static float mval_njf = 0;//N积分
        public static float mval_sjf = 0;//S积分
        public static float mval_gas_jql = 0;//气体进样器-进气量设置，上位机设置并保存，不需下发给下位机

        /// <summary>
        /// 定时任务时间
        /// </summary>
        //是否启动定时任务标志，默认false
        public static bool bl_startTimedTask = false;
        //试验结束后是否关机标志，默认false
        public static bool bl_shutdownTimedTask = false;
        //打开激发源时间
        public static DateTime mtime_OpenEsTime = DateTime.ParseExact("1900-01-01 00:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        //关闭激发源时间
        public static DateTime mtime_CloseEsTime = DateTime.ParseExact("1900-01-01 00:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        //打开加热炉时间
        public static DateTime mtime_OpenHfTime = DateTime.ParseExact("1900-01-01 00:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        //关闭加热炉时间
        public static DateTime mtime_CloseHfTime = DateTime.ParseExact("1900-01-01 00:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);




       // private static ILog log = LogManager.GetLogger(typeof(Common));
        //公共串口对象
        public static SerialPort sp = new SerialPort();
        //串口连接标识，默认false未连接
        //可以用sp.isopen替代，检验后删除
        public static bool sp_connected = false;
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

        public static List<byte> lst_lower_computer_d = new List<byte>();//下位机数据保存，list
        // 0xaa 0x55 len cmd D0 D1 D2 D3 C1 C2 0xcc 0x33 0xc3 0x3c

        public struct SerialPortInfo
        {
            public static string PortName = "";//设置串口名
            public static int BaudRate = 115200;   //波特率
            public static int DataBits = 8;   //数据位
            public static StopBits StopBits = StopBits.One;   //停止位
            public static bool DtrEnable = true; //该值在串行通信过程中启用数据终端就绪 (DTR) 信号  
            public static bool RtsEnable = true; //串行通信中是否启用请求发送 (RTS) 信号
            public static int ReadTimeout = 1000;//设置数据读取超时为1秒
            public static int ReceivedBytesThreshold = 10;//DataReceived 事件发生前内部输入缓冲区中的字节数
            public static Parity Parity = Parity.None;     //校验位
            public static bool isAutoScan = true;     //自动扫描串口标志，默认true，即自动扫描

        };
        /// <summary>
        ///初始化串口组件 
        /// </summary>
        public static SerialPort initSerialPort()
        {
            if (sp.IsOpen)//如果打开状态，则先关闭一下
            {
                sp.Close();
            }

            sp.PortName = SerialPortInfo.PortName;//设置串口名
            sp.BaudRate = SerialPortInfo.BaudRate;  //波特率
            sp.DataBits = SerialPortInfo.DataBits;  //数据位
            sp.StopBits = SerialPortInfo.StopBits;//停止位
            sp.DtrEnable = SerialPortInfo.DtrEnable;//该值在串行通信过程中启用数据终端就绪 (DTR) 信号  
            sp.RtsEnable = SerialPortInfo.RtsEnable;//串行通信中是否启用请求发送 (RTS) 信号
            sp.ReadTimeout = SerialPortInfo.ReadTimeout;//设置数据读取超时为1秒
            sp.ReceivedBytesThreshold = SerialPortInfo.ReceivedBytesThreshold;//DataReceived 事件发生前内部输入缓冲区中的字节数
            sp.Parity = SerialPortInfo.Parity; //校验位
            sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);//数据接收事件
            try
            {
                sp.Open();
                return sp;
            }
            catch (UnauthorizedAccessException)
            {//System.UnauthorizedAccessException 端口占用异常
                //RuntimeParameters.AnomalySignal.online = true;
                sp_connected = false;
                UnauthorizedAccessException err = new UnauthorizedAccessException();
                frmMain.rtbinfo.AppendText(err.Message);
                frmMain.rtbinfo.SelectionStart = frmMain.rtbinfo.Text.Length;
                frmMain.rtbinfo.SelectionLength = 0;
                //fctb.ScrollToCaret();
                //frmMain.sbtnstart.Enabled = true;
                throw err;
                //log.Error(err.Message);
                //MessageBox.Show("异常信息：" + err.Message.ToString(), "错误提示");
            }
            catch (Exception err)
            {
                //RuntimeParameters.AnomalySignal.online = true;
                sp_connected = false;
                frmMain.rtbinfo.AppendText(err.Message);
                frmMain.rtbinfo.SelectionStart = frmMain.rtbinfo.Text.Length;
                frmMain.rtbinfo.SelectionLength = 0;
                //frmMain.sbtnstart.Enabled = true;
                //log.Error(err.Message);
                //MessageBox.Show("异常信息：" + err.Message.ToString(), "错误提示");
                throw err;

            }
            return sp;

        }
        public static void setForm(FrmMain frm)
        {
            frmMain = frm;
            //X.frmMain2 = frm;
        }
        private static void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //SerialPort sp2 = sender as SerialPort;//获取当前接收数据的串口
            frmMain.mre.WaitOne();//根据是否收到信号判断阻塞当前线程
            //Thread.Sleep(500);
            //lock (sp2)
            {
                if (!sp.IsOpen)//安全措施
                {
                    //Exception error = new Exception();
                    //throw error;
                    return;
                }
                //生成随机数，判断下位机是否联线或者usb已联但电源未联
                state_hasData = Guid.NewGuid().ToString();
                int len = sp.BytesToRead;//数据的长度
                byte[] lowerData = new byte[len];//下位机上传数据
                sp.Read(lowerData, 0, len);
                g_machine = new byte[len];
                data_length += len;
                g_machine = lowerData;
                
                //Invoke_machine_data invoke = X.showData;// new Invoke_machine_data(showData);
                //invoke(g_machine);

                //System.Windows.Forms.Control.BeginInvoke(invoke, new object[] { g_machine });
                //数据打包，打4个包后进行解析
                //Console.WriteLine("【==================================");
                //Console.WriteLine("收到信息.......");
                string hex2 = BitConverter.ToString(lowerData, 0);
                //Console.WriteLine(hex2);
                //mpkg_framenum条数据合并一个数据
                if (mpkg_cnt - mpkg_framenum < 1)
                {
                    mlst_bt.AddRange(lowerData.ToList());

                    //读包mpkg_framenum次进行处理
                    if (mpkg_cnt == mpkg_framenum)
                    {
                        processBuffer(mlst_bt);
                        mlst_bt.Clear();
                        mpkg_cnt = 0;
                    }
                    mpkg_cnt++;
                }
                return;

                byte dframe_len;//每条指令中包含的数据长度
                byte dframe_cmd;//每条指令中包含的指令数据
                byte[] dframe_data;//帧里的数据内容
                byte[] dframe_crc = new byte[2];//帧里的CRC校验内容
                byte[] data_pack; //拼接下位机完整数据包
                /**下位机数据
                 * AA    55   05 0B  00 00 00 00 A5 C1  CC   33   C3   3C
			     * 0xaa 0x55 len cmd D0 D1 D2 D3 C1 C2 0xcc 0x33 0xc3 0x3c
			     * 0    1    2   3   4  5  6  7  8  9  10   11   12   13
			     * 
			     * AA 55 14 FD 50 43 5F 56 31 2E 30 31 35 36 2E 32 30 30 36 31 38 30 31 26 02 CC 33 C3 3C 
                   ----- le  0 1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 c  c  -----------
			     */
                //Console.WriteLine("【==================================");
                //Console.WriteLine("收到信息.......");
                string hex = BitConverter.ToString(lowerData, 0);
                //Console.WriteLine(hex);
                //log.Info("【==================================");
                //log.Info("收到信息.......\n" + hex);
                for (int i = 0; i < len; i++)
                {
                    if ((i + 1) >= len)
                    {
                        break;
                    }
                    //检测是否是下位机数据头, 0xaa 0x55
                    if (lowerData[i] == FromLowerComputer.headA &&
                        lowerData[i + 1] == FromLowerComputer.headB)
                    {
                        //processBuffer(lowerData.Skip(i).Take(len - i).ToArray());//处理数据未包含完整桢数据情况
                        if ((i + 2) >= len)
                        {
                            break;
                        }
                        dframe_len = lowerData[i + 2];//指令+数据的长度，1字节
                                                      //循环体退出条件
                        if ((i + 8 + dframe_len) >= len)
                        {
                            break;
                        }
                        //检测数据尾是否正确  0xcc 0x33 0xc3 0x3c
                        if (lowerData[i + 5 + dframe_len] == FromLowerComputer.endA &&
                            lowerData[i + 6 + dframe_len] == FromLowerComputer.endB &&
                            lowerData[i + 7 + dframe_len] == FromLowerComputer.endC &&
                            lowerData[i + 8 + dframe_len] == FromLowerComputer.endD)
                        {
                            //AA-55-05-01-73-68-99-40-CD-38-CC-33-C3-3C-AA-55-05-02-73-68-99-40-89-38-CC-33-C3-3C-AA-55-05-03-4A-0C-02-40-92-8B-CC-33-C3-3C-AA-55-05-04-CE-19-21-40-07-4F-CC-33-C3-3C-AA-55-05-10-00-00-40-40-F1-F3-CC-33-C3-3C-AA-55-05-12-00-00-C0-42-68-32-CC-33-C3-3C-AA-55-05-0B-00-00-00-00-A5-C1-CC-33-C3-3C-AA-55-05-0C-00-00-00-00-10-01-CC-33-C3-3C-AA-55-05-09-00-00-00-00-DC-01-CC-33-C3-3C-AA-55-05-0A-00-00-00-00-98-01-CC-33-C3-3C-AA-55-05-11-00-00-80-3F-DD-D3-CC-33-C3-3C-AA-55-05-15-00-00-00-40-0C-33-CC-33-C3-3C
                            dframe_cmd = lowerData[i + 3];//指令，1字节
                            FromLowerComputer.cmd = dframe_cmd;//公共变量赋值
                            dframe_data = new byte[dframe_len - 1];//数据内容，长度取决于dframe_len - 1
                            FromLowerComputer.data = new byte[dframe_len - 1];
                            for (int j = 0; j < dframe_len - 1; j++)
                            {
                                dframe_data[j] = lowerData[i + j + 4];
                            }
                            //将下位机上传的数据内容赋值公共变量，供其他模块使用 
                            Buffer.BlockCopy(dframe_data, 0, FromLowerComputer.data, 0, dframe_data.Length);
                            //取得crc校验数据
                            dframe_crc[0] = lowerData[i + 3 + dframe_len];
                            dframe_crc[1] = lowerData[i + 4 + dframe_len];


                            i += 2 + dframe_len + 2 + 4;
                            //Console.WriteLine(i + "、-----------------<找到一条>-------------\n\r");
                            //Console.WriteLine(temp++);

                            #region 拼接下位机数据
                            data_pack = new byte[9 + dframe_len];
                            data_pack[0] = FromLowerComputer.headA;
                            data_pack[1] = FromLowerComputer.headB;
                            data_pack[2] = dframe_len;
                            data_pack[3] = dframe_cmd;
                            int pos = 1;//记录数据内容在整个数组中的位置
                            for (int k = 0; k < dframe_data.Length; k++)
                            {
                                data_pack[k + 4] = dframe_data[k];
                                pos = k + 4;
                            }

                            data_pack[pos + 1] = dframe_crc[0];
                            data_pack[pos + 2] = dframe_crc[1];
                            data_pack[pos + 3] = FromLowerComputer.endA;
                            data_pack[pos + 4] = FromLowerComputer.endB;
                            data_pack[pos + 5] = FromLowerComputer.endC;
                            data_pack[pos + 6] = FromLowerComputer.endD;
                            hex = BitConverter.ToString(data_pack, 0);
                            #endregion
                        }
                    }
                }
                Console.WriteLine("==================================】");
                //log.Info("==================================】");
            }
        }

        public static void HighlightPhrase(RichTextBox box, string phrase, Color color, string txt)
        {
            int pos = box.SelectionStart;
            string s = box.Text;
            for (int ix = 0; ;)
            {
                int jx = s.IndexOf(phrase, ix, StringComparison.CurrentCultureIgnoreCase);
                if (jx < 0) break;
                box.SelectionStart = jx;
                box.SelectionLength = phrase.Length;
                box.SelectionColor = Color.Blue;// color;
                //box.SelectionBackColor = Color.Green;
                ix = jx + 1;
            }
            box.SelectionStart = pos;
            box.SelectionLength = 0;
        }
        /// <summary>
        /// 从数据包中截取完整数据包，AA 55开头，CC-33-C3-3C结束，但没有结尾则存入缓存变量
        /// 提高数据包的识别率
        /// </summary>
        /// <param name="bt">mpkg_framenum个数据包组合在一起传入</param>
        public static void processBuffer(List<byte> bt)
        {
            string hex = BitConverter.ToString(bt.ToArray(), 0);
            g_newStr = hex;
            //frmMain.fctb.AppendText(DateTime.Now.ToString("HH:mm:ss:fff") + ": " + hex + "\n");
            //frmMain.fctb.AppendText("\n");
            //HighlightPhrase(frmMain.rtbSP, "AA-55", Color.Red, hex);
            //frmMain.rtbSP.ScrollToCaret();
            string[] databuffer = UtilsEx.Getunit(hex, "(?<=(AA-55))[.\\s\\S]*?(?=(CC-33-C3-3C))");
            if (databuffer == null)
            {
                //可能存在出入的数据包仍不是一个完整的数据包
                return;
            }

            byte dframe_len = 0x0;//每条指令中包含的数据长度
            float fvalue = 0;

            for (int i = 1; i <= databuffer.Length; i++)
            {
                databuffer[i - 1] = databuffer[i - 1].Substring(1, databuffer[i - 1].Length - 2);
                string[] stemp = databuffer[i - 1].Split('-');
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
                fvalue = BitConverter.ToSingle(Common.FromLowerComputer.data, 0);

                switch (bt_temp[1])
                {
                    case 0x01://臭氧数值采集
                        //四舍五入
                        //fvalue = (float)Math.Round(fvalue, 0, MidpointRounding.AwayFromZero);
                        mval_o3 = (Int32)fvalue;
                        break;
                    case 0x02://裂解氧数值采集
                        //四舍五入
                        //fvalue = (float)Math.Round(fvalue, 0, MidpointRounding.AwayFromZero);
                        mval_ljy = (Int32)fvalue;
                        break;
                    case 0x03://进口氧数值采集
                        //四舍五入
                        //fvalue = (float)Math.Round(fvalue, 0, MidpointRounding.AwayFromZero);
                        mval_jky = (Int32)fvalue;
                        break;
                    case 0x04://氩气数值采集
                        //四舍五入
                        //fvalue = (float)Math.Round(fvalue, 0, MidpointRounding.AwayFromZero);
                        mval_yq = (Int32)fvalue;
                        break;
                    case 0x12://报警  00000000-00000000-11000000-01000010-
                              //换算步骤：float》十六进制》二进制无符号，即为7位无符号二进制位
                        fvalue = BitConverter.ToSingle(Common.FromLowerComputer.data, 0);
                        malarm = Convert.ToString((uint)fvalue, 2).PadLeft(7, '0');
                        break;
                    case 0x10://下位机指令开关状态
                        fvalue = BitConverter.ToSingle(Common.FromLowerComputer.data, 0);
                        mon_off = Convert.ToString((uint)fvalue, 2).PadLeft(8, '0');
                        break;
                    case 0x11://下位机作样过程状态
                        m_status_zygc = fvalue;
                        break;
                    case 0x15://进样器状态，0停止 1 作样 2 复位
                        m_status_jyq = fvalue;
                        break;

                    case 0x09://S电压数值
                        mval_sv = (Int32)fvalue;
                        break;
                    case 0x0A://N电压数值
                        mval_nv = (Int32)fvalue;
                        break;
                    case 0x0B://S基线
                        mval_sjx = (Int32)fvalue;
                        break;
                    case 0x0C://N基线
                        mval_njx = (Int32)fvalue;
                        break;
                    case 0x05://S积分
                        mval_sjf = fvalue;
                        break;
                    case 0x07://N积分
                        mval_njf = fvalue;
                        break;
                    case 0x52://开始分析 StartFX 
                        m_status_ksfx = fvalue;
                        break;
                    case 0x53://取消分析 CancelFX
                        m_status_qxfx = fvalue;
                        break;
                    case 0x54://暂停分析 PauseFX
                        m_status_ztfx = fvalue;
                        break;
                    default:
                        break;
                }
            }

        }
        /// <summary>
        /// 10进制数据用16进制显示
        /// </summary>
        public static string HexShow(byte b)
        {
            char[] HexChar = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            char hexH = HexChar[b / 16];
            char hexL = HexChar[b % 16];
            return hexH.ToString() + hexL.ToString();
        }
        public static void InvokeEx(Control ctl, MethodInvoker method)
        {
            
            if (!ctl.IsHandleCreated)
                return;

            if (ctl.IsDisposed)
                return;

            if (ctl.InvokeRequired)
            {
                ctl.Invoke(method);
            }
            else
            {
                method();
            }
        }
        /// <summary>
        /// 打开下位机各功能开关
        /// </summary>
        /// <param name="p_cmd">参数值</param>
        /// <param name="p_cmd_type">参数类型</param>
        public static void sendONOFFcmd(float p_cmd, byte p_cmd_type)
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
        public static void sendPARAcmd(byte[] p_cmds, float[] p_values)
        {
            ToLowerComputer_parameters cmd = new ToLowerComputer_parameters();
            cmd.cmds = p_cmds;
            cmd.datas = p_values;
            sendInfo(cmd.toByteArry());
        }

        private static void sendInfo(byte[] cmdCode)
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

    }
}
