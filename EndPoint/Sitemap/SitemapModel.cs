using System;

namespace EndPoint.Sitemap
{
    public class SitemapModel
    {
        public string Url { get; }
        public DateTime Modified { get; }
        public string ChangeFrequency { get; }
        public string Priority { get; }

        public SitemapModel(string url, DateTime? modified, string? changeFrequency, string? priority)
        {
            Url = url;
            Modified = (DateTime)modified;
            ChangeFrequency = changeFrequency;
            Priority = priority;
        }
        // ChangeFrequency : always - hourly - daily - weekly - monthly - yearly - never
    }
}
