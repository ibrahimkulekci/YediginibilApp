using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yediginibil.WebUI.Areas.Admin.Models.Page
{
    public class AddViewModel:YediginiBil.Entities.Page
    {
        public IFormFile File { get; set; }
    }
}
