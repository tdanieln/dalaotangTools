///*    枚举类用于客户端各类枚举
///*

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mytools
{
    public enum ReturnSW : int
    {
        /// <summary>
        /// 执行成功
        /// </summary>
        /// <remarks>0x9000</remarks>
        executionSuccess = 0x9000,
        /// <summary>
        /// 回送的数据可能错误
        /// </summary>
        retDataWrong = 0x6281,
        /// <summary>
        /// 选择文件或者密钥验证错误
        /// </summary>
        selectFileFailOrVerifyFile = 0x6283,
        /// <summary>
        /// 还剩3次可以尝试的机会
        /// </summary>
        /// <remarks>0x63C3</remarks>
        stillThreeTimesChance = 0x63C3,
        /// <summary>
        /// 还剩2次可以尝试的机会
        /// </summary>
        /// <remarks>0x63C2</remarks>
        stillTwoTimesChance = 0x63C2,
        /// <summary>
        /// 还剩1次可以尝试的机会
        /// </summary>
        /// <remarks>0x63C1</remarks>
        stillOneTimeChance = 0x63C1,
        /// <summary>
        /// 还剩0次可以尝试的机会
        /// </summary>
        /// <remarks>0x63C0</remarks>
        stillZeroTimeChance = 0x63C0,
        /// <summary>
        /// 状态标志未改变
        /// </summary>
        statusChangeFail = 0x6400,
        /// <summary>
        /// 写EEPROM失败
        /// </summary>
        writeEEPROMFail = 0x6581,
        /// <summary>
        /// 长度错误
        /// </summary>
        /// <remarks>0x6700</remarks>
        lengthWrong = 0x6700,
        /// <summary>
        /// CLA与线路保护要求不匹配
        /// </summary>
        CLAMatchcircuitFile = 0x6900,
        /// <summary>
        /// 无效的状态
        /// </summary>
        inVaildStatus = 0x6901,
        /// <summary>
        /// 当前的文件类型错误,目标不是二进制或者循环文件
        /// </summary>
        /// <remarks>0x6981</remarks>
        fileWrong = 0x6981,
        /// <summary>
        /// 写的条件不满足
        /// </summary>
        /// <remarks>0x6982</remarks>
        writePremiseWrong = 0x6982,
        /// <summary>
        /// 密钥被锁死
        /// </summary>
        keyLock = 0x6983,
        /// <summary>
        /// 不满足使用条件
        /// </summary>
        donotConfirmConditions = 0x6985,
        /// <summary>
        /// 不存在安全报文
        /// </summary>
        donotExistSecureMessage = 0x6988,
        /// <summary>
        /// 参数错误
        /// </summary>
        wrongParameter = 0x6A80,
        /// <summary>
        /// 不支持此功能
        /// </summary>
        /// <remarks>0x6A81</remarks>
        donotSupportFunction = 0x6A81,
        /// <summary>
        /// 没有找到该文件
        /// </summary>
        /// <remarks>0x6A82</remarks>
        donotFindFile = 0x6A82,
        /// <summary>
        /// 未找到记录
        /// </summary>
        /// <remarks>0x6A83</remarks>
        donotFindRecord = 0x6A83,
        /// <summary>
        /// 文件无足够空间
        /// </summary>
        /// <remarks>0x6A84</remarks>
        fileBeyondSpace = 0x6A84,
        /// <summary>
        /// 参数P1P2错误
        /// </summary>
        parameterP1P2Wrong = 0x6A86,
        /// <summary>
        /// 偏移量错误
        /// </summary>
        wrongOffset = 0x6B00,
        /// <summary>
        /// 无效的CLA
        /// </summary>
        wrongCLA = 0x6E00,
        /// <summary>
        /// 数据无效
        /// </summary>
        wrongData = 0x6F00,
        /// <summary>
        /// 错误的MAC
        /// </summary>
        wrongMAC = 0x9302,
        /// <summary>
        /// 应用已经被锁定
        /// </summary>
        appLock = 0x9303,
        /// <summary>
        /// 金额不足
        /// </summary>
        lackofMoney = 0x9401,
        /// <summary>
        /// 密钥未找到
        /// </summary>
        notFindkey = 0x9403,
        /// <summary>
        /// 所需的MAC不可用
        /// </summary>
        MACinVaild = 0x9406

    }

    /// <summary>
    /// 枚举类型:读卡器厂商
    /// </summary>
    public enum ReaderVender : int
    {
        /// <summary>
        /// 金溢:0
        /// </summary>
        GENVICT = 0,
        /// <summary>
        /// 握奇:1
        /// </summary>
        WATCH = 1,
        /// <summary>
        /// 中兴:2
        /// </summary>
        ZTE = 2,
        /// <summary>
        /// 聚利:3
        /// </summary>
        JULI = 3
    }

    /// <summary>
    /// 枚举类型：设备连接方式
    /// </summary>
    public enum DevConnMode : int
    {
        /// <summary>
        /// 串口
        /// </summary>
        COM = 0,
        /// <summary>
        /// 网络连接TCPIP:1
        /// </summary>
        TCPIP = 1,
        /// <summary>
        /// USB口
        /// </summary>
        USB = 2
    }

    /// <summary>
    ///  枚举类型:卡片类型
    /// </summary>
    public enum CardType : short
    {
        /// <summary>
        /// 储值卡:22
        /// </summary>
        PURSECARD = 22,
        /// <summary>
        /// 记账卡:23
        /// </summary>
        CREDITCARD = 23
    }

    /// <summary>
    /// 枚举类型：加密机动态库类型
    /// </summary>
    public enum EncryptionDllType : short
    {
        /// <summary>
        /// 软加密机：0
        /// </summary>
        SoftEncryption = 0,
        /// <summary>
        /// 北京加密机：1
        /// </summary>
        EncryptionMachineBJ = 1,
        /// <summary>
        /// 辽宁加密机：2
        /// </summary>
        EncryptionMachineLN = 2
    }

    /// <summary>
    /// RSU连接模式
    /// </summary>
    public enum RSUConnectMode : short
    {
        /// <summary>
        /// 通过COM口连接：0
        /// </summary>
        COM = 0,
        /// <summary>
        /// 通过网络连接：1
        /// </summary>
        TCPIP = 1,
        /// <summary>
        /// 通过USB连接：2
        /// </summary>
        USB = 2
    }



    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserTpye : short
    {
        /// <summary>
        /// 个人：0 
        /// </summary>
        Personal = 0,
        /// <summary>
        /// 企业：1 
        /// </summary>
        EnterPrise = 1
    }

    /// <summary>
    /// 操作结果
    /// </summary>
    public enum OperationResult : short
    {
        /// <summary>
        /// 操作成功：0
        /// </summary>
        oprSuccess = 0,
        /// <summary>
        /// 操作失败：-1
        /// </summary>
        oprFault= -1
    }

    /// <summary>
    /// 账户类型
    /// </summary>
    public enum AccountType : short
    {
        /// <summary>
        /// 储值卡：22
        /// </summary>
        PurchaseCard = 22,
        /// <summary>
        /// 记账卡：23
        /// </summary>
        CreditCard = 23
    }

    /// <summary>
    /// 证件类型
    /// </summary>
    public enum IdentifyType : short
    {
        /// <summary>
        ///身份证:0
        /// </summary>
        ID = 0,             
        /// <summary>
        /// 军官证:1
        /// </summary>
        OFFICER = 1,        
        /// <summary>
        /// 护照:2
        /// </summary>
        PASSPORT = 2,       
        /// <summary>
        /// 入境证:3
        /// </summary>
        IMMIGRATION = 3,    
        /// <summary>
        /// 临时身份证:4
        /// </summary>
        TEMP_ID = 4,        
        /// <summary>
        /// 驾驶证:5
        /// </summary>
        DRIVER_LICENSE = 5        
    }

    /// <summary>
    /// 绑定标识
    /// </summary>
    public enum BindFlag:int
    {
        /// <summary>
        /// 非绑定:0
        /// </summary>
        NotBind = 0,
        /// <summary>
        /// 绑定:1
        /// </summary>
        Bind = 1
    }

    public enum BankOperationKind : int
    {
        /// <summary>
        /// 签约：100001
        /// </summary>
        Contract = 100001,
        /// <summary>
        /// 签约信息修改：100002
        /// </summary>
        ContractInfoChange = 100002,
        /// <summary>
        /// 加减卡：100003
        /// </summary>
        AddAndRemoveCard = 100003,
        /// <summary>
        /// 卡禁用：100004
        /// </summary>
        ForbiddenCard = 100004,
        /// <summary>
        /// 卡更换开始：100005
        /// </summary>
        CardReplaceStart = 100005,
        /// <summary>
        /// 卡更换确认：100006
        /// </summary>
        CardReeplaceEnsure = 100006,
        /// <summary>
        /// 解约/预清户:300001
        /// </summary>
        PrepareClearAccount = 300001
    }

    /// <summary>
    /// 交易类型标识，拼装IC卡数据域使用
    /// </summary>
    public enum TranTypeSign : short
    {
        /// <summary>
        /// 电子存折圈存:0x01(Electronic Deposit Load)
        /// </summary>
        EDLoad = 0x01,
        /// <summary>
        /// 电子存折圈存：0x02(Eletronic Purse Load)
        /// </summary>
        EPLoad = 0x02,
        /// <summary>
        /// 电子存折消费：0x05
        /// </summary>
        EDPurchase = 0x05,
        /// <summary>
        /// 电子钱包消费：0x06
        /// </summary>
        EPPurchase = 0x06,
        /// <summary>
        /// 电子钱包符合消费:0x09
        /// </summary>
        EPCappPurchase = 0x09
    }

    class Enum
    {
        public static string Card_ReturnSW(int returnSW)
        {
            string retValueExplain = "";
            string returnString = returnSW.ToString("X");
            switch (returnSW)
            {
                case (int)ReturnSW.executionSuccess: retValueExplain = "卡片返回值:" + returnString.ToString() + "执行成功";
                    break;
                case (int)ReturnSW.retDataWrong: retValueExplain = "卡片返回值：" + returnString.ToString() + "回送的数据错误";
                    break;
                case (int)ReturnSW.selectFileFailOrVerifyFile: retValueExplain = "卡片返回值：" + returnString.ToString() + "选择文件或密钥验证错误";
                    break;
                case (int)ReturnSW.stillThreeTimesChance: retValueExplain = "卡片返回值：" + returnString.ToString() + "验证失败，还剩3次尝试机会";
                    break;
                case (int)ReturnSW.stillTwoTimesChance: retValueExplain = "卡片返回值：" + returnString.ToString() + "验证失败，还剩2次尝试机会";
                    break;
                case (int)ReturnSW.stillOneTimeChance: retValueExplain = "卡片返回值：" + returnString.ToString() + "验证失败，还剩1次尝试机会";
                    break;
                case (int)ReturnSW.stillZeroTimeChance: retValueExplain = "卡片返回值：" + returnString.ToString() + "验证失败，还剩0次尝试机会";
                    break;
                case (int)ReturnSW.statusChangeFail: retValueExplain = "卡片返回值：" + returnString.ToString() + "状态标识未改变";
                    break;
                case (int)ReturnSW.writeEEPROMFail: retValueExplain = "卡片返回值：" + returnString.ToString() + "写EEPROM失败";
                    break;
                case (int)ReturnSW.lengthWrong: retValueExplain = "卡片返回值：" + returnString.ToString() + "长度错误";
                    break;
                case (int)ReturnSW.CLAMatchcircuitFile: retValueExplain = "卡片返回值：" + returnString.ToString() + "CLA与线路保护要求不匹配";
                    break;
                case (int)ReturnSW.inVaildStatus: retValueExplain = "卡片返回值：" + returnString.ToString() + "无效的状态";
                    break;
                case (int)ReturnSW.fileWrong: retValueExplain = "卡片返回值：" + returnString.ToString() + "文件类型错误";
                    break;
                case (int)ReturnSW.writePremiseWrong: retValueExplain = "卡片返回值：" + returnString.ToString() + "写文件条件不满足";
                    break;
                case (int)ReturnSW.keyLock: retValueExplain = "卡片返回值：" + returnString.ToString() + "密钥被锁死";
                    break;
                case (int)ReturnSW.donotConfirmConditions: retValueExplain = "卡片返回值：" + returnString.ToString() + "不满足使用条件";
                    break;
                case (int)ReturnSW.donotExistSecureMessage: retValueExplain = "卡片返回值：" + returnString.ToString() + "不存在安全报文";
                    break;
                case (int)ReturnSW.wrongParameter: retValueExplain = "卡片返回值：" + returnString.ToString() + "参数错误";
                    break;
                case (int)ReturnSW.donotSupportFunction: retValueExplain = "卡片返回值：" + returnString.ToString() + "不支持此功能";
                    break;
                case (int)ReturnSW.donotFindFile: retValueExplain = "卡片返回值：" + returnString.ToString() + "没找到该文件";
                    break;
                case (int)ReturnSW.donotFindRecord: retValueExplain = "卡片返回值：" + returnString.ToString() + "未找到记录";
                    break;
                case (int)ReturnSW.fileBeyondSpace: retValueExplain = "卡片返回值：" + returnString.ToString() + "文件空间不足";
                    break;
                case (int)ReturnSW.parameterP1P2Wrong: retValueExplain = "卡片返回值：" + returnString.ToString() + "参数P1P2错误";
                    break;
                case (int)ReturnSW.wrongOffset: retValueExplain = "卡片返回值：" + returnString.ToString() + "偏移量错误";
                    break;
                case (int)ReturnSW.wrongCLA: retValueExplain = "卡片返回值：" + returnString.ToString() + "无效CLA";
                    break;
                case (int)ReturnSW.wrongData: retValueExplain = "卡片返回值：" + returnString.ToString() + "无效数据";
                    break;
                case (int)ReturnSW.wrongMAC: retValueExplain = "卡片返回值：" + returnString.ToString() + "无效MAC";
                    break;
                case (int)ReturnSW.appLock: retValueExplain = "卡片返回值：" + returnString.ToString() + "应用已经被锁定";
                    break;
                case (int)ReturnSW.lackofMoney: retValueExplain = "卡片返回值：" + returnString.ToString() + "金额不足";
                    break;
                case (int)ReturnSW.notFindkey: retValueExplain = "卡片返回值：" + returnString.ToString() + "密钥未找到";
                    break;
                case (int)ReturnSW.MACinVaild: retValueExplain = "卡片返回值：" + returnString.ToString() + "MAC无效";
                    break;
                default: retValueExplain = "未知返回码" + returnSW.ToString();
                    break;
            }
            return retValueExplain;
        }


    }


}
