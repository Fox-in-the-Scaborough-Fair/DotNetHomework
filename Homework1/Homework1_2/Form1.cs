using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework1_2
{
    public partial class Form1 : Form
    {
        double num1;
        double num2;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            num1 = double.Parse(textBox1.Text);
            num2 = double.Parse(textBox2.Text);
            switch (comboBox1.Text)     //选择运算符
            {
                case "+":
                    textBox3.Text = Convert.ToString(num1 + num2);
                    break;
                case "-":
                    textBox3.Text = Convert.ToString(num1 - num2);
                    break;
                case "*":
                    textBox3.Text = Convert.ToString(num1 * num2);
                    break;
                case "/":
                    if (num2 == 0)
                        textBox3.Text = "输入错误，结果无效！";
                    else
                        textBox3.Text = Convert.ToString(num1 / num2);
                    break;
                case "%":
                    if (num2 == 0)
                        textBox3.Text = "输入错误，结果无效！";
                    else
                        textBox3.Text = Convert.ToString(num1 % num2);
                    break;
                default:
                    textBox3.Text = "输入错误，结果无效！";
                    break;
            }
        }
    }
}
