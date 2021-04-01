using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Homework5
{
    //订单明细类
    public class OrderDetials
    {
        public string orderName { get; }       //商品名称
        public double orderPrice { get; }      //商品价格
        public OrderDetials(string orderName, double orderPrice)
        {
            this.orderName = orderName;
            this.orderPrice = orderPrice;
        }

        public override string ToString()
        {
            return "商品名称:"+orderName+"  商品价格:"+orderPrice;
        }

        public override bool Equals(object obj)
        {
            return obj is OrderDetials detials &&
                   orderName == detials.orderName &&
                   orderPrice == detials.orderPrice;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(orderName, orderPrice);
        }
    }

    public class Order
    {
        public string client { get; set; }       //订单客户
        public string orderNo { get; set; }         //订单号
        public double totalPrice { get; set; }       //总金额

        public Dictionary<string, double> orderDic;        //订单明细字典<orderName,orderPrice>


        Order(string client, string orderNo, OrderDetials[] allOrderDetials)
        {
            this.client = client;
            this.orderNo = orderNo;
            this.orderDic = new Dictionary<string, double>();
            foreach (OrderDetials anOrder in allOrderDetials)
            {
                this.totalPrice += anOrder.orderPrice;
                this.orderDic.Add(anOrder.orderName, anOrder.orderPrice);
            }

        }

        public override string ToString()
        {
            string temp = "";
            foreach (string key in orderDic.Keys)
            {
                temp += "商品名称:" + key + "  商品价格:" + orderDic[key] + "\n";
            }
            return "订单客户:" + client + "  订单号:" + orderNo + "\n" + temp;
        }

        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   client == order.client &&
                   orderNo == order.orderNo &&
                   EqualityComparer<Dictionary<string, double>>.Default.Equals(orderDic, order.orderDic);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(client, orderNo, orderDic);
        }
    }

    class OrderService
    {
        List<Order> orderList;
        OrderService(Order[] allOrders)
        {
            this.orderList = new List<Order>();
            foreach (Order anOrder in orderList)
            {
                orderList.Add(anOrder);
            }
        }
        
        //添加订单
        void AddOrder(Order myOrder)
        {
            try
            {
                orderList.Add(myOrder);
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("订单无效!");
            }
        }

        //删除订单,通过订单号删除订单
        void DeleteOrder(string orderNo)
        {
            int i=0;
            try
            {
                while (i < orderList.Count)
                {
                    if (orderList[i].orderNo == orderNo)
                    {
                        orderList.RemoveAt(i);
                        break;
                    }
                    i++;
                }
            }
            catch (System.IndexOutOfRangeException)     //越界
            {
                Console.WriteLine("订单号无效");
            }
        }

        void ModifyOrder(string oldOrderNo, string client, string orderNo, OrderDetials[] allOrderDetials)
        {
            int i = 0;
            try
            {
                while (i < orderList.Count)
                {
                    if (orderList[i].orderNo == oldOrderNo)
                    {
                        orderList[i].client = client;
                        orderList[i].orderNo = orderNo;
                        orderList[i].orderDic = new Dictionary<string, double>();
                        foreach (OrderDetials anOrder in allOrderDetials)
                        {
                            orderList[i].totalPrice += anOrder.orderPrice;
                            orderList[i].orderDic.Add(anOrder.orderName, anOrder.orderPrice);
                        }
                        break;
                    }
                    i++;
                }
            }
            catch (System.IndexOutOfRangeException)     //越界
            {
                Console.WriteLine("订单号无效");
            }
        }

        //通过订单号查询订单
        IEnumerable<Order> FindOrderNo(string orderNo)
        {
            var query = from oneOrder in orderList
                        where oneOrder.orderNo == orderNo
                        select oneOrder;
            if (query.Count() > 0)
            {
                return query;
            }
            else
                return null;
        }

        //通过商品名称查询订单
        IEnumerable<Order> FindOrderName(string orderName)
        {
            var query = from oneOrder in orderList
                        where oneOrder.orderDic.ContainsKey(orderName)
                        orderby oneOrder.totalPrice
                        select oneOrder;
            if (query.Count() > 0)
            {
                return query;
            }
            else
                return null;
        }

        //通过客户查询订单
        IEnumerable<Order> FindClient(string client)
        {
            var query = from oneOrder in orderList
                        where oneOrder.client == client
                        orderby oneOrder.totalPrice
                        select oneOrder;
            if (query.Count() > 0)
            {
                return query;
            }
            else
                return null;
        }

        //通过金额查询订单
        IEnumerable<Order> FindTotalPrice(double totalPrice)
        {
            var query = from oneOrder in orderList
                        where oneOrder.totalPrice == totalPrice
                        orderby oneOrder.totalPrice
                        select oneOrder;
            if (query.Count() > 0)
            {
                return query;
            }
            else
                return null;
        }

        void OrderSort()
        {
            this.orderList.Sort((p1, p2) => int.Parse(p1.orderNo) - int.Parse(p2.orderNo));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
