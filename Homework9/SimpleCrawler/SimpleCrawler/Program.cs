using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCrawler
{
    public class MyCrawler
    {
        public Hashtable urls = new Hashtable();
        public int count = 0;
        public List<string> UrlGetList = new List<string>();
        public List<string> UrlLostList = new List<string>();

        public void Crawl()
        {
            Console.WriteLine("开始爬行了.... ");
            while (true)
            {
                string current = null;
                foreach (string url in urls.Keys)
                {
                    if ((bool)urls[url]) continue;
                    current = url;
                }

                if (current == null || count > 10) break;
                Console.WriteLine("爬行" + current + "页面!");
                string html = this.DownLoad(current); // 下载
                urls[current] = true;
                count++;
                this.Parse(html, current);//解析,并加入新的链接
                Console.WriteLine("爬行结束");
            }
        }

        private string DownLoad(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                string fileName = count.ToString();
                File.WriteAllText(fileName, html, Encoding.UTF8);
                this.UrlGetList.Add(url);
                return html;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                //添加URL爬取错误的信息
                UrlLostList.Add(ex.Message);
                return "";
            }
        }


        private void Parse(string html,string current)
        {
            string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+(html|htm|aspx|jsp)[""']";
            MatchCollection matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                strRef = match.Value.Substring(match.Value.IndexOf('=') + 1)
                          .Trim('"', '\"', '#', '>');
                if (strRef.Length == 0) continue;
                if (!Regex.IsMatch(strRef, @"^https://"))
                {
                    //去掉子地址开头的"/"，连接成完整地址
                    strRef = current + "/" + strRef;

                    //添加爬取成功的URL
                }
                if (urls[strRef] == null) urls[strRef] = false;
            }
        }
    }

    class SimpleCrawler
    {

        static void Main(string[] args)
        {
            MyCrawler myCrawler = new MyCrawler();
            string startUrl = "http://www.cnblogs.com/dstang2000/";
            if (args.Length >= 1) startUrl = args[0];
            myCrawler.urls.Add(startUrl, false);//加入初始页面
            new Thread(myCrawler.Crawl).Start();
        }
    }     
}
