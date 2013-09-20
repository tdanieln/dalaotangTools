using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mytools
{
    public class CallGenvictDevice : CallDevice
    {
        /// <summary>
        /// 串口号
        /// </summary>
        public static byte iComNo;
        /// <summary>
        /// 卡座标志: 0x00(NO_PSAM)：无PSAM卡；
        ///           0x01(PSAM1)：SAM卡座1；
        ///           0x02(PSAM2)：SAM卡座2；
        ///           0x03(PRO1): C6卡座
        /// </summary>
        public static int SAM = 0x00;

        //初始化设备
        public bool InitDevice(string CommId)
        {
            if (!byte.TryParse(CommId, out iComNo))
                return false;
            int ret = GenvictDLL.Open_Reader(iComNo, 115200, SAM);
            if (ret != 0)
            {
                Console.WriteLine(string.Format("金溢读卡器 打开串口号{0}设备失败！",CommId));
                return false;
            }

            return true;
        }
        //初始化IC卡
        public bool InitCPUcard()
        {
            byte[] cardNo = new byte[12];

            //射频复位
            if (GenvictDLL.RF_Reset(iComNo) != 0)
            {
                Console.WriteLine("金溢读卡器 射频复位失败！");
                return false;
            }

            //搜寻卡片
            if (GenvictDLL.Open_Card(iComNo, cardNo) != 0)
            {
                Console.WriteLine("金溢读卡器 打开卡片失败！");
                return false;
            }

            //判断卡类型
            if (GenvictDLL.Get_CardType(iComNo) == 1)
                return true;
            else
                return false;
        }

        //初始化IC片及PSAM卡
        public bool InitCPUcardAndPSAMcard()
        {
            byte[] cardNo = new byte[12];

            //射频复位
            if (GenvictDLL.RF_Reset(iComNo) != 0)
                return false;

            //搜寻卡片
            int ret = GenvictDLL.Open_Card(iComNo, cardNo);
            if (ret != 0)
                return false;

            //判断卡类型
            if (GenvictDLL.Get_CardType(iComNo) == 1)
            {
                //金溢读卡器需要先复位SAM卡后选择SAM卡座
                //复位PSAM卡
                ret = GenvictDLL.CPU_Reset(iComNo, SAM, new byte[] {0});
                if (ret != 0)
                    return false;

                //选择卡槽
                if (GenvictDLL.SAM_SELECT(iComNo, SAM) != 0)
                    return false;
            }
            else
            {
                return false;
            }
            return true;
        }
        //IC卡TimeCos指令
        public bool CPUTimecosCmd(string timeCosCmd, ref byte[] resp, int dataLength, ref int sw12)
        {
            byte[] sw = new byte[4];
            byte[] respBuff = new byte[dataLength * 2];
            int ret = GenvictDLL.PRO_CommandEx(iComNo, timeCosCmd, respBuff, sw);
            if (ret != 0 && ret != 37894)
            {
                
                return false;
            }
            else
            {
                for (int i = sw.Length - 1; i >= 0; i--)
                {
                    sw[i] -= 0x30;
                }
                sw12 = sw[0] * 4096 + sw[1] * 256 + sw[2] * 16 + sw[3];
                resp = this.ASCiiToCompression(respBuff, respBuff.Length);
            }
            return true;
        }

        //PSAM卡TimeCos指令
        public bool PSAMTimecosCmd(string timeCosCmd, ref byte[] resp, int dataLength, ref int sw12)
        {
            byte[] sw = new byte[4];
            byte[] respBuff = new byte[dataLength * 2];
            int ret = GenvictDLL.SAM_CommandEx(iComNo, timeCosCmd, respBuff, sw);
            if (ret != 0)
            {
                return false;
            }
            else
            {
                for (int i = sw.Length - 1; i >= 0; i--)
                {
                    sw[i] -= 0x30;
                }
                sw12 = sw[0] * 4096 + sw[1] * 256 + sw[2] * 16 + sw[3];
                resp = this.ASCiiToCompression(respBuff, respBuff.Length);
            }
            return true;
        }
        //关闭设备
        public bool CloseDevice()
        {
            if (GenvictDLL.Close_Reader(iComNo) != 0)
                return true;

            return false;
        }

        //ASCIIbyte[]转压缩十进制byte[]
        public byte[] ASCiiToCompression(byte[] data, int dataLength)
        {
            byte[] compressionData = new byte[dataLength / 2];
            for (int i = dataLength - 1; i >= 0; i--)
            {
                if (data[i] >= 0x30 && data[i] <= 0x39)
                {
                    data[i] -= 0x30;
                }
                else if (data[i] >= 0x41 && data[i] <= 0x5A)
                {
                    data[i] -= 0x37;
                }
            }
            for (int i = 0; i < dataLength / 2; i++)
            {
                compressionData[i] = (byte)(data[i * 2] * 0x10 + data[i * 2 + 1]);
            }
            return compressionData;
        }
    }
}
