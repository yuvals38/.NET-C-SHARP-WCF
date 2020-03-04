using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;




namespace RIPCommon
{
    public enum RequestType
    {
        Connect,
        Disconnect,
        Operation,
        Handshake,
        Ciao,
        Info,
        DoRIP
    }


    [DataContract]
    public class RIPMessage : IExtensibleDataObject
    {
        private Object m_Obj;

        private string _request;

        private string _eventData;

        [DataMember]
        public string Request { get { return _request; } set { _request = value; } }

        [DataMember]
        public string EventData { get { return _eventData; } set { _eventData = value; } }

        public RIPMessage(Object obj)
        {
            m_Obj = obj;
        }

        private ExtensionDataObject extensionData_Value;

        public ExtensionDataObject ExtensionData
        {
            get
            {
                return extensionData_Value;
            }
            set
            {
                extensionData_Value = value;
            }
        }
    }
}