using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using CommonProj;
using System.IO;
using System.Threading;
using ScodixServiceClient;
using Newtonsoft.Json;

namespace WcfClientForm
{
    public class Client
    {
        
        public delegate void DataProcessor(Data d);

        public delegate void DataRecived(Data d);        
        public event DataRecived dataRecivedEvent;

        public event ClientCommManager.SendDataToServer SendDataEvent;

        byte[] m_bytes;

        ClientCommManager m_wcfClientManager;

        
        int m_clientId = 0; //Need to read from Configuration.

        public Client()
        {
            m_clientId = 1;
            m_bytes = new byte[2048];
            m_wcfClientManager = new ClientCommManager(this, m_clientId);
        }

        public void StartClient(int clientId, string configServicePort, string configServiceAddress)
        {            
            try
            {               
              
                try
                {
                    // Connect to Remote EndPoint                                    
                    ClientManager.Instance.Load(clientId, configServicePort, configServiceAddress);
                    ClientManager.Instance.MessageRecievedEvent += RecieveData;


                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void RecieveData(object sender, string message)
        {
            try
            {
                Data data = new Data();
                data.DataToSendAndRecieve = message;                              
                dataRecivedEvent.Invoke(data);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }


        public void SendData(Data d)
        {
            try
            {
                new Thread(delegate () {
                    SendDataEvent.Invoke(d);
                }).Start();                                     
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

    }
}
