using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Homework8
{
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

        /*
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
        */

        public void AddOrder(Orders myOrder)
        {
            try
            {
                using (var db = new OrderDBContext())
                {
                    var orders = new Orders
                    {
                        client = myOrder.client,
                        orderDetailsList = myOrder.orderDetailsList
                    };
                    db.Orders.Add(orders);
                    db.SaveChanges();
                }
            }
            catch (System.NullReferenceException)
            {
                Console.WriteLine("订单无效!");
            }
        }

        /*
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
        */

        public void DeleteOrder(int OrdersIdIn)
        {
            using (var db = new OrderDBContext())
            {
                var order = db.Orders.Include("orderDetailsList").FirstOrDefault(p => p.OrdersId == OrdersIdIn);
                if (order != null)
                {
                    db.Orders.Remove(order);
                    db.SaveChanges();
                }
            }
        }

        /*
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
        */

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

        /*
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
        */

        //通过订单号查询订单
        public List<Orders> FindOrderNo(int OrdersId)
        {
            using (var db = new OrderDBContext())
            {
                var query = db.Orders.Where(b => b.OrdersId == OrdersId);
                if (query != null)
                {
                    return query.ToList();
                }
                else
                {
                    return null;
                }
            }
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
                using (var db = new OrderDBContext())
                {
                    using (FileStream fs = new FileStream(exportName, FileMode.Create))
                    {
                        xmlSerializer.Serialize(fs, db.Orders.ToList()) ;
                        return true;
                    }
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
                    using (var db = new OrderDBContext())
                    {
                        foreach (Orders anOrder in orderList)
                        {
                                db.Orders.Add(anOrder); 
                        }
                        db.SaveChanges();
                    }
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
}
