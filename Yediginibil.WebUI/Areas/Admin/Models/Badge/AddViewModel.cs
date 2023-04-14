using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yediginibil.WebUI.Areas.Admin.Models.Badge
{
    public class AddViewModel:YediginiBil.Entities.Badge
    {
        public IFormFile File { get; set; }
    }
}
