using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Linq;

namespace EndPoint.Sitemap
{
    public class SitemapGenerator
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public SitemapGenerator(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        public void GenerateSitemap()
        {
            var urls = _actionDescriptorCollectionProvider.ActionDescriptors.Items
                .Where(ad => ad.AttributeRouteInfo != null)
                .Select(ad => ad.AttributeRouteInfo.Template)
                .ToList();

            foreach (var url in urls)
            {
                // اینجا فرض بر این است که تابع add_url شما به صورت زیر است:
                // add_url(string url, DateTime modified, string changeFrequency, string priority)
                add_url($"https://example.ir/{url}", DateTime.Now, "daily", "1.0");
            }
        }

        // تابع add_url خود را اینجا قرار دهید
        private void add_url(string url, DateTime modified, string changeFrequency, string priority)
        {
            // کد مربوط به اضافه کردن URL به سایت مپ
        }
    }
}
