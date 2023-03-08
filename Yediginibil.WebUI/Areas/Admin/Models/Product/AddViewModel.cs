using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace Yediginibil.WebUI.Areas.Admin.Models.Product
{
    public class AddViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Barcode { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }

        public string SeoTitle { get; set; }
        public string SeoUrl { get; set; }
        public string SeoDescription { get; set; }


        public DateTime CreatingDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public List<ProductImages> ProductImages { get; set; }

        public IFormFile File { get; set; }
    }
}
