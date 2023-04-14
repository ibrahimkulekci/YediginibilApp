using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace Yediginibil.WebUI.Models.Product
{
    public class DetailsViewModel
    {
        //public YediginiBil.Entities.Product Product { get; set; }
        public IEnumerable<YediginiBil.Entities.Ingredient> Ingredients { get; set; }
        public List<YediginiBil.Entities.Comment> Comments { get; set; }
        public int CommentCount { get; set; }
        public int CommentStarAvg { get; set; }

        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string SeoUrl { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }


    }
}
