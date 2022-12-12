using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Common
{
    public class PageingParamerDto
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; } = 1;

        public string Search { get; set; }

        public long Count { get; set; }

        public string SortOrder { get; set; }

        public byte? IsActive { get; set; }

    }

    public class Pagination<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public long Rows { get; private set; }

        public Pagination(List<T> items, PageingParamerDto pageingParamer)
        {
            PageIndex = pageingParamer.PageIndex;
            TotalPages = (int)Math.Ceiling(pageingParamer.Count / (double)pageingParamer.PageSize);

            Rows = pageingParamer.Count;

            this.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<Pagination<T>> CreateAsync(IQueryable<T> source, PageingParamerDto pageingParamer)
        {
            pageingParamer.Count = await source.LongCountAsync();
            var items = await source.Skip((pageingParamer.PageIndex - 1) * pageingParamer.PageSize).Take(pageingParamer.PageSize).ToListAsync();
            return new Pagination<T>(items, pageingParamer);
        }
        
    }
}
