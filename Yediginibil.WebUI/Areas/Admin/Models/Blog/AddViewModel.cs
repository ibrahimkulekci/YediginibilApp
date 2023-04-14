using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yediginibil.WebUI.Areas.Admin.Models.Blog
{
    public class AddViewModel:YediginiBil.Entities.Blog
    {
        public IFormFile File { get; set; }
        public List<SelectListItem> BlogCategorySelectList { get; set; }
    }
}
