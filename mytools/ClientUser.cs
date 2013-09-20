using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;

namespace mytools
{
    class ClientUser
    {
        public const int DEFAULT_READ_TIMEOUT = 150000; // 150 seconds, to cooperate with the GRPS wireless network.
        public const int DEFAULT_WRITE_TIMEOUT = 30000; // 30 seconds
        public const int DEFAULT_RECEIVEBUFFER_SIZE = 2048;    //接受缓冲区大小
        public const int DEFAULT_BINARYBUFFER_SIZE = 8096;    //接受缓冲区大小

        public TcpClient client;                    //Tcp客户端
        public Socket clientSocket;              //Socket客户端
        public NetworkStream netWorkStream;             //网路数据流
        public System.Net.EndPoint userIPaddress;       //专门用于存储该用户接入的IP地址
        public string strUserIPaddress = "";

        public int chanllengeInServer = 0;         //前置机服务器随机数
        public int chanllengeInPos = 0;            //终端机随机数
        public bool isValidatePosOK = false;     //终端机认证成功否
        public bool isValidateServerOK = false;     //前置机认证成功否

        public int amountForLoad = 0;              //圈存金额
        public int amountForPreAssign = 0;         //返回数2
        public int amountForAdd = 0;               //充值金额

        public int randomOfServer = 0;                //服务器随机数
        public int randomOfPos = 0;                //终端机随机数

        public byte[] sessionKey = new byte[20];      //会话密钥间
        public DateTime logTime;                       //连接客户端登入时间

        public ClientUser(TcpClient client)
        {
            this.client = client;
            client.NoDelay = false;
            netWorkStream = client.GetStream();
            netWorkStream.WriteTimeout = DEFAULT_WRITE_TIMEOUT;
            netWorkStream.ReadTimeout = DEFAULT_READ_TIMEOUT;
            userIPaddress = client.Client.RemoteEndPoint;
            strUserIPaddress = client.Client.RemoteEndPoint.ToString();
        }

        public ClientUser(Socket client)
        {
            this.clientSocket = client;
            clientSocket.NoDelay = false;
            clientSocket.SendTimeout = DEFAULT_WRITE_TIMEOUT;
            clientSocket.NoDelay = true;    // disable the Nagle algorithm for better response time
            //接收延时
            clientSocket.ReceiveTimeout = DEFAULT_READ_TIMEOUT;
            // 关闭前逗留时间仅1 second。注意单位与发送延迟或接收延迟不同，设置得足够短                
            //Gracefully close without lingering
            clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);
            //接受缓冲区大小
            clientSocket.ReceiveBufferSize = DEFAULT_BINARYBUFFER_SIZE;
            //禁止接受带外数据
            clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.OutOfBandInline, false);
            userIPaddress = clientSocket.RemoteEndPoint;
            strUserIPaddress = clientSocket.RemoteEndPoint.ToString();

            logTime = DateTime.Now;         //连接客户端登入时间
        }
    }
}
