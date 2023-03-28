using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace YediginiBil.DataAccess.Abstract
{
    public interface IProductIngredientDal:IRepository<ProductIngredient>
    {
        List<ProductIngredient> GetByProductId(int id);
    }
}
