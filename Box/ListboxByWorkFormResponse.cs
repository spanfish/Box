using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box
{
    public class ListboxByWorkFormResponse
    {
        public int Status { get; set; }

        public string Msg { get; set; }

        public List<Boxlist> RetData { get; set; }
    }

    public class Boxlist
    {
        public string Bboxtype { get; set; }

        public List<string> BoxSN { get; set; }
    }
}
