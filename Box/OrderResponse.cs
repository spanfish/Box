using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Box
{

    public class OrderInfo
    {
        /**
        * 订单编码
        **/

        public string AgreementId
        {
            get;
            set;
        }

        public string OrderId
        {
            get;
            set;
        }

        //产品型号
        public string ProductModel
        {
            get;
            set;
        }

        //产品名称
        public string ProductName
        {
            get;
            set;
        }

        //产品描述
        public string ProductDesc
        {
            get;
            set;
        }

        //工单
        public string Workform
        {
            get;
            set;
        }
        
        //供应商
        public string FactoryName
        {
            get;
            set;
        }

        //古北编码
        public string MaterialCode
        {
            get;
            set;
        }
        //客户编码
        public string KehuCode
        {
            get;
            set;
        }
        //供应商
        public string VendorName
        {
            get;
            set;
        }
        //Ver
        public string ProductVerTag
        {
            get;
            set;
        }
        //批次号
        public string BatchNo
        {
            get;
            set;
        }
    }

    public class OrderResponse
    {
        public int Status
        {
            get;
            set;
        }

        public string Msg
        {
            get;
            set;
        }

        public OrderInfo OrderInfo
        {
            get;
            set;
        }
    }
}
