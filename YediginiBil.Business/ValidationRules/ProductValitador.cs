using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YediginiBil.Business.ValidationRules
{
    public class ProductValitador: AbstractValidator<YediginiBil.Entities.Product>
    {
        public ProductValitador()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Lütfen ürün adı alanını boş bırakmayınız!");
            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Lütfen ürün resmi alanını boş bırakmayınız!");
        }
    }
}
