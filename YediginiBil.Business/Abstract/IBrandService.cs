using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace YediginiBil.Business.Abstract
{
    public interface IBrandService
    {
        Brand GetById(int id);
        List<Brand> GetAll(int page, int pageSize);
        IEnumerable<Brand> GetAll();
        void Create(Brand entity);
        void Update(Brand entity);
        void Delete(Brand entity);
        int GetAllCount();
    }
}
