using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace YediginiBil.Business.Abstract
{
    public interface IProductBadgeService
    {
        ProductBadge GetById(int id);
        List<ProductBadge> GetAll(int page, int pageSize);
        IEnumerable<ProductBadge> GetAll();
        void Create(ProductBadge entity);
        void Update(ProductBadge entity);
        void Delete(ProductBadge entity);
        int GetAllCount();
    }
}
