using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Scodix.PressToStudio;

namespace ScodixServiceHost
{
    public class ReflectionServiceProvider
    {
        ServiceHost _subscriptionServiceHost = null;
        ReflectionServiceManager reflectionServiceManager;

        public ReflectionServiceProvider()
        {
            try
            {
                HostSubscriptionService();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            //CreateProxy();
        }
        public ReflectionServiceManager GetReflectionServiceManager()
        {
            return this.reflectionServiceManager;
        }
        private void HostSubscriptionService()
        {
            reflectionServiceManager = new ReflectionServiceManager();
            _subscriptionServiceHost = new ServiceHost(reflectionServiceManager/*typeof(ReflectionServiceManager)*/);
            NetTcpBinding tcpBinding = new NetTcpBinding(SecurityMode.None);
            tcpBinding.MaxBufferPoolSize = 50000000;
            tcpBinding.MaxBufferSize = 50000000;
            tcpBinding.MaxReceivedMessageSize = 50000000;
            tcpBinding.ReliableSession.Enabled = true;
            tcpBinding.ReliableSession.InactivityTimeout = TimeSpan.FromMinutes(10);
            tcpBinding.ReceiveTimeout = TimeSpan.FromMinutes(10);

            _subscriptionServiceHost.AddServiceEndpoint(typeof(ISubscription), tcpBinding,
                                "net.tcp://localhost:7052/ReflectionServiceManager");
            _subscriptionServiceHost.Open();
        }
    }
}
