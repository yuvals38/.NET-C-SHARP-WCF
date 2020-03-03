using System.Runtime.Serialization;


namespace ScodixServiceHost
{   
    public class NotifyObject
    {
        public int m_clientId;        
    }

    [DataContract]
    class ServiceData
    {
        [DataMember]
        public NotifyObject obj { get; set; }
    }
}
