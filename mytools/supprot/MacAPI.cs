

/**
 * 加密机接口调用封装类
 */





//空间引用
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace mytools
{
    class MacAPI
    {
        /// <summary>
        /// 软加密机动态库地址
        /// </summary>
         public const string SOFTENCRYPTION_DLL_FILE_PATH= "./dll/CMac_soft.dll";

        /// <summary>
        /// 加密机动态库地址
        /// </summary>
         public const string ENCRYPTION_DLL_FILE_PATH = "./dll/CMac.dll";

        /* 功能：验证MAC1,并生成MAC2。
                加密机处理流程：
                1、	根据密钥索引号唯一确定该交易主密钥;
                2、	根据分散因子对交易主密钥进行分散（支持1-3次）,得到交易子密钥; 
                3、	根据交易子密钥和过程密钥种子计算得到本次交易过程密钥;
                4、	用过程密钥对MAC1进行验证,如果MAC1无效,则返回相应的错误代码;如果有效则进入步骤5;
                5、	用过程密钥计算MAC2。
         *  参数说明:
            参数名称	    参数含义
            Key_Index	    用于产生过程密钥的主密钥的索引号
            div_flag	    分散次数（1-3）
            div_factor	    分散因子（分散次数 * 8个字节）
            SessionKey_Seed	过程密钥因子(8个字节)
            MAC1_Data_Len	被验证MAC的数据长度
            MAC1_Data	    被验证MAC的数据
            MAC1	        被验证的MAC(4个字节)
            MAC2_Data_Len	需要生成MAC的数据长度
            MAC2_Data	    需要生成MAC的数据
            MAC2	        生成的MAC
            Return_code	    返回码
         */

        /// <summary>
        /// 软加密机 验证MAC1,并生成MAC2
        /// </summary>
        /// <param name="Key_Index">用于产生过程密钥的主密钥的索引号</param>
        /// <param name="div_flag">分散次数（1-3</param>
        /// <param name="div_factor">分散因子（分散次数 * 8个字节</param>
        /// <param name="SessionKey_Seed">过程密钥因子(8个字节)</param>
        /// <param name="MAC1_Data_Len">被验证MAC的数据长度</param>
        /// <param name="MAC1_Data">被验证MAC的数据</param>
        /// <param name="MAC1">被验证的MAC(4个字节)</param>
        /// <param name="MAC2_Data_Len">需要生成MAC的数据长度</param>
        /// <param name="MAC2_Data">需要生成MAC的数据</param>
        /// <param name="MAC2">生成的MAC</param>
        /// <param name="ret">返回码</param>
        [DllImport(SOFTENCRYPTION_DLL_FILE_PATH, EntryPoint = "MAC_VerifyAndGenerate")]
         public static extern void Soft_MAC_VerifyAndGenerate(
                int Key_Index,			        //输入参数
                int div_flag,                  //输入参数   
                byte[] div_factor,		        //输入参数
                byte[] SessionKey_Seed,	        //输入参数
                int MAC1_Data_Len,		        //输入参数
                byte[] MAC1_Data,		        //输入参数
                byte[] MAC1,			        //输入参数
                int MAC2_Data_Len,		        //输入参数
                byte[] MAC2_Data,		        //输入参数
                byte[] MAC2,		            //输出参数
                byte[] ret);	    	        //输出参数

        /// <summary>
        /// 加密机 验证MAC1,并生成MAC2
        /// </summary>
        /// <param name="Key_Index">用于产生过程密钥的主密钥的索引号</param>
        /// <param name="div_flag">分散次数（1-3</param>
        /// <param name="div_factor">分散因子（分散次数 * 8个字节</param>
        /// <param name="SessionKey_Seed">过程密钥因子(8个字节)</param>
        /// <param name="MAC1_Data_Len">被验证MAC的数据长度</param>
        /// <param name="MAC1_Data">被验证MAC的数据</param>
        /// <param name="MAC1">被验证的MAC(4个字节)</param>
        /// <param name="MAC2_Data_Len">需要生成MAC的数据长度</param>
        /// <param name="MAC2_Data">需要生成MAC的数据</param>
        /// <param name="MAC2">生成的MAC</param>
        /// <param name="ret">返回码</param>
        [DllImport(ENCRYPTION_DLL_FILE_PATH)]
        public static extern void MAC_VerifyAndGenerate(
                int Key_Index,			        //输入参数
                int div_flag,                  //输入参数   
                byte[] div_factor,		        //输入参数
                byte[] SessionKey_Seed,	        //输入参数
                int MAC1_Data_Len,		        //输入参数
                byte[] MAC1_Data,		        //输入参数
                byte[] MAC1,			        //输入参数
                int MAC2_Data_Len,		        //输入参数
                byte[] MAC2_Data,		        //输入参数
                byte[] MAC2,		            //输出参数
                byte[] ret);	    	        //输出参数
        /*
         * 功能：  验证交易TAC。
            加密机处理流程：
            1、	根据密钥索引号唯一确定该交易主密钥;
            2、	根据分散因子对交易主密钥进行分散,得到交易子密钥; 
            3、	TAC计算不采用过程密钥方式,它用交易子密钥左右8位字节异或运算的结果对ＴＡＣ数据进行验证,返回验证结果; 
         * 
         *  参数说明:
            参数名称	    参数含义
            Key_Index	    用于产生分散密钥的主密钥的索引号
            div_flag	    分散次数（1-3）
            div_factor	    分散因子（分散次数 * 8个字节）
            TAC_Data_Len	被验证TAC的数据的长度
            TAC_Data	    被验证TAC的数据
            TAC	            被验证的TAC
            Return_code	    返回码
         */

        /// <summary>
        /// 软加密机 验证交易TAC
        /// </summary>
        /// <param name="Key_Index">参数含义</param>
        /// <param name="div_flag">用于产生分散密钥的主密钥的索引号</param>
        /// <param name="div_factor">分散次数（1-3）</param>
        /// <param name="TAC_Data_Len">分散因子（分散次数 * 8个字节）</param>
        /// <param name="TAC_Data">被验证TAC的数据的长度</param>
        /// <param name="TAC">被验证TAC的数据</param>
        /// <param name="ret">返回码</param>
        [DllImport(SOFTENCRYPTION_DLL_FILE_PATH, EntryPoint = "TAC_Verify")]
        public static extern void Soft_TAC_Verify(
                    int Key_Index,		            //输入参数
                    int div_flag,                  //输入参数   
                    byte[] div_factor,	            //输入参数
                    int TAC_Data_Len,		        //输入参数
                    byte[] TAC_Data,	            //输入参数
                    byte[] TAC,		                //输入参数
                    byte[] ret);	            //输出参数

        /// <summary>
        /// 加密机 验证交易TAC
        /// </summary>
        /// <param name="Key_Index">参数含义</param>
        /// <param name="div_flag">用于产生分散密钥的主密钥的索引号</param>
        /// <param name="div_factor">分散次数（1-3）</param>
        /// <param name="TAC_Data_Len">分散因子（分散次数 * 8个字节）</param>
        /// <param name="TAC_Data">被验证TAC的数据的长度</param>
        /// <param name="TAC">被验证TAC的数据</param>
        /// <param name="ret">返回码</param>
        [DllImport(ENCRYPTION_DLL_FILE_PATH)]
        public static extern void TAC_Verify(
                    int Key_Index,		            //输入参数
                    int div_flag,                  //输入参数   
                    byte[] div_factor,	            //输入参数
                    int TAC_Data_Len,		        //输入参数
                    byte[] TAC_Data,	            //输入参数
                    byte[] TAC,		                //输入参数
                    byte[] ret);	            //输出参数

        /* 功能：生成MAC1。
               加密机处理流程：
               1、	根据密钥索引号唯一确定该交易主密钥;
               2、	根据分散因子对交易主密钥进行分散（支持1-3次）,得到交易子密钥; 
               3、	根据交易子密钥和过程密钥种子计算得到本次交易过程密钥;
               4、	用过程密钥计算MAC1。
        *  参数说明:
           参数名称	    参数含义
           Key_Index	    用于产生过程密钥的主密钥的索引号
           div_flag	    分散次数（1-3）
           div_factor	    分散因子（分散次数 * 8个字节）
           SessionKey_Seed	过程密钥因子(8个字节)
           MAC1_Data_Len	需要生成MAC的数据长度
           MAC1_Data	    需要生成MAC的数据
           MAC1	            MAC1(4个字节)
           Return_code	    返回码
        */

        /// <summary>
        /// 软加密机生成消费MAC
        /// </summary>
        /// <param name="Key_Index">用于产生过程密钥的主密钥的索引号</param>
        /// <param name="div_flag">分散次数（1-3）</param>
        /// <param name="div_factor">分散因子（分散次数 * 8个字节）</param>
        /// <param name="SessionKey_Seed">过程密钥因子(8个字节)</param>
        /// <param name="MAC1_Data_Len">需要生成MAC的数据长度</param>
        /// <param name="MAC1_Data">需要生成MAC的数据</param>
        /// <param name="MAC1"> MAC1(4个字节)</param>
        /// <param name="ret">返回码</param>
        /// <remarks>           Key_Index	    用于产生过程密钥的主密钥的索引号</remarks>
        /// <remarks>   div_flag	    分散次数（1-3）</remarks>
        /// <remarks>   div_factor	    分散因子（分散次数 * 8个字节）</remarks>
        /// <remarks>   SessionKey_Seed	过程密钥因子(8个字节)</remarks>
        /// <remarks>   MAC1_Data_Len	需要生成MAC的数据长度</remarks>
        /// <remarks>   MAC1_Data	    需要生成MAC的数据</remarks>
        /// <remarks>   MAC1	            MAC1(4个字节)</remarks>
        /// <remarks>  Return_code	    返回码</remarks>
        [DllImport(SOFTENCRYPTION_DLL_FILE_PATH,EntryPoint = "MAC_Generate")]
        public static extern void Soft_MAC_Generate(
                   int Key_Index,			        //输入参数
                   int div_flag,                   //输入参数   
                   byte[] div_factor,		        //输入参数
                   byte[] SessionKey_Seed,	        //输入参数
                   int MAC1_Data_Len,		        //输入参数
                   byte[] MAC1_Data,		        //输入参数
                   byte[] MAC1,			            //输入参数
                   byte[] ret);		                //输出参数

        /// <summary>
        /// 加密机生成消费MAC
        /// </summary>
        /// <param name="Key_Index">用于产生过程密钥的主密钥的索引号</param>
        /// <param name="div_flag">分散次数（1-3）</param>
        /// <param name="div_factor">分散因子（分散次数 * 8个字节）</param>
        /// <param name="SessionKey_Seed">过程密钥因子(8个字节)</param>
        /// <param name="MAC1_Data_Len">需要生成MAC的数据长度</param>
        /// <param name="MAC1_Data">需要生成MAC的数据</param>
        /// <param name="MAC1"> MAC1(4个字节)</param>
        /// <param name="ret">返回码</param>
        [DllImport(ENCRYPTION_DLL_FILE_PATH)]
        public static extern void MAC_Generate(
                   int Key_Index,			        //输入参数
                   int div_flag,                   //输入参数   
                   byte[] div_factor,		        //输入参数
                   byte[] SessionKey_Seed,	        //输入参数
                   int MAC1_Data_Len,		        //输入参数
                   byte[] MAC1_Data,		        //输入参数
                   byte[] MAC1,			            //输入参数
                   byte[] ret);		                //输出参数
        /*
          * 功能： MAC3验证。
             加密机处理流程：
             1、	根据密钥索引号唯一确定该交易主密钥;
             2、	根据分散因子对交易主密钥进行分散（支持1-3次）,得到交易子密钥;  
             3、	根据交易子密钥和过程密钥种子计算得到本次交易过程密钥;
             4、	用过程密钥对输入MAC进行验证;
          *  参数说明:
             参数名称	    参数含义
             Key_Index	    用于产生过程密钥的主密钥的索引号
             div_flag	    分散次数（1-3）
             div_factor	    分散因子（分散次数 * 8个字节）
             SessionKey_Seed	过程密钥因子(8个字节)
             MAC_Data_Len	被验证MAC的数据长度
             MAC_Data	    被验证MAC的数据
             MAC	            被验证的MAC
             Return_code	    返回码

      */
        /// <summary>
        /// 软加密机 MAC3验证
        /// </summary>
        /// <param name="Key_Index">用于产生过程密钥的主密钥的索引号</param>
        /// <param name="div_flag">分散次数（1-3）</param>
        /// <param name="div_factor">分散因子（分散次数 * 8个字节）</param>
        /// <param name="SessionKey_Seed">过程密钥因子(8个字节)</param>
        /// <param name="MAC_Data_Len">被验证MAC的数据长度</param>
        /// <param name="MAC_Data">被验证MAC的数据</param>
        /// <param name="MAC">被验证的MAC</param>
        /// <param name="ret">返回码</param>
        [DllImport(SOFTENCRYPTION_DLL_FILE_PATH, EntryPoint = "MAC_Verify")]
        public static extern void Soft_MAC_Verify(
                    int Key_Index,			        //输入参数
                    int div_flag,                   //输入参数   
                    byte[] div_factor,		        //输入参数
                    byte[] SessionKey_Seed,	        //输入参数
                    int MAC_Data_Len,			    //输入参数
                    byte[] MAC_Data,		        //输入参数
                    byte[] MAC,			            //输入参数
                    byte[] ret);                   //输出参数

        /// <summary>
        /// 加密机 MAC3验证
        /// </summary>
        /// <param name="Key_Index">用于产生过程密钥的主密钥的索引号</param>
        /// <param name="div_flag">分散次数（1-3）</param>
        /// <param name="div_factor">分散因子（分散次数 * 8个字节）</param>
        /// <param name="SessionKey_Seed">过程密钥因子(8个字节)</param>
        /// <param name="MAC_Data_Len">被验证MAC的数据长度</param>
        /// <param name="MAC_Data">被验证MAC的数据</param>
        /// <param name="MAC">被验证的MAC</param>
        /// <param name="ret">返回码</param>
        [DllImport(ENCRYPTION_DLL_FILE_PATH)]
        public static extern void MAC_Verify(
                    int Key_Index,			        //输入参数
                    int div_flag,                   //输入参数   
                    byte[] div_factor,		        //输入参数
                    byte[] SessionKey_Seed,	        //输入参数
                    int MAC_Data_Len,			    //输入参数
                    byte[] MAC_Data,		        //输入参数
                    byte[] MAC,			            //输入参数
                    byte[] ret);                   //输出参数


         /// <summary>
         /// 软加密机 生成TAC
         /// </summary>
         /// <param name="Key_Index">用于产生过程密钥的主密钥的索引号</param>
         /// <param name="div_flag">分散次数（1-3）</param>
         /// <param name="div_factor">分散因子（分散次数 * 8个字节）</param>
         /// <param name="TAC_Data_Len">生成的TAC的数据长度</param>
         /// <param name="TAC_Data">产生TAC用的数据</param>
         /// <param name="TAC">TAC码</param>
         /// <param name="ret">返回码</param>
        [DllImport(SOFTENCRYPTION_DLL_FILE_PATH, EntryPoint = "TAC_Generate")]
        public static extern void Soft_TAC_Generate(
                    int Key_Index,			        //输入参数
                    int div_flag,                   //输入参数   
                    byte[] div_factor,		        //输入参数     
                    int TAC_Data_Len,			    //输入参数
                    byte[] TAC_Data,		        //输入参数
                    byte[] TAC,			            //输入参数
                    byte[] ret);                   //输出参数

        /// <summary>
        /// 加密机 生成TAC
        /// </summary>
        /// <param name="Key_Index">用于产生过程密钥的主密钥的索引号</param>
        /// <param name="div_flag">分散次数（1-3）</param>
        /// <param name="div_factor">分散因子（分散次数 * 8个字节）</param>
        /// <param name="TAC_Data_Len">生成的TAC的数据长度</param>
        /// <param name="TAC_Data">产生TAC用的数据</param>
        /// <param name="TAC">TAC码</param>
        /// <param name="ret">返回码</param>
        [DllImport(ENCRYPTION_DLL_FILE_PATH)]
        public static extern void TAC_Generate(
                    int Key_Index,			        //输入参数
                    int div_flag,                   //输入参数   
                    byte[] div_factor,		        //输入参数     
                    int TAC_Data_Len,			    //输入参数
                    byte[] TAC_Data,		        //输入参数
                    byte[] TAC,			            //输入参数
                    byte[] ret);                   //输出参数
          //TAC_Generate(keyindex,divtime,CardId,datalen,data,tac,return_code);

        /* 功能：  应用锁定,应用解锁,卡片锁定、更新二进制文件、PIN重装、PIN解锁等应用维护命令的MAC产生。	
           加密机处理流程：
           1、	根据密钥索引号唯一确定该应用主密钥;
           2、	对应用主密钥进行一次分散,得到应用子密钥; 
           3、	用应用子密钥对数据算MAC,并返回计算结果;
        * 
        * 参数说明:
           参数名称	    参数含义
           Key_Index	    用于产生分散密钥的应用主密钥的索引号
           div_factor	    分散因子（8个字节）
           MAC_Data_Len	产生MAC的数据的长度
           MAC_Data	    产生MAC的数据
           MAC	            产生的MAC
           Return_code	    返回码
        * 
        */


        /// <summary>
        /// 软加密机 应用MAC生成
        /// </summary>
        /// <param name="Key_Index"> 用于产生分散密钥的应用主密钥的索引号</param>
        /// <param name="div_factor">分散因子（8个字节）</param>
        /// <param name="MAC_Data_Len">产生MAC的数据的长度</param>
        /// <param name="MAC_Data">产生MAC的数据</param>
        /// <param name="MAC">产生的MAC</param>
        /// <param name="ret">返回码</param>
        [DllImport(SOFTENCRYPTION_DLL_FILE_PATH, EntryPoint = "App_MAC_Generate")]
        public static extern void Soft_App_MAC_Generate(
                    int Key_Index,			            //输入参数
                    byte[] div_factor,			        //输入参数
                    int MAC_Data_Len,				    //输入参数
                    byte[] MAC_Data,			        //输入参数
                    byte[] MAC,				            //输入参数
                    byte[] ret);				        //输出参数

        /// <summary>
        /// 加密机 应用MAC生成
        /// </summary>
        /// <param name="Key_Index"> 用于产生分散密钥的应用主密钥的索引号</param>
        /// <param name="div_factor">分散因子（8个字节）</param>
        /// <param name="MAC_Data_Len">产生MAC的数据的长度</param>
        /// <param name="MAC_Data">产生MAC的数据</param>
        /// <param name="MAC">产生的MAC</param>
        /// <param name="ret">返回码</param>
        [DllImport(ENCRYPTION_DLL_FILE_PATH)]
        public static extern void App_MAC_Generate(
                    int Key_Index,			            //输入参数
                    byte[] div_factor,			        //输入参数
                    int MAC_Data_Len,				    //输入参数
                    byte[] MAC_Data,			        //输入参数
                    byte[] MAC,				            //输入参数
                    byte[] ret);				        //输出参数

        /*功能：对应用维护命令的MAC进行校验。
            加密机处理流程：
            1、	根据密钥索引号唯一确定该应用主密钥；
            2、	对应用主密钥进行一次分散,得到应用子密钥； 
            3、	用应用子密钥对数据校验MAC,并返回校验结果；
         * 
         * 参数说明:
            参数名称	    参数含义
            Key_Index	    用于产生分散密钥的应用主密钥的索引号
            div_factor	    分散因子（8个字节）
            MAC_Data_Len	被验证MAC的数据长度
            MAC_Data	    被验证MAC的数据
            MAC	            被验证的MAC
            Return_code	    返回码

         * 
         */


        /// <summary>
        /// 软加密机 对应用维护命令的MAC进行校验。
        /// </summary>
        /// <param name="Key_Index">用于产生分散密钥的应用主密钥的索引号</param>
        /// <param name="div_factor">分散因子（8个字节）</param>
        /// <param name="MAC_Data_Len">被验证MAC的数据长度</param>
        /// <param name="MAC_Data">被验证MAC的数据</param>
        /// <param name="MAC">被验证的MAC</param>
        /// <param name="ret">返回码</param>
        [DllImport(SOFTENCRYPTION_DLL_FILE_PATH, EntryPoint = "App_MAC_Verify")]
        public static extern void Soft_App_MAC_Verify(
                    int Key_Index,			        //输入参数
                    byte[] div_factor,			    //输入参数
                    int MAC_Data_Len,			    //输入参数
                    byte[] MAC_Data,		        //输入参数
                    byte[] MAC,			            //输入参数
                    byte[] ret);                    //输出参数

        /// <summary>
        /// 加密机 对应用维护命令的MAC进行校验。
        /// </summary>
        /// <param name="Key_Index">用于产生分散密钥的应用主密钥的索引号</param>
        /// <param name="div_factor">分散因子（8个字节）</param>
        /// <param name="MAC_Data_Len">被验证MAC的数据长度</param>
        /// <param name="MAC_Data">被验证MAC的数据</param>
        /// <param name="MAC">被验证的MAC</param>
        /// <param name="ret">返回码</param>
        [DllImport(ENCRYPTION_DLL_FILE_PATH)]
        public static extern void App_MAC_Verify(
                    int Key_Index,			        //输入参数
                    byte[] div_factor,			    //输入参数
                    int MAC_Data_Len,			    //输入参数
                    byte[] MAC_Data,		        //输入参数
                    byte[] MAC,			            //输入参数
                    byte[] ret);                    //输出参数


        /*功能：	数据加密
            加密机处理流程：
            1、	根据密钥索引号唯一确定加密主密钥；
            2、	根据分散因子对主密钥进行分散,得到加密子密钥； 
            3、	直接用子密钥对数据加密,返回计算结果；

         * 
         * 参数说明:
            参数名称	        参数含义
            Key_Index	        用于产生分散密钥的主密钥的索引号
            div_factor	        分散因子（8个字节）
            Plain_Data_Len	    明文数据的长度
            Plain_Data	        明文数据（明文数据格式为 长度+数据）
            Encrypted_Data_Len	产生的密文的长度
         *  Encrypted_Data	    产生的密文
            Return_code	        返回码


         */

        /// <summary>
        /// 软加密机 数据加密
        /// </summary>
        /// <param name="Key_Index">用于产生分散密钥的主密钥的索引号</param>
        /// <param name="div_factor">分散因子（8个字节）</param>
        /// <param name="Plain_Data_Len">明文数据的长度</param>
        /// <param name="Plain_Data">明文数据（明文数据格式为 长度+数据）</param>
        /// <param name="Encrypted_Data_Len">产生的密文的长度</param>
        /// <param name="Encrypted_Data">产生的密文</param>
        /// <param name="ret">返回码</param>
        [DllImport(SOFTENCRYPTION_DLL_FILE_PATH, EntryPoint = "Data_Encrypt")]
        public static extern void Soft_Data_Encrypt(
                   int Key_Index,			            //输入参数
                   byte[] div_factor,		            //输入参数
                   int Plain_Data_Len,			        //输入参数
                   byte[] Plain_Data,		            //输入参数
                   int Encrypted_Data_Len,		    //输出参数
                   byte[] Encrypted_Data,		        //输出参数
                   byte[] ret);		                    //输出参数

        /// <summary>
        /// 加密机 数据加密
        /// </summary>
        /// <param name="Key_Index">用于产生分散密钥的主密钥的索引号</param>
        /// <param name="div_factor">分散因子（8个字节）</param>
        /// <param name="Plain_Data_Len">明文数据的长度</param>
        /// <param name="Plain_Data">明文数据（明文数据格式为 长度+数据）</param>
        /// <param name="Encrypted_Data_Len">产生的密文的长度</param>
        /// <param name="Encrypted_Data">产生的密文</param>
        /// <param name="ret">返回码</param>
        [DllImport(ENCRYPTION_DLL_FILE_PATH)]
        public static extern void Data_Encrypt(
                   int Key_Index,			            //输入参数
                   byte[] div_factor,		            //输入参数
                   int Plain_Data_Len,			        //输入参数
                   byte[] Plain_Data,		            //输入参数
                   int Encrypted_Data_Len,		    //输出参数
                   byte[] Encrypted_Data,		        //输出参数
                   byte[] ret);		                    //输出参数
        /*功能：    数据解密
            加密机处理流程：
            4、	根据密钥索引号唯一确定解密主密钥；
            5、	根据分散因子对主密钥进行分散,得到解密子密钥； 
            6、	直接用子密钥对数据解密,返回计算结果；

         * 参数说明:
            参数名称	        参数含义
            Key_Index	        用于产生分散密钥的主密钥的索引号
            div_factor	        分散因子（8个字节）
            Encrypted_Data_Len	密文数据的长度
            Encrypted_Data	    密文
            Plain_Data_Len	    解密后的明文数据的长度
            Plain_Data	        解密后的明文数据
            Return_code	        返回码

         */

        /// <summary>
        /// 软加密机 数据解密
        /// </summary>
        /// <param name="Key_Index">用于产生分散密钥的主密钥的索引号</param>
        /// <param name="div_factor"> 分散因子（8个字节）</param>
        /// <param name="Encrypted_Data_Len">密文数据的长度</param>
        /// <param name="Encrypted_Data">密文</param>
        /// <param name="Plain_Data_Len">解密后的明文数据的长度</param>
        /// <param name="Plain_Data">解密后的明文数据</param>
        /// <param name="ret">返回码</param>
        [DllImport(SOFTENCRYPTION_DLL_FILE_PATH, EntryPoint = "Data_Decrypt")]
        public static extern void Soft_Data_Decrypt(
                        int Key_Index,				            //输入参数
                        byte[] div_factor,		                //输入参数
                        int Encrypted_Data_Len,		        //输入参数
                        byte[] Encrypted_Data,		            //输入参数
                        ref int Plain_Data_Len,		        //输出参数
                        byte[] Plain_Data,		                //输出参数
                        byte[] ret);		                    //输出参数

        /// <summary>
        /// 加密机 数据解密
        /// </summary>
        /// <param name="Key_Index">用于产生分散密钥的主密钥的索引号</param>
        /// <param name="div_factor"> 分散因子（8个字节）</param>
        /// <param name="Encrypted_Data_Len">密文数据的长度</param>
        /// <param name="Encrypted_Data">密文</param>
        /// <param name="Plain_Data_Len">解密后的明文数据的长度</param>
        /// <param name="Plain_Data">解密后的明文数据</param>
        /// <param name="ret">返回码</param>
        [DllImport(ENCRYPTION_DLL_FILE_PATH)]
        public static extern void Data_Decrypt(
                        int Key_Index,				            //输入参数
                        byte[] div_factor,		                //输入参数
                        int Encrypted_Data_Len,		        //输入参数
                        byte[] Encrypted_Data,		            //输入参数
                        ref int Plain_Data_Len,		        //输出参数
                        byte[] Plain_Data,		                //输出参数
                        byte[] ret);		                    //输出参数

        

        //外部计算
        //public static bool MACGenerate(int keyIndex, byte[] cardNo, byte[] initVector, byte[] data, ref byte[] MAC)
        //{
        //    byte[] MACData = new byte[data.Length + 8];
        //    Security.MemoryCopy(ref MACData, 0, initVector, 0, 8);
        //    Security.MemoryCopy(ref MACData, 8, data, 0, data.Length);
        //    byte[] returnValue = new byte[1] { 0xff };
        //    MAC = new byte[4];

        //    try
        //    {
        //        MacAPI.App_MAC_Generate(keyIndex,
        //                                 cardNo,
        //                                 MACData.Length,
        //                                 MACData,
        //                                 MAC,
        //                                 returnValue);
        //    }
        //    catch (Exception err)
        //    {
        //        Message.WriteLine("加密机调用失败" + err.Message);
        //    }

        //    return returnValue[0] == 0x00 ? true : false;
        //}

    }
}
