using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MF900
{
    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 256;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }

    //服务端类
    public class TCPServer
    {
        //私有变量
        private SocketMessage m_SocketMsgDelegate = null;
        private Socket m_Listener = null;
        private Socket m_Handler = null;

        //公开变量
        public delegate void SocketMessage(string str);
        public string m_strInfo = null;

        public void InitServer(string strAddr, int nPort, SocketMessage sockMsg = null)
        {
            if (sockMsg != null)
            {
                m_SocketMsgDelegate = sockMsg;
            }

            //创建套接字  
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(strAddr), nPort);
            m_Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                m_Listener.Bind(ipe);
                m_Listener.Listen(10);
 
                //开启异步监听连接    
                m_Listener.BeginAccept(new AsyncCallback(AcceptCallback), m_Listener);
                //等待直到连接 
                if (m_SocketMsgDelegate!=null)
                    m_SocketMsgDelegate("Server On");//服务器启动
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                m_SocketMsgDelegate(e.Message);
            }
        }

        private void AcceptCallback(IAsyncResult iar)
        {
            Socket listener = (Socket)iar.AsyncState;
            m_Handler = listener.EndAccept(iar); //结束接收请求

            //创建状态对象    
            StateObject state = new StateObject();
            state.workSocket = m_Handler;

            m_strInfo = m_Handler.RemoteEndPoint.ToString();

            //发送信息
            if (m_SocketMsgDelegate != null)
            {
                m_SocketMsgDelegate("CLIENT ONLINE");
            }

            //开启数据回调
            m_Handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                String content = String.Empty;
                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.workSocket;

                if (handler!=null && handler.Connected)
                {
                    //读取数据   
                    int bytesRead = handler.EndReceive(ar);
                    if (bytesRead > 0)
                    {
                        state.sb.Append(Encoding.GetEncoding("GB2312").GetString(state.buffer, 0, bytesRead));
                        //state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                        content = state.sb.ToString();

                        if (m_SocketMsgDelegate != null)
                        {
                            m_SocketMsgDelegate(content);
                        }

                        //发送数据
                        Send(content);

                        state.sb.Clear();
                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                    }
                    else
                    {
                        if (m_SocketMsgDelegate != null)
                            m_SocketMsgDelegate("Client Off");  //客户端断开
                        m_Listener.BeginAccept(new AsyncCallback(AcceptCallback), m_Listener);
                    }
                }
                else
                {
                    if (m_SocketMsgDelegate != null)
                        m_SocketMsgDelegate("Server Off");  //服务器关闭
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.ToString());
                if (m_SocketMsgDelegate != null)
                {
                    string str = string.Format("{0} {1}", ex.ErrorCode, ex.Message);
                    m_SocketMsgDelegate(str);
                    m_SocketMsgDelegate("Client Off");
                }
                m_Listener.BeginAccept(new AsyncCallback(AcceptCallback), m_Listener);
            }
        }

        public void Send(String data)
        {
            //byte[] byteData = Encoding.ASCII.GetBytes(data);
            byte[] byteData = Encoding.GetEncoding("GB2312").GetBytes(data);
            m_Handler?.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), m_Handler);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                int bytesSent = handler.EndSend(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void CloseSocket()
        {
            try
            {
                if (m_Handler != null && m_Handler.Connected)
                {
                    m_Handler.Shutdown(SocketShutdown.Both);
                    m_Handler.Close();
                    m_Listener.Dispose();
                }
            }
            catch (Exception ex)
            { 
            }
        }
    }

    //客户端类
    public class TCPClient
    {
        public delegate void SocketMessage(string str);
        private SocketMessage m_SocketMsgDelegate = null;
        private Socket m_Client = null;
        private Socket m_Handler = null;
        private IPEndPoint m_ipe;
        public string m_strInfo;

        public void InitClient(string strAddr, int nPort, SocketMessage sockMsg = null)
        {
            if (sockMsg != null)
            {
                m_SocketMsgDelegate = sockMsg;
            }
            //端口及IP  
            m_ipe = new IPEndPoint(IPAddress.Parse(strAddr), nPort);
            //创建套接字  
            m_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //开始连接到服务器  
            m_Client.BeginConnect(m_ipe, new AsyncCallback(ConnectCallback), m_Client);
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                m_Handler = (Socket)ar.AsyncState;
                m_Handler.EndConnect(ar);

                if (m_SocketMsgDelegate != null)
                {
                    m_strInfo = m_Handler.LocalEndPoint.ToString();
                    m_SocketMsgDelegate("CONNECT");
                }

                //创建状态对象    
                StateObject state = new StateObject();
                state.workSocket = m_Handler;

                //开启数据回调
                m_Handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallBack), state);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ReceiveCallBack(IAsyncResult iar)
        {
            String content = String.Empty;
            StateObject state = (StateObject)iar.AsyncState;
            Socket handler = state.workSocket;

            try
            {
                if (handler != null && handler.Connected)
                {
                    //读取数据   
                    int bytesRead = handler.EndReceive(iar);
                    if (bytesRead > 0)
                    {
                        state.sb.Append(Encoding.GetEncoding("GB2312").GetString(state.buffer, 0, bytesRead));
                        //state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                        content = state.sb.ToString();

                        if (m_SocketMsgDelegate != null)
                        {
                            m_SocketMsgDelegate(content);
                        }

                        state.sb.Clear();
                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallBack), state);
                    }
                    else
                    {
                        if (m_SocketMsgDelegate != null)
                        {
                            m_SocketMsgDelegate("SERVER OFFLINE");
                        }

                        //关闭并且允许重复使用
                        m_Client.Disconnect(true);
                        //重新连接到服务器  
                        m_Client.BeginConnect(m_ipe, new AsyncCallback(ConnectCallback), m_Client);
                    }
                }
                else
                {
                    if (m_SocketMsgDelegate != null)
                    {
                        m_SocketMsgDelegate("CLIENT OFFLINE");
                    }
                }

            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
                if (m_SocketMsgDelegate != null)
                {
                    string str = string.Format("{0} {1}", ex.ErrorCode, ex.Message);
                    m_SocketMsgDelegate(str);
                    m_SocketMsgDelegate("DISCONNECT");
                }
            }
        }

        public void Send(String data)
        {
            try
            {
                //byte[] byteData = Encoding.ASCII.GetBytes(data);
                byte[] byteData = Encoding.GetEncoding("GB2312").GetBytes(data);
                m_Handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), m_Handler);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void CloseSocket()
        {
            try
            {
                if (m_Handler != null)
                {
                    m_Handler.Shutdown(SocketShutdown.Both);
                    m_Handler.Close();
                    m_Client.Dispose();
                }
            }
            catch (Exception ex)
            {
            }
            

        }
    }

    public class IPInfo
    {
        public string M_strIPAddr { get; set; }
        public int M_nIPPort { get; set; }
        public IPInfo()
        {

        }
        public IPInfo(string ip,int port)
        {
            M_strIPAddr = ip;
            M_nIPPort = port;
        }
    }
   
    public class SocketEx
    {
        public delegate void SocketMessage(string str);
        //定义服务器变量
        public TCPServer m_TcpServer = new TCPServer();
        //定义客户端变量
        public TCPClient m_TcpClient = new TCPClient();
        public bool m_bTcpServe = true;

        //ip地址和端口
        public IPInfo m_IPInfo;
        public SocketMessage m_SockMsg = null;

        //析构函数
        ~SocketEx()
        {
            CloseSocket();
        }
        public SocketEx(IPInfo m_IPInfo)
        {
            this.m_IPInfo = m_IPInfo;
        }

        public string GetDirectory()
        {
            string str = System.Environment.CurrentDirectory;
            str += "\\";
            return str;
        }

        //网口初始化   bTCPServer： true为服务器；false为客户端
        public bool InitSocket(SocketMessage sockMsg,bool bTcpServer)
        {
            try
            {
                m_SockMsg = new SocketMessage(sockMsg);
                m_bTcpServe = bTcpServer;
                if (m_bTcpServe)
                {
                    TCPServer.SocketMessage msg = new TCPServer.SocketMessage(RecvSockMsg);
                    m_TcpServer.InitServer(m_IPInfo.M_strIPAddr, m_IPInfo.M_nIPPort, msg);
                }
                else
                {
                    TCPClient.SocketMessage msg = new TCPClient.SocketMessage(RecvSockMsg);
                    m_TcpClient.InitClient(m_IPInfo.M_strIPAddr, m_IPInfo.M_nIPPort, msg);
                }
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
                return false;
            }
            return true;
        }

        //关闭网口
        public void CloseSocket()
        {
            if (m_bTcpServe)
                m_TcpServer.CloseSocket();
            else
                m_TcpClient.CloseSocket();
        }

        //接收网口信息
        private void RecvSockMsg(string str)
        {
            if (m_SockMsg != null)
            {
                m_SockMsg(str);
            }
        }

        //发送数据
        public void Send(string str)
        {
            if (m_bTcpServe)
            {
                m_TcpServer.Send(str);
            }
            else
            {
                m_TcpClient.Send(str);
            }
        }

        //获取网口信息
        public string GetTCPInfo()
        {
            return m_bTcpServe ? m_TcpServer.m_strInfo : m_TcpClient.m_strInfo;
        }
    }

}
