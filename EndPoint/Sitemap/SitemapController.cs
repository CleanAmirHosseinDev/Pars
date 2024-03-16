using EndPoint.Sitemap;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace EndPoint.Sitemap
{
    public class SitemapController : SitemapList
    {
        [HttpGet("sitemap.xml")]
        public IActionResult Index()
        {
            add_url(Url.Action("Index", "Home"), modified: DateTime.Now, changeFrequency: "daily", priority: "1.0");


            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            var sitemap = new XDocument(new XDeclaration("1.0", "utf-8", null),
                new XElement(ns + "urlset",
                    // Add URL entries here
                    from item in GetSitemapItems()
                    select CreateUrlElement(item)
                ));

            return Content(sitemap.ToString(), "text/xml", Encoding.UTF8);
        }
    }

}