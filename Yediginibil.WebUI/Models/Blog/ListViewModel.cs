using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yediginibil.WebUI.Areas.Admin.Models;

namespace Yediginibil.WebUI.Models.Blog
{
    public class ListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<YediginiBil.Entities.Blog> Blogs { get; set; }
    }
}
