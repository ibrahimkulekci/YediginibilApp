using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Business.Abstract;
using YediginiBil.DataAccess.Abstract;
using YediginiBil.Entities;

namespace YediginiBil.Business.Concrete
{
    public class MenuLinkManager : IMenuLinkService
    {
        private IMenuLinkDal _menuLinkDal;

        public MenuLinkManager(IMenuLinkDal menuLinkDal)
        {
            _menuLinkDal = menuLinkDal;
        }

        public void Create(MenuLink entity)
        {
            _menuLinkDal.Create(entity);
        }

        public void Delete(MenuLink entity)
        {
            _menuLinkDal.Delete(entity);
        }

        public List<MenuLink> GetAll(int page, int pageSize)
        {
            return _menuLinkDal.GetAll(page, pageSize);
        }

        public IEnumerable<MenuLink> GetAll()
        {
            return _menuLinkDal.GetAll();
        }

        public int GetAllCount()
        {
            return _menuLinkDal.GetAllCount();
        }

        public MenuLink GetById(int id)
        {
            return _menuLinkDal.GetById(id);
        }

        public void Update(MenuLink entity)
        {
            _menuLinkDal.Update(entity);
        }
    }
}
