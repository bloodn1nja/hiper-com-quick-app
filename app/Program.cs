using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace quick_app
{
    class Program
    {
        const string url = "https://www.auchandirect.pl/auchan-warszawa/pl/search?text=pepsi+cola";
        static void Main(string[] args)
        {
            List<string> item = new List<string>();
            HtmlWeb htmlWeb = new HtmlWeb();

            var doc = Request.GetPage(url);
            var products = doc.DocumentNode.SelectNodes("//div[@id='search-product-list']/article");
            foreach (var prod in products)
            {
                item.Add("title:" + prod.SelectSingleNode(".//div[@class='content']/div[@class='description']/a").Attributes["title"].Value);
                item.Add("price:" + (prod.SelectSingleNode(".//aside[contains(@class, 'prices')]/p[contains(@class, 'standard')]/span[contains(@class, 'p-nb')]").InnerText +
                    "." + prod.SelectSingleNode(".//aside[@class='prices']/p[@class='standard']/span[@class='p-cents']").InnerText));
                item.Add("packaging:" + prod.SelectSingleNode(".//div[@class = 'description'] / p[@class = 'packaging']/strong").InnerText);
                item.Add("imagePath:" + prod.SelectSingleNode(".//div[@class='picture']/img").GetAttributeValue("data-src", "nothing"));
            }
        }
    }
    class Request
    {
        public static HtmlDocument GetPage(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AllowAutoRedirect = false;
            request.Method = "GET";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            var stream = response.GetResponseStream();
            using (var reader = new StreamReader(stream))
            {
                var doc = new HtmlDocument();
                string html = reader.ReadToEnd();
                doc.LoadHtml(html);
                return doc;
            }
        }
    }
}
