using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using CommonProj;
using System.Configuration;
using Scodix.Kernel;
using Newtonsoft.Json;
using Scodix.PressToStudio;


using System.IO;
using System.ServiceModel;
using System.Runtime.InteropServices;
using ScodixServiceClient;

namespace WcfClientForm
{
    public partial class ClientForm : Form
    {
        Client m_client;
        private int m_clientId = 0;
        string m_serviceAddress = null;
        string m_servicePort = null;
        string m_streamingServicePort = null;
        string m_ripServicePort = null;
        FileTransferClient m_ftClient = null;      
        RipData m_ripData = null;
        public ClientForm()
        {
            InitializeComponent();
            m_client = new Client();
            m_client.dataRecivedEvent += RecieveData;         
        }

        private void RecieveData(Data d)
        {
            string dataRecieved = d.DataToSendAndRecieve;           
            if (dataRecieved == "connect")
            {
                ClientStatusTextBoxUpdate(dataRecieved);
                MessageRecievedTextBoxUpdate(dataRecieved);
            }
            else
            {
                MessageRecievedTextBoxUpdate(dataRecieved);
            }

        }

        public void ClientStatusTextBoxUpdate(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(ClientStatusTextBoxUpdate), new object[] { value });
                return;
            }
            ClientStatusTextBox.Text = value;
        }


        public void MessageRecievedTextBoxUpdate(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(MessageRecievedTextBoxUpdate), new object[] { value });
                return;
            }
            MessageRecievedTextBox.Text = value;
        }

        private void ClientConnectButton_Click(object sender, EventArgs e)
        {
            (new Thread(() => { m_client.StartClient(ClientIdTextBox.Text.ToInt32(), PortTextBox.Text, IPTextBox.Text); })).Start();
            
            string port = (Int32.Parse(m_streamingServicePort)).ToString();
            m_ftClient.Init(m_clientId, port, m_serviceAddress);
           
            port = (Int32.Parse(m_ripServicePort)).ToString();
            RipDataFileTransferClient.Instance().Init(m_clientId, port, m_serviceAddress);
           
            m_ripData = new RipData()
            {               
                Parameters = new Dictionary<string, string>()
            };
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            m_clientId = ConfigurationManager.AppSettings.Get("clientId").ToInt32();
            string configServiceAddress = ConfigurationManager.AppSettings.Get("ConfigServiceAddress");
            string configServicePort = ConfigurationManager.AppSettings.Get("ConfigServicePort");
            string configStreamingServicePort = ConfigurationManager.AppSettings.Get("ConfigStreamingServicePort");
            string configRipServicePort = ConfigurationManager.AppSettings.Get("ConfigRipServicePort");

            string useConfig = ConfigurationManager.AppSettings.Get("UseConfig");


            ClientIdTextBox.Text = m_clientId.ToString();
            IPTextBox.Text = configServiceAddress;
            PortTextBox.Text = configServicePort;

            m_serviceAddress = configServiceAddress;
            m_servicePort = configServicePort;
            m_streamingServicePort = configStreamingServicePort;
            m_ripServicePort = configRipServicePort;
            
            ClientManager.Instance.ServerDisconnectEvent += ServerDisconnect;
            RipDataFileTransferClient.Instance().ServerDisconnectEvent += RipServerDisconnect;
            m_ftClient = new FileTransferClient();
            m_ftClient.ServerDisconnectEvent += FTServerDisconnect;
        }

        private void FTServerDisconnect(object sender, string e)
        {
            DialogResult dialogResult = MessageBox.Show("FileTransferServerDisconnect", "FileTransferServerDisconnect", MessageBoxButtons.OK);
        }

        private void RipServerDisconnect(object sender, string e)
        {
            DialogResult dialogResult = MessageBox.Show("RipServerDisconnect", "RipServerDisconnect", MessageBoxButtons.OK);
        }

        private void ServerDisconnect(object sender, string e)
        {
            DialogResult dialogResult = MessageBox.Show("ServerDisconnect", "ServerDisconnect", MessageBoxButtons.OK);
        }

        private void StringToSendButton_Click(object sender, EventArgs e)
        {
            Data data = new Data();
            data.DataToSendAndRecieve = StringToSendTextBox.Text;
            data.clientId = m_clientId;

            m_client.SendData(data);
            
        }

        private void FileToSendButton_Click(object sender, EventArgs e)
        {
            bool IsBitMap = false;
            string temp = Path.GetExtension(PathFileToSendTextBox.Text);
            IsBitMap = Path.GetExtension(PathFileToSendTextBox.Text) == ".tif" ? true : false;
            StartRetriveFile(PathFileToSendTextBox.Text, PathToTextBox.Text, IsBitMap);
        }

        private void StartRetriveFile(string filePath, string dirToSaveTo, bool IsBitMap)
        {           
            try
            {                                        
                m_ftClient.RetrieveFile(filePath, dirToSaveTo, IsBitMap);                                           
            }
           
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);              
            }
        }



        private void BrowsButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string strFileName = openFileDialog.FileName;

                PathFileToSendTextBoxUpdate(strFileName);
            }
        }


        public void PathFileToSendTextBoxUpdate(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(PathFileToSendTextBoxUpdate), new object[] { value });
                return;
            }
            PathFileToSendTextBox.Text = value;
        }

     

        private void RetrieveRipDadaButton_Click(object sender, EventArgs e)
        {
            try
            {                
                string strFileName = null;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    strFileName = openFileDialog.FileName;                   
                }


                m_ripData.Name = "Test";
                m_ripData.Description = "Test of RIP server";
                string value = null;
                

                bool isKeyExists = m_ripData.Parameters.TryGetValue("RipTask", out value);
                if (!isKeyExists)
                {
                    m_ripData.Parameters.Add("RipTask", strFileName);
                }

                isKeyExists = m_ripData.Parameters.TryGetValue("RipToFile", out value);
                if (!isKeyExists)
                {
                    m_ripData.Parameters.Add("RipToFile", "C:\\temp\\MyTest2.pdf");
                }


                Bitmap rippedBitMap = RipDataFileTransferClient.Instance().RetrieveFile(m_ripData);
                rippedBitMap.Save("C:\\temp\\MyTest2.tif");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void BrowsButtonPathTo_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string pathName = openFileDialog.SelectedPath;

                PathToSendTextBoxUpdate(pathName);
            }
        }

        public void PathToSendTextBoxUpdate(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(PathToSendTextBoxUpdate), new object[] { value });
                return;
            }
            PathToTextBox.Text = value;
        }

    }
}
