using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Business.Abstract;
using YediginiBil.Business.Model.ProductIngredient;
using YediginiBil.DataAccess.Abstract;
using YediginiBil.Entities;

namespace YediginiBil.Business.Concrete
{
    public class ProductIngredientManager : IProductIngredientService
    {
        private IProductIngredientDal _productIngredientDal;

        public ProductIngredientManager(IProductIngredientDal productIngredientDal)
        {
            _productIngredientDal = productIngredientDal;
        }

        public void Create(ProductIngredient entity)
        {
            _productIngredientDal.Create(entity);
        }

        public void Delete(ProductIngredient entity)
        {
            _productIngredientDal.Delete(entity);
        }

        public List<ProductIngredient> GetAll()
        {
            return _productIngredientDal.GetAll().ToList();
        }

        public ProductIngredient GetById(int id)
        {
            return _productIngredientDal.GetById(id);
        }

        public List<ProductIngredient> GetByProductId(int id)
        {
            return _productIngredientDal.GetByProductId(id);
        }

        public void Update(ProductIngredient entity)
        {
            _productIngredientDal.Update(entity);
        }
    }
}
