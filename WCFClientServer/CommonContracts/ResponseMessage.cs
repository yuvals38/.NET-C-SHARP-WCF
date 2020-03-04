using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RIPCommon
{

    [DataContract(Name = "ResponseMessage")]
    [Serializable]
    public class ResponseMessage : RequestMessage
    {
        [DataMember]
        public int RespondID;

        public ResponseMessage()
        {      

        }
            
    }
}
