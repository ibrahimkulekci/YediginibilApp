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
    public class PageManager:IPageService
    {
        private IPageDal _pageDal;

        public PageManager(IPageDal pageDal)
        {
            _pageDal = pageDal;
        }

        public void Create(Page entity)
        {
            _pageDal.Create(entity);
        }

        public void Delete(Page entity)
        {
            _pageDal.Delete(entity);
        }

        public List<Page> GetAll(int page, int pageSize)
        {
            return _pageDal.GetAll(page, pageSize).ToList();
        }

        public IEnumerable<Page> GetAll()
        {
            return _pageDal.GetAll();
        }

        public int GetAllCount()
        {
            return _pageDal.GetAllCount();
        }

        public Page GetById(int id)
        {
            return _pageDal.GetById(id);
        }

        public void Update(Page entity)
        {
            _pageDal.Update(entity);
        }
    }
}
