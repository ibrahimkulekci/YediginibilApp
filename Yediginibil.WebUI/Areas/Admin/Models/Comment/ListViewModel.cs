using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yediginibil.WebUI.Areas.Admin.Models.Comment
{
    public class ListViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<YediginiBil.Entities.Comment> Comments { get; set; }
    }
}
