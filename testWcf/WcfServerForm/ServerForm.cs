using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
//using Scodix.Kernel;

namespace WcfServerForm
{
    public partial class ServerForm : Form
    {

        Server m_server;
        private int m_clientId = 0;

        public ServerForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_clientId = Int32.Parse(ConfigurationManager.AppSettings.Get("clientId"));
            string configServiceAddress = ConfigurationManager.AppSettings.Get("ConfigServiceAddress");

            string configServicePort = ConfigurationManager.AppSettings.Get("ConfigServicePort");
           

            string useConfig = ConfigurationManager.AppSettings.Get("UseConfig");


            ClientIdTextBox.Text = m_clientId.ToString();
            IPTextBox.Text = configServiceAddress;
            PortTextBox.Text = configServicePort;

            m_server = new Server(m_clientId, configServicePort, configServiceAddress);
        }


        public void Start()
        {
            m_server.StartServer();
        }

        private void LoadServerButton_Click(object sender, EventArgs e)
        {
            Start();
            ServerStatusTextBox.Text = "Server is Up!!!";
            LoadServerButton.Enabled = false;
        }
    }
}
