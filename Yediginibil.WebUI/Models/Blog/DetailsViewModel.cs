using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yediginibil.WebUI.Models.Blog
{
    public class DetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string SeoTitle { get; set; }
        public string SeoUrl { get; set; }
        public string SeoDescription { get; set; }
        public DateTime? CreatingDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public int CategoryId { get; set; }

        public YediginiBil.Entities.BlogCategory Category { get; set; }

        public List<YediginiBil.Entities.Comment> Comments { get; set; }
        public int CommentCount { get; set; }

    }
}
