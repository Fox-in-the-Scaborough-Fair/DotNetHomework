using Microsoft.VisualStudio.TestTools.UnitTesting;
using Homework6;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Homework6.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {

        [TestMethod()]
        public void AddOrderTest1()
        {
            OrderDetails[] myOrderDetails = new OrderDetails[3]
            {
                new OrderDetails("笔记本", 4.0, 10),
                new OrderDetails("签字笔", 5.0 , 5),
                new OrderDetails("计算器", 7.5, 1),
            };
            Order[] testOrder =
            {
                new Order("小刘", "2019302110066",myOrderDetails),
                new Order("小詹", "2019302110067",myOrderDetails),
                new Order("小龙", "2019302110063",myOrderDetails),
            };

            OrderService testOrderService = new OrderService(testOrder);
            Order newOrder = new Order("小徐", "2019302110068", myOrderDetails);
            testOrderService.AddOrder(newOrder);

            Order[] myOrder =
            {
                new Order("小刘", "2019302110066",myOrderDetails),
                new Order("小詹", "2019302110067",myOrderDetails),
                new Order("小龙", "2019302110063",myOrderDetails),
                new Order("小徐", "2019302110068",myOrderDetails)
            };
            OrderService myOrderService = new OrderService(myOrder);
            Assert.IsTrue(myOrderService.Equals(testOrderService));
            
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddOrderTest2()
        {
            OrderDetails[] myOrderDetails = new OrderDetails[3]
            {
                new OrderDetails("笔记本", 4.0, 10),
                new OrderDetails("签字笔", 5.0 , 5),
                new OrderDetails("计算器", 7.5, 1),
            };
            Order[] testOrder =
            {
                new Order("小刘", "2019302110066",myOrderDetails),
                new Order("小詹", "2019302110067",myOrderDetails),
                new Order("小龙", "2019302110063",myOrderDetails),
            };

            OrderService testOrderService = new OrderService(testOrder);
            testOrderService.AddOrder(null);

        }

        [TestMethod()]
        public void DeleteOrderTest1()
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
            };

            OrderService myOrderService = new OrderService(myOrder);

            Order[] testOrder =
            {
                new Order("小刘", "2019302110066",myOrderDetails),
                new Order("小詹", "2019302110067",myOrderDetails),
                new Order("小龙", "2019302110063",myOrderDetails),
                new Order("小徐", "2019302110068",myOrderDetails)
            };
            OrderService testOrderService = new OrderService(testOrder);
            testOrderService.DeleteOrder("2019302110068");

            CollectionAssert.AreEqual(testOrderService.orderList, myOrderService.orderList);
        }

        [TestMethod()]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void DeleteOrderTest2()
        {
            OrderDetails[] myOrderDetails = new OrderDetails[3]
            {
                new OrderDetails("笔记本", 4.0, 10),
                new OrderDetails("签字笔", 5.0 , 5),
                new OrderDetails("计算器", 7.5, 1),
            };
            Order[] testOrder =
            {
                new Order("小刘", "2019302110066",myOrderDetails),
                new Order("小詹", "2019302110067",myOrderDetails),
                new Order("小龙", "2019302110063",myOrderDetails),
                new Order("小徐", "2019302110068",myOrderDetails)
            };
            OrderService testOrderService = new OrderService(testOrder);
            testOrderService.DeleteOrder("2019302110099");
        }

        [TestMethod()]
        public void ModifyOrderTest1()
        {
            OrderDetails[] myOrderDetails = new OrderDetails[3]
            {
                new OrderDetails("笔记本", 4.0, 10),
                new OrderDetails("签字笔", 5.0 , 5),
                new OrderDetails("计算器", 7.5, 1),
            };
            Order[] myOrder =
            {
                new Order("刘雨辛", "2019302110066",myOrderDetails),
                new Order("小詹", "2019302110067",myOrderDetails),
                new Order("小龙", "2019302110063",myOrderDetails),
                new Order("小徐", "2019302110068",myOrderDetails)
            };

            OrderService myOrderService = new OrderService(myOrder);

            Order[] testOrder =
            {
                new Order("小刘", "2019302110066",myOrderDetails),
                new Order("小詹", "2019302110067",myOrderDetails),
                new Order("小龙", "2019302110063",myOrderDetails),
                new Order("小徐", "2019302110068",myOrderDetails)
            };
            OrderService testOrderService = new OrderService(testOrder);
            testOrderService.ModifyOrder("2019302110066","刘雨辛","2019302110066",myOrderDetails);
            CollectionAssert.AreEqual(testOrderService.orderList, myOrderService.orderList);
            
        }

        [TestMethod()]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ModifyOrderTest2()
        {
            OrderDetails[] myOrderDetails = new OrderDetails[3]
            {
                new OrderDetails("笔记本", 4.0, 10),
                new OrderDetails("签字笔", 5.0 , 5),
                new OrderDetails("计算器", 7.5, 1),
            };

            Order[] testOrder =
            {
                new Order("小刘", "2019302110066",myOrderDetails),
                new Order("小詹", "2019302110067",myOrderDetails),
                new Order("小龙", "2019302110063",myOrderDetails),
                new Order("小徐", "2019302110068",myOrderDetails)
            };
            OrderService testOrderService = new OrderService(testOrder);
            testOrderService.ModifyOrder("2019302110099", "刘雨辛", "2019302110066", myOrderDetails);

        }


        [TestMethod()]
        public void FindOrderNoTest()
        {
            OrderDetails[] myOrderDetails =
            {
                new OrderDetails("笔记本", 4.0, 10),
                new OrderDetails("签字笔", 5.0 , 5),
                new OrderDetails("计算器", 7.5, 1),
            };

            OrderDetails[] myAnotherOrderDetails =
            {
                new OrderDetails("笔记本", 4.0, 10),
                new OrderDetails("签字笔", 5.0 , 5),
                new OrderDetails("计算器", 7.5, 1),
                new OrderDetails("橡皮",2.0,1)
            };

            Order[] testOrder =
            {
                new Order("小刘", "2019302110066",myOrderDetails),
                new Order("小詹", "2019302110067",myOrderDetails),
                new Order("小龙", "2019302110063",myOrderDetails),
                new Order("小徐", "2019302110068",myAnotherOrderDetails)
            };

            OrderService testOrderService = new OrderService(testOrder);

            List<Order> testOrders = testOrderService.FindOrderNo("2019302110066");
            List<Order> myOrders = new List<Order>();
            myOrders.Add(new Order("小刘", "2019302110066", myOrderDetails));
            CollectionAssert.AreEqual(testOrders, myOrders);
        }

        [TestMethod()]
        public void FindOrderNameTest()
        {
            OrderDetails[] myOrderDetails =
            {
                new OrderDetails("笔记本", 4.0, 10),
                new OrderDetails("签字笔", 5.0 , 5),
                new OrderDetails("计算器", 7.5, 1),
            };

            OrderDetails[] myAnotherOrderDetails =
            {
                new OrderDetails("笔记本", 4.0, 10),
                new OrderDetails("签字笔", 5.0 , 5),
                new OrderDetails("计算器", 7.5, 1),
                new OrderDetails("橡皮",2.0,1)
            };

            Order[] testOrder =
            {
                new Order("小刘", "2019302110066",myOrderDetails),
                new Order("小詹", "2019302110067",myOrderDetails),
                new Order("小龙", "2019302110063",myOrderDetails),
                new Order("小徐", "2019302110068",myAnotherOrderDetails)
            };

            OrderService testOrderService = new OrderService(testOrder);

            List<Order> testOrders = testOrderService.FindOrderName("橡皮");
            List<Order> myOrders = new List<Order>();
            myOrders.Add(new Order("小徐", "2019302110068", myAnotherOrderDetails));
            CollectionAssert.AreEqual(testOrders, myOrders);
        }

        [TestMethod()]
        public void FindClientTest()
        {
            OrderDetails[] myOrderDetails =
            {
                new OrderDetails("笔记本", 4.0, 10),
                new OrderDetails("签字笔", 5.0 , 5),
                new OrderDetails("计算器", 7.5, 1),
            };

            OrderDetails[] myAnotherOrderDetails =
            {
                new OrderDetails("笔记本", 4.0, 10),
                new OrderDetails("签字笔", 5.0 , 5),
                new OrderDetails("计算器", 7.5, 1),
                new OrderDetails("橡皮",2,1)
            };

            Order[] testOrder =
            {
                new Order("小刘", "2019302110066",myOrderDetails),
                new Order("小詹", "2019302110067",myOrderDetails),
                new Order("小龙", "2019302110063",myOrderDetails),
                new Order("小徐", "2019302110068",myAnotherOrderDetails)
            };

            OrderService testOrderService = new OrderService(testOrder);

            List<Order> testOrders = testOrderService.FindClient("小詹");
            List<Order> myOrders = new List<Order>();
            myOrders.Add(new Order("小詹", "2019302110067", myOrderDetails));
            CollectionAssert.AreEqual(testOrders, myOrders);
        }

        [TestMethod()]
        public void FindTotalPriceTest()
        {
            OrderDetails[] myOrderDetails =
            {
                new OrderDetails("笔记本", 4.0, 10),
                new OrderDetails("签字笔", 5.0 , 5),
                new OrderDetails("计算器", 7.5, 1),
            };

            OrderDetails[] myAnotherOrderDetails =
            {
                new OrderDetails("笔记本", 4.0, 10),
                new OrderDetails("签字笔", 5.0 , 5),
                new OrderDetails("计算器", 7.5, 1),
                new OrderDetails("橡皮",2,1)
            };

            Order[] testOrder =
            {
                new Order("小刘", "2019302110066",myOrderDetails),
                new Order("小詹", "2019302110067",myOrderDetails),
                new Order("小龙", "2019302110063",myOrderDetails),
                new Order("小徐", "2019302110068",myAnotherOrderDetails)
            };

            OrderService testOrderService = new OrderService(testOrder);

            List<Order> testOrders = testOrderService.FindTotalPrice(72.5);
            List<Order> myOrders = new List<Order>();
            myOrders.Add(new Order("小刘", "2019302110066", myOrderDetails));
            myOrders.Add(new Order("小詹", "2019302110067", myOrderDetails));
            myOrders.Add(new Order("小龙", "2019302110063", myOrderDetails));
            CollectionAssert.AreEqual(testOrders, myOrders);
        }

        [TestMethod()]
        public void OrderSortTest()
        {
            OrderDetails[] myOrderDetails =
            {
                new OrderDetails("笔记本", 4.0, 10),
                new OrderDetails("签字笔", 5.0 , 5),
                new OrderDetails("计算器", 7.5, 1),
            };

            OrderDetails[] myAnotherOrderDetails =
            {
                new OrderDetails("笔记本", 4.0, 10),
                new OrderDetails("签字笔", 5.0 , 5),
                new OrderDetails("计算器", 7.5, 1),
                new OrderDetails("橡皮",2,1)
            };

            Order[] testOrder =
            {
                new Order("小刘", "2019302110066",myOrderDetails),
                new Order("小詹", "2019302110067",myOrderDetails),
                new Order("小龙", "2019302110063",myOrderDetails),
                new Order("小徐", "2019302110068",myAnotherOrderDetails)
            };


            Order[] myOrder =
            {
                new Order("小龙", "2019302110063",myOrderDetails),
                new Order("小刘", "2019302110066",myOrderDetails),
                new Order("小詹", "2019302110067",myOrderDetails),
                new Order("小徐", "2019302110068",myAnotherOrderDetails)
            };

            OrderService testOrderService = new OrderService(testOrder);
            OrderService myOrderService = new OrderService(myOrder);

            testOrderService.OrderSort();

            CollectionAssert.AreEqual(testOrderService.orderList, myOrderService.orderList);
        }

        [TestMethod()]
        public void ExportTest()
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
            };

            OrderService myOrderService = new OrderService(myOrder);

            myOrderService.Export("test.xml");

            FileStream testReadStream = new FileStream("test.xml",FileMode.Open, FileAccess.Read);
            FileStream myReadStream = new FileStream("orderExport.xml", FileMode.Open, FileAccess.Read);

            string testResult = "";
            string myResult = "";
            for (int a = testReadStream.ReadByte(); a != -1; a = testReadStream.ReadByte())
            {
                testResult += a;
            }

            for (int b = myReadStream.ReadByte(); b != -1; b = myReadStream.ReadByte())
            {
                myResult += b;
            }

            StringAssert.Equals(myResult, testResult);

        }

        [TestMethod()]
        public void ImportTest1()
        {
            OrderService testOrderService = new OrderService();
            testOrderService.Import("orderExport.xml");

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
            };

            OrderService myOrderService = new OrderService(myOrder);

            CollectionAssert.AreEqual(myOrderService.orderList, testOrderService.orderList);

        }

        [TestMethod()]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ImportTest2()
        {
            OrderService testOrderService = new OrderService();
            testOrderService.Import("anError.xml");
        }
    }
}