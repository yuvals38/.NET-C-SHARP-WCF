using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Threading;

namespace Scodix.PressToStudio
{
    [ServiceContract]    
    public interface IRipData
    {       
        [OperationContract]
        ResponseFileDetails RetrieveRipFileFromServer(RipData ripData);

        [OperationContract]
        void KeepAlive();
    }   
}
