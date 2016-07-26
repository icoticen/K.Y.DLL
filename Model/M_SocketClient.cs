using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace K.Y.DLL.Model
{
    public class M_SocketClient
    {
        public byte[] Buffer { get; set; }
        public Int32 BufferLength { get; set; }
        public Socket MySocket { get; set; }


        IPEndPoint ServerIPEndPoint;
        //是否连接
        private bool IsConnected;
        //是否接受消息
        private bool IsReceived;

        public String ErrorMsg { get; private set; }

        public Action<byte[]> MessageReceiveFunc;

        public Action<Boolean> ConnectOpenFunc;
        public Action<Boolean> ConnectCloseFunc;

        public Action<Exception> ErrorFunc;

        //启动连接
        public void ConnectOpen(String ServerIP, int ServerPort)
        {
            //如果监听服务已经打开~就不需要再重复打开了
            if (IsConnected)
                return;
            //  byte[] data = new byte[1024];
            MySocket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //设置连接的对象地址==》对象
            ServerIPEndPoint = new IPEndPoint(IPAddress.Parse(ServerIP), ServerPort);
            try
            {
                MySocket.Connect(ServerIPEndPoint);
                IsConnected = true;
            }
            catch (SocketException e)
            {
                IsConnected = false;
                ErrorMsg = "连接服务器失败" + e.Message;
                if (ErrorFunc != null)
                    ErrorFunc(e);
            }
            IsReceived = false;

            if (ConnectOpenFunc != null)
                ConnectOpenFunc(IsConnected);
        }
        //接收数据
        public void MessageReceive(Boolean IsEnableMsgReceive)
        {
            MessageReceive(IsEnableMsgReceive, 1024);
        }
        public void MessageReceive(Boolean IsEnableMsgReceive, Int32 _BufferLength)
        {
            BufferLength = _BufferLength;
            if (!IsReceived)
                try
                {
                    Buffer = new byte[BufferLength];
                    MySocket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), MySocket);
                }
                catch (Exception e)
                {
                    if (ErrorFunc != null)
                        ErrorFunc(e);
                }
            IsReceived = IsEnableMsgReceive;
        }
        private void ReceiveMessage(IAsyncResult ar)
        {
            try
            {
                var ConnectSocket = (Socket)ar.AsyncState;
                //方法参考：http://msdn.microsoft.com/zh-cn/library/system.net.sockets.socket.endreceive.aspx
                var length = ConnectSocket.EndReceive(ar);
                //读取出来消息内容
                //var ReceivedMsg = Encoding.Unicode.GetString(Buffer, 0, length);

                if (MessageReceiveFunc != null) MessageReceiveFunc(Buffer.Take(length).ToArray());

                Buffer = new byte[BufferLength];
                //接收下一个消息(因为这是一个递归的调用，所以这样就可以一直接收消息了）
                if (MySocket != null && IsReceived && IsConnected)
                    MySocket.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), MySocket);
            }
            catch (Exception e)
            {
                if (ErrorFunc != null)
                    ErrorFunc(e);
            }
        }
        //传送数据
        public void MessageSend(byte[] data)
        {
            ErrorMsg = "";
            try
            {
                if (MySocket.Connected && IsConnected)
                    MySocket.Send(data);
                else ErrorMsg = "连接已断开";
            }
            catch (Exception e)
            {
                ErrorMsg = e.Message;
                if (ErrorFunc != null)
                    ErrorFunc(e);
            }
        }

        //public void MessageSend(byte[] DataSend, Int32 ReceiveLength, Action<byte[], Int32> F, Action<Exception> A)
        //{
        //    ErrorMsg = "";
        //    byte[] DataReturn = new byte[ReceiveLength];
        //    var _mySocket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //    try
        //    {
        //        _mySocket.Connect(ServerIPEndPoint);
        //        if (_mySocket.Connected)
        //        {
        //            _mySocket.BeginReceive(DataReturn, 0, DataReturn.Length, SocketFlags.None, new AsyncCallback(ar =>
        //          {
        //              try
        //              {
        //                  var ConnectSocket = (System.Net.Sockets.Socket)ar.AsyncState;
        //                  var length = ConnectSocket.EndReceive(ar);
        //                  if (F != null)
        //                      F(DataReturn, length);
        //              }
        //              catch (Exception e)
        //              {
        //                  if (A != null)
        //                      A(e);
        //                  _mySocket.Close();
        //                  _mySocket.Dispose();
        //              }
        //              finally
        //              {
        //              }
        //          }), _mySocket);
        //            _mySocket.Send(DataSend);
        //        }
        //        else if (A != null) A(new Exception("连接已断开"));
        //    }
        //    catch (Exception e)
        //    {
        //        if (A != null) A(e);
        //    }
        //}
        //关闭连接
        public void ConnectDispose()
        {
            MySocket.Close();
            MySocket.Dispose();
            IsReceived = false;
            IsConnected = false;
            if (ConnectCloseFunc != null)
                ConnectCloseFunc(IsConnected);
        }
    }
    //}

}
