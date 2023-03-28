using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.DataAccess.Abstract;
using YediginiBil.Entities;

namespace YediginiBil.DataAccess.Concrete.EfCore
{
    public class EfCorePageDal : EfCoreGenericRepository<Page, YediginibilDbContext>, IPageDal
    {
        public List<Page> GetAll(int page, int pageSize)
        {
            List<Page> resultList = new List<Page>();

            using (var context = new YediginibilDbContext())
            {
                var products = context.Pages.AsQueryable();

                return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int GetAllCount()
        {
            using (var context = new YediginibilDbContext())
            {
                var pages = context.Pages.AsQueryable();

                return pages.Count();
            }
        }

        public Page GetOne()
        {
            throw new NotImplementedException();
        }
    }
}
