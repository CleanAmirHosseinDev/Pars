using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Mvc;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Enums;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace EndPoint.Sitemap
{
    public class SitemapController : SitemapList
    {
        private IUserFacad _userFacad;
        public SitemapController(IUserFacad userFacad)
        {
            _userFacad = userFacad;
        }
        [HttpGet("sitemap.xml")]
        public async Task<IActionResult> IndexAsync()
        {
            add_url(Url.Action("Index", "Home"), modified: DateTime.Now, changeFrequency: "never", priority: "1.0");
            add_url(Url.Action("Register", "Home"), modified: DateTime.Now, changeFrequency: "never", priority: "1.0");
            add_url(Url.Action("RankList", "Home"), modified: DateTime.Now, changeFrequency: "never", priority: "1.0");
            add_url(Url.Action("ContentList", "Article"), modified: DateTime.Now, changeFrequency: "never", priority: "1.0");
            add_url(Url.Action("NewsList", "Article"), modified: DateTime.Now, changeFrequency: "never", priority: "1.0");
            add_url(Url.Action("Index", "ContactUs"), modified: DateTime.Now, changeFrequency: "never", priority: "1.0");

            var article = (await _userFacad.GetNewsAndContentsService.Execute(new RequestNewsAndContentDto()
            {
                KindOfContent = 61,
                IsActive = (byte)TablesGeneralIsActive.Active,
                PageSize = 0,
                PageIndex = 0
            })).Data;

            var content = (await _userFacad.GetNewsAndContentsService.Execute(new RequestNewsAndContentDto()
            {
                KindOfContent = 62,
                IsActive = (byte)TablesGeneralIsActive.Active,
                PageSize = 0,
                PageIndex = 0
            })).Data;
			
			var person = (await _userFacad.GetNewsAndContentsService.Execute(new RequestNewsAndContentDto()
            {
                KindOfContent = 60,
                IsActive = (byte)TablesGeneralIsActive.Active,
                PageSize = 0,
                PageIndex = 0
            })).Data;

            foreach (var item in article.ToList())
            {
                add_url(Url.Action("News", "Article", new { id = item.ContentId }), (DateTime)item.DateSave, "monthly", priority: "1.0");
            }
            foreach (var item in content.ToList())
            {
                add_url(Url.Action("Content", "Article", new { id=item.ContentId }), (DateTime)item.DateSave, "monthly", priority: "1.0");
            }
			
			foreach (var item in person.ToList())
            {
                add_url(Url.Action("Page", "Article", new { dlink=item.DirectLink }), (DateTime)item.DateSave, "monthly", priority: "1.0");
            }
            //var sitemap = new XDocument(
            //    new XDeclaration("1.0", "utf-8", "yes"),
            //    new XElement("urlset",
            //        new XAttribute(XNamespace.Xmlns + "xhtml", "http://www.w3.org/1999/xhtml"),
            //        new XAttribute("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9"),
    
            //        from item in GetSitemapItems()
            //        select CreateUrlElement(item)
            //    )
            //);

            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XNamespace xhtmlNs = "http://www.w3.org/1999/xhtml";
            var sitemap = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(ns + "urlset",
                    new XAttribute("xmlns", ns),
                    new XAttribute(XNamespace.Xmlns + "xhtml", xhtmlNs),

                    from item in GetSitemapItems()
                    select CreateUrlElement(item, ns)
                )
            );
            var sitemapStr = sitemap.ToString();
            byte[] sitemapByte = Encoding.UTF8.GetBytes(sitemapStr);
			return new FileContentResult(sitemapByte, "application/xml");
        }
    }

}