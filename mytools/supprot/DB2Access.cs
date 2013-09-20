using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

using IBM.Data.DB2;
using IBM.Data.DB2Types;


namespace mytools
{
    /**
     * DB2数据库访问模块
     * 除了构造函数和析构函数之外，其他功能函数均可能抛出数据库异常
     * 构造函数是否执行成功，可从isOperationOK的状态判断(默认为false，只有构造成功方为true)
     */
    /// <summary>
    /// 定义一个基类继承接口
    /// </summary>
    public class DB2Access : IDisposable
    {
        //属性定义
        public const string singleQuote = "'";   //单引号
        public const string doubleQuote = "\"";  //双引号
        public const string strComma = ",";      //逗号
        public const string strAnd = " AND ";      //AND
        public const string SQLFMT_DATE_TIME = "yyyy-MM-dd HH:mm:ss";//统一时间字符串格式
        public const string SQLFMT_DATE = "yyyy-MM-dd";              //统一时间字符串格式
        static private DbProviderFactory factory;

        public bool isOperationOk = false;      //操作结果
        protected DB2Connection myConnection;   //数据库连接对象
        protected DB2Command myCommand;         //数据库连接对象
        protected DB2Transaction myTrans = null;//事务处理
        public string strCmd = "";              //命令字符串
        public object result = null;           //查询结果
        public string lastErr = "";             //异常信息

        /// <summary>
        /// DB2数据库操作类 静态构造函数，检查DB2驱动的安装情况配置情况
        /// </summary>
        static DB2Access()
        {
            try
            {
                factory = DbProviderFactories.GetFactory("IBM.Data.DB2");
            }
            catch (Exception err)
            {
                 System.Console.WriteLine("请检查是否安装了IBM DB2 Data Server Runtime Driver\r\n" + err.Message);
                return;
            }
        }

        /// <summary>
        /// DB2数据库操作类 构造函数，建立数据库连接对象和执行操作的上下文环境
        /// </summary>
        public DB2Access()
        {
            try
            {
                DB2ConnectionStringBuilder sb = new DB2ConnectionStringBuilder("Server=192.168.1.80:50000;Database=ISSUE;UserID=DB2INST2;Password=Test1234;Connection Lifetime=0;Max Pool Size=100;Min Pool Size=10");
                //DB2ConnectionStringBuilder sb = new DB2ConnectionStringBuilder("Server=localhost:50000;Database=ISSUE;UserID=db2inst2;Password=Test1234;Connection Lifetime=0;Max Pool Size=100;Min Pool Size=10");
                myConnection = (DB2Connection)factory.CreateConnection();
                myConnection.ConnectionString = sb.ConnectionString;
                //
                myConnection.Open();
                //
                myCommand = new DB2Command();
                myCommand.Connection = myConnection;
                isOperationOk = true;
            }
            catch (DbException dbEx)
            {
                MessageBox.Show("连接数据库发生数据库例外!" + dbEx.Message);
                Trace.WriteLine(dbEx.Message);
                return;
            }
            catch (Exception Ex)
            {
                MessageBox.Show("连接数据库发生例外!" + Ex.Message);
                Trace.WriteLine(Ex.Message);
                return;
            }
        }

        /// <summary>
        /// DB2数据库操作类 构造函数，用于复用连接和事务处理上下文
        /// </summary>
        /// <param name="sharedConnection">复用的数据库连接</param>
        /// <param name="sharedTransaction">复用的事务处理上下文</param>
        public DB2Access(DbConnection sharedConnection, DbTransaction sharedTransaction)
        {
            myConnection = sharedConnection as DB2Connection;
            myTrans = sharedTransaction as DB2Transaction;

            try
            {
                myCommand = myConnection.CreateCommand();
                myCommand.Connection = myConnection;
                myCommand.Transaction = myTrans;
                isOperationOk = true;
            }
            catch (Exception Ex)
            {
                System.Console.WriteLine(Ex.Message);
                Trace.WriteLine("无法创建远程数据库命令执行上下文：" + Ex.Message);
            }
        }

        /// <summary>
        /// 析构函数,强制回收垃圾单元	
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // to assure that the connection is closed, comment out SuppressFinalize
            GC.SuppressFinalize(true);
        }

        /// <summary>
        /// 析构函数,由垃圾收集器调用,它的调用是随机的
        /// </summary>
        ~DB2Access()
        {
            Dispose(false);
        }

        /// <summary>
        /// 析构函数,结束前执行的处理
        /// </summary>
        /// <param name="disposing"></param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    if (myConnection.IsOpen)
                        myConnection.Close();
                }
                catch (Exception err)
                {
                    Trace.WriteLine(err.StackTrace + err.Message);
                    System.Console.WriteLine("关闭数据库连接出现异常！\r\n " + err.Message);
                }
            }
        }

        

        /// <summary>
        /// 执行DB2数据库查询语句,得到查询结果数据集
        /// </summary>
        /// <param name="strCmd">所执行的数据库命令，一般为查询语句，也可以是存储过程调用</param>
        /// <returns>DataReader接口</returns>
        public IDataReader GetDataReader(string strCmd)
        {
            DB2Command myCommand = new DB2Command(strCmd, myConnection);
            myCommand.Transaction = myTrans;
            DB2DataReader dataReader = myCommand.ExecuteReader();
            return dataReader;
        }

        /*方法功能：	
          * 编号：07
          * 返回值类型：
          * 参数说明：
          * 创建日期：2008年7月21日
          * 创建人：
          * 
          * 修改人：
          * 修改日期：
          * 修改说明：
          */
        /// <summary>
        /// 执行DB2数据库非查询语句
        /// </summary>
        /// <param name="strCmd">待执行的数据库命令，应为U,A,D而非R操作</param>
        /// <returns>返回值为1，或者抛出异常</returns>
        public int ExecuteNonQuery(string strCmd)
        {
            DB2Command audCommand = new DB2Command(strCmd, myConnection, myTrans);
            return audCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 执行数据库查询语句，返回一个标量对象
        /// </summary>
        /// <param name="strCmd">所要执行的查询语句</param>
        /// <param name="result">出口参数，所查询的第一条（通常为唯一一条）记录的第一个字段（应为唯一字段）的值</param>
        /// <returns>true如果查到一条记录，false如果未找到</returns>
        public bool GetScalar(string strCmd, ref Object result)
        {
            DB2Command rCommand = new DB2Command(strCmd, myConnection, myTrans);
            result = rCommand.ExecuteScalar();
            return (result != null);
        }

        /// <summary>
        /// 执行数据库查询语句，返回一个标量对象
        /// </summary>
        /// <param name="strCmd">所要执行的查询语句</param>
        /// <param name="result">出口参数，所查询的第一条（通常为唯一一条）记录的第一个字段（应为唯一字段）的值</param>
        /// <returns>true如果查到一条记录，false如果未找到</returns>
        public bool GetScalarNonTransaction(string strCmd, ref Object result)
        {
            DB2Command rCommand = new DB2Command(strCmd, myConnection);
            result = rCommand.ExecuteScalar();
            return (result != null);
        }

        
        /// <summary>
        /// 给一个字符串加上数据库相关的引号
        /// </summary>
        /// <param name="str">未加引号前的字符串</param>
        /// <returns>加引号后的字符串</returns>
        /// <remarks>NULL会被识别，前后空格会削除，原有的单引号会被转义</remarks>
        public static string GetSingleQuote(string str)
        {
            if(str == null)
                return "NULL";

            string s = "'";
            int m = 0;
            for (int k; (k = str.IndexOf("'", m)) > 0; m = k + 1)
            {
                s += str.Substring(m, k - m) + "''";
            }
            return s + str.Substring(m) + "'";
        }

        /// <summary>
        /// 将数据对象转换为数据库所识别的字符串，null/undefined等效为NULL
        /// </summary>
        /// <param name="obj">待转换的字符串</param>
        /// <returns>准许为NULL的数据库值的字符串表示</returns>
        public static string DBString(object obj)
        {
            return obj == null ? "NULL" : obj.ToString();
        }

        /// <summary>
        /// 将指定的时间戳字段的日期部分查询，解析为时间段查询，以运用索引
        
        /// </summary>
        /// <param name="fieldName">时间戳字段名</param>
        /// <param name="date1">日期</param>
        /// <returns>查询条件</returns>
        public static string DateToTimestampRange(string fieldName, DateTime date1)
        {
            return " " + fieldName + " >= TIMESTAMP('" + date1.ToString(SQLFMT_DATE) + " 00:00:00.000') AND "
                + fieldName + " <  TIMESTAMP('" + date1.AddDays(1).ToString(SQLFMT_DATE) + " 00:00:00.000') ";
        }

        public static string ToTimestampFromStartDate(DateTime date0)
        {
            return "TIMESTAMP('" + date0.ToString(SQLFMT_DATE) + " 00:00:00.000000')";
        }

        public static string ToTimestampFromEndDate(DateTime date1)
        {
            return "TIMESTAMP('" + date1.ToString(SQLFMT_DATE) + " 23:59:59.999999')";
        }

        public IDataReader myReader = null;
    }


}
