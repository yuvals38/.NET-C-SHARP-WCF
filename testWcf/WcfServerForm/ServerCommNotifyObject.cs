using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScodixServiceHost;
using System.Runtime.Serialization;
using CommonProj;

namespace WcfServerForm
{
    public class ClientCommNotifyObject : NotifyObject
    {
        public Data m_data;

    }

    [DataContract]
    public class CommServiceData
    {
        [DataMember]
        public ClientCommNotifyObject obj { get; set; }
    }

}
