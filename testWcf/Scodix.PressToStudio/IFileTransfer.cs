using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Scodix.PressToStudio
{
    [ServiceContract]

    public interface IFileTransfer
    {        

        [OperationContract]
        ResponseFileDetails UploadFileDataName(FileData fileData);

        [OperationContract]
        void KeepAlive();
    }

}


