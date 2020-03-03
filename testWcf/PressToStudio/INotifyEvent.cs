using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scodix.PressToStudio
{
    public delegate void NotifyUiEventHandler(object sender, string data, ServiceType type);

    public interface INotifyEvent
    {
        event NotifyUiEventHandler NotifyUi;
    }
}
