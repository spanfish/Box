using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Box
{
    public class Order : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyProperties(Order source)
        {
            // If any this null throw an exception
            if (source == null)
                return;//throw new Exception("Source or/and Destination Objects are null");
            // Getting the Types of the objects
            Type typeDest = this.GetType();
            Type typeSrc = source.GetType();

            // Iterate the Properties of the source instance and  
            // populate them from their desination counterparts  
            PropertyInfo[] srcProps = typeSrc.GetProperties();
            foreach (PropertyInfo srcProp in srcProps)
            {
                if (srcProp.Name == "PropertyChanged")
                {
                    continue;
                }
                if (!srcProp.CanRead)
                {
                    continue;
                }
                PropertyInfo targetProperty = typeDest.GetProperty(srcProp.Name);
                if (targetProperty == null)
                {
                    continue;
                }
                if (!targetProperty.CanWrite)
                {
                    continue;
                }
                if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true).IsPrivate)
                {
                    continue;
                }
                if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
                {
                    continue;
                }
                if (!targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType))
                {
                    continue;
                }
                // Passed all tests, lets set the value
                targetProperty.SetValue(this, srcProp.GetValue(source, null), null);
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(targetProperty.Name));
                }
            }
        }

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


    public class OrderInfo
    {
        /**
        * 合同ID
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

        //订单名称
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

        public string MaterialCode
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
