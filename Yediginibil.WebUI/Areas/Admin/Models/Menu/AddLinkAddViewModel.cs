using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yediginibil.WebUI.Areas.Admin.Models.Menu
{
    public class AddLinkAddViewModel:YediginiBil.Entities.MenuLink
    {
        public int MenuId { get; set; }
        public List<SelectListItem> ParentMenuSelectList { get; set; }
    }
}
