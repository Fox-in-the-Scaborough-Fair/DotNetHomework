using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Data.Entity;

namespace Homework8
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
       
            using (var db = new OrderDBContext())
            {
                if (db.Orders.ToList().Count ==0 )
                {
                    var orders = new Orders
                    {
                        client = "刘雨辛",
                        orderDetailsList = new List<OrderDetails>()
                        {
                            new OrderDetails() {OrderDetailsId=0,orderName = "铅", orderPrice = 1.5, orderNum = 5 },
                            new OrderDetails() {OrderDetailsId=1,orderName = "橡",orderPrice = 3.2, orderNum = 1 },
                            new OrderDetails() {OrderDetailsId=2,orderName = "??",orderPrice = 4.5, orderNum = 7 }
                        }
                    };
                    db.Orders.Add(orders);
                    db.SaveChanges();
                }
            }


            using (var db = new OrderDBContext())
            {
                orderBindingSource.DataSource = db.Orders.ToList();
            }

            using (var db = new OrderDBContext())
            {
                orderDetailsBindingSource.DataSource = db.OrderDetails.ToList();
            }

        }

        public void refresh()
        {
            using (var db = new OrderDBContext())
            {
                orderBindingSource.DataSource = db.Orders.ToList();
            }
            using (var db = new OrderDBContext())
            {
                orderDetailsBindingSource.DataSource = db.OrderDetails.ToList();
            }
            orderBindingSource.ResetBindings(false);
            orderDetailsBindingSource.ResetBindings(false);
        }

        Orders form2Order = new Orders();
        OrderService myOrderService = new OrderService();

        /*
        void f2_myClick(object sender, Form2.myEventArgs e)
        {
            form2Order = e.MyNewOrder;
        }
        */


        public List<Orders> myOrders = new List<Orders>();

        //Form2 createAndModify = new Form2();

        public int No;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            label1.Text = "新建订单";
            Form2 createAndModify = new Form2(myOrderService, true, No);
            //createAndModify.myClick += new Form2.myEventHandler(f2_myClick);
            createAndModify.Text = "新建订单";
            createAndModify.ShowDialog(this);
            refresh();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            label1.Text = "删除成功！";
            myOrderService.DeleteOrder(No);
            refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            label1.Text = "修改订单";
            Form2 createAndModify = new Form2(myOrderService, false, No);
            //createAndModify.myClick += new Form2.myEventHandler(f2_myClick);
            createAndModify.Text = "修改订单";
            createAndModify.ShowDialog(this);
            refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            label1.Text = "查询订单";
            using (var db = new OrderDBContext())
            {
                if (textBox1.Text == "")
                {
                    orderBindingSource.DataSource = db.Orders.ToList();
                    orderBindingSource.ResetBindings(false);
                }
                else
                {
                    try
                    {
                        orderBindingSource.DataSource = myOrderService.FindOrderNo(int.Parse(textBox1.Text));
                        orderBindingSource.ResetBindings(false);
                    }
                    catch (System.FormatException)
                    {
                    }
                }
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label1.Text = "导出订单";

            SaveFileDialog newExportXml = new SaveFileDialog();
            newExportXml.ShowDialog();
            if (myOrderService.Export(newExportXml.FileName))
            {
                MessageBox.Show("导出成功", "导出提示");
            }
            else
            {
                MessageBox.Show("导出失败", "导入提示");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label1.Text = "导入订单";
            OpenFileDialog newImportXml = new OpenFileDialog();
            newImportXml.ShowDialog();

            if (myOrderService.Import(newImportXml.FileName))
            {
                MessageBox.Show("导入成功", "导入提示");
            }
            else
            {
                MessageBox.Show("导入失败", "导入提示");
            }
            refresh();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

  
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            No = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["OrdersId"].Value.ToString());

            using (var db = new OrderDBContext())
            {
                var orderDetail = db.OrderDetails.Where(b => b.OrdersId == No).OrderBy(b => b.OrderDetailsId);
                orderDetailsBindingSource.DataSource = orderDetail.ToList();
            } 
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }


}
