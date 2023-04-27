using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YediginiBil.Entities
{
    public class Product
    {
        [Key]
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


        public DateTime? CreatingDate { get; set; }
        public DateTime? UpdatedDate { get; set; }


        public virtual ICollection<ProductIngredient> ProductIngredients { get; set; }
        public virtual ICollection<ProductBadge> ProductBadges { get; set; }

        public virtual ICollection<Nutritive> Nutritives { get; set; }
    }
}
