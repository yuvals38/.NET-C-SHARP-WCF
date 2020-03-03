using Scodix.PressToStudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScodixServiceClient
{
    public delegate void NotifyClientEventHandler(object sender, object data, ServiceType type);

    public interface INotifyClient
    {
        event NotifyClientEventHandler NotifyClient;
    }
}
