using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mytools
{
    class CallBeiYangPrinter
    {
        int BYPrinterResult = -1;
        const int size = 26;

        private enum PrintBYRetCode : int
        {
            /// <summary>
            /// 执行成功
            /// </summary>
            OK = 1000,
            /// <summary>
            /// 通讯失败
            /// </summary>
            ComErr = 1001,
            /// <summary>
            /// 参数错误
            /// </summary>
            ParaErr = 1002,
            /// <summary>
            /// 文件打开错误
            /// </summary>
            FileOpenErr = 1003,
            /// <summary>
            /// 文件读取错误
            /// </summary>
            FileReadErr = 1004,
            /// <summary>
            /// 文件写错误
            /// </summary>
            FileWriteErr = 1005,
            /// <summary>
            /// 文件不符合要求
            /// </summary>
            FileErr = 1006,
            /// <summary>
            /// 数据量过大
            /// </summary>
            NumberOver = 1007,
            /// <summary>
            /// 图片类型非法
            /// </summary>
            ImageTypeErr = 1008,
            /// <summary>
            /// 驱动错误
            /// </summary>
            DriverErr = 1009,
            /// <summary>
            /// 超时错误
            /// </summary>
            TimeoutErr = 1010,
            /// <summary>
            /// 动态库加载错误
            /// </summary>
            LoadDllErr = 1011,
            /// <summary>
            /// 加载功能模块错误
            /// </summary>
            LoadFuncErr = 1012,
            /// <summary>
            /// 端口未打开
            /// </summary>
            NoOpenErr = 1013
        }



        /// <summary>
        /// 打开北洋打印机
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="devNo"></param>
        /// <param name="port"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private bool OpenBYPrinter(int mode, string devNo,int port ,ref string errInfo)
        {
            string moduleName = "北洋打印机打开设备模块  ";
            //int BYPrinterResult = -1;
            bool operateResult = false;

            try
            {
                //用USB方式打开设备
                if (mode == (int)DevConnMode.USB)
                {
                    this.BYPrinterResult = PrinterBeiYangDll.OpenCommWithUSB();

                    if (this.BYPrinterResult != 1000)
                    {
                        errInfo = moduleName + "使用USB方式打开设备失败";
                        Log.WriteLog(errInfo);
                        throw new Exception(errInfo);
                    }
                }
                else if (mode == (int)DevConnMode.COM)
                {
                    this.BYPrinterResult = PrinterBeiYangDll.OpenCommWithCom("COM1",9600,2);
                    if (this.BYPrinterResult != 1000)
                    {
                        errInfo = moduleName + "使用COM方式打开设备失败";
                        Log.WriteLog(errInfo);
                        throw new Exception(errInfo);
                    }
                }
                else
                    throw new Exception("打开北洋打印机失败");

                operateResult = true;
            }
            catch (Exception err)
            {
                errInfo = errInfo + err.Message;
                operateResult = false;
            }

            return operateResult;
        }

        /// <summary>
        /// 设置打印模式
        /// </summary>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private bool SetPrintMode(ref string errInfo)
        {
            bool returnResult =false;
            try
            {
                //撕离模式 + 非连续纸 + 热敏打印
                this.BYPrinterResult = PrinterBeiYangDll.SetMode(0, 0, 1);

                if (this.BYPrinterResult == (int)PrintBYRetCode.OK)
                {
                    returnResult = true;
                }
            }
            catch (Exception err)
            {
                errInfo = "设置纸张撕离模式失败！";
                Log.WriteLog(errInfo + err.Message);
                returnResult = false;
            }

            return returnResult;
        }

        /// <summary>
        /// 设置介质反射模式
        /// </summary>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private bool SetSensor(ref string errInfo)
        {
            bool returnResult = false;
            try
            {
                //设置介质传感器为黑标纸
                this.BYPrinterResult = PrinterBeiYangDll.SetSensor(0);

                if (this.BYPrinterResult == (int)PrintBYRetCode.OK)
                {
                    returnResult = true;
                }
            }
            catch (Exception err)
            {
                errInfo = "设置纸张撕离模式失败！";
                Log.WriteLog(errInfo + err.Message);
                returnResult = false;
            }

            return returnResult;
        }


        /// <summary>
        /// 设置翻转模式
        /// </summary>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private bool SetReversalMode(ref string errInfo)
        {
            bool returnResult = false;

            try
            {
                this.BYPrinterResult = PrinterBeiYangDll.SetReversalMode(1);
                if (this.BYPrinterResult == (int)PrintBYRetCode.OK)
                {
                    returnResult = true;
                }
            }
            catch (Exception err)
            {
                errInfo = "设置翻转模式失败！";
                Log.WriteLog(errInfo + err.Message);
                returnResult = false;
            }

            return returnResult;
        }

        /// <summary>
        /// 设置连续纸长度
        /// </summary>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private bool SetPaperLength(ref string errInfo)
        {

            bool returnResult = false;
            try
            {
                this.BYPrinterResult = PrinterBeiYangDll.SetPaperLength(505, 0);
                if (this.BYPrinterResult == (int)PrintBYRetCode.OK)
                {
                    returnResult = true;
                }
            }
            catch (Exception err)
            {
                errInfo = "设置连续纸长度失败！";
                Log.WriteLog(errInfo + err.Message);
                returnResult = false;
            }
            return returnResult;
        }

        /// <summary>
        /// 设置停止位置
        /// </summary>
        /// <param name="position"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private bool SetEnd(ref string errInfo)
        {
            bool returnResult = false;
            try
            {
                this.BYPrinterResult = PrinterBeiYangDll.SetEnd(112);
                if (this.BYPrinterResult == (int)PrintBYRetCode.OK)
                {
                    returnResult = true;
                }
            }
            catch (Exception err)
            {
                errInfo = "设置连续纸长度失败！";
                Log.WriteLog(errInfo + err.Message);
                returnResult = false;
            }
            return returnResult;
        }

        /// <summary>
        /// 票面排版
        /// </summary>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private bool SetArea(ref string errInfo)
        {
            bool returnResult = false;
            try
            {
                this.BYPrinterResult = PrinterBeiYangDll.SetArea(0, 900, 0, 0, 0, 0, 0, 0);
                if (this.BYPrinterResult == (int)PrintBYRetCode.OK)
                {
                    returnResult = true;
                }
            }
            catch (Exception err)
            {
                errInfo = "票面排版失败！";
                Log.WriteLog(errInfo + err.Message);
                returnResult = false;
            }
            return returnResult;
        }

        /// <summary>
        /// 设置打印内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="size"></param>
        /// <param name="infoErr"></param>
        /// <returns></returns>
        public bool SetPrintInfo(string text, int x, int y, ref string errInfo)
        {
            bool returnResult = false;
            try
            {
                this.BYPrinterResult = PrinterBeiYangDll.SetPrintInfo(text, x, y, "黑体", size, 0);
                if (this.BYPrinterResult == (int)PrintBYRetCode.OK)
                {
                    returnResult = true;
                }
            }
            catch (Exception err)
            {
                errInfo = "设置打印内容失败！";
                Log.WriteLog(errInfo + err.Message);
                returnResult = false;
            }
            return returnResult;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public bool Print(ref string errInfo)
        {
            bool returnResult = false;
            try
            {
                this.BYPrinterResult = PrinterBeiYangDll.Print(1, 1, 1);
                if (this.BYPrinterResult == (int)PrintBYRetCode.OK)
                {
                    returnResult = true;
                }
            }
            catch(Exception err)
            {
                errInfo = "打印动作执行失败！";
                Log.WriteLog(errInfo + err.Message);
                returnResult = false;
            }
            return returnResult;
        }

        /// <summary>
        /// 状态检查
        /// </summary>
        /// <returns></returns>
        public bool CheckStatus(ref string errInfo)
        {
            bool returnResult = false;
            byte[] paperStatus = new byte[3], ribbionStatus = new byte[3], busyStatus = new byte[3], pauseStatus = new byte[3], comStatus = new byte[3], headHeatStatus = new byte[3], headOverStatus = new byte[3], cutStatus = new byte[3];
            try
            {
                this.BYPrinterResult = PrinterBeiYangDll.CheckStatus(paperStatus, ribbionStatus, busyStatus, pauseStatus, comStatus
                                                                   , headHeatStatus, headOverStatus, cutStatus);
                if (this.BYPrinterResult == (int)PrintBYRetCode.OK)
                {
                    returnResult = true;
                }
                else
                {
                    if (paperStatus[0] != 'N')
                        errInfo += "缺纸";
                    if (ribbionStatus[0] != 'N')
                        errInfo += "缺色带";
                    if (busyStatus[0] != 'N')
                        errInfo += "解释器忙";
                    if (pauseStatus[0] != 'N')
                        errInfo += "暂停";
                    if (comStatus[0] != 'N')
                        errInfo += "串口通信错误";
                    if (headHeatStatus[0] != 'N')
                        errInfo += "打印头抬起";
                    if (headOverStatus[0] != 'N')
                        errInfo += "打印头过热";
                    if (cutStatus[0] != 'N')
                        errInfo += "切刀响应超时";
                }
            }
            catch (Exception err)
            {
                Log.WriteLog(errInfo + err.Message);
                returnResult = false;
            }

            return returnResult;
        }
            

        /// <summary>
        /// 初始化设备
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="devNo"></param>
        /// <param name="port"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public bool InitialDev(int mode,string devNo,int port,ref string errInfo)
        {
            bool returnResult = false;

            try
            {
                //打开打印机
                if (!OpenBYPrinter(mode, devNo, port, ref errInfo))
                {
                    returnResult = false;
                    throw new Exception(errInfo);
                }

                //设置打印模式
                if (!SetPrintMode(ref errInfo))
                {
                    returnResult = false;
                    throw new Exception(errInfo);
                }

                //设置反射模式
                if (!SetSensor(ref errInfo))
                {
                    returnResult = false;
                    throw new Exception(errInfo);
                }

                ////设置停止位置
                //if (!SetEnd(ref errInfo))
                //{
                //    returnResult = false;
                //    throw new Exception(errInfo);
                //}

                //设置翻转模式
                if (!SetReversalMode(ref errInfo))
                {
                    returnResult = false;
                    throw new Exception(errInfo);
                }

                //设置纸张区域
                if (!SetArea(ref errInfo))
                {
                    returnResult = false;
                    throw new Exception(errInfo);
                }

                returnResult = true;
            }
            catch (Exception err)
            {
                Log.WriteLog(err.Message);
                returnResult = false;
            }

            return returnResult;
        }



    }
}
