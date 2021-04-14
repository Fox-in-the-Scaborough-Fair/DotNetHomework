using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox1.Refresh();
            graphics = null;
            
            try
            {
                n = int.Parse(textBox1.Text);
            }
            catch (FormatException)
            {
                label9.Text = "递归深度参数错误，作图失败!";
            }

            try
            {
                leng = int.Parse(textBox2.Text);
            }
            catch (FormatException)
            {
                label9.Text = "主干长度参数错误，作图失败!";
            }

            if (graphics == null)
            {
                graphics = pictureBox1.CreateGraphics();
                drawCaleyTree(n, pictureBox1.Width/2, pictureBox1.Bottom, leng, -Math.PI / 2);
                label9.Text = "";
            }
        }

        private Graphics graphics;
        double th1 = 30 * Math.PI / 180;
        double th2 = 20 * Math.PI / 180;
        double per1 = 0;
        double per2 = 0;
        int n = 0;
        double leng = 0;
        string pen = "";


        void drawCaleyTree(int n, double x0, double y0, double leng, double th)
        {
            if (n == 0)
            {
                return;
            }
            double x1 = x0 + leng * Math.Cos(th);
            double y1 = y0 + leng * Math.Sin(th);

            drawLine(x0, y0, x1, y1);

            drawCaleyTree(n - 1, x1, y1, per1 * leng, th + th1);
            drawCaleyTree(n - 1, x1, y1, per2 * leng, th - th2);
            
        }

        void drawLine(double x0, double y0, double x1, double y1)
        {
            switch (pen)
            {
                case "red":
                    {
                        graphics.DrawLine(Pens.Red, (int)x0, (int)y0, (int)x1, (int)y1);
                        break;
                    }
                case "yellow":
                    {
                        graphics.DrawLine(Pens.Yellow, (int)x0, (int)y0, (int)x1, (int)y1);
                        break;
                    }
                case "green":
                    {
                        graphics.DrawLine(Pens.Green, (int)x0, (int)y0, (int)x1, (int)y1);
                        break;
                    }
                case "blue":
                    {
                        graphics.DrawLine(Pens.Blue, (int)x0, (int)y0, (int)x1, (int)y1);
                        break;
                    }
                default:
                    {
                        graphics.DrawLine(Pens.Black, (int)x0, (int)y0, (int)x1, (int)y1);
                        break;
                    }
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            per1 = Convert.ToDouble(trackBar1.Value) / 100;
            label3.Text = "右长度分支比: " + per1;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            per2 = Convert.ToDouble(trackBar2.Value) / 100;
            label4.Text = "左长度分支比: " + per2;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            th1 = Convert.ToDouble(trackBar3.Value) / 1800;
            label5.Text = "右分支角度: " + Math.Round(th1, 3) * 180 + "°";
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            th2 = Convert.ToDouble(trackBar4.Value) / 1800;
            label6.Text = "左分支角度: " + Math.Round(th2,3) * 180 + "°";
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "红色":
                    {
                        pen = "red";
                        break;
                    }
                case"黄色":
                    {
                        pen = "yellow";
                        break;
                    }
                case "绿色":
                    {
                        pen = "green";
                        break;
                    }
                case "蓝色":
                    {
                        pen = "blue";
                        break;
                    }
                default:
                    {
                        pen = "black";
                        break;
                    }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
