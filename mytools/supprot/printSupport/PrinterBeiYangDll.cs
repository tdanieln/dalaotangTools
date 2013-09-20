using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace mytools
{
    class PrinterBeiYangDll
    {
        private const string Dll_Path_BeiYang_API = @".\dll\bpladll.dll";

        /// <summary>
        ///  北洋打印机 打开USB端口
        /// </summary>
        /// <returns></returns>
        [DllImport(Dll_Path_BeiYang_API, EntryPoint = "BPLA_OpenUsb")]
        public static extern int OpenCommWithUSB();

        /// <summary>
        /// 北洋打印机 打开Com端口
        /// </summary>
        /// <param name="COMName">端口号</param>
        /// <param name="iBaudrate">波特率</param>
        /// <param name="handshak">握手信号</param>
        /// <returns></returns>
        [DllImport(Dll_Path_BeiYang_API, EntryPoint = "BPLA_OpenCom")]
        public static extern int OpenCommWithCom(string COMName,int iBaudrate,int handshak);


        /// <summary>
        /// 北洋打印机 设置处理纸张模式
        /// </summary>
        /// <param name="outmode">出纸方式：0为切刀</param>
        /// <param name="papermode">纸张模式：1为连续纸</param>
        /// <param name="printmode">打印模式：1为热转印打印</param>
        /// <returns></returns>
        [DllImport(Dll_Path_BeiYang_API, EntryPoint = "BPLA_Set")]
        public static extern int SetMode(int outmode, int papermode, int printmode);

        /// <summary>
        /// 北洋打印机 设置纸张长度
        /// </summary>
        /// <param name="continuelength">连续纸长度，如果为0则不进行设置，单位为毫米/10</param>
        /// <param name="labellength"></param>
        /// <returns></returns>
        [DllImport(Dll_Path_BeiYang_API, EntryPoint = "BPLA_SetPaperLength")]
        public static extern int SetPaperLength(int continuelength, int labellength);

        /// <summary>
        /// 北洋打印机 设置翻转模式
        /// </summary>
        /// <param name="rotatemode">翻转模式：1 ，180°翻转</param>
        /// <returns></returns>
        [DllImport(Dll_Path_BeiYang_API, EntryPoint = "BPLA_SetAllRotate")]
        public static extern int SetReversalMode(int rotatemode);

        /// <summary>
        /// 北洋打印机 设置反射模式
        /// </summary>
        /// <param name="mode">0，黑标纸；1，透明标签</param>
        /// <returns></returns>
        [DllImport(Dll_Path_BeiYang_API, EntryPoint = "BPLA_SetSensor")]
        public static extern int SetSensor(int mode);

        /// <summary>
        /// 北洋打印机 设置停止打印位置
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        [DllImport(Dll_Path_BeiYang_API, EntryPoint = "BPLA_SetEnd")]
        public static extern int SetEnd(int position);


        /// <summary>
        /// 北洋打印机 票面排版
        /// </summary>
        /// <param name="unitmode">单位模式：0默认</param>
        /// <param name="printwidth">打印宽度范围</param>
        /// <param name="column">列偏移</param>
        /// <param name="row">行偏移</param>
        /// <param name="darkness">打印浓度</param>
        /// <param name="speedprint">打印速度</param>
        /// <param name="speedfor">进纸速度</param>
        /// <param name="speedbac">退纸速度</param>
        /// <returns></returns>
        [DllImport(Dll_Path_BeiYang_API,EntryPoint = "BPLA_StartArea")]
        public static extern int SetArea(int unitmode, int printwidth, int column, int row, int darkness, int speedprint, int speedfor, int speedbac);

        /// <summary>
        /// 北洋打印机 设置打印内容
        /// </summary>
        /// <param name="text">需要打印的文字</param>
        /// <param name="startx">起点横坐标</param>
        /// <param name="starty">起点纵坐标</param>
        /// <param name="fontname">字体名称</param>
        /// <param name="fontheight">字体高度</param>
        /// <param name="fontwidth">字体宽度</param>
        /// <returns></returns>
        [DllImport(Dll_Path_BeiYang_API, EntryPoint = "BPLA_PrintTruetype")]
        public static extern int SetPrintInfo(string text, int startx, int starty, string fontname, int fontheight, int fontwidth);

        /// <summary>
        /// 北洋打印机 打印
        /// </summary>
        /// <param name="pieces">打印数量</param>
        /// <param name="samepieces"></param>
        /// <param name="outunit">出纸单位</param>
        /// <returns></returns>
        [DllImport(Dll_Path_BeiYang_API, EntryPoint = "BPLA_Print")]
        public static extern int Print(int pieces, int samepieces, int outunit);

        /// <summary>
        /// 北洋打印机 检查状态
        /// </summary>
        /// <param name="papershort"></param>
        /// <param name="ribbionshort"></param>
        /// <param name="busy"></param>
        /// <param name="pause"></param>
        /// <param name="com"></param>
        /// <param name="headheat"></param>
        /// <param name="headover"></param>
        /// <param name="cut"></param>
        /// <returns></returns>
        [DllImport(Dll_Path_BeiYang_API, EntryPoint = "BPLA_CheckStatus")]
        public static extern int CheckStatus(byte[] papershort, byte[] ribbionshort, byte[] busy, byte[] pause, byte[] com, byte[] headheat, byte[] headover, byte[] cut);
    }
}
