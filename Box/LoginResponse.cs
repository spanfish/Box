using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box
{
    public class LoginResponse
    {
        public int Status { get; set; }

        public string Msg { get; set; }

        public string Userid { get; set; }

        public string Loginsession { get; set; }

        public string Nickname { get; set; }

        public string Loginip { get; set; }

        public string Logintime { get; set; }
    }
}
