using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box
{
    public class BoxSearchResponse
    {
        public int Status { get; set; }

        public string Msg { get; set; }

        public RetData RetData { get; set; }
    }

    public class RetData
    {
        public BoxInfo BoxInfo { get; set; }
    }

    public class BoxInfo
    {
        public string BoxSN { get; set; }
        public string OemFactoryId { get; set; }
        public string RequserId { get; set; }
        public string LicenseReqId { get; set; }
        public string OrderId { get; set; }
        public int Capacity { get; set; }
        public int Occupied { get; set; }
        public int RealCount { get; set; }
        public string BoxType { get; set; }
        public string CreateTime { get; set; }
        public string BeginBoxTime { get; set; }
        public string FinishBoxTime { get; set; }
        public string Status { get; set; }
    }
}
