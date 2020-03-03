using Castle.DynamicProxy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Scodix.Kernel;
using Scodix.LoggingSystem;
using Scodix.PressToStudio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using static Scodix.Kernel.STT;

namespace ScodixServiceClient
{
    public class ReflectionClientManager : IPublishing, INotifyClient
    {
        private const string URL = "net.tcp://localhost:7052/ReflectionServiceManager";
        #region Properties
        ISubscription _proxy;
        string _endpoint = string.Empty;

        public event NotifyClientEventHandler NotifyClient;
        public static ReflectionClientManager Instance { get; } = new ReflectionClientManager();
        #endregion

        #region c'tor
        static ReflectionClientManager()
        {

        }

        private ReflectionClientManager()
        {
            _endpoint = URL;//ConfigurationManager.AppSettings["EndpointAddress"];
            MakeProxy(_endpoint, this);
        }

        public void MakeProxy(string EndpoindAddress, object callbackinstance)
        {
            NetTcpBinding netTcpbinding = new NetTcpBinding(SecurityMode.None);
            netTcpbinding.MaxBufferPoolSize = 50000000;
            netTcpbinding.MaxBufferSize = 50000000;
            netTcpbinding.MaxReceivedMessageSize = 50000000;
            netTcpbinding.ReliableSession.Enabled = true;
            netTcpbinding.ReliableSession.InactivityTimeout = TimeSpan.FromMinutes(10);
            netTcpbinding.ReceiveTimeout = TimeSpan.FromMinutes(10);
            EndpointAddress endpointAddress = new EndpointAddress(EndpoindAddress);
            InstanceContext context = new InstanceContext(callbackinstance);

            DuplexChannelFactory<ISubscription> channelFactory = new DuplexChannelFactory<ISubscription>(new InstanceContext(this), netTcpbinding, endpointAddress);
            _proxy = channelFactory.CreateChannel();
        }
        #endregion

        #region Client Methods
        public bool SendCommandToManager(ServiceType type, string data)
        {
            try
            {
                if (data != null && !data.Equals(String.Empty))
                {
                    _proxy.Invoke(type, data);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogServer.LogException(ex, General.EXCEPTION_GENERAL, $"A problem occured while communicating with device of type {type}");
                return false;
            }
        }

        public object GetFromManager(ServiceType type, string data)
        {
            try
            {
                if (data != null && !data.Equals(String.Empty))
                {
                    var temp =_proxy.Get(type, data);
                    DataBoundary serialized = JsonConvert.DeserializeObject<DataBoundary>(temp.ToString());
                    var vals = serialized.Attributes.FirstOrDefault();
                    object reflected;
                    if (vals.Value.GetType().IsEquivalentTo(typeof(JArray)))
                    {
                        reflected = ((JArray)vals.Value).ToObject(Type.GetType(vals.Key.ToString()));
                    }
                    else if (vals.Value.GetType().IsEquivalentTo(typeof(JObject)))
                        reflected = ((JObject)vals.Value).ToObject(Type.GetType(vals.Key.ToString()));
                    else
                    {
                        var typo = Type.GetType(vals.Key.ToString());
                        if (typo.IsEnum)
                            reflected = Enum.Parse(typo, vals.Value.ToString());
                        else
                            reflected = vals.Value;
                    }
                    return reflected;
                }
                return null;
            }
            catch (Exception ex)
            {
                LogServer.LogException(ex, STT.General.EXCEPTION_GENERAL, $"A problem occured while communicating with device of type {type}");
                return null;
            }
        }

        public object GetPropertyFromManager(ServiceType type, string data)
        {
            try
            {
                if (data != null && !data.Equals(String.Empty))
                {
                    var temp = _proxy.GetPropertyData(type, data);
                    DataBoundary serialized = JsonConvert.DeserializeObject<DataBoundary>(temp.ToString());
                    var vals = serialized.Attributes.FirstOrDefault();
                    object reflected = ((JArray)vals.Value).ToObject(Type.GetType(vals.Key.ToString()));
                    return reflected;
                }
                return null;
            }
            catch (Exception ex)
            {
                LogServer.LogException(ex, STT.General.EXCEPTION_GENERAL, $"A problem occured while communicating with device of type {type}");
                return null;
            }
        }

        public object GetDevice(ServiceType type)
        {
            var typeClass = JsonConvert.DeserializeObject<Type>(_proxy.GetDeviceType(type));
            ProxyGenerator generator = new ProxyGenerator();
            var proxyClass = generator.CreateClassProxy(typeClass, new DeviceInterceptor());
            return proxyClass;
        }

        public void RegisterReflectedDevice(ServiceType type, IManager manager)
        {
            _proxy.RegisterClient(type, manager);
        }

        public void ReflectionNotify(ServiceType type, string data)
        {
            var result = JsonConvert.DeserializeObject<DataBoundary>(data);

            var returnedAnswer = result.Attributes.FirstOrDefault(r => r.Key.Equals("Result"));
            if (!returnedAnswer.IsNull())
                NotifyClient?.Invoke(this, returnedAnswer.Value, type);
            else
                NotifyClient?.Invoke(this, result, type);
        }

        public void ServerNotify(string state)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    #region Utilites
    public static class KeyValuePairExtensions
    {
        public static bool IsNull<T, TU>(this KeyValuePair<T, TU> pair)
        {
            return pair.Equals(new KeyValuePair<T, TU>());
        }
    }
    #endregion
}
