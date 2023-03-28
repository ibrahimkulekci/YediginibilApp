using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yediginibil.WebUI.Areas.Admin.Models.Ingredient
{
    public class AddViewModel:YediginiBil.Entities.Ingredient
    {
        public IFormFile File { get; set; }
    }
}
