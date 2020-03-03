using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ScodixServiceClient
{

    public class NotifyObject
    {
        public int m_clientId;
    }

    [DataContract]
    public class ServiceData
    {
        [DataMember]
        public NotifyObject obj { get; set; }
    }
}
