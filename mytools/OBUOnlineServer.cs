using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;

namespace mytools
{
    class OBUOnlineServer
    {
        //添加的代码
        //读等待时间限制
        private const int READ_SLEEP_INTERVAL = 50;

        //自定义属性和代理
        //用户线程集合，用来动态管理用户通信线程
        public static System.Collections.Generic.List<ClientUser> userList = new List<ClientUser>();

        //用户线程
        IPEndPoint servicePoint = null;

        Socket listeningSocket = null;


        //自动检测侦听线程的时钟
        private static System.Timers.Timer timerToCheckThread;
        //侦听线程
        Thread myThreadListen;

        SessionCache sessionCache = SessionCache.getInstance();

        public void Start(IPAddress localAddress, int port)
        {
            try
            {
                servicePoint = new IPEndPoint(localAddress, port);

                //监控线程是否正常运行
                timerToCheckThread = new System.Timers.Timer(1000);
                timerToCheckThread.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
                timerToCheckThread.Enabled = true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (myThreadListen != null && myThreadListen.ThreadState != System.Threading.ThreadState.Stopped)
            {
                if (listeningSocket.Poll(50, SelectMode.SelectRead))
                    Console.WriteLine("准备新连接...");
                return;
            }
            try
            {
                //启动侦听线程,侦听客户端发来的网络数据流
                ThreadStart tsListen = new ThreadStart(AcceptClientConnect);
                myThreadListen = new Thread(tsListen);
                myThreadListen.IsBackground = true;
                // 使用全局变量listeningSocket是为了监控状态
                listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listeningSocket.Bind(servicePoint);
                myThreadListen.Start();
                Console.WriteLine("快充终端侦听线程自动开始在" + servicePoint.ToString() + "监听客户连接");
            }
            catch (Exception err)
            {
                Console.WriteLine("监听客户连接时出现异常" + err.Message);
                timerToCheckThread.Interval = 600000;   // on exception, retry every 10 minutes
            }
        }

        private void AcceptClientConnect()
        {
            Socket mySocket = null;

            //利用循环，侦听所有来访客户端连接
            listeningSocket.Listen(100);    // 限制等待的连接数
            while (true)
            {
                //Socket  newClient = null;
                try
                {
                    //等待用户进入
                    mySocket = listeningSocket.Accept();
                }
                catch (Exception err)
                {
                    Console.WriteLine("连接异常,退出侦听状态\r\n    " + err.Message);
                    break;
                }
                // TODO: 需要增加从网络崩溃中恢复的代码，办法是一旦发现上述异常退出，就应先Sleep(5)，给OS系统以恢复的时间
                // 然后重新扫描网卡、重新获得IP地址、重新启动侦听。这是配合网络维护而做的可维护性措施。
                //每接收一个客户连接，就创建一个对应的线程循环接受该客户端发来的信息
                ParameterizedThreadStart ptsClient = new ParameterizedThreadStart(ReceiveData);
                Thread threadReceive = new Thread(ptsClient);

                ClientUser user = new ClientUser(mySocket);
                threadReceive.IsBackground = true;  // make it non-blocking when close

                //启动用户线程，开始循环监听用户端发送来的消息
                threadReceive.Start(user);

                //添加到用户列表
                userList.Add(user);
                //                
                //TODO: 在用户信息列表中添加用户连接信息
            }
        }


        private void ReceiveData(object obj)
        {
            //接收数据线程，参数初始化
            //客户端连接对象，已经封装为包含相关信息的集合元素对象
            ClientUser user = (ClientUser)obj;
            //client = user.client;

            //用于控制是否接受线程
            bool isReceiving = true;
            //接受字节数组
            byte[] receivedBytes;
            //速通卡的物理编号，不包含网络编号
            byte[] byteCardID = new byte[8];
            //速通卡 网络编号
            byte[] byteCardNetID = new byte[2];

            //接收到的帧的整体长度
            byte length = 0;
            byte[] lengthByte = null;
            //接收到的帧的类型
            byte[] kindByte = null;
            byte kind = 0;
            //解析帧的游标位置
            int index = 0;
            //用于保存终端机编号,专门用来检索会话密钥
            ulong KEY = 0;

            //信息码
            byte inforID = (byte)0x00;


            //保存MAC值
            byte[] macCalculation = new byte[4];

            //时间戳
            byte[] nowTimeOfServer = new byte[8];   //前置机时间戳
            byte[] nowTimeOfPos = new byte[8];   //终端机时间戳


            //认证流程中前置机随机数,无符号整形,占4个字节 
            int randomOfServer = 0;
            //认证流程中终端机随机数,无符号整形,占4个字节 
            int randomOfPos = 0;

            //用于存储会话秘钥,将来会用内存数据库替代存储方式
            byte[] sessionKey = new byte[20];
            //循环侦听，直到出现异常，退出侦听
            string agentNo;
            string operatorNo;
            while (isReceiving)
            {
                //与客户端连接的一些局部变量初始化
                length = 0;
                lengthByte = new byte[1];
                kindByte = new byte[1];
                kind = 0;
                index = 0;

                try
                {
                    int buffCount = 0;
                    try
                    {
                        buffCount = user.clientSocket.Receive(kindByte, 1, SocketFlags.None);
                    }
                    catch
                    {
                        break;
                    }
                    //接收的字节数不为一,直接忽略跳过
                    if (buffCount < 1)
                        break;
                    kind = kindByte[0];

                    buffCount = user.clientSocket.Receive(lengthByte, 1, SocketFlags.None);//读取第二个字节，帧长度（包括帧类型）

                    //if (lengthByte == 0x00)



                    //if (buffCount < 1)
                    //    break;
                    //length = lengthByte[0];
                    //Console.WriteLine(user.strUserIPaddress + string.Format("帧的类型:{0}，长度:{1}", kind, length));

                    ////非法帧,忽略处理,扩充帧编号时，需要首先对该判断条件进行修改
                    //if (kind > 0x0A || length <= 2)
                    //    break;

                    ////接受字节数组，初始化 
                    //receivedBytes = new byte[length - 2];
                    ////从网络字节流中读取帧其余的字节
                    //if (user.clientSocket.Receive(receivedBytes, length - 2, SocketFlags.None) != length - 2)
                    //{
                    //    Console.WriteLine(user.strUserIPaddress + "接受指定长度的数据失败！ ");
                    //    Console.WriteLine(user.strUserIPaddress + "断开与该客户端的连接！ ");
                    //    break;
                    //}

                    //Console.WriteLine(user.strUserIPaddress + "收到的数据：");
                    //Console.WriteLine(user.strUserIPaddress + string.Format("收到的第 0 个数据：{0:X2} ", kind));
                    //Console.WriteLine(user.strUserIPaddress + string.Format("收到的第 1 个数据：{0:X2} ", length));
                    //Console.WriteLine(user.strUserIPaddress + "收到的数据：");
                    //string strLine = string.Format("POS:{0}\t", user.strUserIPaddress);
                    //string strLine1 = "";

                    //for (int i = 0; i < receivedBytes.Length; i++)
                    //{
                    //    strLine += receivedBytes[i].ToString("X2");
                    //    strLine1 += receivedBytes[i].ToString("X2");
                    //    if ((i & 0xF) == 0xF)    // output every 16 bytes.
                    //    {
                    //        Console.WriteLine(user.strUserIPaddress + strLine1);
                    //        strLine = string.Format("POS:{0}\t", user.strUserIPaddress);
                    //        strLine1 = "";
                    //    }
                    //}
                    //Console.WriteLine(user.strUserIPaddress + strLine1);
                }
                catch (Exception err)
                {

                }
            }
        }
    }
}
