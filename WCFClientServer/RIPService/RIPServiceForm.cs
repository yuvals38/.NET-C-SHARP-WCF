using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.Configuration;
using RIPCommon;

namespace RIPService
{
    public partial class RIPServiceForm : Form
    {
        IPublishing _proxy;
        int _eventCounter;
        ServiceHost _publishServiceHost = null;
        ServiceHost _subscribeServiceHost = null;
        public RIPServiceForm()
        {
            InitializeComponent();
            try
            {
                HostPublishService();
                HostSubscriptionService();
            }
            catch
            {
            }
            CreateProxy();
            _eventCounter = 0;
          
            txtEventData.Text = "event data";

            Filter.Instance.Filtered += OnFiltered;
            Filter.Instance.ClientConnected += OnClientConnected;
            Filter.Instance.ClientDisConnected+= OnClientDisconnected;
            Filter.Instance.ResponseSent += OnRequestSent;
        }

        private void OnRequestSent(object sender, EventArgs e)
        {
            string s = ((ResponseEventArgs)e).S;
        }

        private void OnClientDisconnected(object sender, EventArgs e)
        {
            int reqid = 1;
            int responseid = 2;
            SerializableDictionary<int, string> aa = new SerializableDictionary<int, string>();
            aa.Add(1, "param_1");
            Random r = new Random();

            RequestType t = RequestType.Disconnect;

            RIPMessage alertData = PrepareEvent("moti", "oleg", reqid, responseid, t, aa);
            alertData.EventData = DateTime.Now.ToString();
            _proxy.Publish(alertData, "RegisterForAnyEvents");
            _eventCounter += 1;
        }

        private void OnClientConnected(object sender, EventArgs e)
        {
            int reqid = 1;
            int responseid = 2;
            SerializableDictionary<int, string> aa = new SerializableDictionary<int, string>();
            aa.Add(1, "param_1");
            Random r = new Random();

            RequestType t = RequestType.Connect;

            RIPMessage alertData = PrepareEvent("moti", "oleg", reqid, responseid, t, aa);
            alertData.EventData = DateTime.Now.ToString();
            _proxy.Publish(alertData, "RegisterForAnyEvents");
            _eventCounter += 1;
        }

        private Timer t;
        private void OnFiltered(object sender, FilterEventArgs e)
        {
            if(t != null)
            {
                t.Tick -= T_Tick;
                t.Enabled = false;
                t.Dispose();
                t = null;
            }
            t = new Timer();
            t.Interval = 500;
            t.Tick += T_Tick;
            t.Enabled = true;
           
        }
        
        private void T_Tick(object sender, EventArgs e)
        {           
            try
            {
                int reqid = 1;
                int responseid = 2;
                SerializableDictionary<int, string> aa = new SerializableDictionary<int, string>();
                aa.Add(1, "param_1");
                Random r = new Random();
                
                RequestType t = (RequestType)r.Next(2,6);

                RIPMessage alertData = PrepareEvent("moti", "oleg", reqid, responseid, t, aa);
                alertData.EventData = DateTime.Now.ToString();
                _proxy.Publish(alertData, "RequestMessage");
                _eventCounter += 1;
                txtEventCount.Text = _eventCounter.ToString();
                
            }
            catch { }
            t.Enabled = false;
        }

        private void HostSubscriptionService()
        {
            _subscribeServiceHost = new ServiceHost(typeof(Subscription));
            NetTcpBinding tcpBinding = new NetTcpBinding(SecurityMode.None);

            _subscribeServiceHost.AddServiceEndpoint(typeof(ISubscription), tcpBinding,
                                "net.tcp://localhost:7002/Sub");
            _subscribeServiceHost.Open();
        }

        private void HostPublishService()
        {
            _publishServiceHost = new ServiceHost(typeof(Publishing));
            NetTcpBinding tcpBindingpublish = new NetTcpBinding();

            _publishServiceHost.AddServiceEndpoint(typeof(IPublishing), tcpBindingpublish,
                                    "net.tcp://localhost:7001/Pub");
            _publishServiceHost.Open();
        }
        private void CreateProxy()
        {
            string endpointAddressInString = ConfigurationManager.AppSettings["EndpointAddress"];
            EndpointAddress endpointAddress = new EndpointAddress(endpointAddressInString);
            NetTcpBinding netTcpBinding = new NetTcpBinding();
            _proxy = ChannelFactory<IPublishing>.CreateChannel(netTcpBinding, endpointAddress);
        }

        void SendEvent(object sender, EventArgs e)
        {
            try
            {
                string request = "RequestMessage";
                if (string.IsNullOrEmpty(request))
                {
                    MessageBox.Show("Please Enter a request ");
                    return;
                }
                int reqid = 1;
                int responseid = 2;
                SerializableDictionary<int, string> aa = new SerializableDictionary<int, string>();
                aa.Add(1, "requested message");
                RIPMessage alertData = PrepareEvent(txtEventData.Text, "oleg", reqid, responseid, RequestType.Ciao, aa);
               
                _proxy.Publish(alertData, request);
                _eventCounter += 1;
                txtEventCount.Text = _eventCounter.ToString();

                
            }
            catch { }
        }

        private RIPMessage PrepareEvent(string sender , string receiver, int reqId, int responseId, RequestType type, SerializableDictionary<int, string> data)
        {
            RIPMessage e = new RIPMessage(new object());
           
            RequestMessage msg = new RequestMessage()
            {
                SenderID = sender,
                ReceiverID = receiver,
                TimeOfSending = DateTime.Now,
                RequestID = reqId,
                RequestType = type,
                DataParams = data,
               
            };

            //msg.DataParams.Add(1, "dddd");
            string xmlString = GenericDataContractSerializer<RequestMessage>.SerializeObject(msg);

            e.Request = xmlString;
            return e;
        }

        void SendAutoEvent(object sernder, EventArgs e)
        {
            string request = "RequestMessage";
            if (string.IsNullOrEmpty(request))
            {
                MessageBox.Show("Please Enter a request Name");
                return;
            }
            txtTopicName.ReadOnly = true;
            int interval = 1000;
            tmrEvent.Interval = interval;
            tmrEvent.Enabled = true;
        }

        void StopAutoEvent(object sender, EventArgs e)
        {
            if (tmrEvent.Enabled)
                tmrEvent.Enabled = false;
            txtTopicName.ReadOnly = false;
        }

        void OnResetCounter(object sender, EventArgs e)
        {
            _eventCounter = 0;
            txtEventCount.Text = _eventCounter.ToString();
        }

        private void tmrEvent_Tick(object sender, EventArgs e)
        {
            SendEvent(sender, e);
        }
    }
}