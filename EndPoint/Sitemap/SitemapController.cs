using Microsoft.AspNetCore.Mvc;
using ParsKyanCrm.Application.Dtos.Users;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using System;
using System.Linq;
using System.Text;
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
        public async System.Threading.Tasks.Task<IActionResult> IndexAsync()
        {
            add_url(Url.Action("Index", "Home"), modified: DateTime.Now, changeFrequency: "daily", priority: "1.0");
            var content = (await _userFacad.GetNewsAndContentsService.Execute(new RequestNewsAndContentDto()));

            //add_url(Url.Action("Article", "Content", obj.ContentId), (DateTime)obj.DateSave, "monthly", priority: "1.0");

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