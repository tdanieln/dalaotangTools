//****      本文件为卡内文件结构体
//****      现有0015结构体，0016结构体，0018结构体，圈存初始化结构体，消费初始化结构体
//****
//****
//****
//****
//****
//****
//****
//****
//****
//****

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mytools
{
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
        public byte[] oldBalance;               // 电子钱包旧余额
        public byte[] ICCardTransID;         // 电子钱包脱机交易序号
        public byte[] OverdraftLimit;        //透支限额
        public byte keyVersionNo;               // 密钥版本号
        public byte AlgorithmID;               // 算法标识
        public byte[] RandomNum;             // 伪随机数

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
}
