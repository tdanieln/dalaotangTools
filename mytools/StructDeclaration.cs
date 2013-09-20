using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace mytools
{
    #region 卡片结构体声明

    /// <summary>
    /// 0015文件结构体
    /// </summary>
    public class File0015Content // 用于存放0015文件的内容
    {
        public byte[] CardIssuersNo; // 发卡方标识
        public byte CardType;         // 卡片类型
        public byte CardVersionNo;    // 卡片版本号
        public byte[] CardNet;       // 卡片网络编号
        public byte[] CardSerialNo;  // CPU卡片内部编号
        public byte[] OpenTime;      // 启用时间         
        public byte[] EndTime;       // 到期时间           
        public byte[] NumOfPlate;   // 车牌号码          
        public byte UserType;         // 客户类型        
        public byte[] FCIContent;    // 发卡自定义FCI数据

        //为充值卡另准备数据
        public byte[] DispersionNo;//卡片分散编号
        public byte[] Amount;       //卡面金额
        public byte[] CheckCode;   //校验码
        public byte[] PayingCardSerialNo;  // CPU卡片内部编号

        public File0015Content()
        {
            this.CardIssuersNo = new byte[8];
            this.CardType = new byte();
            this.CardVersionNo = new byte();
            this.CardNet = new byte[2];
            this.CardSerialNo = new byte[8];
            this.OpenTime = new byte[4];
            this.EndTime = new byte[4];
            this.NumOfPlate = new byte[12];
            this.UserType = new byte();
            this.FCIContent = new byte[2];
            this.DispersionNo = new byte[2];
            this.Amount = new byte[2];
            this.CheckCode = new byte[2];
            this.PayingCardSerialNo = new byte[4];
        }
    }

    /// <summary>
    /// 0016文件结构体
    /// </summary>
    public class File0016Content // 用于存放0016文件的内容
    {
        public byte CardholderID;                 // 持卡人身份标识   
        public byte WorkerID;                     // 本系统职工标识
        public byte[] NameofCardholder;         // 持卡人名称
        public byte[] CardholderCertificateNum; // 持卡人证件号码
        public byte TypeofCardholderCertificate;  // 持卡人证件类型

        public File0016Content()
        {
            this.CardholderID = new byte();
            this.WorkerID = new byte();
            this.NameofCardholder = new byte[20];
            this.CardholderCertificateNum = new byte[32];
            this.TypeofCardholderCertificate = new byte();
        }
    }


    /// <summary>
    /// 0018文件结构体
    /// </summary>
    public class File0018Content // 用于存放0018文件的内容
    {
        public byte[] ICCardTransID;             // 联机交易序号  
        public byte[] OverdraftLimit;            // 透支限额
        public byte[] amountForTransaction;        // 交易金额
        public byte operationKind;               // 交易类型标示
        public byte[] posID;                     // 终端机ID
        public byte[] transactionDate;           // 交易日期
        public byte[] transactionTime;           // 交易时间 

        public File0018Content()
        {
            this.ICCardTransID = new byte[2];
            this.OverdraftLimit = new byte[3];
            this.amountForTransaction = new byte[4];
            this.operationKind = new byte();
            this.posID = new byte[6];
            this.transactionDate = new byte[4];
            this.transactionTime = new byte[3];
        }
    }

    /// <summary>
    /// 0019文件结构体
    /// </summary>
    public class File0019Content
    {
        /// <summary>
        /// 复合应用标识类型
        /// </summary>
        public byte consumeSign;
        /// <summary>
        /// 记录长度
        /// </summary>
        public byte length;
        /// <summary>
        /// 应用锁定标志
        /// </summary>
        public byte appSign;
        /// <summary>
        /// 入口收费路网号
        /// </summary>
        public byte[] enNetNo;
        /// <summary>
        /// 入口收费站号
        /// </summary>
        public byte[] enStationNo;
        /// <summary>
        /// 入口收费车道号
        /// </summary>
        public byte[] enRoadNo;
        /// <summary>
        /// 入口时间
        /// </summary>
        public byte[] enTime;
        /// <summary>
        /// 车型
        /// </summary>
        public byte carType;
        /// <summary>
        /// 入、出口状态
        /// </summary>
        public byte status;
        /// <summary>
        /// 标识站
        /// </summary>
        public byte[] signStation;
        /// <summary>
        /// 收费员工号
        /// </summary>
        public byte[] operateNo;
        /// <summary>
        /// 入口班次
        /// </summary>
        public byte numberOfFlights;
        /// <summary>
        /// 车牌号码
        /// </summary>
        public byte[] plateNumber;
        public File0019Content()
        {
            this.consumeSign = new byte();
            this.length = new byte();
            this.appSign = new byte();
            this.enNetNo = new byte[2];
            this.enStationNo = new byte[2];
            this.enRoadNo = new byte[2];
            this.enTime = new byte[4];
            this.carType = new byte();
            this.status = new byte();
            this.signStation = new byte[9];
            this.operateNo = new byte[3];
            this.plateNumber = new byte[12];
        }
    }




    /// <summary>
    /// 圈存初始化后卡片返回的结构体
    /// </summary>
    public class InitializeForLoadTimecosResponse // 用于存放LnitializeForLoad操作后TimeCos返回的数据
    {
        public byte[] oldBalance;       // 电子钱包旧余额
        public byte[] ICCardTransID;  // 电子钱包联机交易序号
        public byte keyVersionNo;      // 密钥版本号
        public byte AlgorithmID;      // 算法标识
        public byte[] RandomNum;      // 伪随机数
        public byte[] MAC1;           // MAC1

        public InitializeForLoadTimecosResponse()
        {
            this.oldBalance = new byte[4];
            this.ICCardTransID = new byte[2];
            this.RandomNum = new byte[4];
            this.MAC1 = new byte[4];
        }
    }

    // 用于存放消费初始化操作后TimeCos返回的数据
    /// <summary>
    /// 消费初始化后卡片返回的结构体
    /// </summary>
    public class InitializeForPurchaseTimecosResponse
    {
        /// <summary>
        /// 电子钱包旧余额
        /// </summary>
        public byte[] oldBalance;              
        /// <summary>
        /// 电子钱包脱机交易序号
        /// </summary>
        public byte[] ICCardTransID;         
        /// <summary>
        /// 透支限额
        /// </summary>
        public byte[] OverdraftLimit;        
        /// <summary>
        /// 密钥版本号
        /// </summary>
        public byte keyVersionNo;               
        /// <summary>
        /// 算法标识
        /// </summary>
        public byte AlgorithmID;               
        /// <summary>
        /// 伪随机数
        /// </summary>
        public byte[] RandomNum;              

        public InitializeForPurchaseTimecosResponse()
        {
            this.oldBalance = new byte[4];
            this.ICCardTransID = new byte[2];
            this.OverdraftLimit = new byte[3];
            this.keyVersionNo = new byte();
            this.AlgorithmID = new byte();
            this.RandomNum = new byte[4];
        }
    }
    #endregion


    #region OBU结构体声明
    /// <summary>
    /// 中兴设备初始化参数
    /// </summary>
    public struct DeviceInitParameterType
    {
        /// <summary>
        /// Unix时间
        /// </summary>
        public byte[] UnixTime;//[4]
        /// <summary>
        /// BST间隔时间，以毫秒为单位
        /// </summary>
        public byte[] BSTInterval;//[1]0x00
        /// <summary>
        /// 发行器发射功率：0到31表示32个功率级别
        /// </summary>
        public byte[] TxPower;//[1]0xff
        /// <summary>
        /// 发射信道的选择，0信道或者1信道
        /// </summary>
        public byte[] PHYChannelID;//[1]0x00
        /// <summary>
        /// 保留
        /// </summary>
        public byte[] Reserved;//[5]
    };


    /// <summary>
    /// OBU厂商定义的结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SysInfoType
    {
        /// <summary>
        /// 服务商名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] contractProvider;
        /// <summary>
        /// 协约类型
        /// </summary>
        public byte contractType;
        /// <summary>
        /// 合同版本
        /// </summary>
        public byte contractVersion;
        /// <summary>
        /// 合同序列号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] contractSerialNumber;
        /// <summary>
        /// 合同签署日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] contractSignedDate;
        /// <summary>
        /// 合同国企日期
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] contractExpiredDate;
        /// <summary>
        /// 预留字段
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] Reserved;

        public SysInfoType(IntPtr ptr)
        {
            this.contractProvider = new byte[8];
            this.contractType = 0;
            this.contractVersion = 0;
            this.contractSerialNumber = new byte[8];
            this.contractSignedDate = new byte[4];
            this.contractExpiredDate = new byte[4];
            this.Reserved = new byte[64];
        }
    };

    /// <summary>
    /// 车辆信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ETCVehicleInfoType
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public byte[] vehicleLicencePlateNumber;
        /// <summary>
        /// 车牌颜色
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] vehicleLicencePlateColor;
        /// <summary>
        /// 车型
        /// </summary>
        public byte vehicleClass;
        /// <summary>
        /// 车辆用户类型
        /// </summary>
        public byte vehicleUserType;
        /// <summary>
        /// 车辆尺寸
        /// </summary>
        public VehicleDimensionsType vehicleDimensions;
        /// <summary>
        /// 车轮数
        /// </summary>
        public byte vehicleWheels;
        /// <summary>
        /// 车轴数
        /// </summary>
        public byte vehicleAxles;
        /// <summary>
        /// 轴距
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] vehicleWheelBases;
        /// <summary>
        /// 车辆载重/座位数
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] vehicleWeitghtLimits;
        /// <summary>
        /// 车辆特征描述
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] vehicleSpecificInfomation;
        /// <summary>
        /// 车辆发动机号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] vehicleEngineNumber;
        /// <summary>
        /// 保留字段
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] vehicleReserved;

        public ETCVehicleInfoType(IntPtr ptr)
        {
            this.vehicleLicencePlateNumber = new byte[12];
            this.vehicleLicencePlateColor = new byte[2];
            this.vehicleClass = 0;
            this.vehicleUserType = 0;
            this.vehicleDimensions = new VehicleDimensionsType(ptr);
            this.vehicleWheels = 0;
            this.vehicleAxles = 0;
            this.vehicleWheelBases = new byte[2];
            this.vehicleWeitghtLimits = new byte[3];
            this.vehicleSpecificInfomation = new byte[16];
            this.vehicleEngineNumber = new byte[16];
            this.vehicleReserved = new byte[10];
        }
    };

    /// <summary>
    /// 车辆尺寸
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct VehicleDimensionsType
    {
        /// <summary>
        /// 车长度
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] vehicleLength;
        /// <summary>
        /// 车宽度
        /// </summary>
        public byte vehicleWidth;
        /// <summary>
        /// 车高度
        /// </summary>
        public byte vehicleHeigth;
        public VehicleDimensionsType(IntPtr ptr)
        {
            this.vehicleLength = new byte[2];
            this.vehicleWidth = 0;
            this.vehicleHeigth = 0;
        }
    };
    #endregion


    /// <summary>
    /// 银行XML格式返回值
    /// </summary>
    public struct RetCodeBankXml
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int errCode;
        /// <summary>
        /// 返回信息
        /// </summary>
        public String errMessage;
        /// <summary>
        /// 账号
        /// </summary>
        public string accountNo;
        /// <summary>
        /// 加卡的数量
        /// </summary>
        public int numCardAdded;
        /// <summary>
        /// 减卡的数量
        /// </summary>
        public int numCardDroped;
        /// <summary>
        /// 日志号
        /// </summary>
        public int logID;
        /// <summary>
        /// 返回的数据长度
        /// </summary>
        public int dataInfoLength;
        /// <summary>
        /// 返回的数据内容
        /// </summary>
        public string dataInfo;
        /// <summary>
        /// 消费MAC1
        /// </summary>
        public byte[] MAC1;
    }


    public class ExcelStruct
    {
        /// <summary>
        /// 边框样式
        /// </summary>
        public int bordersLineStyle;
        /// <summary>
        /// 单元格格式
        /// </summary>
        public string numberFormat;
        /// <summary>
        ///字体加粗
        /// </summary>
        public bool fontBond;
        /// <summary>
        /// 字号
        /// </summary>
        public int fontSize;
        /// <summary>
        /// 字体颜色
        /// </summary>
        public int fontColor;
        /// <summary>
        /// 背景颜色
        /// </summary>
        public int interiorColor;
        /// <summary>
        /// 初始X坐标(A1中的A)
        /// </summary>
        public int X_ini;
        /// <summary>
        /// 初始Y坐标(A1中的1)
        /// </summary>
        public int Y_ini;
        /// <summary>
        /// 结束X坐标(B1中的B)
        /// </summary>
        public int X_end;
        /// <summary>
        /// 结束Y坐标(B1中的1)
        /// </summary>
        public int Y_end;

        //构造函数
        public ExcelStruct()
        {
            this.bordersLineStyle = 1;
            this.numberFormat = "";
            this.fontBond = false;
            this.fontSize = 10;
            this.fontColor = 1;
            this.interiorColor = 0;
            this.X_ini = 1;
            this.Y_ini = 1;
            this.X_end = 2;
            this.Y_end = 2;
        }
    }
}
