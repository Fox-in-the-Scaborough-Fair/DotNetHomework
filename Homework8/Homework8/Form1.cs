using Homework6;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Homework8
{


    public partial class Form1 : Form
    {
        public Action<string> SendMsg { get; set; }

        public Form1()
        {
            InitializeComponent();

            dataGridView1.DataSource = myOrderService.orderList;

            dataGridView1.TopLeftHeaderCell.Value = "序号";
            dataGridView3.TopLeftHeaderCell.Value = "序号";

        }

        //OrderService myOrderService = new OrderService();

        static OrderDetails[] myOrderDetails1 = new OrderDetails[4]
            {
                new OrderDetails("笔记本", 4.0, 1),
                new OrderDetails("签字笔", 5.0 , 5),
                new OrderDetails("计算器", 7.5, 1),
                new OrderDetails("圆规", 10.0, 1),
            };

        static OrderDetails[] myOrderDetails2 = new OrderDetails[3]
        {
            new OrderDetails("笔记本", 4.0, 2),
            new OrderDetails("签字笔", 5.0 , 5),
            new OrderDetails("计算器", 7.5, 1),
        };
        static OrderDetails[] myOrderDetails3 = new OrderDetails[2]
        {
            new OrderDetails("橡皮",1.5, 1),
            new OrderDetails("铅笔", 1.0 , 5),
        };
        static OrderDetails[] myOrderDetails4 = new OrderDetails[1]
        {
                new OrderDetails("笔记本", 4.0, 4),
        };

        static Order[] myOrder =
        {
                new Order("小刘", "2019302110066",myOrderDetails1),
                new Order("小詹", "2019302110067",myOrderDetails2),
                new Order("小龙", "2019302110063",myOrderDetails3),
                new Order("小徐", "2019302110068",myOrderDetails4)
            };

        public OrderService myOrderService = new OrderService(myOrder);

        Form2 createAndModify = new Form2();

        public int No;

        public event EventHandler SendMsgEvent;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "新建订单";
            Form2 createAndModify = new Form2();
            createAndModify.Text = "新建订单";
            createAndModify.ShowDialog(this);
            createAndModify.createOrModify = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "删除成功！";
            myOrderService.DeleteByNum(No);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = myOrderService.orderList;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = "修改订单";
            Form2 createAndModify = new Form2();
            createAndModify.fatherForm = this;
            createAndModify.Text = "修改订单";
            createAndModify.Show(this);
            createAndModify.createOrModify = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = "查询订单";
            List<Order> ordersFound;
            if (textBox1.Text == "")
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = myOrderService.orderList;
            }
            else
            {
                ordersFound = myOrderService.FindOrderNo(textBox1.Text);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = ordersFound;
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
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = myOrderService.orderList;
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
            dataGridView3.DataSource = myOrderService.orderList[e.RowIndex].orderDetailsList;
            No = e.RowIndex;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }


}
