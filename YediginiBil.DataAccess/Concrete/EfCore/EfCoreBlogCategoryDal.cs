using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.DataAccess.Abstract;
using YediginiBil.Entities;

namespace YediginiBil.DataAccess.Concrete.EfCore
{
    public class EfCoreBlogCategoryDal : EfCoreGenericRepository<BlogCategory, YediginibilDbContext>, IBlogCategoryDal
    {
        public List<BlogCategory> GetAll(int page, int pageSize)
        {
            List<BlogCategory> resultList = new List<BlogCategory>();

            using (var context = new YediginibilDbContext())
            {
                var blogcategories = context.BlogCategories.AsQueryable().OrderByDescending(x => x.Id).ToList();

                return blogcategories.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int GetAllCount()
        {
            using (var context = new YediginibilDbContext())
            {
                var blogcategories = context.BlogCategories.AsQueryable();

                return blogcategories.Count();
            }
        }

        public BlogCategory GetOne()
        {
            throw new NotImplementedException();
        }
    }
}
