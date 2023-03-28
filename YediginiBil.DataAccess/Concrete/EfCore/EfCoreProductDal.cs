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
    public class EfCoreProductDal : EfCoreGenericRepository<Product, YediginibilDbContext>, IProductDal
    {
        public List<Product> GetAll(int page, int pageSize)
        {
            using (var context=new YediginibilDbContext())
            {
                var products = context.Products.AsQueryable();

                return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int GetAllCount()
        {
            using (var context = new YediginibilDbContext())
            {
                var products = context.Products.AsQueryable();

                return products.Count();
            }
        }
    }
}
