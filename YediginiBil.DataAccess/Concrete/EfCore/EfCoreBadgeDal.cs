using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.DataAccess.Abstract;
using YediginiBil.Entities;

namespace YediginiBil.DataAccess.Concrete.EfCore
{
    public class EfCoreBadgeDal : EfCoreGenericRepository<Badge, YediginibilDbContext>, IBadgeDal
    {
        public List<Badge> GetAll(int page, int pageSize)
        {
            List<Badge> resultList = new List<Badge>();

            using (var context = new YediginibilDbContext())
            {
                var badges = context.Badges.AsQueryable().ToList();

                return badges.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int GetAllCount()
        {
            using (var context = new YediginibilDbContext())
            {
                var badges = context.Badges.AsQueryable();

                return badges.Count();
            }
        }

        public Badge GetOne()
        {
            throw new NotImplementedException();
        }
    }
}
