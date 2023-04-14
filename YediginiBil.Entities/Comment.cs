using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YediginiBil.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public bool Type { get; set; } //0=product,1=blog
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public int ProductId { get; set; }
        public int Star { get; set; }
        public bool Status { get; set; }

        public DateTime CreatingDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
