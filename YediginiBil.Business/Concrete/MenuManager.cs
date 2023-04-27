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
    public class MenuManager : IMenuService
    {
        private IMenuDal _menuDal;

        public MenuManager(IMenuDal menuDal)
        {
            _menuDal = menuDal;
        }

        public void Create(Menu entity)
        {
            _menuDal.Create(entity);
        }

        public void Delete(Menu entity)
        {
            _menuDal.Delete(entity);
        }

        public List<Menu> GetAll(int page, int pageSize)
        {
            return _menuDal.GetAll(page, pageSize);
        }

        public IEnumerable<Menu> GetAll()
        {
            return _menuDal.GetAll();
        }

        public int GetAllCount()
        {
            return _menuDal.GetAllCount();
        }

        public Menu GetById(int id)
        {
            return _menuDal.GetById(id);
        }

        public IEnumerable<Menu> GetByProductId(int id)
        {
            return _menuDal.GetByProductId(id);
        }

        public void Update(Menu entity)
        {
            _menuDal.Update(entity);
        }
    }
}
