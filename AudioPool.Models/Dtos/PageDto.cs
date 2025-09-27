using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioPool.Models.Dtos
{
    public class PageDto<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int MaxPages { get; set; }
        public List<T> Items { get; set; } = new();
    }
}