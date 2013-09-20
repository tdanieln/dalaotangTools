using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace mytools
{
    class JuLiDLL
    {
        const string DLL_PATH_JuLi_API = @".\dll\IccReader_JL.dll";
        
        /// <summary>
        /// 打开与com口相连的机具
        /// </summary>
        /// <param name="iComNo">串口号,如：1、2、3、4</param>
        /// <param name="BaudRate">串口波特率，如：115200</param>
        /// <param name="cmdtype">
        /// 卡座标志: 0x00(NO_PSAM)：无PSAM卡；
        ///           0x01(PSAM1)：SAM卡座1；
        ///           0x02(PSAM2)：SAM卡座2；
        ///           0x03(PRO1): C6卡座
        /// </param>
        /// <returns>
        /// 操作结果代码：如果为NO_ERROR，正常；如果不等于NO_ERROR,出错，详情见Put_Error函数说明。
        /// </returns>
        /// <remarks>
        /// int  Open_Reader(BYTE iComNo, int BaudRate, int cmdtype);
        /// </remarks>
        [DllImport(DLL_PATH_JuLi_API, EntryPoint = "Open_Reader")]
        public static extern int Open_Reader(byte iComNo, int BaudRate, int cmdtype);

        /// <summary>
        /// 关闭已经打开的机具
        /// </summary>
        /// <param name="iComNo">已打开的串口号,如：1、2、3、4</param>
        /// <returns>
        /// 操作结果代码：如果为NO_ERROR，正常；如果不等于NO_ERROR,出错，详情见Put_Error函数说明。
        /// </returns>
        /// <remarks>
        /// int Close_Reader(BYTE iComNo);
        /// </remarks>
        [DllImport(DLL_PATH_JuLi_API, EntryPoint = "Close_Reader")]
        public static extern int Close_Reader(byte iComNo);

        /*
         * 功能 取操作返回信息
         * ret返回信息码
         * 操作成功,返回信息码对应的消息
         */
        [DllImport(DLL_PATH_JuLi_API, EntryPoint = "Put_Error")]
        public static extern string Put_Error(int ret);

        /// <summary>
        /// 复位射频，相当于短时间关闭天线后重新开启。该操作执行后能使所有MIFARE 
        ///  卡能被机具打开（Open_Card），包含原来被锁定（MF1_Halt）的卡;
        /// </summary>
        /// <param name="iComNo">已打开的串口号,如：1、2、3、4</param>
        /// <returns>
        /// 操作结果代码：如果为NO_ERROR，正常；如果不等于NO_ERROR,出错，详情见Put_Error函数说明。
        /// </returns>
        /// <remarks>
        /// int RF_Reset(BYTE iComNo);
        /// </remarks>
        [DllImport(DLL_PATH_JuLi_API, EntryPoint = "RF_Reset")]
        public static extern int RF_Reset(byte iComNo);

        /// <summary>
        /// 复位SAM卡，对SAM卡进行操作前需进行复位；
        /// </summary>
        /// <param name="iComNo">已打开的串口号,如：1、2、3、4</param>
        /// <param name="cmdtype">
        /// 卡座标志: 0x00(NO_PSAM)：无PSAM卡；
        ///           0x01(PSAM1)：SAM卡座1；
        ///           0x02(PSAM2)：SAM卡座2；
        ///           0x03(PRO1): C6卡座
        /// </param>
        /// <returns>
        /// 操作结果代码：如果为NO_ERROR，正常；如果不等于NO_ERROR,出错，详情见Put_Error函数说明。
        /// </returns>
        ///<remarks>
        ///int CPU_Reset(BYTE iComNo);
        ///</remarks>
        [DllImport(DLL_PATH_JuLi_API, EntryPoint = "CPU_Reset")]
        public static extern int CPU_Reset(byte iComNo);


        /// <summary>
        /// 选择SAM卡座
        /// </summary>
        /// <param name="cmdtype">
        /// 卡座标志: 0x00(NO_PSAM)：无PSAM卡；
        ///           0x01(PSAM1)：SAM卡座1；
        ///           0x02(PSAM2)：SAM卡座2；
        ///           0x03(PRO1): C6卡座
        /// </param>
        /// <returns>
        /// 操作结果代码：如果为NO_ERROR，正常；如果不等于NO_ERROR,出错，详情见Put_Error函数说明。
        /// </returns>
        ///<remarks>
        ///int CPU_Selectt(int cmdtype);
        ///</remarks>
        [DllImport(DLL_PATH_JuLi_API, EntryPoint = "CPU_Select")]
        public static extern int CPU_Select(int cmdtype);


        /// <summary>
        /// 搜寻天线范围内是否存在MIFARE非接触式IC卡，该操作是MIFARE ONE和PRO
        /// 卡都必须进行的操作；
        /// </summary>
        /// <param name="iComNo">已打开的串口号</param>
        /// <param name="cardno">
        /// 返回的IC卡号
        /// Open_Card：非desfire卡长度为8，desfire卡长度为14;
        /// </param>
        /// <returns>
        /// 操作结果代码：如果为NO_ERROR，正常；如果不等于NO_ERROR,出错，详情见Put_Error函数说明。
        /// </returns>
        /// <remarks>
        /// int Open_Card(BYTE iComNo,DWORD &dwcardno); 
        /// </remarks>
        [DllImport(DLL_PATH_JuLi_API, EntryPoint = "Open_Card")]
        public static extern int Open_Card(byte iComNo, byte[] cardno);

        /// <summary>
        /// 返回当前所选定的MIFARE卡类型
        /// </summary>
        /// <param name="iComNo">已打开的串口号</param>
        /// <returns>
        /// 卡类型
        /// -1 —— 未知类型
        /// 0，MIFARE_ONE—— MIFARE ONE卡；
        /// 1,MIFARE_PRO—— MIFARE PRO 卡；
        /// </returns>
        /// <remarks>
        /// int Get_CardType(BYTE iComNo);
        /// </remarks>
        [DllImport(DLL_PATH_JuLi_API, EntryPoint = "Get_CardType")]
        public static extern int Get_CardType(byte iComNo);

        /// <summary>
        /// MIFARE PRO卡指令的通用支持函数，可实现MIFARE PRO卡的任意TIMECOS指令
        /// </summary>
        /// <param name="iComNo">已打开的串口号</param>
        /// <param name="command">指令帧（以’\0’结尾的16进制字符串或常量字符串），</param>
        /// <param name="response">响应帧（以’\0’结尾的16进制字符串或常量字符串，含SW1和SW2）</param>
        /// <param name="sw12">响应状态码SW1和SW2的字符串显示，如“9000” （以’\0’结尾的16进制字符串或常量字符串）等； </param>
        /// <returns>操作结果代码：如果为NO_ERROR，正常；如果不等于NO_ERROR,出错，详情见Put_Error函数说明。</returns>
        /// <remarks>
        /// int PRO_CommandEx(BYTE iComNo,char *command,char *response,char *sw12);
        /// </remarks>
        [DllImport(DLL_PATH_JuLi_API, EntryPoint = "PRO_CommandEx")]
        public static extern int PRO_CommandEx(byte iComNo, string command, byte[] response, byte[] sw12);

        /// <summary>
        /// SAM卡指令的通用支持函数，可实现SAM卡的任意TIMECOS指令。
        /// </summary>
        /// <param name="iComNo">已打开的串口号</param>
        /// <param name="command">指令帧（以’\0’结尾的16进制字符串或常量字符串）</param>
        /// <param name="response">响应帧（以’\0’结尾的16进制字符串或常量字符串，含SW1和SW2）</param>
        /// <param name="sw12">响应状态码SW1和SW2的字符串显示，如“9000” （以’\0’结尾的16进制字符串或常量字符串）等；</param>
        /// <returns>操作结果代码：如果为NO_ERROR，正常；如果不等于NO_ERROR,出错，详情见Put_Error函数说明。</returns>
        ///<remarks1>
        ///int SAM_CommandEx(BYTE iComNo,char *command,char *resdata,char *sw12);
        ///</remarks1>
        [DllImport(DLL_PATH_JuLi_API, EntryPoint = "SAM_CommandEx")]
        public static extern int SAM_CommandEx(byte iComNo, string command, byte[] resdata, byte[] sw12);   
    }
}


/* 
         * 功能选择SAM卡卡座,该接口要先于Open_Reader运行
         * cmdtype卡座标志
         * 0x00卡座1
         * 0x01卡座2
         * 0xC8C2机具(为了兼容C2)
         * 0x22不需要PSAM卡(为了兼容一些统一密钥的卡片)
         * 0x63C6的接触式CPU卡座
         * iSpeedy C6机具可以以C2方式运行，但是无法使用第二个SAM卡座
         * void  __stdcall SelectSAM(int cmdtype);
         */
//[DllImport(DLL_PATH_JuLi_API, EntryPoint = "SelectSAM")]
//public static extern void SelectSAM(int cmdtype);
///*
// * 功能 设置机具波特率 , 同时设置PC机串口波特率
// * fd串口句柄.
// * baudrate 波特率值
// * int __stdcall RD_SetBaud(HANDLE fd,unsigned long baudrate);
// */ 
//[DllImport(DLL_PATH_JuLi_API, EntryPoint = "RD_SetBaud")]
//public static extern int RD_SetBaud(IntPtr fd, ulong baudrate);

///*
// * 功能 复位PSAM卡
// * fd串口句柄.
// * int __stdcall SAM_Reset(HANDLE fd);
// */
//[DllImport(DLL_PATH_JuLi_API, EntryPoint = "SAM_Reset")]
//public static extern int SAM_Reset(IntPtr fd);

/*
        * 功能 设置蜂鸣器
        * fd串口句柄.
        * beep 蜂鸣器工作方式
        * NO_CONTROL (0x00)   保持原状态，不控制；
        * BEEP_OFF   (0x80)   关闭蜂鸣器；
        * BEEP_SHORT  (0x81)  蜂鸣器"D-D"鸣叫两声，间隔100毫秒；
        * BEEP_LASTING (0x82) 蜂鸣器鸣叫1秒后停止；
        * green 绿指示灯工作方式
        * NO_CONTROL (0x00)    保持原状态，不控制；
        * LED_OFF   (0x80)     关闭指示灯；
        * LED_LASTING (0x81)   令指示灯一直亮；
        * LED_SHORT  (0x82)    指示灯亮1秒后熄灭；
        * LED_TWINKLE (0x83)   指示灯以1秒的间隔交互闪烁；
        * yellow 黄指示灯工作方式
        * 同green灯；
        * int __stdcall RD_SetMMI(HANDLE fd,int beep,int green,int yellow);
        */
//[DllImport(DLL_PATH_JuLi_API, EntryPoint = "RD_SetMMI")]
//public static extern int RD_SetMMI(IntPtr fd, int beep, int green, int yellow);