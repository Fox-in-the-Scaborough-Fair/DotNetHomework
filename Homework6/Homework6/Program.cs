using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;
using System.Xml.Serialization;

namespace Homework6
{
    //订单明细类
    public class OrderDetails
    {
        public string orderName { get; }       //商品名称
        public double orderPrice { get; }      //商品价格

        public int orderNum { get; }        //商品购买数量
        public OrderDetails(string orderName, double orderPrice,int orderNum)
        {
                this.orderName = orderName;
                this.orderPrice = orderPrice;
                this.orderNum = orderNum;
        }
        public OrderDetails()
        {
            this.orderName = "";
            this.orderPrice = 0;
            this.orderNum = 0;
        }

        public override string ToString()
        {
            return "商品名称:" + orderName + "  商品价格:" + orderPrice + "  商品数量:" + orderNum + "\n";
        }

        public override bool Equals(object obj)
        {
            return obj is OrderDetails details &&
                   orderName == details.orderName &&
                   orderPrice == details.orderPrice &&
                   orderNum == details.orderNum;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(orderName, orderPrice, orderNum);
        }
    }

    [Serializable]
    public class Order
    {
        public string client { get; set; }       //订单客户
        public string orderNo { get; set; }         //订单号
        public double totalPrice { get; set; }       //总金额

        public List <OrderDetails> orderDetailsList;        //订单明细


        public Order(string client, string orderNo, OrderDetails[] allOrderDetails)
        {
            this.client = client;
            this.orderNo = orderNo;
            this.orderDetailsList = new List<OrderDetails>();
            foreach (OrderDetails anOrderDetail in allOrderDetails)
            {
                this.totalPrice += anOrderDetail.orderPrice * anOrderDetail.orderNum;
                this.orderDetailsList.Add(anOrderDetail);
            }

        }

        public Order()
        {
            this.client = "";
            this.orderNo = "";
            this.orderDetailsList = new List<OrderDetails>();
        }


        public override string ToString()
        {
            string temp = "";
            foreach (OrderDetails anOrderDetail in orderDetailsList)
            {
                temp += anOrderDetail.ToString();
            }
            return "订单客户:" + client + "  订单号:" + orderNo + "  总金额:" + totalPrice + "\n" + temp;
        }
        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   client == order.client &&
                   orderNo == order.orderNo &&
                   totalPrice == order.totalPrice &&
                   EqualityComparer<List<OrderDetails>>.Default.Equals(orderDetailsList, order.orderDetailsList);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(client, orderNo, totalPrice, orderDetailsList);
        }
    }

    [Serializable]
    public class OrderService
    {
        public List<Order> orderList;
        public OrderService(Order[] allOrders)
        {
                this.orderList = new List<Order>();
                foreach (Order anOrder in allOrders)
                {
                    orderList.Add(anOrder);
                }
            
        }

        public OrderService()
        {
            this.orderList = new List<Order>();
        }

        //添加订单
        public void AddOrder(Order myOrder)
        {
            try
            {
                this.orderList.Add(myOrder);
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("订单无效!");
            }
        }

        //删除订单,通过订单号删除订单
        public void DeleteOrder(string orderNo)
        {
            int i = 0;
            try
            {
                while (i < orderList.Count)
                {
                    if (orderList[i].orderNo == orderNo)
                    {
                        orderList.RemoveAt(i+1);
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

        //根据旧订单号修改订单信息
        public void ModifyOrder(string oldOrderNo, string client, string orderNo, OrderDetails[] allOrderDetails)
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
                        orderList[i].orderDetailsList.Clear();
                        foreach (OrderDetails anOrder in allOrderDetails)
                        {
                            orderList[i].totalPrice += anOrder.orderPrice * anOrder.orderNum;
                            orderList[i].orderDetailsList.Add(anOrder);
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
        public List<Order> FindOrderNo(string orderNo)
        {
            var query = from oneOrder in orderList
                        where oneOrder.orderNo == orderNo
                        select oneOrder;
            if (query.Count() > 0)
            {
                return query.ToList<Order>();
            }
            else
                return null;
        }

        //通过商品名称查询订单
        public List<Order> FindOrderName(string orderName)
        {
            var query = from oneOrder in orderList
                        from oneOrderDetails in oneOrder.orderDetailsList
                        where oneOrderDetails.orderName == orderName
                        orderby oneOrder.totalPrice
                        select oneOrder;
            if (query.Count() > 0)
            {
                return query.ToList<Order>();
            }
            else
                return null;
        }

        //通过客户查询订单
        public List<Order> FindClient(string client)
        {
            var query = from oneOrder in orderList
                        where oneOrder.client == client
                        orderby oneOrder.totalPrice
                        select oneOrder;
            if (query.Count() > 0)
            {
                return query.ToList<Order>();
            }
            else
                return null;
        }

        //通过金额查询订单
        public List<Order> FindTotalPrice(double totalPrice)
        {
            var query = from oneOrder in orderList
                        where oneOrder.totalPrice == totalPrice
                        orderby oneOrder.totalPrice
                        select oneOrder;
            if (query.Count() > 0)
            {
                return query.ToList<Order>();
            }
            else
                return null;
        }

        public void OrderSort()
        {
            this.orderList.Sort((p1, p2) => int.Parse(p1.orderNo) - int.Parse(p2.orderNo));
        }

        //订单序列化
        public void Export(string exportName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(exportName, FileMode.Create))
            {
                xmlSerializer.Serialize(fs, orderList);
            }
        }

        //载入订单
        public void Import(string importName)
        {
            try
            {
                using (FileStream fs = new FileStream(importName, FileMode.Open))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Order>));
                    orderList = ((List<Order>)xmlSerializer.Deserialize(fs));
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("找不到该文件！");
            }
        }

        public bool ListEquals(OrderService service)
        {
            int i;
            if (this.orderList.Count == 0 && service.orderList.Count == 0)
            {
                return true;
            }

            if (this.orderList.Count == service.orderList.Count)
            {
                for (i = 0; i < this.orderList.Count; i++)
                {
                    {
                        if (this.orderList[i] == service.orderList[i])
                        {
                            i++;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else 
            {
                return false;
            }
        }
        

        public override bool Equals(object obj)
        {

            return obj is OrderService service && ListEquals(service);

            
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(orderList);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
            OrderDetails[] myOrderDetails = new OrderDetails[3]
            {
                new OrderDetails("笔记本", 4.0, 10),
                new OrderDetails("签字笔", 5.0 , 5),
                new OrderDetails("计算器", 7.5, 1),
            };
            Order[] myOrder =
            {
                new Order("小刘", "2019302110066",myOrderDetails),
                new Order("小詹", "2019302110067",myOrderDetails),
                new Order("小龙", "2019302110063",myOrderDetails),
                new Order("小徐", "2019302110068",myOrderDetails)
            };

            OrderService myOrderService = new OrderService(myOrder);

        }

    }

}

