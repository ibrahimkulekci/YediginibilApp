using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public int BrandId { get; set; }
        public string[] IngredientList { get; set; }


        public DateTime? CreatingDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public IFormFile File { get; set; }

        public List<SelectListItem> BrandSelectList { get; set; }
        public List<SelectListItem> IngredientSelectList { get; set; }

        //
        public List<SelectListItem> drpIngredients { get; set; }
        public int[] IngredientsIds { get; set; }
        //badge select list
        public List<SelectListItem> BadgeSelectList { get; set; }
        public List<SelectListItem> drpBadges { get; set; }
        public int[] BadgesIds { get; set; }

        //
        public string[] nutritiveName { get; set; }
        public string[] nutritiveValue { get; set; }
        public IEnumerable<Nutritive> Nutritives { get; set; }


    }
}
