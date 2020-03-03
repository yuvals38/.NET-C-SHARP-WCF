using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Scodix.PressToStudio
{
    [Serializable]
    [MessageContract]
    public class FileData
    {
        [DataMember]
        [MessageBodyMember]
        public string FileName
        {
            get;
            set;
        }
        [DataMember]
        [MessageBodyMember]
        public string DirectoryName
        {
            get;
            set;
        }

        [DataMember]
        [MessageBodyMember]
        public string FileNameFullPath
        {
            get;
            set;
        }

        
        [DataMember]
        [MessageBodyMember]
        public byte[] BufferData
        {
            get;
            set;
        }
        [DataMember]
        [MessageBodyMember]
        public int FilePosition
        {
            get;
            set;
        }
    }
}
