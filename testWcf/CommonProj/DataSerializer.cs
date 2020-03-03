using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace CommonProj
{
    [DataContract]
    public class Data
    {
        [DataMember]
        public string DataToSendAndRecieve { get; set; }

        public int clientId;

    }

    public class DataSerializer
    {
        public static void Serialize(Data wpObj, Stream streamObj)
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Data));
            js.WriteObject(streamObj, wpObj);
        }
        public static void DeSerialize(Stream streamObj, out Data wpObj)
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Data));
            wpObj = (Data)js.ReadObject(streamObj);
        }

        public static String ToString(Data wpObj)
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Data));
            MemoryStream msObj = new MemoryStream();
            js.WriteObject(msObj, wpObj);
            msObj.Position = 0;
            StreamReader sr = new StreamReader(msObj);
            string json = sr.ReadToEnd();
            sr.Close();
            msObj.Close();
            return json;
        }


        public static Data FromString(string str)
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Data));
            byte[] byteArray = Encoding.ASCII.GetBytes(str);
            MemoryStream stream = new MemoryStream(byteArray);
            return (Data)js.ReadObject(stream);
        }
    }
}
