using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Business.Model;
using YediginiBil.Business.Model.Product;
using YediginiBil.Entities;

namespace YediginiBil.Business.Abstract
{
    public interface IProductService
    {
        Product GetById(int id);
        List<Product> GetAll(int page, int pageSize);

        void Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
        int GetAllCount();
    }
}
