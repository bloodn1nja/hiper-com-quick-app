using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace app
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
                item.Add("imagePath:" + prod.SelectSingleNode("..//div[@class='picture']/img").GetAttributeValue("data-src", "nothing"));
            }
        }
    }
}
