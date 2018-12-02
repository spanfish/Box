using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box
{
    public class Order
    {
        /**
        * 合同ID
        **/
        public string AgreementId
        {
            get;
            set;
        }

        public string Boxfinishtime
        {
            get;
            set;
        }

        public string Boxstarttime
        {
            get;
            set;
        }

        /**
         * 装箱状态
        **/
        public int BoxStatus
        {
            get;
            set;
        }

        /**
         * 订单状态,
        **/
        public int CheckStatus
        {
            get;
            set;
        }

        public string Checktime
        {
            get;
            set;
        }

        /**
         * 订单确认状态 yes/no 已确认/未确认
        **/
        public string ConfirmStatus
        {
            get;
            set;
        }

        public string Confirmtime
        {
            get;
            set;
        }

        /**
         * 订单设备数
         **/
        public int Count
        {
            get;
            set;
        }

        public string Factoryname
        {
            get;
            set;
        }

        public string Finishtime
        {
            get;
            set;
        }

        /**
         * 物料编码
         *
         */
        public string MaterialCode
        {
            get;
            set;
        }

        /**
         * 工厂id
        **/
        public string OemFactoryId
        {
            get;
            set;
        }

       
        /*
         * 订单id
         */
        public string OrderId
        {
            get;
            set;
        }

        public string Platcode
        {
            get;
            set;
        }

        /**
         * 生产状态
         **/
        public int ProduceStatus
        {
            get;
            set;
        }

        /**
        * 产品ID
        **/
        public string ProductId
        {
            get;
            set;
        }

        public string Productmodel
        {
            get;
            set;
        }

        public string ProductName
        {
            get;
            set;
        }

        public string Productpcbver
        {
            get;
            set;
        }

        public string Salesusername
        {
            get;
            set;
        }

        public string Starttime
        {
            get;
            set;
        }

        public string Updatetime
        {
            get;
            set;
        }

        public string Weichatappid
        {
            get;
            set;
        }

        /**
         * 订单名称
         **/
        public string Workform
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

        public List<Order> Orderlist
        {
            get;
            set;
        }

        public int Count
        {
            get;
            set;
        }
    }
}
