using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.DataAccess.Abstract;
using YediginiBil.Entities;

namespace YediginiBil.DataAccess.Concrete.EfCore
{
    public class EfCoreBlogDal : EfCoreGenericRepository<Blog, YediginibilDbContext>, IBlogDal
    {
        public List<Blog> GetAll(int page, int pageSize)
        {
            List<Blog> resultList = new List<Blog>();

            using (var context = new YediginibilDbContext())
            {
                var blogs = context.Blogs.AsQueryable();

                return blogs.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int GetAllCount()
        {
            using (var context = new YediginibilDbContext())
            {
                var blogs = context.Blogs.AsQueryable();

                return blogs.Count();
            }
        }

        public Blog GetOne()
        {
            throw new NotImplementedException();
        }
    }
}
