using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YediginiBil.Entities
{
    public class ProductIngredient
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int IngredientId { get; set; }
    }
}
