using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonProj;
using Newtonsoft.Json;
using ScodixServiceClient;

namespace WcfClientForm
{
    public class ClientCommManager : ServiceManager
    {
        public delegate void SendDataToServer(Data d);

        public ClientCommNotifyObject m_CommObj;
        private ServiceData m_serviceData;   
        
        Client m_client;
        int m_clientId;
        public ClientCommManager(Client client, int clientId) : base(clientId)
        {
            m_client = client;
            m_clientId = clientId;
            m_CommObj = new ClientCommNotifyObject();
            m_serviceData = new ServiceData();
            ClientRegisterMessages();
        }

        public override void ClientRegisterMessages()
        {
            //This is the Registration:
            m_client.SendDataEvent += OnDataToSend;
        }

        private void OnDataToSend(Data data)
        {
            m_CommObj.m_data = data;
            NotifyHost();
        }


        override public string SerializeToJson()
        {
            m_serviceData.obj = m_CommObj;

            string actualJsonData = JsonConvert.SerializeObject(m_serviceData);
            return actualJsonData;
        }

        override public void NotifyHost()
        {
            string jsonStr = SerializeToJson();
            ClientManager.Instance.Notify(m_clientId, jsonStr);
        }
    }
}
