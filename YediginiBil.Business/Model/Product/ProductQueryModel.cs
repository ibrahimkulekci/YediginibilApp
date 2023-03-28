using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YediginiBil.Business.Model.Product
{
    public class ProductQueryModel:BaseQueryModel
    {
        // filters
        public long? Id { get; set; }
        public bool? IsActive { get; set; }

        public DateTime? PublishDateTime_Begin { get; set; }
        public DateTime? PublishDateTime_End { get; set; }
    }
}
