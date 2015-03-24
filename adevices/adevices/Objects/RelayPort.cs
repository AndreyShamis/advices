using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adevices
{
    public class RelayPort
    {
        public short    PortNumber { set; get; }
        public bool     PortStatus { set; get; }
        public bool     PortReserved { set; get; }
    }
}
