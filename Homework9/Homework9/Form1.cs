using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using SimpleCrawler;

namespace Homework9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //绑定数据
            UrlGet.DataSource = myCrawler.UrlGetList;
            UrlLose.DataSource = myCrawler.UrlLostList;
        }

        MyCrawler myCrawler = new MyCrawler();

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                myCrawler.urls.Add(UrlInput.Text, false);
                new Thread(myCrawler.Crawl).Start();
            }
            catch (System.ArgumentException)
            {
                Warning.Text = "请勿重复添加相同网址!";
            }

            UrlGet.ResetBindings(false);
            UrlLose.ResetBindings(false);
        }

        private void UrlInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void UrlGetView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}