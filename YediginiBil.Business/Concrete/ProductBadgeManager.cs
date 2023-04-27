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
    public class ProductBadgeManager : IProductBadgeService
    {
        private IProductBadgeDal _productBadgeDal;

        public ProductBadgeManager(IProductBadgeDal productBadgeDal)
        {
            _productBadgeDal = productBadgeDal;
        }

        public void Create(ProductBadge entity)
        {
            _productBadgeDal.Create(entity);
        }

        public void Delete(ProductBadge entity)
        {
            _productBadgeDal.Delete(entity);
        }

        public List<ProductBadge> GetAll(int page, int pageSize)
        {
            return _productBadgeDal.GetAll(page, pageSize);
        }

        public IEnumerable<ProductBadge> GetAll()
        {
            return _productBadgeDal.GetAll();
        }

        public int GetAllCount()
        {
            return _productBadgeDal.GetAllCount();
        }

        public ProductBadge GetById(int id)
        {
            return _productBadgeDal.GetById(id);
        }

        public void Update(ProductBadge entity)
        {
            _productBadgeDal.Update(entity);
        }
    }
}
