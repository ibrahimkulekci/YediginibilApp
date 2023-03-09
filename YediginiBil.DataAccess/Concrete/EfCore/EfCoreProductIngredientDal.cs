using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.DataAccess.Abstract;
using YediginiBil.Entities;

namespace YediginiBil.DataAccess.Concrete.EfCore
{
    public class EfCoreProductIngredientDal: EfCoreGenericRepository<ProductIngredient, YediginibilDbContext>, IProductIngredientDal
    {
    }
}
