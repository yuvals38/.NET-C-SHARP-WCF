using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;


namespace RIPCommon
{

    [ServiceContract(CallbackContract = typeof(IPublishing))]
    public interface ISubscription
    {
        [OperationContract]
        void Subscribe(string request);

        [OperationContract]
        void UnSubscribe(string request);

        [OperationContract]
        void Response(string s);
    }
}