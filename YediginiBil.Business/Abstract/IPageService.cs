using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace YediginiBil.Business.Abstract
{
    public interface IPageService
    {
        Page GetById(int id);
        List<Page> GetAll(int page, int pageSize);
        IEnumerable<Page> GetAll();
        void Create(Page entity);
        void Update(Page entity);
        void Delete(Page entity);
        int GetAllCount();
    }
}
