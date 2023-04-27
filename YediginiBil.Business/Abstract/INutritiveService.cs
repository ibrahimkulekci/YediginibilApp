using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace YediginiBil.Business.Abstract
{
    public interface INutritiveService
    {
        Nutritive GetById(int id);
        List<Nutritive> GetAll(int page, int pageSize);
        IEnumerable<Nutritive> GetAll();
        IEnumerable<Nutritive> GetByProductId(int id);

        void Create(Nutritive entity);
        void Update(Nutritive entity);
        void Delete(Nutritive entity);
        int GetAllCount();
    }
}
