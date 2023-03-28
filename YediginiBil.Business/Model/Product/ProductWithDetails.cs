using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YediginiBil.Business.Model.Product
{
    public class ProductWithDetails: YediginiBil.Entities.Product
    {
        public List<Entities.Ingredient> Ingredients { get; set; }
    }
}
