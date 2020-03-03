using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScodixServiceClient;
using System.Runtime.Serialization;
using CommonProj;

namespace WcfClientForm
{
    public class ClientCommNotifyObject
    {
        public Data m_data;
    }

    [DataContract]
    public class ServiceData
    {
        [DataMember]
        public ClientCommNotifyObject obj { get; set; }
    }
}
