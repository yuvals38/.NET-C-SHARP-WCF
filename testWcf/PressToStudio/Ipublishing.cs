using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Scodix.PressToStudio
{
  
    [ServiceContract]
    public interface IPublishing
    {
        [OperationContract(IsOneWay = true)]        
        void ServerNotify(string state);

        // Reflection Service
        [OperationContract]
        void ReflectionNotify(ServiceType type, string data);
    }
}
