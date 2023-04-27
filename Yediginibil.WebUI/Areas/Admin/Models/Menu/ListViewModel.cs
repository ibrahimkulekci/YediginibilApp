using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yediginibil.WebUI.Areas.Admin.Models.Menu
{
    public class ListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<YediginiBil.Entities.Menu> Menus { get; set; }

        public string MenuTitle { get; set; }
        public int MenuId { get; set; }
        public List<YediginiBil.Entities.MenuLink> MenuLinks { get; set; }
    }
}
