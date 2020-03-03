using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Scodix.PressToStudio
{
    /// <summary>
    /// Data structure for response.
    /// </summary>
    [MessageContract]    
    public class ResponseFileDetails
    {
        /// <summary>
        /// Name of the File.
        /// </summary>
        [MessageHeader]
        public string FileName;

        /// <summary>
        /// Length of the stream.
        /// </summary>
        [MessageHeader]
        public long Length;

        /// <summary>
        /// Position in the stream to start the download.
        /// </summary>
        [MessageHeader]
        public long ByteStart = 0;

        /// <summary>
        /// Position in the stream to start the download.
        /// </summary>
        [MessageHeader]
        public string CheckSum = string.Empty;

        /// <summary>
        /// The actual stream to download.
        /// </summary>
        [MessageBodyMember]
        public Stream FileByteStream;

        /// <summary>
        /// Disposal of the stream.
        /// </summary>
        public void Dispose()
        {
            if (FileByteStream != null)
            {
                FileByteStream.Close();
                FileByteStream = null;
            }
        }
    }

    /// <summary>
    /// Data structure for response.
    /// </summary>

    [Serializable]
    [MessageContract]
    public class RipData
    {
        [DataMember]
        [MessageBodyMember]  public string Name { get; set; }

        [DataMember]
        [MessageBodyMember] public string Description { get; set; }

        [DataMember]
        [MessageBodyMember] public Dictionary<string, string> Parameters { get; set; }    

        [DataMember]
        [MessageBodyMember] public int clientId { get; set; }

    }
}