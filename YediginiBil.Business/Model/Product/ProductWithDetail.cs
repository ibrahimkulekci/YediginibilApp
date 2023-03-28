using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YediginiBil.Business.Model.Product
{
    public class ProductWithDetail:YediginiBil.Entities.Product
    {
        public string Brand_Name { get; set; }
        public string Category_Name { get; set; }
    }
}
