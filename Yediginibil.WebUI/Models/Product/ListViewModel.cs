using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yediginibil.WebUI.Areas.Admin.Models;

namespace Yediginibil.WebUI.Models.Product
{
    public class ListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<YediginiBil.Entities.Product> Products { get; set; }
    }
}
