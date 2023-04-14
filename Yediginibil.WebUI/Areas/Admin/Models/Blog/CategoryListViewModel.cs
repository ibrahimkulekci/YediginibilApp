using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yediginibil.WebUI.Areas.Admin.Models.Blog
{
    public class CategoryListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<YediginiBil.Entities.BlogCategory> BlogCategories { get; set; }
    }
}
