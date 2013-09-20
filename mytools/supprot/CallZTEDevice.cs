using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mytools
{
    class CallZTEDevice : CallDevice
    {
        public static IntPtr CommHandle;
        //初始化设备
        public bool InitDevice(string CommId)
        {
            //CommId = "7";
            IntPtr InitState;
            string ComS;
            ComS = "COM";
            string USBS;
            USBS = "USB";
            ComS = ComS + CommId;
            USBS = USBS + CommId;


            //先用USB口初始化设备
            
            InitState = ZTEDLL.CT_open(USBS, 1, 0);
            if ((int)InitState > 0)
            {
                CallZTEDevice.CommHandle = InitState;
            }
            else
            {
                //COM打开读卡器设备
                InitState = ZTEDLL.CT_open(ComS, 9600, 'E');
                if ((int)InitState > 0)
                {
                    CallZTEDevice.CommHandle = InitState;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
        //初始化IC卡
        public bool InitCPUcard()
        {
            bool ret = false;

            int sw12 = 0;
            byte[] resp = new byte[600];

            //设置非接触读卡
            ZTEDLL.ICC_set_NAD(CommHandle, Convert.ToByte(0x15));
            

            //对IC卡复位
            ret = this.CPUTimecosCmd("0012000000", ref resp, 600, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                Log.WriteLog("中兴读卡器对卡片复位失败，复位指令内容为：0012000000");
                return false;
            }
            else
            {
                Log.WriteLog("中兴读卡器对卡片复位成功");
                return true;
            }

        }

        /// <summary>
        /// 初始化IC卡和PSAM卡
        /// </summary>
        /// <returns></returns>
        /// <remarks>对于握奇读卡器实际只初始化PSAM卡，对于IC卡的初始化</remarks>
        public bool InitCPUcardAndPSAMcard()
        {
            bool ret = false;

            int sw12 = 0;
            byte[] resp = new byte[600];

            //复位
            ret = this.CPUTimecosCmd("0012000000", ref resp, 600, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                return false;
            }

            //复位
            ret = this.PSAMTimecosCmd("0012000000", ref resp, 600, ref sw12);
            if (!ret || sw12 != 0x9000)
            {
                return false;
            }



            return true;

        }
        //IC卡TimeCos指令
        public bool CPUTimecosCmd(string timeCosCmd, ref byte[] resp, int dataLength, ref int sw12)
        {
            byte[] byteTimeCosCmd = null;
            timeCosCmd = timeCosCmd.Trim();
            byte lenr = new byte(); //从卡上接收到的数据的长度
            byteTimeCosCmd = this.HexStringToHexByteArrary(timeCosCmd, timeCosCmd.Length / 2);

            ZTEDLL.ICC_set_NAD(CommHandle, Convert.ToByte(0x15));

            //TimeCos指令
            sw12 = ZTEDLL.ICC_tsi_api(CallZTEDevice.CommHandle, byteTimeCosCmd.Length, byteTimeCosCmd, ref lenr, resp);
            return true;
        }

        //PSAM卡TimeCos指令
        public bool PSAMTimecosCmd(string timeCosCmd, ref byte[] resp, int dataLength, ref int sw12)
        {
            byte[] byteTimeCosCmd = null;
            timeCosCmd = timeCosCmd.Trim();
            byte lenr = new byte(); //从卡上接收到的数据的长度
            byteTimeCosCmd = this.HexStringToHexByteArrary(timeCosCmd, timeCosCmd.Length / 2);

            ZTEDLL.ICC_set_NAD(CommHandle, Convert.ToByte(0x13));

            //TimeCos指令
            sw12 = ZTEDLL.ICC_tsi_api(CallZTEDevice.CommHandle, byteTimeCosCmd.Length, byteTimeCosCmd, ref lenr, resp);
            return true;
        }

        //关闭设备
        public bool CloseDevice()
        {
            if ((int)CommHandle > 0)
            {
                ZTEDLL.CT_close(CallZTEDevice.CommHandle);
                CallZTEDevice.CommHandle = new IntPtr();
            }
            return true;
        }

        //16进制String转换成16进制字节数组
        public byte[] HexStringToHexByteArrary(string hexString, int sizeOfHexByteArrary)
        {
            byte[] hexByteArrary = new byte[sizeOfHexByteArrary];
            for (int i = 0, n = 0; n < sizeOfHexByteArrary; i += 2, n++)
            {
                hexByteArrary[n] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return hexByteArrary;
        }
    }
}
