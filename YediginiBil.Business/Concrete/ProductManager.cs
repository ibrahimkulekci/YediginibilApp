﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Business.Abstract;
using YediginiBil.Business.Model;
using YediginiBil.Business.Model.Product;
using YediginiBil.DataAccess.Abstract;
using YediginiBil.DataAccess.Concrete.EfCore;
using YediginiBil.Entities;

namespace YediginiBil.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Create(Product entity)
        {
            _productDal.Create(entity);
        }

        public void Delete(Product entity)
        {
            _productDal.Delete(entity);
        }

        public List<Product> GetAll(int page, int pageSize)
        {
            return _productDal.GetAll(page, pageSize).ToList();
        }

        public int GetAllCount()
        {
            return _productDal.GetAllCount();
        }

        public Product GetById(int id)
        {
            return _productDal.GetById(id);
        }

        public void Update(Product entity)
        {
            _productDal.Update(entity);
        }
    }
}
