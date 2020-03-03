using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Scodix.PressToStudio;
using Scodix.Kernel;
using Scodix.LoggingSystem;
using System.ServiceModel.Description;
using System.Configuration;

namespace ScodixServiceHost
{
    public class ServiceProvider 
    {
        private static ServiceProvider m_Instance = null;        

        private ServiceProvider()
        {
        }

        public static ServiceProvider Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new ServiceProvider();
                }
                return m_Instance;
            }

        }

        private void HostSubscribeService(string port, string address)
        {

            ServiceHost subscriptionServiceHost = new ServiceHost(typeof(CommClientNotifier));
            NetTcpBinding tcpBinding = new NetTcpBinding(SecurityMode.None);
            
            string endpointSub = "net.tcp://" + address + ":" + port + "/ServiceManagerSubscriber";

            subscriptionServiceHost.AddServiceEndpoint(typeof(ISubscription), tcpBinding, endpointSub);

            subscriptionServiceHost.Open();
        }


        private void HostSubscribeServiceStreaming(string port, string address)
        {
            port = (Int32.Parse(port)).ToString();
            ServiceHost subscriptionServiceHost = new ServiceHost(typeof(CommClientNotifier));
            NetTcpBinding tcpBinding = new NetTcpBinding(SecurityMode.None);
            tcpBinding.Security.Message.ClientCredentialType = MessageCredentialType.None;
            tcpBinding.Security.Transport.ClientCredentialType = TcpClientCredentialType.None;
            tcpBinding.ReaderQuotas.MaxDepth = 32;
            tcpBinding.ReaderQuotas.MaxStringContentLength = 8192;
            tcpBinding.ReaderQuotas.MaxArrayLength = 555555555;
            tcpBinding.ReaderQuotas.MaxBytesPerRead = 4096;
            tcpBinding.ReaderQuotas.MaxNameTableCharCount = 79623599;
            tcpBinding.TransferMode = TransferMode.Streamed;

            ServiceMetadataBehavior behaviour = new ServiceMetadataBehavior();
            behaviour.HttpGetEnabled = false;
            subscriptionServiceHost.Description.Behaviors.Add(behaviour);

            string endpointSub = "net.tcp://" + address + ":" + port + "/ServiceManagerSubscriberStreaming";

            subscriptionServiceHost.AddServiceEndpoint(typeof(IFileTransfer), tcpBinding, endpointSub);           

            subscriptionServiceHost.Open();
        }

        private void HostSubscribeServiceRip(string port, string address)
        {
            port = (Int32.Parse(port)).ToString();
            ServiceHost subscriptionServiceHost = new ServiceHost(typeof(CommClientNotifier));
            NetTcpBinding tcpBinding = new NetTcpBinding(SecurityMode.None);
            tcpBinding.Security.Message.ClientCredentialType = MessageCredentialType.None;
            tcpBinding.Security.Transport.ClientCredentialType = TcpClientCredentialType.None;
            tcpBinding.ReaderQuotas.MaxDepth = 32;
            tcpBinding.ReaderQuotas.MaxStringContentLength = 8192;
            tcpBinding.ReaderQuotas.MaxArrayLength = 555555555;
            tcpBinding.ReaderQuotas.MaxBytesPerRead = 4096;
            tcpBinding.ReaderQuotas.MaxNameTableCharCount = 79623599;
            tcpBinding.TransferMode = TransferMode.Streamed;

            ServiceMetadataBehavior behaviour = new ServiceMetadataBehavior();
            behaviour.HttpGetEnabled = false;
            subscriptionServiceHost.Description.Behaviors.Add(behaviour);

            string endpointSub = "net.tcp://" + address + ":" + port + "/ServiceManagerSubscriberRip";

            subscriptionServiceHost.AddServiceEndpoint(typeof(IRipData), tcpBinding, endpointSub);

            subscriptionServiceHost.Open();
        }

        public void Load(string port, string address)
        {
            try
            {
                
                string configStreamingServicePort = ConfigurationManager.AppSettings.Get("ConfigStreamingServicePort");
                string configRipServicePort = ConfigurationManager.AppSettings.Get("ConfigRipServicePort");

                HostSubscribeService(port, address);
                HostSubscribeServiceStreaming(configStreamingServicePort, address);
                HostSubscribeServiceRip(configRipServicePort, address);

                LogServer.LogStatus(STT.General.INFO_GENERAL, $"ServiceProvider Address= {address}, Port = {port} loaded successfuly.");
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
        }   
    }
}

