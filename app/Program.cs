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
        const String path = @"text.txt";
        const String web = "https://www.auchandirect.pl";
        static void Main(string[] args)
        {
            Item[] items = new Item[200];
            HtmlDocument document = new HtmlDocument();
            document.Load(path);
            int i = 0;
            foreach (HtmlNode node in document.DocumentNode.SelectNodes("//div[@id='search-product-list']/article"))
            {
                items[i] = new Item();
                items[i].tittle= node.SelectNodes("//div[@class='content']/div[@class='description']/a")[i].Attributes["title"].Value;
                items[i].price = Double.Parse(node.SelectNodes("//p[@class='standard']/span[@class='p-nb']")[i].InnerText) 
                    + Double.Parse(node.SelectNodes("//aside[@class='prices']/p[@class='standard']/span[@class='p-cents']")[i].InnerText)/100;
                items[i].packaging = node.SelectNodes("//div[@class = 'description'] / p[@class = 'packaging']/strong")[i].InnerText;
                items[i].imagePath=web+ node.SelectNodes("//div[@class='picture']/img")[i].GetAttributeValue("data-src","nothing");
                i++;
            }
            Console.ReadLine(); // Make an breakpoint to see results 
        }
    }

    class Item
    {
        public string imagePath, tittle, packaging;
        public double price;
        public Item()
        {

        }
    }
}
