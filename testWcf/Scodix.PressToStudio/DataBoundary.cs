using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Json Example for correct Data Boundary
//{
//  "CommandName": "",
//  "Attributes": {}
//}

// { "CommandName":"TEST", "Attributes":{"One":"1234", "Two":"456789"}}
namespace Scodix.PressToStudio
{
    [Serializable]
    public class DataBoundary
    {
        public string CommandName { get; set; }
        public string TypeName { get; set; }
        public Dictionary<object, object> Attributes { get; set; }
    }
}
