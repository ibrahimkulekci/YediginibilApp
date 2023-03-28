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
    public class EfCoreBrandDal : EfCoreGenericRepository<Brand, YediginibilDbContext>, IBrandDal
    {
        public List<Brand> GetAll(int page, int pageSize)
        {
            List<Brand> resultList = new List<Brand>();

            using (var context = new YediginibilDbContext())
            {
                var products = context.Brands.AsQueryable();

                return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int GetAllCount()
        {
            using (var context = new YediginibilDbContext())
            {
                var brands = context.Brands.AsQueryable();

                return brands.Count();
            }
        }

        public Brand GetOne()
        {
            throw new NotImplementedException();
        }
    }
}
