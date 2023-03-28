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
    public class BlogManager:IBlogService
    {
        private IBlogDal _blogDal;

        public BlogManager(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }

        public void Create(Blog entity)
        {
            _blogDal.Create(entity);
        }

        public void Delete(Blog entity)
        {
            _blogDal.Delete(entity);
        }

        public List<Blog> GetAll(int page, int pageSize)
        {
            return _blogDal.GetAll(page, pageSize).ToList();
        }

        public IEnumerable<Blog> GetAll()
        {
            return _blogDal.GetAll();
        }

        public int GetAllCount()
        {
            return _blogDal.GetAllCount();
        }

        public Blog GetById(int id)
        {
            return _blogDal.GetById(id);
        }

        public void Update(Blog entity)
        {
            _blogDal.Update(entity);
        }
    }
}
