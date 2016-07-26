using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace K.Y.DLL.Model
{
    public class M_SocketServer
    {
        public Socket MySocket { get; set; }
        public Int32 BufferLength = 1024;
        //是否处于工作状态
        public Boolean IsServered { get; private set; }
        //是否接受消息
        public Boolean IsReceived { get; private set; }
        public String ErrorMsg { get; private set; }

        public IPEndPoint ServerIPEndPoint { get; private set; }//本地ip和端口对
        public List<System.Net.Sockets.Socket> ClientEndPointList { get; private set; }

        //private System.Net.Sockets.Socket mySocket;//服务器侦听socket


        //用来实现回调函数
        //消息传送接收委托函数
        public delegate void DelegateMessageFunc(byte[] data, Socket ClientAddress);
        public DelegateMessageFunc MessageReceiveFunc;
        public DelegateMessageFunc MessageSendFunc;
        //socket连接关闭委托函数
        public delegate void DelegateServerFunc(Boolean IsSuccess);
        public DelegateServerFunc ServerOpenFunc;
        public DelegateServerFunc ServerCloseFunc;

        public delegate void DelegateErrorFunc(Exception ex);
        public DelegateErrorFunc ErrorFunc;



        public void ServerOpen(Int32 Port)
        { //如果监听服务已经打开~就不需要再重复打开了
            if (IsServered)
                return;
            //侦听规则~~~所有IP的Port端口数据
            ServerIPEndPoint = new IPEndPoint(IPAddress.Any, Port);//IPAddress.Any=>本机IP
            ClientEndPointList = new List<System.Net.Sockets.Socket>();
            //   ClientSessionList = new Hashtable(53);//Hashtable有许多其它的构造函数,但这是最常用的一个,注意我选择了一个不太觉的最初容量其大小为53,其原因是在字典中使用内部算法时,如果容量是一个素数(不能被其它整数整除的数,),他们的工作效率最高.

            //初始化SOCKET实例
            MySocket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //允许SOCKET被绑定在已使用的地址上。
            MySocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            try
            {
                //绑定
                MySocket.Bind(ServerIPEndPoint);//虚拟端口绑定到物理端口（否则随机分配？的意思？）
                //监听
                MySocket.Listen(10);
                IsServered = true;
            }
            catch (Exception e)
            {
                IsServered = false;
                ErrorMsg = "开启服务失败  " + e.Message;
                if (ErrorFunc != null)
                    ErrorFunc(e);
            }
            IsReceived = false;
            if (ServerOpenFunc != null)
                ServerOpenFunc(IsServered);
        }

        public void MessageReceive()
        {
            if (!IsReceived)
                try
                {
                    MySocket.BeginAccept(new AsyncCallback(AcceptConnect), new M_SocketState { Socket = MySocket, Buffer = new byte[BufferLength] });
                }
                catch (Exception e)
                {
                    if (ErrorFunc != null)
                        ErrorFunc(e);
                }
            IsReceived = true;
        }
        public void MessageReceive(Boolean IsEnableMsgReceive)
        {
            if (!IsReceived)
                try
                {
                    MySocket.BeginAccept(new AsyncCallback(AcceptConnect), new M_SocketState { Socket = MySocket, Buffer = new byte[BufferLength] });
                }
                catch (Exception e)
                {
                    if (ErrorFunc != null)
                        ErrorFunc(e);
                }
            IsReceived = IsEnableMsgReceive;
        }

        public void MessageSend(byte[] MessageInfo)
        {
            if (IsServered)
                new Thread(new ThreadStart(() =>
                {
                    ClientEndPointList.ForEach(p =>
                    {
                        ErrorMsg = "";
                        try
                        {
                            p.Send(MessageInfo);
                            if (MessageSendFunc != null)
                                MessageSendFunc(MessageInfo, p);
                        }
                        catch (SocketException e)
                        {
                            ErrorMsg += "连接服务器失败" + e.Message;
                            ClientEndPointList.Remove(p);
                            p.Dispose();
                            if (ErrorFunc != null)
                                ErrorFunc(e);
                        }
                    });
                })).Start();
        }
        public void MessageSend(byte[] MessageInfo, System.Net.Sockets.Socket User)
        {
            ErrorMsg = "";
            //int m_length = MessageInfo.Length;
            //_lUserEndPoint = lUser;
            //_MessageInfo = new byte[m_length];
            //_MessageInfo = Encoding.Unicode.GetBytes(MessageInfo);
            if (IsServered)
                new Thread(new ThreadStart(() =>
                {
                    try
                    {
                        ErrorMsg = "";
                        User.Send(MessageInfo);
                        if (MessageSendFunc != null)
                            MessageSendFunc(MessageInfo, User);
                    }
                    catch (SocketException e)
                    {
                        ErrorMsg = "连接服务器失败" + e.Message;
                        ClientEndPointList.Remove(User);
                        User.Dispose();
                        if (ErrorFunc != null)
                            ErrorFunc(e);
                    }
                })).Start();
            //if (IsServered)
            //    lUser.ForEach(p =>
            //    {
            //        try
            //        {
            //            var MsgSendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //            MsgSendSocket.BeginConnect(p, new AsyncCallback((arforsend) =>
            //            {
            //                #region arforsend匿名委托回调函数
            //                var socketforsend = ((Socket)arforsend.AsyncState);
            //                socketforsend.EndConnect(arforsend);
            //                socketforsend.Send(data);

            //                socketforsend.BeginDisconnect(true, new AsyncCallback((arfordispose) =>
            //                {
            //                    #region arfordispose匿名委托回调函数
            //                    var socketfordispose = ((Socket)arfordispose.AsyncState);
            //                    socketfordispose.EndDisconnect(arfordispose);
            //                    socketfordispose.Dispose();
            //                    #endregion
            //                }), socketforsend); 
            //                #endregion
            //            }), MsgSendSocket);
            //        }
            //        catch (Exception e)
            //        {
            //            if (ErrorFunc != null)
            //                ErrorFunc(e);
            //        }
            //    });
        }
        public void MessageSend(byte[] MessageInfo, List<System.Net.Sockets.Socket> lUser)
        {
            ErrorMsg = "";
            //int m_length = MessageInfo.Length;
            //_lUserEndPoint = lUser;
            //_MessageInfo = new byte[m_length];
            //_MessageInfo = Encoding.Unicode.GetBytes(MessageInfo);
            if (IsServered)
                new Thread(new ThreadStart(() =>
                {
                    lUser.ForEach(p =>
                    {
                        ErrorMsg = "";
                        try
                        {
                            p.Send(MessageInfo);
                        }
                        catch (SocketException e)
                        {
                            ErrorMsg += "连接服务器失败" + e.Message;
                            ClientEndPointList.Remove(p);
                            p.Dispose();
                            if (ErrorFunc != null)
                                ErrorFunc(e);
                        }
                    });
                })).Start();
            //if (IsServered)
            //    lUser.ForEach(p =>
            //    {
            //        try
            //        {
            //            var MsgSendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //            MsgSendSocket.BeginConnect(p, new AsyncCallback((arforsend) =>
            //            {
            //                #region arforsend匿名委托回调函数
            //                var socketforsend = ((Socket)arforsend.AsyncState);
            //                socketforsend.EndConnect(arforsend);
            //                socketforsend.Send(data);

            //                socketforsend.BeginDisconnect(true, new AsyncCallback((arfordispose) =>
            //                {
            //                    #region arfordispose匿名委托回调函数
            //                    var socketfordispose = ((Socket)arfordispose.AsyncState);
            //                    socketfordispose.EndDisconnect(arfordispose);
            //                    socketfordispose.Dispose();
            //                    #endregion
            //                }), socketforsend); 
            //                #endregion
            //            }), MsgSendSocket);
            //        }
            //        catch (Exception e)
            //        {
            //            if (ErrorFunc != null)
            //                ErrorFunc(e);
            //        }
            //    });
        }

        public void ConnectDispose()
        {
            // if (mySocket != null) mySocket = null;
            //MessageSend("Cmd:Close", ClientEndPointList);
            ClientEndPointList = null;
            MySocket.Dispose();
            IsReceived = false;
            IsServered = false;
            if (ServerCloseFunc != null)
                ServerCloseFunc(IsServered);

            Guid G = System.Guid.NewGuid();
         
        }

        private void AcceptConnect(IAsyncResult ar)
        {
            try
            {
                var ListenSocket = (M_SocketState)ar.AsyncState;
                //方法参考：http://msdn.microsoft.com/zh-cn/library/system.net.sockets.socket.endreceive.aspx
                var ClientSocket = ListenSocket.Socket.EndAccept(ar);


                if (!ClientEndPointList.Contains(ClientSocket)) ClientEndPointList.Add(ClientSocket);
                //接收客户端的消息(这个和在客户端实现的方式是一样的）
                var _ClientState = new M_SocketState { Socket = ClientSocket, Buffer = new byte[BufferLength] };
                _ClientState.Socket.BeginReceive(_ClientState.Buffer, 0, _ClientState.Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), _ClientState);

                //准备接受下一个客户端请求
                if (MySocket != null && IsReceived && IsServered)
                    ListenSocket.Socket.BeginAccept(new AsyncCallback(AcceptConnect), ListenSocket);
                //ListenSocket.Dispose();
                //ClientSocket.Dispose();
            }
            catch (Exception e)
            {
                if (ErrorFunc != null)
                    ErrorFunc(e);
            }
        }
        private void ReceiveMessage(IAsyncResult ar)
        {
            try
            {
                var _ClientState = (M_SocketState)ar.AsyncState;
                //方法参考：http://msdn.microsoft.com/zh-cn/library/system.net.sockets.socket.endreceive.aspx
                var length = _ClientState.Socket.EndReceive(ar);
                //读取出来消息内容
                //var ReceivedMsg = Encoding.Unicode.GetString(buffer, 0, length);
                ////显示消息
                //MessageTranslate(ReceivedMsg, ClientSocket);

                if (MessageReceiveFunc != null) MessageReceiveFunc(_ClientState.Buffer.Take(length).ToArray(), _ClientState.Socket);

                _ClientState.Buffer = new byte[BufferLength];
                if (_ClientState.Socket != null && IsReceived && IsReceived)
                    // ClientSocket.Connect(ClientSocket.RemoteEndPoint);
                    _ClientState.Socket.BeginReceive(_ClientState.Buffer, 0, _ClientState.Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), _ClientState);
                else
                    if (ErrorFunc != null)
                        ErrorFunc(new Exception(_ClientState.Socket.RemoteEndPoint + "连接已断开"));
            }
            catch (Exception e)
            {
                if (ErrorFunc != null)
                    ErrorFunc(e);
            }
        }

        //private byte[] _MessageInfo;
        //private List<System.Net.Sockets.Socket> _lUserEndPoint;
        //传送数据
        //enum MessageTypeTag { Cmd, Msg }
        //public void MessageSend(String MessageInfo, List<Socket> lUser, MessageTypeTag Tag)
        //{
        //    switch (Tag)
        //    {
        //        case MessageTypeTag.Cmd: MessageInfo = "Cmd:" + MessageInfo; break;
        //        case MessageTypeTag.Msg: MessageInfo = "Msg:" + MessageInfo; break;
        //    }
        //    MessageSend(MessageInfo, lUser);
        //}
        //关闭连接
        //private void MessageTranslate(String ReceivedMsg, Socket ConnectSocket)
        //{
        //    if (ReceivedMsg.StartsWith("Msg:"))
        //        //显示消息
        //        if (MessageReceiveFunc != null)
        //            MessageReceiveFunc(ReceivedMsg.Substring(4), ConnectSocket.RemoteEndPoint);

        //    if (ReceivedMsg.StartsWith("Cmd:"))
        //    {
        //        switch (ReceivedMsg)
        //        {
        //            case "Cmd:Close": { ClientEndPointList.Remove(ConnectSocket); ConnectSocket.Dispose(); } break;
        //        }
        //    }
        //}
    }

    public class M_SocketState
    {
        public byte[] Buffer { get; set; }
        public Socket Socket { get; set; }
    }
}
