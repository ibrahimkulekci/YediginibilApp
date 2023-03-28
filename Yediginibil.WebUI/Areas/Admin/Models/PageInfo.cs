using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yediginibil.WebUI.Areas.Admin.Models
{
    public class PageInfo
    {
        public long TotalItems { get; set; }
        public long ItemsPerPage { get; set; }
        public long CurrentPage { get; set; }


        public long TotalPages()
        {
            return (long)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }
    }
}
