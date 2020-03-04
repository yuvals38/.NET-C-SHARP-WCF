using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;

using System.Diagnostics;

using System.Configuration;
using RIPCommon;

namespace RIPEngine
{
 
    public partial class RIPTask : Form, IPublishing
    {
        ISubscription _proxy;
        static int _eventCount;
        string _endpoint = string.Empty;

        public RIPTask()
        {
            InitializeComponent();
            _endpoint = ConfigurationManager.AppSettings["EndpointAddress"];
            MakeProxy(_endpoint, this);
            _eventCount = 0;
            OnAny();
           
        }   


        public void MakeProxy(string EndpoindAddress, object callbackinstance)
        {
            NetTcpBinding netTcpbinding = new NetTcpBinding(SecurityMode.None);
            EndpointAddress endpointAddress = new EndpointAddress(EndpoindAddress);
            InstanceContext context = new InstanceContext(callbackinstance);        

            DuplexChannelFactory<ISubscription> channelFactory = new DuplexChannelFactory<ISubscription>(new InstanceContext(this), netTcpbinding, endpointAddress);
            _proxy  = channelFactory.CreateChannel();
        }

        void OnAny()
        {
            try
            {
                string register = "RegisterForAnyEvents";
                _proxy.Subscribe(register);              
            }
            catch
            {

            }
        }


        void OnSubscribe(object sender, EventArgs e)
        {
            try
            {
                string topicName = "RequestMessage";
               
                _proxy.Subscribe(topicName);
                ((Button)sender).Visible = false;
                button2.Visible = true;
            }
            catch
            {

            }
        }

        void OnUnSubscribe(object sender, EventArgs e)
        {
            string topicName = txtTopicName.Text.Trim();
            if (string.IsNullOrEmpty(topicName))
            {
                MessageBox.Show("Please Enter a request Name");
                return;
            }
            ((Button)sender).Visible = false;
            button1.Visible = true;
            _proxy.UnSubscribe(topicName);
        }



        private void btnClearAstaListView_Click(object sender, EventArgs e)
        {
            lstEvents.Items.Clear();
        }

        #region IMyEvents Members

        public void Publish(RIPMessage e, String topicName)
        {
            if (e != null)
            {
                int itemNum = (lstEvents.Items.Count < 1) ? 0 : lstEvents.Items.Count;
                lstEvents.Items.Add(itemNum.ToString());

                RequestMessage p = GenericDataContractSerializer<RequestMessage>.DeserializeXml(e.Request);

            //    lstEvents.Items[itemNum].SubItems.AddRange(new string[] { "sender id : " + p.SenderID + ", receiver id : " + p.ReceiverID  + ", req type is : " + 
            //                                                              p.RequestType.ToString() + " data : " +p.DataParams[1]  , e.EventData });

              lstEvents.Items[itemNum].SubItems.AddRange(new string[] { "sender id : " + p.SenderID , ", receiver id : " + p.ReceiverID  , ", req type is : " + 
                                                                          p.RequestType.ToString() , " data : " +p.DataParams[1]  , e.EventData });



                _eventCount += 1;
                txtAstaEventCount.Text = _eventCount.ToString();

            }
        }

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            _proxy.Response("OK!!!!!");
            
        }
    }

}