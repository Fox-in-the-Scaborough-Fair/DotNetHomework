using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Homework8
{
    public partial class Form2 : Form
    {
        public Form2(OrderService myOrderService, bool createOrModifyIn, int postNo)
        {
            InitializeComponent();
            tempOrderService = myOrderService;
            createOrModify = createOrModifyIn;
            if (createOrModify)
            {
                this.Text = "新建订单";

            }
            else
            {
                this.Text = "修改订单";
                using (var db = new OrderDBContext())
                {
                    var order = db.Orders.SingleOrDefault(b => b.OrdersId == postNo);
                    var orderDetail = db.OrderDetails.Where(b => b.OrdersId == postNo).OrderBy(b => b.OrderDetailsId);
                    if (order != null)
                    {
                        tempOrder = order;
                        tempOrder.orderDetailsList = orderDetail.ToList();
                        textBox1.Text = $"{order.OrdersId}";
                        textBox2.Text = order.client;
                    }
                }
            }
            outNo = postNo;
            OrderDetailsBindingSource.DataSource = tempOrder.orderDetailsList;
        }

        OrderService tempOrderService = new OrderService();

        /*
        public class myEventArgs : EventArgs
        {
            Orders myNewOrder;
            public Orders MyNewOrder
            {
                get
                {
                    return myNewOrder;
                }
                set
                {
                    if (value != null)
                        myNewOrder = value;
                    else
                        myNewOrder = null;
                }
            }
            public myEventArgs(Orders s)
            {
                this.MyNewOrder = s;
            }

        }

        public delegate void myEventHandler(object sender, myEventArgs e);

        public event myEventHandler myClick;
        */

        public int No;
        public int outNo;
        public bool createOrModify;

        Orders temp = new Orders();
        Orders tempOrder = new Orders();

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }



        private void button4_Click(object sender, EventArgs e)
        {

            tempOrder.client = textBox2.Text;

            /*
            if (this.myClick != null)
            {
                this.myClick(this, new myEventArgs(tempOrder));
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
            */

            if (createOrModify)
            {
                tempOrderService.AddOrder(tempOrder);
            }
            else
            {
                using (var db = new OrderDBContext())
                {
                    var order = db.Orders.FirstOrDefault(p => p.OrdersId == outNo);
                    if (order != null)
                    {
                        order.client = tempOrder.client;
                        foreach (OrderDetails anOrder in tempOrder.orderDetailsList)
                        {
                            order.totalPrice += anOrder.orderPrice * anOrder.orderNum;
                        }
                        order.orderDetailsList = tempOrder.orderDetailsList;
                        db.SaveChanges();
                    }
                }
            }


            MessageBox.Show("保存成功");

        }


        //输入订单号
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //输入客户
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        //添加明细
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OrderDetails newDetails = new OrderDetails(textBox3.Text, Convert.ToDouble(textBox4.Text), int.Parse(textBox5.Text));
                tempOrder.orderDetailsList.Add(newDetails);
            }
            catch (System.FormatException)
            {

            }

            //OrderDetailsBindingSource.DataSource = tempOrder.orderDetailsList;
            OrderDetailsBindingSource.ResetBindings(false);


        }


        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            No = e.RowIndex;
        }

        private void dataGridView2_RowPostPaint_1(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                        e.RowBounds.Location.Y,
                        dataGridView2.RowHeadersWidth - 4,
                        e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics,
                                 (e.RowIndex + 1).ToString(),
                                 dataGridView2.RowHeadersDefaultCellStyle.Font,
                                 rectangle,
                                 dataGridView2.RowHeadersDefaultCellStyle.ForeColor,
                                 TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        //输入商品名称
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        //输入商品价格
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        //输入商品数量
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }


        //修改明细
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                
                tempOrder.orderDetailsList[No].orderName = textBox3.Text;
                tempOrder.orderDetailsList[No].orderPrice = Convert.ToDouble(textBox4.Text);
                tempOrder.orderDetailsList[No].orderNum = int.Parse(textBox5.Text);
                //OrderDetailsBindingSource.DataSource = tempOrder.orderDetailsList;
                OrderDetailsBindingSource.ResetBindings(false);
                
            }
            catch (System.FormatException)
            {

            }
            catch (System.ArgumentOutOfRangeException)
            {

            }
        }

        //删除明细
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                tempOrder.orderDetailsList.RemoveAt(No);
                //OrderDetailsBindingSource.DataSource = tempOrder.orderDetailsList;
                OrderDetailsBindingSource.ResetBindings(false);
            }
            catch (System.FormatException)
            {

            }
            catch (System.ArgumentOutOfRangeException)
            {

            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
