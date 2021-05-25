using System.Collections.Generic;
using System.Linq;
namespace Homework12.Models
{
    public class Order
    {
        public int OrderId { get; set; }         //订单号
        public string client { get; set; }       //订单客户
        public double totalPrice { get => ((orderDetailsList == null)? 0:orderDetailsList.Sum(item => (item.orderNum*item.orderPrice)));}       //总金额
        public List<OrderDetail> orderDetailsList { get; set; }        //订单明细
    }
}