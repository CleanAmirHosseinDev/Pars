using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EndPoint.Sitemap
{
    public class SitemapList : Controller
    {
        private readonly List<SitemapModel> _sitemap_list;

        public SitemapList()
        {
            _sitemap_list = new List<SitemapModel>();
        }
        public void add_url(SitemapModel item)
        {
            _sitemap_list.Add(item);
        }
        public void add_url(string url, DateTime modified, string changeFrequency, string priority)
        {
            SitemapModel item = new SitemapModel(url, modified, changeFrequency, priority);
            _sitemap_list.Add(item);
        }
        public List<SitemapModel> GetSitemapItems()
        {
            return _sitemap_list;
        }
        public XElement CreateUrlElement(SitemapModel item)
        {
            return new XElement("url",
                new XElement("loc", $"https://parscrc.ir{item.Url}"),
                new XElement("lastmod", item.Modified.ToString()),
                new XElement("changefreq", item.ChangeFrequency),
                new XElement("priority", item.Priority)
            );
        }
    }
}
