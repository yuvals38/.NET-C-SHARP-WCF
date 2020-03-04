using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RIPCommon
{ 
    [DataContract(Name = "RequestMessage")]
    [KnownType(typeof(ResponseMessage))]
    [XmlInclude(typeof(ResponseMessage))]
    [Serializable]
    public class RequestMessage : IExtensibleDataObject
    {
        [DataMember]
        public string SenderID;
        [DataMember]
        public string ReceiverID;
        [DataMember]
        public int RequestID;
        [DataMember]
        public DateTime TimeOfSending;
        [DataMember]
        public RequestType RequestType;
        [DataMember]
        public SerializableDictionary<int,string> DataParams;

        public RequestMessage(string senderId, string receiverId, int reqId, RequestType type)
        {
            SenderID = senderId;
            ReceiverID = receiverId;
            RequestID = reqId;
            RequestType = type;
        }
        public RequestMessage() { }
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
