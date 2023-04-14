using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yediginibil.WebUI.Models.Blog
{
    public class SearchViewModel
    {
        public List<YediginiBil.Entities.Blog> Blogs { get; set; }

        public string inputSearch { get; set; }
    }
}
