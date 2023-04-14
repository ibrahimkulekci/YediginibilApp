using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YediginiBil.Entities
{
    public class BlogCategory
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoUrl { get; set; }
        public bool Status { get; set; }

        public DateTime CreatingDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        
    }
}
