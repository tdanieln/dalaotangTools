using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace mytools
{
    class ZTEOBUReaderDll
    {
        const string DLL_PATH_ZTE_OBU = @".\dll\RSUComm.dll";

        /// <summary>
        /// 中兴OBU发行器：使用网络的方式初始化设备
        /// </summary>
        /// <param name="deviceInitParameterType">使用USB连接时的初始化参数结构体</param>
        /// <param name="ipaddr">IP地址</param>
        /// <param name="RSU_SOCKET_PORT">IP连接时的端口号</param>
        /// <param name="piManufactureorID"></param>
        /// <param name="pcDllVer"></param>
        /// <param name="pcDevVer"></param>
        /// <returns></returns>
        [DllImport(DLL_PATH_ZTE_OBU, CallingConvention = CallingConvention.StdCall)]
        public unsafe static extern int OBUProg_DevInit_Socket(DeviceInitParameterType deviceInitParameterType, char[] ipaddr, int RSU_SOCKET_PORT, int* piManufactureorID, byte[] pcDllVer, byte[] pcDevVer);


        /// <summary>
        /// 中兴OBU发行器：使用USB的方式初始化设备
        /// </summary>
        /// <param name="deviceInitParameterType"></param>
        /// <returns></returns>
        [DllImport(DLL_PATH_ZTE_OBU, EntryPoint = "OBUProg_DevInit_USB")]
        public unsafe static extern int OBUProg_DevInit_USB(DeviceInitParameterType deviceInitParameterType);


        /// <summary>
        /// 中兴OBU发行器：释放设备
        /// </summary>
        /// <returns></returns>
        [DllImport(DLL_PATH_ZTE_OBU, EntryPoint = "OBUProg_DevRelease")]
        public unsafe static extern int OBUProg_DevRelease();

        /// <summary>
        /// 中兴OBU发行器：显示错误信息
        /// </summary>
        /// <param name="err"></param>
        /// <param name="pMessage"></param>
        /// <returns></returns>
        [DllImport(DLL_PATH_ZTE_OBU, EntryPoint = "ZTE_ErrorMessage")]
        public unsafe static extern int ZTE_ErrorMessage(int err, ref byte[] pMessage);

        /// <summary>
        /// 中兴OBU发行器：OBU初始化
        /// </summary>
        /// <returns></returns>
        [DllImport(DLL_PATH_ZTE_OBU, EntryPoint = "OBUProg_OBUInit")]
        public unsafe static extern int OBUProg_OBUInit();

        /// <summary>
        /// 中兴OBU发行器：写OBU系统信息
        /// </summary>
        /// <param name="struSystemInfo"></param>
        /// <returns></returns>
        [DllImport(DLL_PATH_ZTE_OBU, EntryPoint = "OBUProg_Write_SysInfo")]
        public unsafe static extern int OBUProg_Write_SysInfo(ref SysInfoType struSystemInfo);

        /// <summary>
        /// 中兴OBU发行器：读OBU系统信息
        /// </summary>
        /// <param name="struSystemInfo"></param>
        /// <returns></returns>
        [DllImport(DLL_PATH_ZTE_OBU, EntryPoint = "OBUProg_Read_SysInfo")]
        public unsafe static extern int OBUProg_Read_SysInfo(ref SysInfoType struSystemInfo);

        /// <summary>
        /// 中兴OBU发行器：写ETC车辆信息
        /// </summary>
        /// <param name="struETCVehicleInfoType"></param>
        /// <returns></returns>
        [DllImport(DLL_PATH_ZTE_OBU, EntryPoint = "OBUProg_Write_ETCVehicleInfo")]
        public unsafe static extern int OBUProg_Write_ETCVehicleInfo(ref ETCVehicleInfoType struETCVehicleInfoType);

        /// <summary>
        /// 中兴OBU发行器：读ETC车辆信息
        /// </summary>
        /// <param name="struETCVehicleInfoType"></param>
        /// <returns></returns>
        [DllImport(DLL_PATH_ZTE_OBU, EntryPoint = "OBUProg_Read_ETCVehicleInfo")]
        public unsafe static extern int OBUProg_Read_ETCVehicleInfo(ref ETCVehicleInfoType struETCVehicleInfoType);


        /// <summary>
        /// 中兴OBU发行器：国标接口，打开设备
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="dev"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        [DllImport(DLL_PATH_ZTE_OBU, EntryPoint = "RSU_Open")]
        public unsafe static extern long RSU_Open(int mode,string dev,int port);

        /// <summary>
        /// 中兴发行期：国标接口，关闭设备
        /// </summary>
        /// <returns></returns>
        [DllImport(DLL_PATH_ZTE_OBU, EntryPoint = "RSU_Close")]
        public unsafe static extern int RSU_Close(long fd);

        /// <summary>
        /// 中兴发行器：国标接口 RSU初始化
        /// </summary>
        /// <param name="fd">设备句柄</param>
        /// <param name="time">Unix时间</param>
        /// <param name="BstInterVal">发送Bst间隔</param>
        /// <param name="TxPower">发射功率</param>
        /// <param name="pllChannelID">信道号</param>
        /// <param name="timeOUT">超时时间</param>
        /// <returns></returns>
        [DllImport(DLL_PATH_ZTE_OBU, EntryPoint = "RSU_INIT_rq")]
        public unsafe static extern int RSU_Init_rq(long fd, string time, int BstInterVal, int TxPower, int pllChannelID, int timeOUT);

        /// <summary>
        /// 中兴发行器：国标接口 RSU初始化响应
        /// </summary>
        /// <param name="fd">句柄</param>
        /// <param name="RSUStatus">RSU状态</param>
        /// <param name="rlen">返回信息长度</param>
        /// <param name="RSUInfo">RSU信息</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        [DllImport(DLL_PATH_ZTE_OBU, EntryPoint = "RSU_INIT_rs")]
        public unsafe static extern int RSU_Init_rs(long fd, int RSUStatus, int rlen, string RSUInfo, int timeOut);
    }
}
