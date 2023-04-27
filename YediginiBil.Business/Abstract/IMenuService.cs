using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace YediginiBil.Business.Abstract
{
    public interface IMenuService
    {
        Menu GetById(int id);
        List<Menu> GetAll(int page, int pageSize);
        IEnumerable<Menu> GetAll();
        IEnumerable<Menu> GetByProductId(int id);

        void Create(Menu entity);
        void Update(Menu entity);
        void Delete(Menu entity);
        int GetAllCount();
    }
}
