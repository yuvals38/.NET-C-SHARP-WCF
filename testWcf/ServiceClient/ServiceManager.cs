using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Newtonsoft.Json;
using Scodix.Kernel;
using Scodix.LoggingSystem;

namespace ScodixServiceClient
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceManager
    {
        public NotifyObject m_obj;
        private ServiceData m_serviceData;
        public ServiceManager(int clientId)
        {
            try
            {
                m_serviceData = new ServiceData();
                m_obj = new NotifyObject();
                m_obj.m_clientId = clientId;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
        }

        public virtual void ClientRegisterMessages()
        {
        }

        virtual public string SerializeToJson()
        {
            LogServer.LogStatus(STT.General.INFO_GENERAL, $"Serializing data from client ID= {m_obj.m_clientId}");
            m_serviceData.obj = m_obj;

            string actualJsonData = JsonConvert.SerializeObject(m_serviceData);
            return actualJsonData;
        }

        virtual public void NotifyHost()
        {
            string jsonStr = SerializeToJson();
            ClientManager.Instance.Notify(m_obj.m_clientId, jsonStr);
            LogServer.LogStatus(STT.General.INFO_GENERAL, $"ClientManager notify server with serializing data from client ID= {m_obj.m_clientId}");
        }
        
    }
}
