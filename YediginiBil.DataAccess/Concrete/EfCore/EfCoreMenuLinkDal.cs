using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.DataAccess.Abstract;
using YediginiBil.Entities;

namespace YediginiBil.DataAccess.Concrete.EfCore
{
    public class EfCoreMenuLinkDal : EfCoreGenericRepository<MenuLink, YediginibilDbContext>, IMenuLinkDal
    {
        public List<MenuLink> GetAll(int page, int pageSize)
        {
            List<MenuLink> resultList = new List<MenuLink>();

            using (var context = new YediginibilDbContext())
            {
                var menuLinks = context.MenuLinks.AsQueryable();

                return menuLinks.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int GetAllCount()
        {
            using (var context = new YediginibilDbContext())
            {
                var menuLinks = context.MenuLinks.AsQueryable();

                return menuLinks.Count();
            }
        }

        public MenuLink GetOne()
        {
            throw new NotImplementedException();
        }
    }
}
