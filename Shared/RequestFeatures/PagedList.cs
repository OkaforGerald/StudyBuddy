using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedAPI.RequestFeatures
{
    public class PagedList<T> : List<T>
    {
        public Metadata metadata;

        public PagedList(IEnumerable<T> items, int PageSize, int CurrentPage, int count)
        {
            metadata = new Metadata
            {
                CurrentPage = CurrentPage,
                PageSize = PageSize,
                PageCount = (int) Math.Ceiling(count/(double)PageSize),
                TotalCount = count
            };
            
            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> source, int CurrentPage, int PageSize)
        {
            var count = source.Count();
            var items = source.Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            return new PagedList<T>(items, PageSize, CurrentPage, count);
        }
    }
}
