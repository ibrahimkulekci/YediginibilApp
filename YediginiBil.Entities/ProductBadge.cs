﻿using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YediginiBil.Entities
{
    public class ProductBadge
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int BadgeId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Badge Badge { get; set; }
    }
}
