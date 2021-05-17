using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework8
{
    public class Orders
    {
        public int OrdersId { get; set; }         //订单号
        public string client { get; set; }       //订单客户
        public double totalPrice { get; set; }       //总金额
        public List<OrderDetails> orderDetailsList { get; set; }        //订单明细


        public Orders(string client, int OrdersId, OrderDetails[] allOrderDetails)
        {
            this.client = client;
            this.OrdersId = OrdersId;
            this.orderDetailsList = new List<OrderDetails>();
            foreach (OrderDetails anOrderDetail in allOrderDetails)
            {
                this.totalPrice += anOrderDetail.orderPrice * anOrderDetail.orderNum;
                this.orderDetailsList.Add(anOrderDetail);
            }

        }

        public Orders()
        {
            this.client = "";
            this.OrdersId = 0;
            this.orderDetailsList = new List<OrderDetails>();
        }


        public override string ToString()
        {
            string temp = "";
            foreach (OrderDetails anOrderDetail in orderDetailsList)
            {
                temp += anOrderDetail.ToString();
            }
            return "订单客户:" + client + "  订单号:" + OrdersId + "  总金额:" + totalPrice + "\n" + temp;
        }

        public override bool Equals(object obj)
        {
            return obj is Orders orders &&
                   OrdersId == orders.OrdersId &&
                   client == orders.client &&
                   totalPrice == orders.totalPrice &&
                   EqualityComparer<List<OrderDetails>>.Default.Equals(orderDetailsList, orders.orderDetailsList);
        }

        public override int GetHashCode()
        {
            int hashCode = -679373293;
            hashCode = hashCode * -1521134295 + OrdersId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(client);
            hashCode = hashCode * -1521134295 + totalPrice.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<OrderDetails>>.Default.GetHashCode(orderDetailsList);
            return hashCode;
        }
    }
}
