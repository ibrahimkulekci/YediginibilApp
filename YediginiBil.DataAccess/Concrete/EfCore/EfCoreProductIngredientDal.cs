using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.DataAccess.Abstract;
using YediginiBil.Entities;

namespace YediginiBil.DataAccess.Concrete.EfCore
{
    public class EfCoreProductIngredientDal : EfCoreGenericRepository<ProductIngredient, YediginibilDbContext>, IProductIngredientDal
    {
        public List<ProductIngredient> GetByProductId(int id)
        {
            using (var context = new YediginibilDbContext())
            {
                return context.Set<ProductIngredient>().Where(x=>x.Id==id).ToList();
            }
        }
    }
}
