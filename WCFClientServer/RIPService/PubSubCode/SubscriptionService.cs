using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using RIPCommon;

namespace RIPService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class Subscription : ISubscription
    {
       
        #region ISubscription Members

        public void Subscribe(string request)
        {
            IPublishing Client = OperationContext.Current.GetCallbackChannel<IPublishing>();
            Filter.Instance.AddClient(request, Client);
        }

        public void UnSubscribe(string request)
        {
            IPublishing Client = OperationContext.Current.GetCallbackChannel<IPublishing>();
            Filter.Instance.RemoveClient(request, Client);
        }
        public void Response(string s)
        {
            Filter.Instance.RequestReceived(s);
        }
        #endregion
    }
}
