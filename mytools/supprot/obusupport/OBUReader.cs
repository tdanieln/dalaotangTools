using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mytools
{
    public interface OBUReader
    {
        /// <summary>
        /// OBU设备打开
        /// </summary>
        /// <param name="mode">输入参数：设备接口模式，0：串口；1：TCP/IP；2：USB</param>
        /// <param name="devNo">输入参数：设备连接号，串口模式“COM1”，网络模式，“192.168.1.1”，USB模式“USB1”，</param>
        /// <param name="port">输入参数：TCP/IP模式下填端口号，串口及USB模式填0</param>
        /// <param name="errInfo">输出参数:错误信息</param>
        /// <returns>返回值：返回指令执行成功或者失败，0成功，其他为失败</returns>
        bool OBUProg_DevInit(int mode, string devNo, int port, ref string errInfo);

        /// <summary>
        /// OBU设备关闭
        /// </summary>
        /// <param name="errInfo">输出参数:错误信息</param>
        /// <returns>返回值：返回指令执行成功或者失败，0成功，其他为失败</returns>
        bool OBUProg_DevRelease(ref string errInfo);

        /// <summary>
        /// 读OBU系统信息
        /// </summary>
        /// <param name="sysInfoType">输出参数：OBU系统信息</param>
        /// <param name="errInfo">输出参数：错误信息</param>
        /// <returns>返回值：返回指令执行成功或者失败，0成功，其他为失败</returns>
        bool OBUProg_Read_SysInfo(ref SysInfoType sysInfoType, ref string errInfo);

        /// <summary>
        /// 写OBU系统信息
        /// </summary>
        /// <param name="sysInfoType">输入参数：OBU系统信息</param>
        /// <param name="errInfo">输出参数：错误信息</param>
        /// <returns>返回值：返回指令执行成功或者失败，0成功，其他为失败</returns>
        bool OBUProg_Write_SysInfo(SysInfoType sysInfoType, ref string errInfo);

        /// <summary>
        /// 读OBU车辆信息
        /// </summary>
        /// <param name="etcVehicleInfoType">输出参数：OBU车辆信息结构体</param>
        /// <param name="errInfo">输出参数：错误信息</param>
        /// <returns>返回指令执行成功或者失败，0成功，其他为失败</returns>
        bool OBUProg_Read_ETCVehicleInfo(ref ETCVehicleInfoType etcVehicleInfoType, ref string errInfo);

        /// <summary>
        /// 写OBU车辆信息
        /// </summary>
        /// <param name="etcVehicleInfoType">输入参数：OBU车辆信息结构体</param>
        /// <param name="errInfo">输出参数：错误信息</param>
        /// <returns>返回指令执行成功或者失败，0成功，其他为失败</returns>
        bool OBUProg_Write_ETCVehicleInfo(ETCVehicleInfoType etcVehicleInfoType, ref string errInfo);

        /// <summary>
        /// OBU信息初始化(一次发行)
        /// </summary>
        /// <param name="sysInfoType">输入参数：OBU系统信息</param>
        /// <param name="errInfo">输出参数：错误信息</param>
        /// <returns>返回指令执行成功或者失败，0成功，其他为失败</returns>
        bool OBUProg_OBUInit(SysInfoType sysInfoType, ref string errInfo);

        /// <summary>
        /// OBU防拆标识位标识重置
        /// </summary>
        /// <param name="errInfo">输出参数：错误信息</param>
        /// <returns>返回指令执行成功或者失败，0成功，其他为失败</returns>
        bool OBUProg_Reset_TamperFlag(ref string errInfo);



    }
}
