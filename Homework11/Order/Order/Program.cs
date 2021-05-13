using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Data.Entity;

namespace Homework6
{
    //订单明细类
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }    //商品序号
        public string orderName { get; set; }       //商品名称
        public double orderPrice { get; set; }      //商品价格

        public int orderNum { get; set; }        //商品购买数量
        public string OrdersId { get; set; }     //外键
        public Orders Orders { set; get; }

        public OrderDetails(string orderName, double orderPrice, int orderNum)
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
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrdersId);
            hashCode = hashCode * -1521134295 + EqualityComparer<Orders>.Default.GetHashCode(Orders);
            return hashCode;
        }
    }

    [Serializable]
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

    [Serializable]
    public class OrderService
    {
        public List<Orders> orderList { get; set; }
        public OrderService(Orders[] allOrders)
        {
            this.orderList = new List<Orders>();
            foreach (Orders anOrder in allOrders)
            {
                orderList.Add(anOrder);
            }

        }

        public OrderService()
        {
            this.orderList = new List<Orders>();
        }

        //添加订单
        public void AddOrder(Orders myOrder)
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
        public void DeleteOrder(int OrdersId)
        {
            int i = 0;
            try
            {
                while (i < orderList.Count)
                {
                    if (orderList[i].OrdersId == OrdersId)
                    {
                        orderList.RemoveAt(i + 1);
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

        public void DeleteByNum(int num)
        {
            try
            {
                orderList.RemoveAt(num);
            }
            catch (System.ArgumentOutOfRangeException)     //越界
            {
                Console.WriteLine("订单号无效");
            }
        }

        //根据旧订单号修改订单信息
        public void ModifyOrder(int oldOrdersId, string client, int OrdersId, OrderDetails[] allOrderDetails)
        {
            int i = 0;
            try
            {
                while (i < orderList.Count)
                {
                    if (orderList[i].OrdersId == oldOrdersId)
                    {
                        orderList[i].client = client;
                        orderList[i].OrdersId = OrdersId;
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
        public List<Orders> FindOrderNo(int OrdersId)
        {
            var query = from oneOrder in orderList
                        where oneOrder.OrdersId == OrdersId
                        select oneOrder;
            if (query.Count() > 0)
            {
                return query.ToList<Orders>();
            }
            else
                return null;
        }

        //通过商品名称查询订单
        public List<Orders> FindOrderName(string orderName)
        {
            var query = from oneOrder in orderList
                        from oneOrderDetails in oneOrder.orderDetailsList
                        where oneOrderDetails.orderName == orderName
                        orderby oneOrder.totalPrice
                        select oneOrder;
            if (query.Count() > 0)
            {
                return query.ToList<Orders>();
            }
            else
                return null;
        }

        //通过客户查询订单
        public List<Orders> FindClient(string client)
        {
            var query = from oneOrder in orderList
                        where oneOrder.client == client
                        orderby oneOrder.totalPrice
                        select oneOrder;
            if (query.Count() > 0)
            {
                return query.ToList<Orders>();
            }
            else
                return null;
        }

        //通过金额查询订单
        public List<Orders> FindTotalPrice(double totalPrice)
        {
            var query = from oneOrder in orderList
                        where oneOrder.totalPrice == totalPrice
                        orderby oneOrder.totalPrice
                        select oneOrder;
            if (query.Count() > 0)
            {
                return query.ToList<Orders>();
            }
            else
                return null;
        }

        public void OrderSort()
        {
            this.orderList.Sort((p1, p2) => p1.OrdersId - p2.OrdersId);
        }

        //订单序列化
        public bool Export(string exportName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Orders>));

            try
            {
                using (FileStream fs = new FileStream(exportName, FileMode.Create))
                {
                    xmlSerializer.Serialize(fs, orderList);
                    return true;
                }
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("未选择文件！");
                return false;
            }
        }

        //载入订单
        public bool Import(string importName)
        {
            try
            {
                using (FileStream fs = new FileStream(importName, FileMode.Open))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Orders>));
                    orderList = ((List<Orders>)xmlSerializer.Deserialize(fs));
                    return true;
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("找不到该文件!");
                return false;
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("未选择路径!");
                return false;
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
            Orders[] myOrder =
            {
                new Orders("小刘", 0, myOrderDetails),
                new Orders("小詹", 1, myOrderDetails),
                new Orders("小龙", 2, myOrderDetails),
                new Orders("小徐", 3, myOrderDetails)
            };

            OrderService myOrderService = new OrderService(myOrder);
        }
    }
}

