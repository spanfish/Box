using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box
{
    public class Account
    {
        public string Userid { get; set; }
        public string Nickname { get; set; }
        public string Grouptype { get; set; }
        public string OemfactoryId { get; set; }
        public string OemfactoryName { get; set; }
        public List<string> Roletype { get; set; }
    }

    public class AccountResponse
    {
        public int Status { get; set; }

        public string Msg { get; set; }

        public List<Account> Data { get; set; }
    }
}
