using Newtonsoft.Json;
using Scodix.LoggingSystem;
using Scodix.PressToStudio;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using static Scodix.Kernel.STT;

namespace ScodixServiceHost
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, MaxItemsInObjectGraph = 100000)]
    public class ReflectionServiceManager : ISubscription
    {
        private static ConcurrentDictionary<ServiceType, IPublishing> _callbackList = new ConcurrentDictionary<ServiceType, IPublishing>();

        public ConcurrentDictionary<ServiceType, IManager> Services { get; } = new ConcurrentDictionary<ServiceType, IManager>();

        public ReflectionServiceManager()
        {
        }

        #region ISubscription Impl'
        public object Get(ServiceType type, string data)
        {
            return ServiceGenericLogic(type, data);
        }

        public object GetPropertyData(ServiceType type, string data)
        {
            if (OperationContext.Current != null)
            {
                IPublishing registeredUser = OperationContext.Current.GetCallbackChannel<IPublishing>();

                var result = false;
                if (!_callbackList.Contains(new KeyValuePair<ServiceType, IPublishing>(type, registeredUser)))
                    result = _callbackList.TryAdd(type, registeredUser);
            }
            IManager manager;
            object childManager = null;

            DataBoundary serializedObject = JsonConvert.DeserializeObject<DataBoundary>(data);

            if (Services.TryGetValue(type, out manager) && serializedObject != null)
            {
                MethodInfo method;
                if (serializedObject.TypeName != string.Empty && serializedObject.TypeName != null)
                {
                    method = manager.GetType().GetMethod(serializedObject.TypeName);
                    childManager = method.Invoke(manager, new object[0]);
                }

                object temp;
                PropertyInfo info;
                if (childManager != null)
                {
                    info = childManager.GetType().GetProperty(serializedObject.CommandName);
                    temp = childManager.GetType().GetProperty(serializedObject.CommandName).GetValue(childManager, new object[0]);
                }
                else
                {
                    info = manager.GetType().GetProperty(serializedObject.CommandName);
                    temp = manager.GetType().GetProperty(serializedObject.CommandName).GetValue(manager, new object[0]);
                }

                serializedObject.Attributes.Clear();
                serializedObject.Attributes.Add(info.GetMethod.ReturnType, temp);

                return JsonConvert.SerializeObject(serializedObject);
            }

            return null;
        }

        public void Invoke(ServiceType type, string data)
        {
            var returnValue = ServiceGenericLogic(type, data);
        }

        public void RegisterClient(ServiceType type, IManager manager)
        {
            if (OperationContext.Current != null)
            { 
                IPublishing registeredUser = OperationContext.Current.GetCallbackChannel<IPublishing>();

                var result = false;
                if (!_callbackList.Contains(new KeyValuePair<ServiceType, IPublishing>(type, registeredUser)))
                    result = _callbackList.TryAdd(type, registeredUser);
            }

            try
            {
                Services.TryAdd(ServiceType.FOIL, manager);
                manager.NotifyUi += NotifyUi;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public string GetDeviceType(ServiceType type)
        {
            var typeClass = Services.FirstOrDefault(m => m.Key == type).Value.GetType();   
            return JsonConvert.SerializeObject(typeClass);
            //return Services.FirstOrDefault(m => m.Key == type).Value;
        }
        #endregion

        #region Not Implemented methods of ISubscription
        public void KeepAlive(int clientId)
        {
            throw new NotImplementedException();
        }

        public void SendData(int clientId, string data)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(int clientId)
        {
            throw new NotImplementedException();
        }

        public void UnSubscribe(int clientId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private void NotifyUi(object sender, string data, ServiceType type)
        {
            var registeredUser = _callbackList.FirstOrDefault(ser => ser.Key.Equals(type));

            if (registeredUser.Value != null)
                registeredUser.Value.ReflectionNotify(type, data);
        }

        private object ServiceGenericLogic(ServiceType type, string data)
        {
            try
            {
                if (OperationContext.Current != null)
                {
                    IPublishing registeredUser = OperationContext.Current.GetCallbackChannel<IPublishing>();

                    var result = false;
                    if (!_callbackList.Contains(new KeyValuePair<ServiceType, IPublishing>(type, registeredUser)))
                        result = _callbackList.TryAdd(type, registeredUser);
                }
                IManager manager;
                object childManager = null;

                DataBoundary serializedObject = JsonConvert.DeserializeObject<DataBoundary>(data);

                if (Services.TryGetValue(type, out manager) && serializedObject != null)
                {
                    MethodInfo method;
                    if (serializedObject.TypeName != string.Empty && serializedObject.TypeName != null)
                    {
                        method = manager.GetType().GetMethod(serializedObject.TypeName);
                        childManager = method.Invoke(manager, new object[0]);
                        method = childManager.GetType().GetMethod(serializedObject.CommandName);
                    }
                    else
                    {
                        method = manager.GetType().GetMethod(serializedObject.CommandName);
                    }

                    if (method != null)
                    {
                        List<object> parameters = new List<object>();
                        foreach (var val in serializedObject.Attributes)
                        {
                            var typo = Type.GetType(val.Key.ToString());
                            if (typo.IsEnum)
                                parameters.Add(Enum.Parse(typo, val.Value.ToString()));
                            else
                                parameters.Add(val.Value.ToString());
                        }

                        object result;
                        if (childManager != null)
                            result = method.Invoke(childManager, parameters.ToArray());
                        else
                            result = method.Invoke(manager, parameters.ToArray());

                        serializedObject.Attributes.Clear();
                        serializedObject.Attributes.Add(method.ReturnType, result);

                        return JsonConvert.SerializeObject(serializedObject);
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                LogServer.LogException(e, General.EXCEPTION_GENERAL);
                return null;
            }
        }
        #endregion
    }
}
