using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace YediginiBil.Business.Abstract
{
    public interface IMenuLinkService
    {
        MenuLink GetById(int id);
        List<MenuLink> GetAll(int page, int pageSize);
        IEnumerable<MenuLink> GetAll();
        void Create(MenuLink entity);
        void Update(MenuLink entity);
        void Delete(MenuLink entity);
        int GetAllCount();
    }
}
