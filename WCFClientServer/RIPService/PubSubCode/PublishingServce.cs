using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using RIPCommon;

namespace RIPService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    class Publishing : IPublishing
    {
        #region IPublishing Members
        public void Publish(RIPMessage e, string request)
        {
            List<IPublishing> Clients = Filter.Instance.GetClients(request);
            if (Clients == null) return;

            Type type = typeof(IPublishing);
            MethodInfo publishMethodInfo = type.GetMethod("Publish");

            foreach (IPublishing Client in Clients)
            {
                try
                {                   
                    publishMethodInfo.Invoke(Client, new object[] { e, request });
                }
                catch
                {

                }

            }


        }


        #endregion
    }
}

