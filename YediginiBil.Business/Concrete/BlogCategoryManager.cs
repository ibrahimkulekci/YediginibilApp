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
    public class BlogCategoryManager : IBlogCategoryService
    {

        private IBlogCategoryDal _blogCategoryDal;

        public BlogCategoryManager(IBlogCategoryDal blogCategoryDal)
        {
            _blogCategoryDal = blogCategoryDal;
        }

        public void Create(BlogCategory entity)
        {
            _blogCategoryDal.Create(entity);
        }

        public void Delete(BlogCategory entity)
        {
            _blogCategoryDal.Delete(entity);
        }

        public List<BlogCategory> GetAll(int page, int pageSize)
        {
            return _blogCategoryDal.GetAll(page, pageSize);
        }

        public IEnumerable<BlogCategory> GetAll()
        {
            return _blogCategoryDal.GetAll();
        }

        public int GetAllCount()
        {
            return _blogCategoryDal.GetAllCount();
        }

        public BlogCategory GetById(int id)
        {
            return _blogCategoryDal.GetById(id);
        }

        public void Update(BlogCategory entity)
        {
            _blogCategoryDal.Update(entity);
        }
    }
}
