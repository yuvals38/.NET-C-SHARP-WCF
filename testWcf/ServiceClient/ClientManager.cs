using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Scodix.PressToStudio;
using System.Collections.Concurrent;
using System.Timers;
using Scodix.Kernel;
using Scodix.LoggingSystem;

namespace ScodixServiceClient
{
    public class ClientManager : IPublishing
    {        
        ISubscription _proxySub;
      

        private int m_clientId;
        private ServiceManager m_serviceManager;            
        private DuplexChannelFactory<ISubscription> m_channelFactory;       
        private static ClientManager m_Instance = null;

        public event EventHandler<string> MessageRecievedEvent;
        public event EventHandler<string> ServerDisconnectEvent;

        public static ClientManager Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new ClientManager();
                }
                return m_Instance;
            }
        }

        private ClientManager()
        {
        }

        private void Init(int clientId, string port, string address)
        {
            m_clientId = clientId;
           
            string endpointSub = "net.tcp://" + address + ":" + port + "/ServiceManagerSubscriber";
            MakeProxySub(endpointSub, this);
            
        }

        private void MakeProxySub(string EndpoindAddress, object callbackinstance)
        {
            NetTcpBinding netTcpbinding = new NetTcpBinding(SecurityMode.None);
            netTcpbinding.MaxReceivedMessageSize = int.MaxValue;
            netTcpbinding.MaxBufferSize = int.MaxValue;        

            EndpointAddress endpointAddress = new EndpointAddress(EndpoindAddress);
            InstanceContext context = new InstanceContext(callbackinstance);
            m_channelFactory = new DuplexChannelFactory<ISubscription>(new InstanceContext(this), netTcpbinding, endpointAddress);            
            _proxySub = m_channelFactory.CreateChannel();

            Subscribe(this, null);        

            ((ICommunicationObject)_proxySub).Faulted -= new EventHandler(Channel_Faulted);
            ((ICommunicationObject)_proxySub).Faulted += new EventHandler(Channel_Faulted);
        }

    
        private void Channel_Faulted(object sender, EventArgs e)
        {         
            ServerDisconnectEvent?.Invoke(this, "ServerDisconnect");
        }

        private void Subscribe(object sender, EventArgs e)
        {
            try
            {              
                _proxySub.Subscribe(m_clientId);
                LogServer.LogStatus(STT.General.INFO_GENERAL, $"New Client request to subscribe client ID= {m_clientId}");
            }
            catch
            {
            }
        }     

        public void Notify(int clientId, string data)
        {
            try
            {
                LogServer.LogStatus(STT.General.INFO_GENERAL, $"Data from client ID= {m_clientId} to server side had been sent.");
                _proxySub.SendData(clientId, data);                
            }
            catch (Exception ex)
            {
                //Add something to log
            }
        }


        public void Load(int clientId, string port, string address)
        {
            Init(clientId, port, address);
            m_serviceManager = new ServiceManager(m_clientId);
            LogServer.LogStatus(STT.General.INFO_GENERAL, $"ServiceManager had been loaded.");
        }

        public void ServerNotify(string state)
        {
            try
            {
                MessageRecievedEvent?.Invoke(this, state);
                LogServer.LogStatus(STT.General.INFO_GENERAL, $"Notification from client sent to client side");
            }
            catch (Exception e)
            {
                string s = e.Message;
            }            
        }

        public void ReflectionNotify(ServiceType type, string data)
        {
            throw new NotImplementedException();
        }
    }
}

