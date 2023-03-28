using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YediginiBil.Business.Model.Ingredient;

namespace Yediginibil.WebUI.Areas.Admin.Models.Ingredient
{
    public class ListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<YediginiBil.Entities.Ingredient> Ingredients { get; set; }
    }
}
