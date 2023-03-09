using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace YediginiBil.Business.Abstract
{
    public interface IProductIngredientService
    {
        ProductIngredient GetById(int id);
        List<ProductIngredient> GetAll();

        void Create(ProductIngredient entity);
        void Update(ProductIngredient entity);
        void Delete(ProductIngredient entity);
    }
}
