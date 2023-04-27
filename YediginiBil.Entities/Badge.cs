using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YediginiBil.Entities
{
    public class Badge
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool Status { get; set; }

        public DateTime CreatingDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<ProductBadge> ProductBadges { get; set; }
    }
}
