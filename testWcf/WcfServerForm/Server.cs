using System;
using ScodixServiceHost;
using Newtonsoft.Json;
using  CommonProj;
using System.Drawing;

namespace WcfServerForm
{
    class Server
    {        
        int m_clientId = 0;
        string m_configServicePort = null;
        string m_configServiceAddress = null;
        public Server( int clientId, string configServicePort, string configServiceAddress)
        {          
            m_clientId = clientId;
            m_configServicePort = configServicePort;
            m_configServiceAddress = configServiceAddress;
        }

        public void StartServer()
        {                              
            try
            {               
                CommClientNotifier.Instance.StringDataToServerEvent += StringDataRecieved;

                ServiceProvider.Instance.Load(m_configServicePort, m_configServiceAddress);
                CommClientNotifier.Instance.SetRippedCallBack = new CommClientNotifier.rippedCallBack(TestRippedCallBack);
                Console.WriteLine("Waiting for connection...");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
          
        }    

        private Image TestRippedCallBack(string fullPathRippedFile, int index)
        {
            Image image = new Bitmap(500, 500);
            return image;
        }

        public void DeserializedFromJson(string data, ref ClientCommNotifyObject obj)
        {
            obj = JsonConvert.DeserializeObject<CommServiceData>(data).obj;
        }


        private void StringDataRecieved(int clientId, string data)
        {
            ClientCommNotifyObject obj = null;

            //Should deserialized the data and get the message and clientId
            DeserializedFromJson(data, ref obj);

            RecieveData(obj.m_data);
        }

        public void RecieveData(Data data)
        {
            string messageToClient = "Recived at server: ";
            string dataRecieved = DataSerializer.ToString(data);
            messageToClient += dataRecieved;
            
            data.DataToSendAndRecieve = messageToClient;
            data.clientId = m_clientId;
            SendData(data.clientId, data);              
        }

        public void SendData(int ClientId, Data d)
        {
            try
            {
                string messageToClient = DataSerializer.ToString(d);
                CommClientNotifier.Instance.SendDataToClient(ClientId, messageToClient);                      
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}

