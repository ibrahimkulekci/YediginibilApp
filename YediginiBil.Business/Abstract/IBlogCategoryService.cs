using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace YediginiBil.Business.Abstract
{
    public interface IBlogCategoryService
    {
        BlogCategory GetById(int id);
        List<BlogCategory> GetAll(int page, int pageSize);
        IEnumerable<BlogCategory> GetAll();
        void Create(BlogCategory entity);
        void Update(BlogCategory entity);
        void Delete(BlogCategory entity);
        int GetAllCount();
    }
}
