using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace YediginiBil.Business.Abstract
{
    public interface IBlogService
    {
        Blog GetById(int id);
        List<Blog> GetAll(int page, int pageSize);
        IEnumerable<Blog> GetAll();
        void Create(Blog entity);
        void Update(Blog entity);
        void Delete(Blog entity);
        int GetAllCount();
    }
}
