using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box
{
    class HandshakeResponse
    {
        //{"status":0,"msg":"ok","timestamp":"1543675025"}

        public int Status { get; set; }

        public string Msg { get; set; }

        public string Timestamp { get; set; }
    }
}
