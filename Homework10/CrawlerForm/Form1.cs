using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrawlerForm
{
    public partial class Form1 : Form
    {
        BindingSource resultBindingSource = new BindingSource();

        Crawler crawler = new Crawler();

        public Form1()
        {
            InitializeComponent();

            urlResult.DataSource = resultBindingSource;

            crawler.DownloadPage += Crawler_PageDownloaded;

            crawler.StopCrawler += Crawler_CrawlerStopped;
        }


        private void StartCrawler_Click(object sender, EventArgs e)
        {
            resultBindingSource.Clear();

            crawler.StartURL = txtUrl.Text;

            Match match = Regex.Match(crawler.StartURL, Crawler.parseStrRef);

            if (match.Length == 0) return;

            string host = match.Groups["host"].Value;

            crawler.hostUrl = "^" + host + "$";

            crawler.fileUrl = "((.html?|.aspx|.jsp|.php)$|^[^.]+$)";

            new Thread(crawler.Start).Start();

            lblInfo.Text = "爬虫已启动";
        }

        private void Crawler_CrawlerStopped(Crawler obj)
        {
            Action action = () => lblInfo.Text = "爬虫已停止";

            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void Crawler_PageDownloaded(Crawler crawler, int index, string url, string info)
        {
            var pageInfo = new { Index = resultBindingSource.Count + 1, URL = url, Status = info };

            Action action = () => { resultBindingSource.Add(pageInfo); };

            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }

}