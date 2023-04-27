using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.DataAccess.Abstract;
using YediginiBil.Entities;

namespace YediginiBil.DataAccess.Concrete.EfCore
{
    public class EfCoreProductBadgeDal : EfCoreGenericRepository<ProductBadge, YediginibilDbContext>, IProductBadgeDal
    {
        public List<ProductBadge> GetAll(int page, int pageSize)
        {
            List<ProductBadge> resultList = new List<ProductBadge>();

            using (var context = new YediginibilDbContext())
            {
                var productBadges = context.ProductBadges.AsQueryable().ToList();

                return productBadges.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int GetAllCount()
        {
            using (var context = new YediginibilDbContext())
            {
                var productBadges = context.ProductBadges.AsQueryable();

                return productBadges.Count();
            }
        }

        public ProductBadge GetOne()
        {
            throw new NotImplementedException();
        }
    }
}
