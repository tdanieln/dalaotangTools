using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mytools
{
    class MACCompute
    {
        /// <summary>
        /// 北京 消费主密钥:
        /// </summary>
        public static int DPK1_KEY = 2;
        /// <summary>
        /// 北京 卡片维护主密钥:
        /// </summary>
        /// <remarks>线路保护方式更新MF下文件</remarks>
        public static int DAMK_MF_KEY = 8;
        /// <summary>
        /// 应用维护主密钥:
        /// </summary>
        /// <remarks>线路保护方式更新DF01下文件</remarks>
        public static int DAMK_DF01_KEY = 10;
        /// <summary>
        /// 圈存主密钥:
        /// </summary>
        public static int DLK1_KEY = 11;
        /// <summary>
        /// 应用PIN重装主密钥:
        /// </summary>
        /// <remarks>用于重装DF01应用下的PIN</remarks>
        public static int DRPK_DF01_KEY = 14;
        /// <summary>
        /// 内部TAC主密钥:
        /// </summary>
        /// <remarks>圈存消费计算TAC码</remarks>
        public static int DTK_KEY = 15;
        /// <summary>
        /// 卡片内部认证密钥 未确定
        /// </summary>
        /// <remarks>卡片内部认证</remarks>
        public static int IK_DF01_KEY = 2;


        /// <summary>
        /// 构造函数
        /// </summary>
        public MACCompute()
        {
            //如果是辽宁加密机，加密机密钥索引使用辽宁的
            if (MytoolsIniConstant.EncryptionType == (int)EncryptionDllType.EncryptionMachineLN)
            {
                // 辽宁省消费主密钥1:1
                DPK1_KEY = 1;
                // 辽宁省卡片维护主密钥:5
                DAMK_MF_KEY = 5;
                // 辽宁省应用维护主密钥:7
                DAMK_DF01_KEY = 7;
                // 辽宁省圈存主密钥1:8
                DLK1_KEY = 8;
                // 辽宁省应用PIN重装主密钥:11
                DRPK_DF01_KEY = 11;
                // 辽宁省内部TAC主密钥:3
                DTK_KEY = 3;
                // 辽宁省卡片内部认证:2
                IK_DF01_KEY = 2;
            }
            else
            {
                /// 北京市消费主密钥1:2
                DPK1_KEY = 2;
                /// 北京市卡片维护主密钥:8
                DAMK_MF_KEY = 8;
                /// 北京市应用维护主密钥:10
                DAMK_DF01_KEY = 10;
                /// 北京市圈存主密钥1:11
                DLK1_KEY = 11;
                /// 北京市应用PIN重装主密钥:14
                DRPK_DF01_KEY = 14;
                /// 北京市内部TAC主密钥:15
                DTK_KEY = 15;
                /// 卡片内部认证密钥 未确定
                IK_DF01_KEY = 23;
            }
        }

        /// <summary>
        /// 生成消费MAC
        /// </summary>
        /// <param name="Key_Index">密钥索引</param>
        /// <param name="div_flag">分散次数</param>
        /// <param name="div_factor">分散因子</param>
        /// <param name="sessionKey">过程密钥</param>
        /// <param name="MAC1_Data_Len">生成的MAC长度</param>
        /// <param name="MAC1_Data">生成MAC所需要的数据</param>
        /// <param name="MAC1">生成的MAC码</param>
        /// <param name="returnCode">返回值</param>
        public bool MAC_Generate(int Key_Index,			        
                                        int div_flag,                      
                                        byte[] div_factor,
                                        byte[] sessionKey,
                                        int MAC1_Data_Len,
                                        byte[] MAC1_Data,
                                        byte[] MAC1,
                                        ref byte[] returnCode)
        {
            bool generalResult = false;

            try
            {
                switch (MytoolsIniConstant.EncryptionType)
                {
                        //调用北京软加密机
                    case (int)EncryptionDllType.SoftEncryption:
                        MacAPI.Soft_MAC_Generate(Key_Index, div_flag, div_factor, sessionKey, MAC1_Data.Length, MAC1_Data, MAC1, returnCode);
                        break;
                        //调用北京加密机
                    case (int)EncryptionDllType.EncryptionMachineBJ:
                        MacAPI.MAC_Generate(Key_Index, div_flag, div_factor, sessionKey, MAC1_Data.Length, MAC1_Data, MAC1, returnCode);
                        break;
                        //调用辽宁加密机
                    case (int)EncryptionDllType.EncryptionMachineLN:
                        MacAPI.MAC_Generate(Key_Index, div_flag, div_factor, sessionKey, MAC1_Data.Length, MAC1_Data, MAC1, returnCode);
                        break;
                    default:
                        throw new Exception("调用加密机失败！");
                }
                if (returnCode[0] != 0x00)
                {
                    throw new Exception("加密机计算MAC失败,返回值 0x" + returnCode[0].ToString("X2"));
                }
                generalResult = true;
            }
            catch(Exception err)
            {
                Console.WriteLine("MAC_Generate执行失败" + err.Message);
                generalResult = false;
            }
            return generalResult;
        }


        /// <summary>
        /// MAC验证
        /// </summary>
        /// <param name="Key_Index">密钥索引</param>
        /// <param name="div_flag">分散次数</param>
        /// <param name="div_factor">分散因子</param>
        /// <param name="sessionKey">过程密钥</param>
        /// <param name="MAC1_Data_Len">生成MAC1的长度</param>
        /// <param name="MAC1_Data">生成MAC1所使用的数据</param>
        /// <param name="MAC1">MAC1码</param>
        /// <param name="returnCode">返回值</param>
        public bool MAC_Verify(int Key_Index,
                                int div_flag,
                                byte[] div_factor,
                                byte[] sessionKey,
                                int MAC1_Data_Len,
                                byte[] MAC1_Data,
                                byte[] MAC1,
                                ref byte[] returnCode)
        {
            bool operationResult = false;

            try
            {
                switch (MytoolsIniConstant.EncryptionType)
                {
                    //调用北京软加密机
                    case (int)EncryptionDllType.SoftEncryption:
                        MacAPI.Soft_MAC_Verify(Key_Index, div_flag, div_factor, sessionKey, MAC1_Data.Length, MAC1_Data, MAC1, returnCode);
                        break;
                    //调用北京加密机
                    case (int)EncryptionDllType.EncryptionMachineBJ:
                        MacAPI.MAC_Verify(Key_Index, div_flag, div_factor, sessionKey, MAC1_Data.Length, MAC1_Data, MAC1, returnCode);
                        break;
                    //调用辽宁加密机
                    case (int)EncryptionDllType.EncryptionMachineLN:
                        MacAPI.MAC_Verify(Key_Index, div_flag, div_factor, sessionKey, MAC1_Data.Length, MAC1_Data, MAC1, returnCode);
                        break;
                    default:
                        throw new Exception("调用加密机失败！");
                }
                if (returnCode[0] != 0x00)
                {
                    throw new Exception("加密机计算MAC失败,返回值 0x" + returnCode[0].ToString("X2"));
                }
                operationResult = true;
            }
            catch(Exception err)
            {
                Console.WriteLine("MAC_Verify执行失败" + err.Message);
                operationResult = false;
            }
            return operationResult;

        }

        /// <summary>
        /// MAC2验证+MAC3生成
        /// </summary>
        /// <param name="Key_Index"></param>
        /// <param name="div_flag"></param>
        /// <param name="div_factor"></param>
        /// <param name="sessionKey"></param>
        /// <param name="MAC1_Data_Len"></param>
        /// <param name="MAC1_Data"></param>
        /// <param name="MAC1"></param>
        /// <param name="MAC2_Data_Len"></param>
        /// <param name="MAC2_Data"></param>
        /// <param name="MAC2"></param>
        /// <param name="returnCode"></param>
        /// <returns></returns>
        public bool MAC_VerifyAndGenerate(int Key_Index,
                                          int div_flag,
                                          byte[] div_factor,
                                          byte[] sessionKey,
                                          int MAC1_Data_Len,
                                          byte[] MAC1_Data,
                                          byte[] MAC1,
                                          int MAC2_Data_Len,
                                          byte[] MAC2_Data,
                                          ref byte[] MAC2,
                                          ref byte[] returnCode)
        {
            bool operationResult = false;
            try
            {
                switch (MytoolsIniConstant.EncryptionType)
                {
                    //调用北京软加密机
                    case (int)EncryptionDllType.SoftEncryption:
                        MacAPI.Soft_MAC_VerifyAndGenerate(Key_Index, div_flag, div_factor, sessionKey, MAC1_Data.Length, MAC1_Data, MAC1,
                            MAC2_Data.Length, MAC2_Data, MAC2,returnCode);
                        break;
                    //调用北京加密机
                    case (int)EncryptionDllType.EncryptionMachineBJ:
                        MacAPI.MAC_VerifyAndGenerate(Key_Index, div_flag, div_factor, sessionKey, MAC1_Data.Length, MAC1_Data, MAC1,
                            MAC2_Data.Length, MAC2_Data, MAC2,returnCode);
                        break;
                    //调用辽宁加密机
                    case (int)EncryptionDllType.EncryptionMachineLN:
                        MacAPI.MAC_VerifyAndGenerate(Key_Index, div_flag, div_factor, sessionKey, MAC1_Data.Length, MAC1_Data, MAC1,
                            MAC2_Data.Length, MAC2_Data, MAC2, returnCode);
                        break;
                    default:
                        throw new Exception("调用加密机失败！");
                }
                if (returnCode[0] != 0x00)
                {
                    throw new Exception("加密机计算MAC失败,返回值 0x" + returnCode[0].ToString("X2"));
                }
                operationResult = true;
            }
            catch(Exception err)
            {
                Console.WriteLine("MAC_Verify执行失败" + err.Message);
                operationResult = false;
            }
            return operationResult;
        }

        /// <summary>
        /// 产生应用MAC
        /// </summary>
        /// <param name="Key_Index">密钥索引</param>
        /// <param name="div_factor">分散因子</param>
        /// <param name="MAC_Data_Len">数据域长度</param>
        /// <param name="MAC_Data">数据域</param>
        /// <param name="MAC">输出参数 MAC码</param>
        /// <param name="ret">输出参数 错误原因</param>
        /// <returns></returns>
        public bool App_MAC_Generate(int Key_Index,
                                    byte[] div_factor,
                                    int MAC_Data_Len,
                                    byte[] MAC_Data,
                                    ref byte[] MAC,
                                    ref byte[] ret)
        {
            bool operationResult = false;
            try
            {
                switch (MytoolsIniConstant.EncryptionType)
                {
                    //调用北京软加密机
                    case (int)EncryptionDllType.SoftEncryption:
                        MacAPI.Soft_App_MAC_Generate(Key_Index, div_factor, MAC_Data.Length, MAC_Data, MAC, ret);
                        break;
                    //调用北京加密机
                    case (int)EncryptionDllType.EncryptionMachineBJ:
                        MacAPI.App_MAC_Generate(Key_Index, div_factor, MAC_Data.Length, MAC_Data, MAC, ret);
                        break;
                    //调用辽宁加密机
                    case (int)EncryptionDllType.EncryptionMachineLN:
                        MacAPI.App_MAC_Generate(Key_Index, div_factor, MAC_Data.Length, MAC_Data, MAC, ret);
                        break;
                    default:
                        throw new Exception("调用加密机失败！");
                }
                if (ret[0] != 0x00)
                {
                    throw new Exception("加密机计算MAC失败,返回值 0x" + ret[0].ToString("X2"));
                }
                operationResult = true;
            }
            catch (Exception err)
            {
                Log.WriteLog("App_MAC_Generate执行失败" + err.Message);
                operationResult = false;
            }
            return operationResult;
        }



    }
}
