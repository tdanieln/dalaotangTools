using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace mytools
{
    class CallWanJiOBUReader : CallOBUReader
    {
        public unsafe bool OBUProg_DevInit(int mode, string devNo, int port, ref string errInfo)
        {
            //操作结果
            bool operationDone = false;
            int connectResult = -1;

            //&piManufactureorID指向标签发型设备生产商ID号的指针
            try
            {
                if (mode != (int)RSUConnectMode.COM && mode != (int)RSUConnectMode.TCPIP && mode != (int)RSUConnectMode.USB)
                {
                    errInfo += "OBU连接方式不为串口连接，网络连接，USB连接，连接方式未知！";
                    throw new Exception();
                }

                connectResult = WanJiOBUReaderDll.OBU_Prog_DevInit(mode, devNo, port);

                if (connectResult != (int)OperationResult.oprSuccess)
                {
                    Console.WriteLine("万集OBU发行器打开设备失败，返回值为" + connectResult.ToString());
                    operationDone = false;
                    throw new Exception();
                }
                operationDone = true;
            }
            catch (Exception err)
            {
                operationDone = false;
                Console.WriteLine(errInfo + err.Message);
            }
            return operationDone;
        }

        /// <summary>
        /// 释放设备
        /// </summary>
        /// <param name="errInfo">输出参数：错误信息</param>
        /// <returns></returns>
        public bool OBUProg_DevRelease(ref string errInfo)
        {
            bool operationDone = false;

            int devRelResult = (int)OperationResult.oprFault;

            try
            {
                //释放连接
                devRelResult = WanJiOBUReaderDll.OBUProg_DevRelease();

                if (devRelResult != (int)OperationResult.oprSuccess)
                {
                    Console.WriteLine("万集OBU发行器释放设备失败，返回码：" + devRelResult.ToString());
                    operationDone = false;
                    throw new Exception();
                }
                operationDone = true;
            }
            catch (Exception err)
            {
                operationDone = false;
                Console.WriteLine(errInfo + err.Message);
            }
            return operationDone;
        }

        /// <summary>
        /// 读OBU系统信息
        /// </summary>
        /// <param name="sysInfoType">传出参数，系统信息结构体</param>
        /// <param name="errInfo">传出参数：错误信息</param>
        /// <returns></returns>
        public unsafe bool OBUProg_Read_SysInfo(ref SysInfoType sysInfoType, ref string errInfo)
        {
            bool operationDone = false;
            int readResult;
            int sz = Marshal.SizeOf(typeof(SysInfoType));

            try
            {
                IntPtr ptr = Marshal.AllocCoTaskMem(sz);
                SysInfoType getSysInfoType = new SysInfoType();

                //读OBU系统信息
                readResult = WanJiOBUReaderDll.OBUProg_Read_SysInfo(ref getSysInfoType);

                //释放内存
                Marshal.FreeHGlobal(ptr);

                if (readResult != (int)OperationResult.oprSuccess)
                {
                    Console.WriteLine("万集OBU发行器读系统信息失败，返回码：" + readResult.ToString());
                    operationDone = false;
                    throw new Exception();
                }
                operationDone = true;
            }
            catch (Exception err)
            {
                operationDone = false;
                Console.WriteLine(errInfo + err.Message);
            }
            return operationDone;

        }

        /// <summary>
        /// 万集OBU发行器，写系统信息
        /// </summary>
        /// <param name="sysInfoType">输入参数：系统信息</param>
        /// <param name="errInfo">输出参数：错误信息</param>
        /// <returns></returns>
        public bool OBUProg_Write_SysInfo(SysInfoType sysInfoType, ref string errInfo)
        {
            bool operationDone = false;

            int writeResult = (int)OperationResult.oprFault;

            int sz = Marshal.SizeOf(typeof(SysInfoType));

            IntPtr ptr = Marshal.AllocCoTaskMem(sz);

            try
            {
                SysInfoType writeSysInfo = new SysInfoType();

                writeSysInfo = sysInfoType;

                //读OBU系统信息
                writeResult = WanJiOBUReaderDll.OBUProg_Write_SysInfo(ref writeSysInfo);

                if (writeResult != (int)OperationResult.oprSuccess)
                {
                    Console.WriteLine("万集OBU发行器读系统信息失败，返回码：" + writeResult.ToString());
                    operationDone = false;
                    throw new Exception();
                }
                operationDone = true;
            }
            catch (Exception err)
            {
                operationDone = false;
                Console.WriteLine(errInfo + err.Message);
            }
            finally
            {
                //释放内存
                Marshal.FreeHGlobal(ptr);
            }
            return operationDone;
        }

        /// <summary>
        /// 读ETC车牌号信息
        /// </summary>
        /// <param name="etcVehicleInfoType">车辆信息结构体</param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public bool OBUProg_Read_ETCVehicleInfo(ref ETCVehicleInfoType etcVehicleInfoType, ref string errInfo)
        {
            bool operationDone = false;

            int readResult = (int)OperationResult.oprFault;

            int sz = Marshal.SizeOf(typeof(ETCVehicleInfoType));

            IntPtr ptr = Marshal.AllocCoTaskMem(sz);

            try
            {
                etcVehicleInfoType = new ETCVehicleInfoType(ptr);

                try
                {
                    //读OBU系统信息
                    readResult = WanJiOBUReaderDll.OBUProg_Read_ETCVehicleInfo(ref etcVehicleInfoType);
                }
                finally
                {
                    Marshal.FreeHGlobal(ptr);
                }

                if (readResult != (int)OperationResult.oprSuccess)
                {
                    Console.WriteLine("万集OBU发行器读系统信息失败，返回码：" + readResult.ToString());
                    operationDone = false;
                    throw new Exception();
                }
                operationDone = true;
            }
            catch (Exception err)
            {
                operationDone = false;
                Console.WriteLine(errInfo + err.Message);
            }
            //finally
            //{
            //    //释放内存
            //    Marshal.FreeHGlobal(ptr);
            //}
            return operationDone;
        }

        /// <summary>
        /// 写ETC车辆信息
        /// </summary>
        /// <param name="etcVehicleInfoType"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public bool OBUProg_Write_ETCVehicleInfo(ETCVehicleInfoType etcVehicleInfoType, ref string errInfo)
        {
            bool operationDone = false;

            int writeResult = (int)OperationResult.oprFault;

            int sz = Marshal.SizeOf(typeof(ETCVehicleInfoType));

            IntPtr ptr = Marshal.AllocCoTaskMem(sz);

            try
            {
                ETCVehicleInfoType writeETCVehicleInfo = new ETCVehicleInfoType(ptr);

                writeETCVehicleInfo = etcVehicleInfoType;

                //读OBU系统信息
                writeResult = WanJiOBUReaderDll.OBUProg_Write_ETCVehicleInfo(ref writeETCVehicleInfo);

                if (writeResult != (int)OperationResult.oprSuccess)
                {
                    Console.WriteLine("万集OBU发行器读系统信息失败，返回码：" + writeResult.ToString());
                    operationDone = false;
                    throw new Exception();
                }
                operationDone = true;
            }
            catch (Exception err)
            {
                operationDone = false;
                Console.WriteLine(errInfo + err.Message);
            }
            finally
            {
                //释放内存
                Marshal.FreeHGlobal(ptr);
            }
            return operationDone;
        }

        public bool OBUProg_OBUInit(SysInfoType sysInfoType, ref string errInfo)
        {
            return false;
        }

        public bool OBUProg_Reset_TamperFlag(ref string errInfo)
        {
            return false;
        }
    }
}
