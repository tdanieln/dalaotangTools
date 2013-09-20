using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mytools
{
    class BackStage:DB2Access
    {

        /// <summary>
        /// 银行卡签约
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public bool SignBankCard(string cardNo)
        {
         DateTime cardSignedDate = DateTime.Now;

         string cardEndDate = DateTime.Now.AddYears(5).Day.ToString();

         string cardRegisteDate = DateTime.Now.AddDays(-1).Day.ToString();


            try
            {
                myTrans = myConnection.BeginTransaction();
                myCommand.Transaction = myTrans;
                strCmd = "SELECT ID FROM DB2INST2.BANK_AGENT_ORDERS "
                       + " WHERE CARD_NO = " + GetSingleQuote(cardNo);
                myReader = this.GetDataReader(strCmd);
                if (myReader.Read())
                {
                    myReader.Close();
                    System.Console.WriteLine("卡片已经签约");
                    return false;
                }
                myReader.Close();

                strCmd = "INSERT INTO DB2INST2.BANK_AGENT_ORDERS "
                       + " ( ORDER_DATE ,STAMP_TIME , AGENT_NO , "
                       + " CARD_NO ,  RECORD_TIME ,CANCELED  )"
                       + " VALUES ("
                       + GetSingleQuote(DateTime.Today.ToString(SQLFMT_DATE)) + ","
                       + GetSingleQuote(DateTime.Now.ToString("T")) + ","
                       + "'110800010016' , "
                       + GetSingleQuote(cardNo) + ","
                       + " current timestamp ,"
                       + " 0 )";
                this.ExecuteNonQuery(strCmd);

                strCmd = " INSERT INTO  ISSUE.CARD_ACCOUNT ( "
                       + "CARD_PASSWORD,   CARD_NO,        ACCOUNT_NO ,    MAIN_CARD_ID,    VERSION"
                       + ", PRIMARY_CARD,     CARD_TYPE,      CARD_CLASS,      SELF_FLAG,  CARD_VERSION"
                       + ", CARD_NET_ID,      CARD_SIGNEDDATE,CARD_EXPIREDDATE,ASSIGN_BALANCE,  BALANCE"
                       + " , DEPOSIT,          SERVICE_FEE,    SERVICE_FEE_DATE,REGISTE_DATE,    CARD_STATUS"
                       + ", CLEAR_BALANCE,    USER_TYPE,      USER_NO,         BINDING_FLAG "
                       + ",  OPERATOR,       TRANS_FLAG,      IS_STATISTICS,   ISSUE_STATUS"
                       + ", IS_OFFICE_CAR,    AGENT_NO,       IS_PRINT ,       ACCOUNT_DEPOSIT) VALUES ("
                       + GetSingleQuote(Constant.cardPassword) + ","
                       + GetSingleQuote(cardNo) + ","
                       + GetSingleQuote(Constant.accountNo) + ","
                       + GetSingleQuote(Constant.mainCardNo) + ","
                       + "0,"

                       + " 0 , 22 , 0 , 0 , 16 ,"

                       + GetSingleQuote(Constant.cardNetNo) + ","
                       + GetSingleQuote(DateTime.Today.ToString(SQLFMT_DATE)) + ","
                       + GetSingleQuote(DateTime.Now.AddYears(+5).ToString(SQLFMT_DATE)) + ","
                       + " 0 ,"
                       + " 0 ,"

                       + " 0 , 0 ,"
                       + GetSingleQuote(DateTime.Now.AddYears(+1).ToString(SQLFMT_DATE)) + ","
                       + GetSingleQuote(DateTime.Now.AddDays(-1).ToString(SQLFMT_DATE_TIME)) + ","
                       + " 3, "

                       + " 0 ,"
                       + " 0 ,"
                       + GetSingleQuote(Constant.userNo) + ","
                       + " 0 ,"

                       + " '000051' , 0 , 0 , 0 ,"

                       + " 0 ," + GetSingleQuote(Constant.agentNo) + " ,0 , 0)";
                this.ExecuteNonQuery(strCmd);
                myTrans.Commit();
            }

            catch (Exception err)
            {
                myTrans.Rollback();
                System.Console.WriteLine(err.Message);
                throw new Exception();
            }

            return true;
        }



        public bool ReplaceBankCard(string oldCardNo
                                   , string newCardNo)
        {
            int cardStatus_CardBack = 8;
            int operationKind = 116;
            int operationResult = 1;
            DateTime cardSignedDate = DateTime.Now;
            int terminalTSN = 1;
            string operatorNo = "000051";
            string cardEndDate = DateTime.Now.AddYears(5).Day.ToString();
            string obuNo = null;

            string cardRegisteDate = DateTime.Now.AddDays(-1).Day.ToString();

            try
            {
                myTrans = myConnection.BeginTransaction();
                myCommand.Transaction = myTrans;

                strCmd = "SELECT OBU_NO FROM DB2INST2.BANK_AGENT_ORDERS"
                       + " WHERE CARD_NO = " + GetSingleQuote(oldCardNo)
                       + " AND  CANCELED = " + 0.ToString();
                this.GetScalar(strCmd, ref result);

                if (result == null | result.ToString().Trim() == "")
                    obuNo = null;
                else
                    obuNo = result.ToString();


                strCmd = "UPDATE issue.Card_Account SET  version = version + 1 ,"
                       + " MODIFY_DATE = current timestamp , "
                       + " CARD_STATUS = " + cardStatus_CardBack.ToString()
                       + " WHERE CARD_NO = " + GetSingleQuote(oldCardNo);
                this.ExecuteNonQuery(strCmd);


                strCmd =@"UPDATE DB2INST2.BANK_AGENT_ORDERS
                        SET OBU_NO = NULL
                        , CANCELED = " + 1.ToString()
                    + " WHERE CARD_NO = " + GetSingleQuote(oldCardNo);
                this.ExecuteNonQuery(strCmd);

                strCmd = @"UPDATE DB2INST2.BANK_AGENT_ORDERS SET OBU_NO = " + GetSingleQuote(obuNo)
                      + " WHERE CARD_NO = " + GetSingleQuote(newCardNo);
                this.ExecuteNonQuery(strCmd);


                //strCmd = " INSERT INTO  ISSUE.CARD_ACCOUNT ( "
                //       + "CARD_PASSWORD,   CARD_NO,        ACCOUNT_NO ,    MAIN_CARD_ID,    VERSION"
                //       + ", PRIMARY_CARD,     CARD_TYPE,      CARD_CLASS,      SELF_FLAG,  CARD_VERSION"
                //       + ", CARD_NET_ID,      CARD_SIGNEDDATE,CARD_EXPIREDDATE,ASSIGN_BALANCE,  BALANCE"
                //       + " , DEPOSIT,          SERVICE_FEE,    SERVICE_FEE_DATE,REGISTE_DATE,    CARD_STATUS"
                //       + ", CLEAR_BALANCE,    USER_TYPE,      USER_NO,         BINDING_FLAG "
                //       + ",  OPERATOR,       TRANS_FLAG,      IS_STATISTICS,   ISSUE_STATUS"
                //       + ", IS_OFFICE_CAR,    AGENT_NO,       IS_PRINT ,       ACCOUNT_DEPOSIT) VALUES ("
                //       + GetSingleQuote(Constant.cardPassword) + ","
                //       + GetSingleQuote(newCardNo) + ","
                //       + GetSingleQuote(Constant.accountNo) + ","
                //       + GetSingleQuote(Constant.mainCardNo) + ","
                //       + "0,"

                //       + " 0 , 22 , 0 , 0 , 16 ,"

                //       + GetSingleQuote(Constant.cardNetNo) + ","
                //       + GetSingleQuote(DateTime.Today.ToString(SQLFMT_DATE)) + ","
                //       + GetSingleQuote(DateTime.Now.AddYears(+5).ToString(SQLFMT_DATE)) + ","
                //       + " 0 ,"
                //       + " 0 ,"

                //       + " 0 , 0 ,"
                //       + GetSingleQuote(DateTime.Now.AddYears(+1).ToString(SQLFMT_DATE)) + ","
                //       + GetSingleQuote(DateTime.Now.AddDays(-1).ToString(SQLFMT_DATE_TIME)) + ","
                //       + " 3, "

                //       + " 0 ,"
                //       + " 0 ,"
                //       + GetSingleQuote(Constant.userNo) + ","
                //       + " 0 ,"

                //       + " '000051' , 0 , 0 , 0 ,"

                //       + " 0 ," + GetSingleQuote(Constant.agentNo) + " ,0 , 0)";
                //this.ExecuteNonQuery(strCmd);


                strCmd =
                    @"INSERT INTO DB2INST2.TABLE_OPERATIONLOG
                        (OPERATIONKIND, FRAMEKIND, POSID, POSTSN, OPERATIONTIME
                        , ADMID, CARDID, REMARK, RECORDTIME" + ", \"result\" ) VALUES("
                        + operationKind.ToString() + ", "
                        + operationKind.ToString() + ", "
                        +  "000051,"
                        + terminalTSN.ToString() + ", "
                                        //银行的操作时间非自然日时间而是对账时间，在操作记录里填写银行时间，其他填数据库本地时间
                        + GetSingleQuote(cardSignedDate.ToString(SQLFMT_DATE_TIME)) + ", "

                        
                        + int.Parse(operatorNo).ToString() + ", "
                        + GetSingleQuote(oldCardNo) + ", "
                        + GetSingleQuote(newCardNo) + ", "  //用remark字段保存新卡号
                                        //记录时间为数据库本地时间
                        + "current timestamp, "

                        + ((int)operationResult).ToString()
                        + "); ";
                  this.ExecuteNonQuery(strCmd);

                  myTrans.Commit();


            }
            catch(Exception err)
            {
                myTrans.Rollback();
                Console.WriteLine("更换失败");
                throw new Exception("更换异常");
                
            }
            return true;
        }








    }
}
