using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mytools
{
    public interface CallDevice
    {
        //public static IntPtr CommHandle;
        /*
         * 功能初始化设备
         * CommIdCOM口编号
         */ 
        bool InitDevice(string CommId);
        /*
         * 功能初始化IC卡
         */ 
        bool InitCPUcard();

        /*
         * 功能初始化IC卡和PSAM卡
         */
        bool InitCPUcardAndPSAMcard();

        /*
         * 功能对IC卡进行Timecos操作
         * timeCosCmdstring型Timecos指令
         * resp卡内返回信息,压缩十进制编码格式
         * sw12返回SW1SW2值
         */
        bool CPUTimecosCmd(string timeCosCmd, ref byte[] resp, int dataLength, ref int sw12);

        /*
         * 功能对PSAM卡进行Timecos操作
         * timeCosCmdstring型Timecos指令
         * resp卡内返回信息,压缩十进制编码格式
         * sw12返回SW1SW2值
         */
        bool PSAMTimecosCmd(string timeCosCmd, ref byte[] resp, int dataLength, ref int sw12);
        /*
         * 功能关闭设备
         */ 
        bool CloseDevice();
    }
}
