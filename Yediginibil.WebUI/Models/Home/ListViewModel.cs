using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yediginibil.WebUI.Models.Home
{
    public class ListViewModel
    {
        public List<YediginiBil.Entities.Product> Products { get; set; }
        public List<YediginiBil.Entities.Blog> Blogs { get; set; }
    }
}
