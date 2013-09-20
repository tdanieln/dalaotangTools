using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mytools
{
    class SessionCache
    {
        private static SessionCache _instance = null;

        Dictionary<ulong, Value> sessionCache;

        //DataOfValidatedSession[] sessionArrary;

        //会话时间限制 ,单位毫秒，默认为工作日的8个小时
        public static int SessionTTL = 28800000;    // 改为24小时服务 1000 * 3600 * 24 (毫秒)

        //protected SessionCache()
        //{
        //    ReloadCache();
        //}

        ///// <summary>
        ///// WCF服务通信，获得所有合法的会话密钥
        ///// </summary>
        //private void ReloadCache()
        //{
        //    sessionCache = new Dictionary<ulong, Value>();
        //    try
        //    {
        //        using (ChargeServicesClient client = new ChargeServicesClient(true))
        //        {
        //            sessionArrary = client.FetchAllValidatedSession();
        //        }
        //        if (sessionArrary == null)
        //        {
        //            sessionCache = new Dictionary<ulong, Value>();
        //        }
        //        else
        //        {
        //            foreach (DataOfValidatedSession data in sessionArrary)
        //            {
        //                this.Add(data.operatorNo, data.loginTime, data.agentNo, data.posID, data.sessionKey);
        //            }
        //        }
        //    }
        //    //有任何异常直接断开连接,不发送异常信息,终端需重新发起挑战
        //    catch
        //    {
        //        sessionCache = new Dictionary<ulong, Value>();
        //    }
        //}

        public static SessionCache getInstance()
        {
            if (_instance == null)
                _instance = new SessionCache();
            return _instance;
        }

        //往Cache中添加会话密钥
        public bool Add(string operatorNo, DateTime loginTime, string agentNo, ulong posID, byte[] sessionKey)
        {
            Value temp = new Value(operatorNo, loginTime, agentNo, sessionKey);

            lock (this.sessionCache)
            {
                try
                {
                    if (sessionCache.ContainsKey(posID))
                        sessionCache.Remove(posID);
                    sessionCache.Add(posID, temp);
                }
                catch
                {
                    // just ignore it
                }
            }
            return true;
        }

        ////读取保存在Cache中的会话密钥，在命中失败时，自动重新读取Cache
        //public byte[] GetSessionKey(ulong posID, out string agentNo, out string operatorNo)
        //{
        //    agentNo = null;
        //    operatorNo = null;
        //    try
        //    {
        //        Value temp;
        //        // 未命中，或命中，但已经过期，均应重新(加载)认证
        //        if (!sessionCache.TryGetValue(posID, out temp)
        //            || GetDelayTime(temp.lastAccessTime) > SessionCache.SessionTTL)
        //        {
        //            ReloadCache();
        //            //如果重新加载之后仍然找不到session key，则认为认证失败
        //            if(!sessionCache.TryGetValue(posID, out temp))
        //                return null;
        //        }
        //        agentNo = temp.agentNo;
        //        operatorNo = temp.operatorNo;
        //        return temp.sessionKey;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //用于清除过期的会话密钥,该方法建议在服务器压力较小时执行
        public void ClearSessionKey()
        {
            lock (this.sessionCache)
            {
                Dictionary<ulong, Value>.Enumerator enumerator = sessionCache.GetEnumerator();
                while (enumerator.MoveNext() == true)
                {
                    if (GetDelayTime(enumerator.Current.Value.lastAccessTime) > SessionCache.SessionTTL)
                    {
                        sessionCache.Remove(enumerator.Current.Key);
                    }
                }
            }
        }

        public static double GetDelayTime(DateTime time)
        {
            TimeSpan timeSpan1 = DateTime.Now - time;
            double delalyTime = timeSpan1.TotalSeconds;
            return delalyTime;
        }

        //内部struct,仅供SessionCache调用，之所以用属性，是为了确保最后访问会话密钥时间可及时更新
        struct Value
        {
            public DateTime lastAccessTime;
            public string operatorNo;
            public DateTime loginTime;
            public string agentNo;
            private byte[] _keySession;
            //
            //
            public byte[] sessionKey
            {
                get
                {
                    lastAccessTime = DateTime.Now;
                    return _keySession;
                }
                set
                {
                    lastAccessTime = DateTime.Now;
                    _keySession = value;
                }
            }

            public Value(string operatorNo, DateTime loginTime, string agentNo, byte[] sessionKey)
            {
                this.operatorNo = operatorNo;
                this.loginTime = loginTime;
                this.agentNo = agentNo;
                this._keySession = sessionKey;
                lastAccessTime = DateTime.Now;
            }

            public override string ToString()
            {
                string str = " ";
                for (int i = 0; i < _keySession.Length; i++)
                {
                    str += _keySession[i].ToString();
                }
                return loginTime.ToString() + str;
            }
        }
    }
}
