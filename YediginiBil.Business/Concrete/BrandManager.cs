﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Business.Abstract;
using YediginiBil.DataAccess.Abstract;
using YediginiBil.Entities;

namespace YediginiBil.Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public void Create(Brand entity)
        {
            _brandDal.Create(entity);
        }

        public void Delete(Brand entity)
        {
            _brandDal.Delete(entity);
        }

        public List<Brand> GetAll(int page, int pageSize)
        {
            return _brandDal.GetAll(page, pageSize).ToList();
        }

        public IEnumerable<Brand> GetAll()
        {
            return _brandDal.GetAll();
        }

        public int GetAllCount()
        {
            return _brandDal.GetAllCount();
        }

        public Brand GetById(int id)
        {
            return _brandDal.GetById(id);
        }

        public void Update(Brand entity)
        {
            _brandDal.Update(entity);
        }
    }
}
