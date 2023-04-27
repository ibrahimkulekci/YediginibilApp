using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YediginiBil.Entities
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Status { get; set; }
    }
}
