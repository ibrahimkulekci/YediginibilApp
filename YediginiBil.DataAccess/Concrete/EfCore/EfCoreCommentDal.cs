using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.DataAccess.Abstract;
using YediginiBil.Entities;

namespace YediginiBil.DataAccess.Concrete.EfCore
{
    public class EfCoreCommentDal : EfCoreGenericRepository<Comment, YediginibilDbContext>, ICommentDal
    {
        public List<Comment> GetAll(int page, int pageSize)
        {
            List<Comment> resultList = new List<Comment>();

            using (var context = new YediginibilDbContext())
            {
                var comments = context.Comments.AsQueryable().OrderByDescending(x => x.Id).ToList();

                return comments.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int GetAllCount()
        {
            using (var context = new YediginibilDbContext())
            {
                var comments = context.Comments.AsQueryable();

                return comments.Count();
            }
        }

        public Comment GetOne()
        {
            throw new NotImplementedException();
        }
    }
}
