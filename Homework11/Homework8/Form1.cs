using Homework6;
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

            orderBindingSource.DataSource = myOrders;

            dataGridView1.TopLeftHeaderCell.Value = "序号";
            dataGridView3.TopLeftHeaderCell.Value = "序号";


            /*
            using (var db = new OrderDBContext())
            {
                var orders = new Orders
                {
                    OrdersId = 1,
                    client = "刘雨辛",
                    orderDetailsList = new List<OrderDetails>()
                    {
                        new OrderDetails() { OrderDetailsId=0,orderName = "铅", orderPrice = 1.5, orderNum = 5 },
                        new OrderDetails() {OrderDetailsId=1,orderName = "橡",orderPrice = 3.2, orderNum = 1 },
                        new OrderDetails() {OrderDetailsId=2,orderName = "??",orderPrice = 4.5, orderNum = 7 }
                    }
                };
                db.Orders.Add(orders);
                db.SaveChanges();
            }

            using (var db = new OrderDBContext())
            {
                var orderDetails = new OrderDetails() { OrderDetailsId = 3, orderName = "橡皮",orderPrice = 3.2, orderNum = 1 };
                db.Entry(orderDetails).State = EntityState.Added;
                db.SaveChanges();
            }


            using (var db = new OrderDBContext())
            {
                var orderDetails = new OrderDetails() { OrderDetailsId = 4, orderName = "铅笔", orderPrice = 1.5, orderNum = 5 };
                db.Entry(orderDetails).State = EntityState.Added;
                db.SaveChanges();
            }
            */

            using (var db = new OrderDBContext())
            {
                var query = db.Orders.OrderBy(b => b.OrdersId);
                myOrders = query.ToList<Orders>();
            }
        }
        Orders form2Order = new Orders();

        void f2_myClick(object sender, Form2.myEventArgs e)
        {
            form2Order = e.MyNewOrder;
        }


        public List<Orders> myOrders = new List<Orders>();

        //Form2 createAndModify = new Form2();

        public int No;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            label1.Text = "新建订单";
            Form2 createAndModify = new Form2(myOrderService, true, No);
            createAndModify.myClick += new Form2.myEventHandler(f2_myClick);
            createAndModify.Text = "新建订单";
            createAndModify.ShowDialog(this);
            myOrderService.AddOrder(form2Order);
            orderBindingSource.ResetBindings(false);
            */
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            label1.Text = "删除成功！";
            myOrderService.DeleteByNum(No);
            orderBindingSource.ResetBindings(false);
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
            label1.Text = "修改订单";
            Form2 createAndModify = new Form2(myOrderService, false, No);
            createAndModify.myClick += new Form2.myEventHandler(f2_myClick);
            createAndModify.Text = "修改订单";
            createAndModify.Show(this);
            myOrderService.orderList[No] = form2Order;
            orderBindingSource.ResetBindings(false);
            */

        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*
            label1.Text = "查询订单";
            List<Orders> ordersFound;
            if (textBox1.Text == "")
            {
                orderBindingSource.DataSource = myOrderService.orderList;
                orderBindingSource.ResetBindings(false);
            }
            else
            {
                ordersFound = myOrderService.FindOrderNo(int.Parse(textBox1.Text));
                orderBindingSource.DataSource = ordersFound;
                orderBindingSource.ResetBindings(false);
            }
            */
        }

        private void button5_Click(object sender, EventArgs e)
        {
            /*
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
            */
        }

        private void button6_Click(object sender, EventArgs e)
        {
            /*
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
            orderBindingSource.ResetBindings(false);
            */
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                                e.RowBounds.Location.Y,
                                                dataGridView1.RowHeadersWidth - 4,
                                                e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics,
                                 (e.RowIndex + 1).ToString(),
                                 dataGridView1.RowHeadersDefaultCellStyle.Font,
                                 rectangle,
                                 dataGridView1.RowHeadersDefaultCellStyle.ForeColor,
                                 TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dataGridView3_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                    e.RowBounds.Location.Y,
                                    dataGridView3.RowHeadersWidth - 4,
                                    e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics,
                                 (e.RowIndex + 1).ToString(),
                                 dataGridView3.RowHeadersDefaultCellStyle.Font,
                                 rectangle,
                                 dataGridView3.RowHeadersDefaultCellStyle.ForeColor,
                                 TextFormatFlags.VerticalCenter | TextFormatFlags.Right);

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            using (var db = new OrderDBContext())
            {
                var query = db.Orders.SingleOrDefault(b => b.OrdersId == ;
                myOrders = query.ToList<Orders>();
            }
            orderDetailsBindingSource.DataSource = myOrderService.orderList[e.RowIndex].orderDetailsList;
            orderDetailsBindingSource.ResetBindings(false);
            No = e.RowIndex;
            
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
