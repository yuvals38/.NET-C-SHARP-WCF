using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

namespace Scodix.Diagnostic
{
    public interface ISerializer
    {
        string Serialize<T>(T entity)
            where T : class, new();

        T Deserialize<T>(string entity)
            where T : class, new();
    }

    public class JsonSerializer : ISerializer
    {
        public string Serialize<T>(T entity)
            where T : class, new()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true };
                using (var writer = JsonReaderWriterFactory.CreateJsonWriter(ms, Encoding.UTF8, true, true, "  "))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T), settings);
                    ser.WriteObject(writer, entity);
                    writer.Flush();
                }
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public T Deserialize<T>(string entity)
            where T : class, new()
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(entity)))
            {
                return ser.ReadObject(stream) as T;
            }
        }
    }
}
