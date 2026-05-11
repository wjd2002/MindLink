using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib
{
    /// <summary>
    /// 馏程仪器模型,用于检测仪器各项指标以及发送命令
    /// </summary>
    class Machine
    {

        //lims设置参数
        public struct Lims
        {
            public static string method = "";//分析方法
            public static string username = "";//用户名
            public static string password = "";//密码
            public static string sampleno = "";//样品编号
        };
        public struct SerialPortInfo
        {
            public static string PortName = "";//设置串口名
            public static int BaudRate = 57600;   //波特率
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
        /// 下发给馏程的参数结构体
        /// </summary>
        public struct ToMachineParas
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
            public float[] datas;//
            /// <summary>
            /// 异或校验
            /// </summary>
            public byte xor;
            /// <summary>

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
                int len = 1 + datas.Length * 4;//命令符占1字节，数据4字节
                int arrlength = 8 + len;//单个命令发送包占13个字节，AA-55-05(len)-02(cmd)-00-00-00-00-07(xor)-CC-33-C3-3C
                arr_senddata = new byte[arrlength];//拼接发送命令数据包
                arr_senddata[0] = headA;
                arr_senddata[1] = headB;
                arr_senddata[2] = (byte)len;//命令+数据长度
                byte[] data_arry = new byte[4];

                //命令+数据、命令+数据...此为参数下发的格式
                arr_senddata[3] = cmd;
                for (int i = 0; i < datas.Length; i++)
                {
                    data_arry = BitConverter.GetBytes(datas[i]);
                    Buffer.BlockCopy(data_arry, 0, arr_senddata, 4 + i * 4, 4);

                }
                //数据内容
                //异或校验码1字节
                byte[] xor_src = new byte[len + 1];//异或校验计算用的源数组,+1是吧length所占1字节加入校验

                Buffer.BlockCopy(arr_senddata, 2, xor_src, 0, len + 1);//异或校验从len+cmd+data开始计算，与硫氮不同

                byte xor_result = generateCheckXor(xor_src);
                byte[] xors = { xor_result };//此处用数组是为了使用CopyTo实现数组赋值
                xors.CopyTo(arr_senddata, 3 + len);

                byte[] arr_end = new byte[] { endA, endB, endC, endD };
                arr_end.CopyTo(arr_senddata, 4 + len);

                return arr_senddata;
            }
        }
        public static byte generateCheckXor(byte[] data)
        {
            byte CheckCode = 0;
            int len = data.Length;
            for (int i = 0; i < len; i++)
            {
                CheckCode ^= data[i];
            }
            return CheckCode;
        }
    }
}
