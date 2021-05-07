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

namespace CrawlerForm {
  class Crawler {
    public event Action<Crawler> CrawlerStopped;
    public event Action<Crawler,int,string,string> PageDownloaded;
        //待下载队列
    public ConcurrentQueue<string> pending = new ConcurrentQueue<string>();
    //已下载网页
    public ConcurrentDictionary<string, bool> urls = new ConcurrentDictionary<string, bool>();
    //URL检测表达式，用于在HTML文本中查找URL
    public static readonly string UrlDetectRegex = @"(href|HREF)[]*=[]*[""'](?<url>[^""'#>]+)[""']";
    //URL解析表达式
    public static readonly string urlParseRegex = @"^(?<site>(?<protocal>https?)://(?<host>[\w.-]+)(:\d+)?($|/))(\w+/)*(?<file>[^#?]*)";
    //主机过滤规则
    public string HostFilter { get; set; }
    //文件过滤规则
    public string FileFilter { get; set; }
    //最大下载数量
    public int MaxPage { get; set; }
    //起始网址
    public string StartURL { get; set; }
    //网页编码
    public Encoding HtmlEncoding { get; set; } 
   
    public Crawler() {
      MaxPage = 100;
      HtmlEncoding = Encoding.UTF8;
    }

    public void Start() 
    {
        urls.Clear();
        pending = new ConcurrentQueue<string>();
        pending.Enqueue(StartURL);

        List<Task> tasks = new List<Task>();
        int complatedCount = 0;
        PageDownloaded += (crawler, index, url, info) => { complatedCount++; };

        while (tasks.Count < MaxPage)
        {
            if (!pending.TryDequeue(out string url))
            {
                if (complatedCount < tasks.Count)
                {
                    continue;
                }
                else 
                {
                    break;
                }
            }
            int index = tasks.Count;
            Task task = Task.Run(() => DownloadAndParse(url, index));
            tasks.Add(task);
        }
        Task.WaitAll(tasks.ToArray());
        CrawlerStopped(this);

      while (urls.Count < MaxPage && pending.Count > 0) {
        pending.TryDequeue(out string url);
        try {
          string html = DownLoad(url); // 下载
          urls[url] = true;      //标识为已爬过
          PageDownloaded(this,tasks.Count,url,"success");
          Parse(html, url);//解析,并加入新的链接
        }catch (Exception ex) {
          PageDownloaded(this,tasks.Count,url,"  Error:" + ex.Message);
        }
      }
      CrawlerStopped(this);
    }

        void DownloadAndParse(string url, int index)
        {
            while (index < MaxPage)
            {
                Parse(DownLoad(url), url);
            }
        }

    private string DownLoad(string url) {
      WebClient webClient = new WebClient();
      webClient.Encoding = Encoding.UTF8;
      string html = webClient.DownloadString(url);
      string fileName = urls.Count.ToString();
      File.WriteAllText(fileName, html, Encoding.UTF8);
      return html;
    }

    private void Parse(string html, string pageUrl) {
      var matches = new Regex(UrlDetectRegex).Matches(html);
      foreach (Match match in matches) {
        string linkUrl = match.Groups["url"].Value;
        if (linkUrl == null || linkUrl == ""||linkUrl.StartsWith("javascript:")) continue;

        linkUrl = FixUrl(linkUrl, pageUrl);//转绝对路径
        //解析出host和file两个部分，进行过滤
        Match linkUrlMatch = Regex.Match(linkUrl, urlParseRegex);
        string host = linkUrlMatch.Groups["host"].Value;
        string file= linkUrlMatch.Groups["file"].Value;
        if (Regex.IsMatch(host, HostFilter)&&Regex.IsMatch(file, FileFilter) 
          &&!urls.ContainsKey(linkUrl)) {
          pending.Enqueue(linkUrl);
        }
      }
    }

    //将非完整路径转为完整路径
    static private string FixUrl(string url, string pageUrl) {
      if (url.Contains("://")) { //完整路径
        return url;
      }
      if (url.StartsWith("//")) {
        Match urlMatch = Regex.Match(pageUrl, urlParseRegex);
        string protocal = urlMatch.Groups["protocal"].Value;
        return protocal + ":" + url;
      }
      if (url.StartsWith("/")) {
        Match urlMatch = Regex.Match(pageUrl, urlParseRegex);
        String site = urlMatch.Groups["site"].Value; 
        return site.EndsWith("/") ? site + url.Substring(1) : site + url;
      }
      
      if (url.StartsWith("../")) {
        url = url.Substring(3);
        int idx = pageUrl.LastIndexOf('/');
        return FixUrl(url, pageUrl.Substring(0, idx));
      }

      if (url.StartsWith("./")) {
        return FixUrl(url.Substring(2), pageUrl);
      }
      //非上述开头的相对路径
      int end = pageUrl.LastIndexOf("/");
      return pageUrl.Substring(0, end) + "/" + url;
    }

  }
}
