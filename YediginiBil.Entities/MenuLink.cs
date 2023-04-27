using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YediginiBil.Entities
{
    public class MenuLink
    {
        [Key]
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int MenuId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int Position { get; set; }
    }
}
