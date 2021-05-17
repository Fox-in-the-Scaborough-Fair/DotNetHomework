using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework8
{
    //订单明细类
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }    //商品序号
        public string orderName { get; set; }       //商品名称
        public double orderPrice { get; set; }      //商品价格

        public int orderNum { get; set; }        //商品购买数量

        public int OrdersId { get; set; }      //外键
        public Orders Orders { get; set; }

        public OrderDetails(string orderName, double orderPrice, int orderNum, int OrdersId = 0)
        {
            this.orderName = orderName;
            this.orderPrice = orderPrice;
            this.orderNum = orderNum;
            this.OrdersId = OrdersId;
        }
        public OrderDetails()
        {
            this.orderName = "";
            this.orderPrice = 0;
            this.orderNum = 0;
            this.OrdersId = 0;
        }

        public override string ToString()
        {
            return "商品名称:" + orderName + "  商品价格:" + orderPrice + "  商品数量:" + orderNum + "\n";
        }

        public override bool Equals(object obj)
        {
            return obj is OrderDetails details &&
                   OrderDetailsId == details.OrderDetailsId &&
                   orderName == details.orderName &&
                   orderPrice == details.orderPrice &&
                   orderNum == details.orderNum &&
                   OrdersId == details.OrdersId &&
                   EqualityComparer<Orders>.Default.Equals(Orders, details.Orders);
        }

        public override int GetHashCode()
        {
            int hashCode = 438194288;
            hashCode = hashCode * -1521134295 + OrderDetailsId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(orderName);
            hashCode = hashCode * -1521134295 + orderPrice.GetHashCode();
            hashCode = hashCode * -1521134295 + orderNum.GetHashCode();
            hashCode = hashCode * -1521134295 + OrdersId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Orders>.Default.GetHashCode(Orders);
            return hashCode;
        }
    }
}
