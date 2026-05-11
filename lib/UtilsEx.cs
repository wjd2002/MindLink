using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Management;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
namespace Lib
{
    class ComboboxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
    /// <summary>
    /// 对象深度复制辅助类
    /// </summary>
    public static class ObjectCopier
    {
        /// <summary>
        /// Perform a deep Copy of the object.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

    }
    public class UtilsEx
    {
        /// <summary>
        /// 为INI文件中指定的节点取得字符串
        /// </summary>
        /// <param name="lpAppName">欲在其中查找关键字的节点名称</param>
        /// <param name="lpKeyName">欲获取的项名</param>
        /// <param name="lpDefault">指定的项没有找到时返回的默认值</param>
        /// <param name="lpReturnedString">指定一个字串缓冲区，长度至少为nSize</param>
        /// <param name="nSize">指定装载到lpReturnedString缓冲区的最大字符数量</param>
        /// <param name="lpFileName">INI文件完整路径</param>
        /// <returns>复制到lpReturnedString缓冲区的字节数量，其中不包括那些NULL中止字符</returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        /// <summary>
        /// 修改INI文件中内容
        /// </summary>
        /// <param name="lpApplicationName">欲在其中写入的节点名称</param>
        /// <param name="lpKeyName">欲设置的项名</param>
        /// <param name="lpString">要写入的新字符串</param>
        /// <param name="lpFileName">INI文件完整路径</param>
        /// <returns>非零表示成功，零表示失败</returns>
        [DllImport("kernel32")]
        private static extern int WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);
        /// <summary>
        /// 正则表达式处理字符串
        /// </summary>
        /// <param name="value">待处理字符串</param>
        /// <param name="regx">正则公式</param>
        /// <returns>字符串数组</returns>
        public static string[] Getunit(string value, string regx)
        {
            //例子
            //string[] unit = Getunit(待查字符串, "(?<=(AA 55))[.\\s\\S]*?(?=(CC 33 C3 3C))");
            //for (int i = 1; i <= unit.Length; i++)
            //{

            //    Console.WriteLine(unit[i - 1].ToString());
            //}
            //string test = "0xaa0x55Aaaaaaaaaaaaaaa0xcc0x330xc30x3c0xaa0x55A1230xcc0x330xc30x3c";
            //Console.WriteLine(ExtractStringBetweenBeginAndEnd(test, "AA 55", "CC 33 C3 3C"));
            if (string.IsNullOrWhiteSpace(value))
                return null;
            bool isMatch = Regex.IsMatch(value, regx);
            if (!isMatch)
                return null;
            MatchCollection matchCol = Regex.Matches(value, regx);
            string[] result = new string[matchCol.Count];
            if (matchCol.Count > 0)
            {
                for (int i = 0; i < matchCol.Count; i++)
                {
                    result[i] = matchCol[i].Value;
                }
            }
            return result;
        }
        // byte[]转为二进制字符串表示
        public static string byte2bit(byte[] p)
        {
            string strTemp = "";
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < p.Length; i++)
            {
                strTemp = System.Convert.ToString(p[i], 2);
                strTemp = strTemp.Insert(0, new string('0', 8 - strTemp.Length));
                sb.Append(strTemp).Append("-");
            }
            return sb.ToString();
        }
        //二进制字符串转化为byte[]
        public static byte[] bit2byte(string p)
        {

            byte[] bytes = new byte[p.Length / 8];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(p.Substring(i * 8, 8), 2);
            }
            return bytes;
        }
        //16进制转二进制
        public static string byte2bitE(byte p)
        {
            string strTemp = "";
            StringBuilder sb = new StringBuilder();
            strTemp = System.Convert.ToString(p, 2);
            strTemp = strTemp.Insert(0, new string(' ', 8 - strTemp.Length));
            sb.Append(strTemp);

            return sb.ToString();
        }
        /// <summary>
        /// 异或处理
        /// </summary>
        /// <param name="bytes">待处理数组</param>
        /// <param name="start">开始位置</param>
        /// <param name="end">结束位置</param>
        /// <returns></returns>
        public static byte[] GenerateCRC(byte[] bytes, int start, int end)
        {
            byte out1, data;//用于保存异或结果 
            out1 = 0x00;

            for (int i = start; i <= end; i++)
            {
                data = bytes[i];
                out1 ^= data;
            }

            byte[] result = new byte[] { out1, 0x00 };

            return result;
        }
        /// <summary>
        /// 获取密钥,随机生成
        /// </summary>
        /// <returns></returns>
        public static string CreateSalt()
        {
            byte[] data = new byte[8];
            new RNGCryptoServiceProvider().GetBytes(data);
            return Convert.ToBase64String(data);
        }
        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="pwdString">密码字符串</param>
        /// <returns></returns>
        public static string EncryptPwd(string pwdStr, string salt)
        {
            if (salt == null || salt == "")
            {
                return pwdStr;
            }
            if (pwdStr.Equals("")) return pwdStr;
            byte[] bytes = Encoding.Unicode.GetBytes(salt.ToLower().Trim() + pwdStr.Trim());
            return BitConverter.ToString(((HashAlgorithm)CryptoConfig.CreateFromName("SHA1")).ComputeHash(bytes)).Replace("-", "");
        }
        /// <summary>
        /// 对象复制，10000次以内速度最快
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="tIn"></param>
        /// <returns></returns>
        public static TOut TransReflection<TIn, TOut>(TIn tIn)
        {
            TOut tOut = Activator.CreateInstance<TOut>();
            var tInType = tIn.GetType();
            foreach (var itemOut in tOut.GetType().GetProperties())
            {
                var itemIn = tInType.GetProperty(itemOut.Name); ;
                if (itemIn != null)
                {
                    itemOut.SetValue(tOut, itemIn.GetValue(tIn));
                }
            }
            return tOut;
        }
        /// <summary>
        /// 对象复制，100000次以上速度逐渐加快，复制次数越多越快
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        public static class TransExpV2<TIn, TOut>
        {

            private static readonly Func<TIn, TOut> cache = GetFunc();
            private static Func<TIn, TOut> GetFunc()
            {
                ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "p");
                List<MemberBinding> memberBindingList = new List<MemberBinding>();

                foreach (var item in typeof(TOut).GetProperties())
                {
                    if (!item.CanWrite) continue;
                    MemberExpression property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }

                MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray());
                Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, new ParameterExpression[] { parameterExpression });

                return lambda.Compile();
            }

            public static TOut Trans(TIn tIn)
            {
                return cache(tIn);
            }

        }
        /// <summary>
        /// 检查文件名是否有效
        /// </summary>
        /// <param name="FileName"></param>
        public static Boolean CheckFileName(String fileName)
        {
            Boolean isValid = true;
            String[] strList = new String[] { " ", "/", "\"", @"\", @"\/", ":", "*", "?", "<", ">", "|", "\r\n" };

            if (String.IsNullOrEmpty(fileName))
            {
                isValid = false;
            }
            else
            {
                foreach (String errStr in strList)
                {
                    if (fileName.Contains(errStr))
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            return isValid;
        }
        /// <summary>
        /// 数字转字母
        /// </summary>
        /// <param name="number">数字</param>
        /// <returns></returns>
        public static string NumToChar(int number)
        {
            // if (65 <= number && 90 >= number)
            // {
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            byte[] btNumber = new byte[] { (byte)number };
            return asciiEncoding.GetString(btNumber);
            // }
            //return "数字不在转换范围内";
        }
        /// <summary>
        /// 枚举win32 api
        /// </summary>
        public enum HardwareEnum
        {
            // 硬件
            Win32_Processor, // CPU 处理器
            Win32_PhysicalMemory, // 物理内存条
            Win32_Keyboard, // 键盘
            Win32_PointingDevice, // 点输入设备，包括鼠标。
            Win32_FloppyDrive, // 软盘驱动器
            Win32_DiskDrive, // 硬盘驱动器
            Win32_CDROMDrive, // 光盘驱动器
            Win32_BaseBoard, // 主板
            Win32_BIOS, // BIOS 芯片
            Win32_ParallelPort, // 并口
            Win32_SerialPort, // 串口
            Win32_SerialPortConfiguration, // 串口配置
            Win32_SoundDevice, // 多媒体设置，一般指声卡。
            Win32_SystemSlot, // 主板插槽 (ISA & PCI & AGP)
            Win32_USBController, // USB 控制器
            Win32_NetworkAdapter, // 网络适配器
            Win32_NetworkAdapterConfiguration, // 网络适配器设置
            Win32_Printer, // 打印机
            Win32_PrinterConfiguration, // 打印机设置
            Win32_PrintJob, // 打印机任务
            Win32_TCPIPPrinterPort, // 打印机端口
            Win32_POTSModem, // MODEM
            Win32_POTSModemToSerialPort, // MODEM 端口
            Win32_DesktopMonitor, // 显示器
            Win32_DisplayConfiguration, // 显卡
            Win32_DisplayControllerConfiguration, // 显卡设置
            Win32_VideoController, // 显卡细节。
            Win32_VideoSettings, // 显卡支持的显示模式。

            // 操作系统
            Win32_TimeZone, // 时区
            Win32_SystemDriver, // 驱动程序
            Win32_DiskPartition, // 磁盘分区
            Win32_LogicalDisk, // 逻辑磁盘
            Win32_LogicalDiskToPartition, // 逻辑磁盘所在分区及始末位置。
            Win32_LogicalMemoryConfiguration, // 逻辑内存配置
            Win32_PageFile, // 系统页文件信息
            Win32_PageFileSetting, // 页文件设置
            Win32_BootConfiguration, // 系统启动配置
            Win32_ComputerSystem, // 计算机信息简要
            Win32_OperatingSystem, // 操作系统信息
            Win32_StartupCommand, // 系统自动启动程序
            Win32_Service, // 系统安装的服务
            Win32_Group, // 系统管理组
            Win32_GroupUser, // 系统组帐号
            Win32_UserAccount, // 用户帐号
            Win32_Process, // 系统进程
            Win32_Thread, // 系统线程
            Win32_Share, // 共享
            Win32_NetworkClient, // 已安装的网络客户端
            Win32_NetworkProtocol, // 已安装的网络协议
            Win32_PnPEntity,//all device
        }
        /// <summary>
        /// WMI取硬件信息
        /// </summary>
        /// <param name="hardType"></param>
        /// <param name="propKey"></param>
        /// <returns></returns>
        public static string[] MulGetHardwareInfo(HardwareEnum hardType, string propKey)
        {

            List<string> strs = new List<string>();
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + hardType))
                {
                    var hardInfos = searcher.Get();
                    foreach (var hardInfo in hardInfos)
                    {
                        if (hardInfo.Properties[propKey].Value.ToString().Contains("COM"))
                        {
                            strs.Add(hardInfo.Properties[propKey].Value.ToString());
                        }

                    }
                    searcher.Dispose();
                }
                return strs.ToArray();
            }
            catch
            {
                return null;
            }
            finally
            { strs = null; }
        }
        public static string[] MulGetsSrialPortInfo(string propKey)
        {
            HardwareEnum hardType = HardwareEnum.Win32_PnPEntity;
            List<string> strs = new List<string>();
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + hardType))
                {
                    var hardInfos = searcher.Get();
                    foreach (var hardInfo in hardInfos)
                    {
                        if (hardInfo.Properties[propKey].Value == null) continue;
                        if (hardInfo.Properties[propKey].Value.ToString().Contains("COM"))
                        {
                            strs.Add(hardInfo.Properties[propKey].Value.ToString());
                        }

                    }
                    searcher.Dispose();
                }
                return strs.ToArray();
            }
            catch
            {
                return null;
            }
            finally
            { strs = null; }
        }
        /// <summary>
        /// 使用Guid产生的种子生成真随机数
        /// </summary>
        /// <param name="begin">数字起点</param>
        /// <param name="end">数字重点</param>
        /// <returns></returns>
        /// <returns></returns>
        public static int GetRandomByGuid(int begin, int end)
        {
            Int32 random_seed = Guid.NewGuid().GetHashCode();
            Random random = new Random(random_seed);
            return random.Next(begin, end);
        }
        /// <summary>
        /// 读取INI文件值
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="key">键</param>
        /// <param name="def">未取到值时返回的默认值</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>读取的值</returns>
        public static string Read(string section, string key, string def, string filePath)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def, sb, 1024, filePath);
            return sb.ToString();
        }

        /// <summary>
        /// 写INI文件值
        /// </summary>
        /// <param name="section">欲在其中写入的节点名称</param>
        /// <param name="key">欲设置的项名</param>
        /// <param name="value">要写入的新字符串</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public static int Write(string section, string key, string value, string filePath)
        {
            //CheckPath(filePath);
            return WritePrivateProfileString(section, key, value, filePath);
        }

        /// <summary>
        /// 删除节
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public static int DeleteSection(string section, string filePath)
        {
            return Write(section, null, null, filePath);
        }

        /// <summary>
        /// 删除键的值
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="key">键名</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>非零表示成功，零表示失败</returns>
        public static int DeleteKey(string section, string key, string filePath)
        {
            return Write(section, key, null, filePath);
        }
    }

}
