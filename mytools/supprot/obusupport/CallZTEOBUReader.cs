using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace mytools
{
    class CallZTEOBUReader : OBUReader
    {
        const int timeOut = 1000;

        #region 集成模式接口
        public unsafe bool OBUProg_DevInit(int mode, string devNo, int port, ref string errInfo)
        {
            //操作结果
            bool operationDone = false;
            int connectResult = -1;

            //&piManufactureorID指向标签发型设备生产商ID号的指针
            int piManufactureorID;
            byte[] pcDllVer = new byte[100];
            byte[] pcDevVer = new byte[100];
            try
            {
                if (mode != (int)RSUConnectMode.COM && mode != (int)RSUConnectMode.TCPIP && mode != (int)RSUConnectMode.USB)
                {
                    errInfo += "OBU连接方式不为串口连接，网络连接，USB连接，连接方式未知！";
                    throw new Exception();
                }

                DeviceInitParameterType deviceInitParameterType = new DeviceInitParameterType();
                deviceInitParameterType.UnixTime = new byte[4];
                deviceInitParameterType.TxPower = new byte[1];
                deviceInitParameterType.TxPower[0] = 0xFF;
                deviceInitParameterType.BSTInterval = new byte[1];
                deviceInitParameterType.PHYChannelID = new byte[1];
                deviceInitParameterType.Reserved = new byte[5];

                //使用USB方式打开中兴读卡器连接
                connectResult = ZTEOBUReaderDll.OBUProg_DevInit_Socket(deviceInitParameterType, devNo.ToCharArray(), port, &piManufactureorID, pcDllVer, pcDevVer);

                if (connectResult != (int)OperationResult.oprSuccess)
                {
                    Console.WriteLine("中兴OBU发行器打开设备失败，返回值为" + connectResult.ToString());
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
                devRelResult = ZTEOBUReaderDll.OBUProg_DevRelease();

                if (devRelResult != (int)OperationResult.oprSuccess)
                {
                    Console.WriteLine("中兴OBU发行器释放设备失败，返回码：" + devRelResult.ToString());
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
                SysInfoType getSysInfoType = new SysInfoType(ptr);

                //读OBU系统信息
                readResult = ZTEOBUReaderDll.OBUProg_Read_SysInfo(ref getSysInfoType);

                //释放内存
                Marshal.FreeHGlobal(ptr);

                if (readResult != (int)OperationResult.oprSuccess)
                {
                    Console.WriteLine("中兴OBU发行器读系统信息失败，返回码：" + readResult.ToString());
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
        /// 中兴OBU发行器，写系统信息
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
                SysInfoType writeSysInfo = new SysInfoType(ptr);

                writeSysInfo = sysInfoType;

                //读OBU系统信息
                writeResult = ZTEOBUReaderDll.OBUProg_Write_SysInfo(ref writeSysInfo);

                if (writeResult != (int)OperationResult.oprSuccess)
                {
                    Console.WriteLine("中兴OBU发行器读系统信息失败，返回码：" + writeResult.ToString());
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
                ETCVehicleInfoType etcVehicleInfoType1 = new ETCVehicleInfoType(ptr);

                //读OBU系统信息
                readResult = ZTEOBUReaderDll.OBUProg_Read_ETCVehicleInfo(ref etcVehicleInfoType1);

                if (readResult != (int)OperationResult.oprSuccess)
                {
                    Console.WriteLine("中兴OBU发行器读系统信息失败，返回码：" + readResult.ToString());
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
                writeResult = ZTEOBUReaderDll.OBUProg_Write_ETCVehicleInfo(ref writeETCVehicleInfo);

                if (writeResult != (int)OperationResult.oprSuccess)
                {
                    Console.WriteLine("中兴OBU发行器读系统信息失败，返回码：" + writeResult.ToString());
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

        #endregion

        #region ZTE透传模式接口

        public bool RSU_Open(int mode, string dev, int port,ref long fd, ref string errInfo)
        {
            bool operationOk = false;

            try
            {
                fd = ZTEOBUReaderDll.RSU_Open(mode, dev, port);

                if (fd <= (int)OperationResult.oprSuccess)
                {
                    errInfo = "中兴OBU发行器读系统信息失败，返回码：" + fd.ToString();
                    throw new Exception(errInfo);
                }
                operationOk = true;
            }
            catch (Exception err)
            {
                Log.WriteLog(errInfo + err.Message);
                operationOk = false;
            }
            return operationOk;
        }

        /// <summary>
        /// 设备关闭
        /// </summary>
        /// <param name="fd"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public bool RSU_Close(long fd,ref string errInfo)
        {
            bool operationOk = false;
            int operationResult = (int)OperationResult.oprFault;
            try
            {
                operationResult = ZTEOBUReaderDll.RSU_Close(fd);
                if (operationResult < (int)OperationResult.oprSuccess)
                {
                    errInfo = "中兴发行器 关闭设备失败,返回码：" + operationResult.ToString();
                    throw new Exception(errInfo);
                }
                operationOk = true;
            }
            catch (Exception err)
            {
                Log.WriteLog(errInfo);
                operationOk = false;
            }

            return operationOk;
        }



        /// <summary>
        /// RSU初始化
        /// </summary>
        /// <param name="fd">设备句柄</param>
        /// <param name="errInfo">错误信息</param>
        /// <returns></returns>
        public unsafe bool RSU_Init_rq(long fd, ref string errInfo)
        {
            bool operationOk = false;
            int operationResult = (int)OperationResult.oprFault;
            TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            string unixTime = ((int)timeSpan.TotalSeconds).ToString();
            string unixTimeTest = "10240000000";

            try
            {
               // byte[] unixByte = System.Text.Encoding.Default.GetBytes(unixTime);

                int BstInterVal = 5;
                int txPower = 5;
                int channelID = 1;
                //try
                //{
                operationResult = ZTEOBUReaderDll.RSU_Init_rq(fd, unixTimeTest, BstInterVal, txPower, channelID, timeOut);
                if (operationResult < (int)OperationResult.oprSuccess)
                {
                    errInfo = "中兴发行器初始化失败 错误码为" + operationResult.ToString();
                    throw new Exception(errInfo);
                }
                operationOk = true;
            }
            catch (Exception err)
            {
                Log.WriteLog(err.Message);
                operationOk = false;
            }

            return operationOk;
        }


        #endregion















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
