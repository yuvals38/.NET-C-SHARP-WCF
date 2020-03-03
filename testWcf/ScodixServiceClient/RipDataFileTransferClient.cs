using Scodix.PressToStudio;
using System;
using System.Drawing;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Timers;

namespace ScodixServiceClient
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RipDataFileTransferClient : IPublishing 
    {
        private int m_clientId;

        private ChannelFactory<IRipData> m_channelFactory;
        IRipData _proxySubRip;

        public event EventHandler<string> ServerDisconnectEvent;

        private static object m_Sync = new object();
        private static RipDataFileTransferClient m_Instance;
        private Timer m_keepAliveTimer;
        public static RipDataFileTransferClient Instance()
        {
            lock (m_Sync)
            {
                if (m_Instance == null)
                    m_Instance = new RipDataFileTransferClient();
                return m_Instance;
            }
        }

        public void Init(int clientId, string port, string address)
        {
            m_clientId = clientId;
            string endpointSubStreaming = $"net.tcp://{address}:{port}/ServiceManagerSubscriberRip";
            MakeProxySubStreaming(endpointSubStreaming, this);
        }

        private void MakeProxySubStreaming(string EndpoindAddress, object callbackinstance)
        {
            NetTcpBinding netTcpbinding = new NetTcpBinding();
            netTcpbinding.MaxReceivedMessageSize = int.MaxValue;
            netTcpbinding.MaxBufferSize = int.MaxValue;
            EndpointAddress endpointAddress = new EndpointAddress(EndpoindAddress);

            netTcpbinding.TransferMode = TransferMode.Streamed;
            //netTcpbinding.ReceiveTimeout = TimeSpan.MaxValue;
            //netTcpbinding.CloseTimeout = new TimeSpan(0, 30, 0);
            //netTcpbinding.SendTimeout = TimeSpan.MaxValue;
            //netTcpbinding.OpenTimeout = new TimeSpan(0, 30, 0);
            netTcpbinding.MaxBufferSize = 2147483647;
            netTcpbinding.MaxReceivedMessageSize = 2147483647;
            netTcpbinding.Security.Mode = SecurityMode.None;

            netTcpbinding.ReaderQuotas.MaxStringContentLength = 2147483647;
            netTcpbinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            netTcpbinding.ReaderQuotas.MaxArrayLength = 2147483647;

            InstanceContext context = new InstanceContext(callbackinstance);            
            m_channelFactory = new ChannelFactory<IRipData>( netTcpbinding, endpointAddress);

            _proxySubRip = m_channelFactory.CreateChannel();

          
            keepAlive();
           // CommunicationState state = m_channelFactory.State;
        }


        public void keepAlive()
        {
            //KeepAlive should be invoke at least once each 2 seconds.
            m_keepAliveTimer = new Timer(5000);
            m_keepAliveTimer.Elapsed += KeepAliveTimer_Elapsed;
            m_keepAliveTimer.Start();
        }

        private void KeepAliveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                CommunicationState state = m_channelFactory.State;
                _proxySubRip.KeepAlive();
            }
            catch (Exception ex)
            {
                m_keepAliveTimer.Stop();
                m_keepAliveTimer.Close();
                m_keepAliveTimer.Elapsed -= KeepAliveTimer_Elapsed;
                Channel_Faulted(this, null);
            }
        }


        private void Channel_Faulted(object sender, EventArgs e)
        {           
            //Server disconnected - should reconnect
            ServerDisconnectEvent?.Invoke(this, "ServerDisconnect");
        }

        public Bitmap RetrieveFile(RipData ripData)
        {
            ResponseFileDetails retVal = _proxySubRip.RetrieveRipFileFromServer(ripData);
            Bitmap image = new Bitmap(retVal.FileByteStream);
            return image;
        }      

        public void ServerNotify(string state)
        {
            throw new System.NotImplementedException();
        }

        public void ReflectionNotify(ServiceType type, string data)
        {
            throw new System.NotImplementedException();
        }
    }

}