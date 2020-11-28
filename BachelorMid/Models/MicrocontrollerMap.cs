using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BachelorMid.Models
{
    public class MicrocontrollerMap
    {
        public static readonly Dictionary<int, string> IpMapId =
            new Dictionary<int, string>()
            {
                {1, "192.168.20.6" },
                {2, "192.168.1.177" }
            };
    }
}
