//****      FounctionResoures文件为功能文件
//****      文件主要涉及字节、字节数组、字符串等转化
//****
//****
//****
//****
//****
//****
//****
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;

namespace mytools
{
    class FounctionResources
    {
        /// <summary>
        /// 检查是否为数字
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static bool CheckIsNum(string inputString)
        {
            for (int i = 0; i < inputString.Length; i++)
            {
                if (!char.IsNumber(inputString, i))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 把自己数组每半个字节转化成16进制string输出
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetStringFromByte(byte[] data)
        {
            string aimString = null;

            for (int i = 0; i < data.Length; i++)
            {
                aimString += string.Format("{0:X2}", data[i]);
            }
            return aimString;
        }


        //public static string GetCardIDFrom0015(string strData)
        //{
        //    string retStrData = null;
        //    for (int i = 0; i < strData.Length; i += 2)
        //    {
        //        retStrData += ((strData.Substring(i, 2))).ToString();
        //    }
        //    return retStrData;
        //}

        /// <summary>
        /// 取字节数组特定的几个位置
        /// </summary>
        /// <param name="data">原数组</param>
        /// <param name="startIndex">开始位置</param>
        /// <param name="copyCount">准备截取的长度</param>
        /// <returns></returns>
        public static byte[] GetByteFromSpecialPosition(byte[] data, int startIndex, int copyCount)
        {
            byte[] retByte = new byte[copyCount];
            for (int i = startIndex, j=0; i < (startIndex + copyCount); i++,j++)
            {
                retByte[j] = data[i];
            }
            return retByte;
        }

        /// <summary>
        /// 检查卡片是否为储值卡
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public static bool IsPurseCard(string cardNo)
        {
            if (cardNo.Substring(4, 2) != "22")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// int型转化为字节数组，按int的顺序从高到底排列
        /// </summary>
        /// <param name="sourceInt">需要转化的int数据</param>
        /// <returns>返回4字节的字节数组</returns>
        public static byte[] IntToByteInSequence(int sourceInt)
        {
            int i = 0 ;
            byte [] a = BitConverter.GetBytes(sourceInt);
            byte temp = new byte();
            for (i = 0; i < 2; i++)
            {
                temp = a[i];
                a[i] = a[3 - i];
                a[3 - i] = temp;
            }
            return a;
        }


        public static byte[] IntToByteInSequence(uint sourceInt)
        {
            int i = 0;
            byte[] a = BitConverter.GetBytes(sourceInt);
            byte temp = new byte();
            for (i = 0; i < 2; i++)
            {
                temp = a[i];
                a[i] = a[3 - i];
                a[3 - i] = temp;
            }
            return a;
        }


        public static byte[] StringToByteSequenceWithin16(string sourceString)
        {
            int i = 0, n = 0;
            //bool needAdd = false;
            int j = (sourceString.Length)/2;
            //if (sourceString.Length % 2 == 1)

            //    needAdd = true;

            byte [] a = new byte[j];
            for (i = 0 , n = 0; n < j; i+=2,n++)
            {
                a[n] = Convert.ToByte(sourceString.Substring(i, 2), 16);
            }
            //if (needAdd && n!=0)
            //    a[n + 1] = Convert.ToByte(sourceString.Substring(i + 1, 1), 16);
            return a;
        }

        /// <summary>
        /// 时间数字保持不变转化为字节数组
        /// </summary>
        /// <param name="occurTime"></param>
        /// <returns></returns>
        public static byte[] DateTransToByte(DateTime occurTime)
        {
            byte[] temp =  new byte[7];
            byte[] a = new byte[7];
            temp[0] = Convert.ToByte(occurTime.Year / 100);
            temp[1] = Convert.ToByte(occurTime.Year % 100);
            temp[2] = Convert.ToByte(occurTime.Month);
            temp[3] = Convert.ToByte(occurTime.Day);
            temp[4] = Convert.ToByte(occurTime.Hour);
            temp[5] = Convert.ToByte(occurTime.Minute);
            temp[6] = Convert.ToByte(occurTime.Second);

            for (int i = 0; i < 7; i++)
            {
                a[i] = (byte)(((int)temp[i] / 10) * 16 + (int)temp[i] % 10);
            }
            return a;
        }


        /// <summary>
        /// 10进制String转换成压缩十进制(16进制byte[])
        /// </summary>
        /// <param name="decString">传入的十进制字符串</param>
        /// <param name="sizeOfHexByteArrary">字节数组容量</param>
        /// <returns>压缩十进制字节数组</returns>
        public static byte[] DecStringToHexByteArray(string decString, int sizeOfHexByteArrary)
        {
            byte[] hexByteArrary = new byte[sizeOfHexByteArrary];
            for (int i = 0, n = 0; n < sizeOfHexByteArrary; i += 2, n++)
            {
                hexByteArrary[n] = Convert.ToByte(decString.Substring(i, 2), 16);
            }
            return hexByteArrary;
        }

        /// <summary>
        /// 将流转换为字符数组
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        }


        //计算内存流中的XML文件大小，放入字节数组中
        public static byte[] SizeOfXMLMemorystream(MemoryStream memStreamSend)
        {
            byte[] buffer = { 48, 48, 48, 48, 48, 48, 48, 48 };
            int memStreamLength = (int)memStreamSend.Length;

            buffer[2] = Convert.ToByte(memStreamLength / (int)Math.Pow(10, 5) + 0x30);
            buffer[3] = Convert.ToByte(memStreamLength % (int)Math.Pow(10, 5) / (int)Math.Pow(10, 4) + 0x30);
            buffer[4] = Convert.ToByte(memStreamLength % (int)Math.Pow(10, 4) / (int)Math.Pow(10, 3) + 0x30);
            buffer[5] = Convert.ToByte(memStreamLength % (int)Math.Pow(10, 3) / (int)Math.Pow(10, 2) + 0x30);
            buffer[6] = Convert.ToByte(memStreamLength % (int)Math.Pow(10, 2) / 10 + 0x30);
            buffer[7] = Convert.ToByte(memStreamLength % 10 + 0x30);
            return buffer;
        }

        //接收前8个字节计算XML文件大小
        public static int SizeOfXMLStream(NetworkStream netWorkStream)
        {
            byte[] length = new byte[8];
            netWorkStream.ReadTimeout = 200000;
            netWorkStream.Read(length, 0, 8);
            int sum = 0;
            //byte[] length = new byte[8];
            string temp = ASCIIEncoding.ASCII.GetString(length);
            sum = (int)(Convert.ToInt64(temp));
            return sum;
        }


        //读交易流水号并加一
        public static string ReadSerialnoAndAdd()
        {
            string serialno = "";
            byte[] serialnoByteArrary = new byte[7];

            FileStream fileStream = new FileStream("Serialno.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            fileStream.Seek(0, SeekOrigin.Begin);
            fileStream.Read(serialnoByteArrary, 0, 7);

            for (int i = 0; i < 7; i++)
            {
                serialnoByteArrary[i] -= 0x30;
            }
            int serialnoInt = serialnoByteArrary[5] + serialnoByteArrary[4] * 10 + serialnoByteArrary[3] * 100 + serialnoByteArrary[2] * 1000 + serialnoByteArrary[1] * 10000 + serialnoByteArrary[0] * 100000;
            serialno = serialnoInt.ToString("d6");
            serialnoInt += 1;
            fileStream.Seek(0, SeekOrigin.Begin);
            serialnoByteArrary[0] = (byte)(serialnoInt / 1000000 + 0x30);
            serialnoByteArrary[1] = (byte)(serialnoInt % 100000 / 10000 + 0x30);
            serialnoByteArrary[2] = (byte)(serialnoInt % 10000 / 1000 + 0x30);
            serialnoByteArrary[3] = (byte)(serialnoInt % 1000 / 100 + 0x30);
            serialnoByteArrary[4] = (byte)(serialnoInt % 100 / 10 + 0x30);
            serialnoByteArrary[5] = (byte)(serialnoInt % 10 + 0x30);
            serialnoByteArrary[6] = 0xf0;
            fileStream.Write(serialnoByteArrary, 0, 7);
            fileStream.Close();
            return serialno;
        }

        public static void MemoryCopy(ref byte[] directionArray, int directionStartIndex, byte[] sourceArray, int sourceStartIndex, int count)
        {
            if (directionArray.Length <= 0 && count <= 0 && directionStartIndex < 0 && directionStartIndex + count > directionArray.Length)
                return;
            if (sourceArray.Length <= 0 && sourceStartIndex < 0 && sourceStartIndex + count > sourceArray.Length)
                return;

            for (int i = sourceStartIndex, j = directionStartIndex, k = 0; k < count; i++, j++, k++)
            {
                directionArray[j] = sourceArray[i];
            }
        }


        public static string RMBDecAmountToUpCase(double amount)
        {
            //转换结果
            string convertResult = "";
            //货币数字字符串
            string tMoney = "";
            //小数点位置
            int tn = 0;
            //每位数字
            string t1 = "";
            //小数位大写字符串
            string s1 = "";
            //个十百千位大写字符串
            string s2 = "";
            //万位以上大写字符串
            string s3 = "";
            //转换临时字符串
            string st = "";

            //如为0直接返回"零元"
            if (amount == 0)
            {
                convertResult = "零元";
                return convertResult;
            }
            //如为负数，返回负数大写字符串
            if (amount < 0)
            {
                convertResult = "负" + RMBDecAmountToUpCase(System.Math.Abs(amount));
                return convertResult;
            }

            tMoney = amount.ToString().Trim();
            tn = tMoney.IndexOf(".");

            //生成小数位大写
            if (tn != -1)
            {
                st = tMoney.Substring(tn + 1);
                if (st != "")
                {
                    t1 = st.Substring(0, 1);
                    st = st.Substring(1);
                    if (t1 != "0")
                    {
                        s1 += CCh(int.Parse(t1)) + "角";
                    }
                    if (st != "")
                    {
                        t1 = st.Substring(0, 1);
                        s1 += CCh(int.Parse(t1)) + "分";
                    }
                }
                st = tMoney.Substring(0, tn);

            }
            else
            {
                st = tMoney;
            }

            //生成个十百千位大写
            if (st != "")
            {
                t1 = st.Substring(st.Length - 1);
                st = st.Substring(0, st.Length - 1);
                s2 += CCh(int.Parse(t1));
            }

            if (st != "")
            {
                t1 = st.Substring(st.Length - 1);
                st = st.Substring(0, st.Length - 1);
                if (t1 != "0")
                {
                    s2 = CCh(int.Parse(t1)) + "拾" + s2;
                }
                else if (s2.Substring(0, 1) != "零")
                {
                    s2 = "零" + s2;
                }
            }

            if (st != "")
            {
                t1 = st.Substring(st.Length - 1);
                st = st.Substring(0, st.Length - 1);
                if (t1 != "0")
                {
                    s2 = CCh(int.Parse(t1)) + "佰" + s2;
                }
                else if (s2.Substring(0, 1) != "零")
                {
                    s2 = "零" + s2;
                }
            }

            if (st != "")
            {
                t1 = st.Substring(st.Length - 1);
                st = st.Substring(0, st.Length - 1);
                if (t1 != "0")
                {
                    s2 = CCh(int.Parse(t1)) + "仟" + s2;
                }
                else if (s2.Substring(0, 1) != "零")
                {
                    s2 = "零" + s2;
                }
            }

            //生成万位以上大写，最大为千万位
            if (st != "")
            {
                t1 = st.Substring(st.Length - 1);
                st = st.Substring(0, st.Length - 1);
                s3 += CCh(int.Parse(t1));
            }

            if (st != "")
            {
                t1 = st.Substring(st.Length - 1);
                st = st.Substring(0, st.Length - 1);
                if (t1 != "0")
                {
                    s3 = CCh(int.Parse(t1)) + "拾" + s3;
                }
                else if (s3.Substring(0, 1) != "零")
                {
                    s3 = "零" + s3;
                }
            }

            if (st != "")
            {
                t1 = st.Substring(st.Length - 1);
                st = st.Substring(0, st.Length - 1);
                if (t1 != "0")
                {
                    s3 = CCh(int.Parse(t1)) + "佰" + s3;
                }
                else if (s3.Substring(0, 1) != "零")
                {
                    s3 = "零" + s3;
                }
            }

            if (st != "")
            {
                t1 = st.Substring(st.Length - 1);
                st = st.Substring(0, st.Length - 1);
                if (t1 != "0")
                {
                    s3 = CCh(int.Parse(t1)) + "仟" + s3;
                }
                else if (s3.Substring(0, 1) != "零")
                {
                    s3 = "零" + s3;
                }
            }

            if (s2.Substring(s2.Length - 1) == "零")
                s2 = s2.Substring(0, s2.Length - 1);

            if (s3.Length > 0)
            {
                if (s3.Substring(s3.Length - 1) == "零")
                    s3 = s3.Substring(0, s3.Length - 1);
                s3 += "万";
            }

            //生成转换结果
            convertResult = s3 + s2 == "" ? s1 : s3 + s2 + "元" + s1;
            return convertResult;
        }


        public static string CCh(int n2)
        {
            string chnNum = "";
            switch (n2)
            {
                case 0:
                    chnNum = "零";
                    break;
                case 1:
                    chnNum = "壹";
                    break;
                case 2:
                    chnNum = "贰";
                    break;
                case 3:
                    chnNum = "叁";
                    break;
                case 4:
                    chnNum = "肆";
                    break;
                case 5:
                    chnNum = "伍";
                    break;
                case 6:
                    chnNum = "陆";
                    break;
                case 7:
                    chnNum = "柒";
                    break;
                case 8:
                    chnNum = "捌";
                    break;
                case 9:
                    chnNum = "玖";
                    break;
            }
            return chnNum;
        }

        public static string RMBDecAmountToUpCaseOnlyNumber(double amount)
        {
            //转换结果
            string convertResult = "";
            //货币数字字符串
            string tMoney = "";
            //小数点位置
            int tn = 0;
            //每位数字
            string t1 = "";
            //小数位大写字符串
            string s1 = "";
            //个十百千位大写字符串
            string s2 = "";
            //万位以上大写字符串
            string s3 = "";
            //转换临时字符串
            string st = "";

            //如为0直接返回"零元"
            if (amount == 0)
            {
                convertResult = "零元";
                return convertResult;
            }
            //如为负数，返回负数大写字符串
            if (amount < 0)
            {
                convertResult = "负" + RMBDecAmountToUpCaseOnlyNumber(System.Math.Abs(amount));
                return convertResult;
            }

            tMoney = amount.ToString().Trim();
            tn = tMoney.IndexOf(".");

            //生成小数位大写
            if (tn != -1)
            {
                st = tMoney.Substring(tn + 1);
                if (st != "")
                {
                    t1 = st.Substring(0, 1);
                    st = st.Substring(1);
                    if (t1 != "0")
                    {
                        s1 += CCh(int.Parse(t1));
                    }
                    if (st != "")
                    {
                        t1 = st.Substring(0, 1);
                        s1 += CCh(int.Parse(t1));
                    }
                }
                st = tMoney.Substring(0, tn);

            }
            else
            {
                s1 = "零零";
                st = tMoney;
            }

            //生成个十百千位大写
            if (st != "")
            {
                t1 = st.Substring(st.Length - 1);
                st = st.Substring(0, st.Length - 1);
                s2 += CCh(int.Parse(t1));
            }

            if (st != "")
            {
                t1 = st.Substring(st.Length - 1);
                st = st.Substring(0, st.Length - 1);
                if (t1 != "0")
                {
                    s2 = CCh(int.Parse(t1)) + s2;
                }
                else if (s2.Substring(0, 1) != "零")
                {
                    s2 = "零" + s2;
                }
            }

            if (st != "")
            {
                t1 = st.Substring(st.Length - 1);
                st = st.Substring(0, st.Length - 1);
                if (t1 != "0")
                {
                    s2 = CCh(int.Parse(t1)) + s2;
                }
                else if (s2.Substring(0, 1) != "零")
                {
                    s2 = "零" + s2;
                }
            }

            if (st != "")
            {
                t1 = st.Substring(st.Length - 1);
                st = st.Substring(0, st.Length - 1);
                if (t1 != "0")
                {
                    s2 = CCh(int.Parse(t1)) + s2;
                }
                else if (s2.Substring(0, 1) != "零")
                {
                    s2 = "零" + s2;
                }
            }

            //生成万位以上大写，最大为千万位
            if (st != "")
            {
                t1 = st.Substring(st.Length - 1);
                st = st.Substring(0, st.Length - 1);
                s3 += CCh(int.Parse(t1));
            }

            if (st != "")
            {
                t1 = st.Substring(st.Length - 1);
                st = st.Substring(0, st.Length - 1);
                if (t1 != "0")
                {
                    s3 = CCh(int.Parse(t1)) + s3;
                }
                else if (s3.Substring(0, 1) != "零")
                {
                    s3 = "零" + s3;
                }
            }

            if (st != "")
            {
                t1 = st.Substring(st.Length - 1);
                st = st.Substring(0, st.Length - 1);
                if (t1 != "0")
                {
                    s3 = CCh(int.Parse(t1)) + s3;
                }
                else if (s3.Substring(0, 1) != "零")
                {
                    s3 = "零" + s3;
                }
            }

            if (st != "")
            {
                t1 = st.Substring(st.Length - 1);
                st = st.Substring(0, st.Length - 1);
                if (t1 != "0")
                {
                    s3 = CCh(int.Parse(t1)) + s3;
                }
                else if (s3.Substring(0, 1) != "零")
                {
                    s3 = "零" + s3;
                }
            }

            //if (s2.Substring(s2.Length - 1) == "零")
            //    s2 = s2.Substring(0, s2.Length - 1);

            //if (s3.Length > 0)
            //{
            //    if (s3.Substring(s3.Length - 1) == "零")
            //        s3 = s3.Substring(0, s3.Length - 1);
            //}

            //生成转换结果
            convertResult = s3 + s2 == "" ? s1 : s3 + s2 + s1;
            return convertResult;
        }


       



    }
}
