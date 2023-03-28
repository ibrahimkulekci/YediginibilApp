using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YediginiBil.Business.Model.Ingredient
{
    public class IngredientQueryModel:BaseQueryModel
    {
        public long? Filter_Id { get; set; }
        public bool? Filter_Status { get; set; }
        public string? Filter_Search { get; set; }

        public DateTime? Filter_CreatingDate { get; set; }
        public DateTime? Filter_UpdatedDate { get; set; }
    }
}
