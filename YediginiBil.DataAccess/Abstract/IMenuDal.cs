using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace YediginiBil.DataAccess.Abstract
{
    public interface IMenuDal:IRepository<Menu>
    {
        Menu GetOne();
        List<Menu> GetAll(int page, int pageSize);
        int GetAllCount();
        IEnumerable<Menu> GetByProductId(int id);
    }
}
