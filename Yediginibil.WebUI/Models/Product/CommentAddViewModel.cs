using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yediginibil.WebUI.Models.Product
{
    public class CommentAddViewModel:YediginiBil.Entities.Comment
    {
        public string ProductSeoUrl { get; set; }
    }
}
