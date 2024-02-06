using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedAPI.RequestFeatures
{
    public class Metadata
    {
        public int CurrentPage { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public int PageCount { get; set; }

        public int TotalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < PageCount;
    }
}
