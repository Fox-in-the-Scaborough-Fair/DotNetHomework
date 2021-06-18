using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrawlerForm
{
    class Crawler
    {
        //已参考老师的代码并进行了删改
        public event Action<Crawler> StopCrawler;

        public event Action<Crawler, int, string, string> DownloadPage;

        private ConcurrentQueue<string> urlQueue = new ConcurrentQueue<string>();

        private ConcurrentDictionary<string, bool> urlDictionary = new ConcurrentDictionary<string, bool>();

        private readonly string strRef = @"(href|HREF)\s*=\s*[""'](?<url>[^""'#>]+)[""']";

        public static readonly string parseStrRef = @"^(?<site>(?<protocal>https?)://(?<host>[\w\d.-]+)(:\d+)?($|/))(\w+/)*(?<file>[^#?]*)";
        public string hostUrl { get; set; }
        public string fileUrl { get; set; }
        public int MaxPage { get; set; }
        public string StartURL { get; set; }
        public Crawler()
        {
            MaxPage = 100;
        }

        public void Start()
        {
            urlDictionary.Clear();

            urlQueue = new ConcurrentQueue<string>();

            urlQueue.Enqueue(StartURL);

            List<Task> tasks = new List<Task>();

            int completedCount = 0;//已完成的任务数

            DownloadPage += (crawler, index, url, info) => { completedCount++; };

            while (tasks.Count < MaxPage)
            {
                if (!urlQueue.TryDequeue(out string url))
                {
                    if (completedCount < tasks.Count)
                    {
                        continue;
                    }
                    else
                    {
                        break;//所有任务都完成，队列无url
                    }
                }

                int index = tasks.Count;

                Task task = Task.Run(() => DownloadAndParse(url, index));

                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray()); //等待剩余任务全部执行完毕

            StopCrawler(this);
        }

        private void DownloadAndParse(string url, int index)
        {
            try
            {
                string html = DownLoad(url, index);

                urlDictionary[url] = true;

                Parse(html, url);//解析,并加入新的链接

                DownloadPage(this, index, url, "success");
            }
            catch (Exception ex)
            {
                DownloadPage(this, index, url, "Error:" + ex.Message);
            }
        }

        private string DownLoad(string url, int index)
        {
            WebClient webClient = new WebClient();

            webClient.Encoding = Encoding.UTF8;

            string html = webClient.DownloadString(url);

            File.WriteAllText(index + ".html", html, Encoding.UTF8);

            return html;
        }

        private void Parse(string html, string pageUrl)
        {
            var matches = new Regex(strRef).Matches(html);

            foreach (Match match in matches)
            {
                string linkUrl = match.Groups["url"].Value;

                if (linkUrl == null || linkUrl == "" || linkUrl.StartsWith("javascript:")) continue;

                linkUrl = FixUrl(linkUrl, pageUrl);

                Match linkUrlMatch = Regex.Match(linkUrl, parseStrRef);

                string host = linkUrlMatch.Groups["host"].Value;

                string file = linkUrlMatch.Groups["file"].Value;

                if (file == "") file = "index.html";

                if (Regex.IsMatch(host, hostUrl) && Regex.IsMatch(file, fileUrl) 
                    && !urlDictionary.ContainsKey(linkUrl))
                {
                    urlQueue.Enqueue(linkUrl);

                    urlDictionary.TryAdd(linkUrl, false);
                }
            }
        }

        static private string FixUrl(string url, string baseUrl)
        {
            if (url.Contains("://"))
            {
                return url;
            }

            if (url.StartsWith("//"))
            {
                Match urlMatch = Regex.Match(baseUrl, parseStrRef);

                string protocal = urlMatch.Groups["protocal"].Value;

                return protocal + ":" + url;
            }

            if (url.StartsWith("/"))
            {
                Match urlMatch = Regex.Match(baseUrl, parseStrRef);

                String site = urlMatch.Groups["site"].Value;

                return site.EndsWith("/") ? site + url.Substring(1) : site + url;
            }

            if (url.StartsWith("../"))
            {
                url = url.Substring(3);

                int idx = baseUrl.LastIndexOf('/');

                return FixUrl(url, baseUrl.Substring(0, idx));
            }

            if (url.StartsWith("./"))
            {
                return FixUrl(url.Substring(2), baseUrl);
            }

            int end = baseUrl.LastIndexOf("/");

            return baseUrl.Substring(0, end) + "/" + url;
        }
    }
}
