using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yediginibil.WebUI.Areas.Admin.Models.Badge
{
    public class ListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<YediginiBil.Entities.Badge> Badges { get; set; }
    }
}
