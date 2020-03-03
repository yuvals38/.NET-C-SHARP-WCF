using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Newtonsoft.Json;
using Scodix.PressToStudio;

namespace ScodixServiceClient
{
    public class DeviceInterceptor : Interceptor
    {
        protected override void ExecuteAfter(IInvocation invocation)
        {
            Console.WriteLine("End");
        }

        protected override void ExecuteBefore(IInvocation invocation)
        {
            Console.WriteLine("Start");
            
            DataBoundary data = new DataBoundary()
            {
                CommandName = invocation.Method.Name,
                Attributes = new Dictionary<object, object>()
            };

            invocation.Arguments.ToList().ForEach(arg => data.Attributes.Add(arg.GetType(), arg));
            if (data.CommandName.Contains("get_") || data.CommandName.Contains("set_"))
            {
                data.CommandName = data.CommandName.Remove(0, 4);
                invocation.ReturnValue = ReflectionClientManager.Instance.GetPropertyFromManager(GetServiceType(invocation.Proxy.ToString()), JsonConvert.SerializeObject(data));
            }
            else
            {
                invocation.ReturnValue = ReflectionClientManager.Instance.GetFromManager(GetServiceType(invocation.Proxy.ToString()), JsonConvert.SerializeObject(data));
            }
        }

        private ServiceType GetServiceType(string name)
        {
            if (name.Contains("Foil"))
                return ServiceType.FOIL;

            return ServiceType.UV;
        }
    }
}
