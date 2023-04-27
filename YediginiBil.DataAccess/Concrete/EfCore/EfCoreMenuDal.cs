using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.DataAccess.Abstract;
using YediginiBil.Entities;

namespace YediginiBil.DataAccess.Concrete.EfCore
{
    public class EfCoreMenuDal : EfCoreGenericRepository<Menu, YediginibilDbContext>, IMenuDal
    {
        public List<Menu> GetAll(int page, int pageSize)
        {
            List<Menu> resultList = new List<Menu>();

            using (var context = new YediginibilDbContext())
            {
                var menus = context.Menus.AsQueryable().ToList();

                return menus.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int GetAllCount()
        {
            using (var context = new YediginibilDbContext())
            {
                var menus = context.Menus.AsQueryable();

                return menus.Count();
            }
        }

        public IEnumerable<Menu> GetByProductId(int id)
        {
            throw new NotImplementedException();
        }

        public Menu GetOne()
        {
            throw new NotImplementedException();
        }
    }
}
