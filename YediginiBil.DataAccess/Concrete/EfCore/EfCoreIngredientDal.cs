using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.DataAccess.Abstract;
using YediginiBil.Entities;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace YediginiBil.DataAccess.Concrete.EfCore
{
    public class EfCoreIngredientDal : EfCoreGenericRepository<Ingredient, YediginibilDbContext>, IIngredientDal
    {
        public List<Ingredient> GetAll(int page, int pageSize)
        {
            using (var context = new YediginibilDbContext())
            {
                var products = context.Ingredients.AsQueryable();

                return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int GetAllCount()
        {
            using (var context = new YediginibilDbContext())
            {
                var products = context.Ingredients.AsQueryable();

                return products.Count();
            }
        }

        public IEnumerable<Ingredient> GetByProdcutId(int productId)
        {
            var context = new YediginibilDbContext();

            var record = from i in context.Ingredients
                         join pi in context.ProductIngredient on i.Id equals pi.IngredientId
                         where pi.ProductId == productId
                         select i;

            return record.ToList();
        }
    }
}
