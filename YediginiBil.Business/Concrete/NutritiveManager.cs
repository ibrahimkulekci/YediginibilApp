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
    public class NutritiveManager : INutritiveService
    {
        private INutritiveDal _nutritiveDal;

        public NutritiveManager(INutritiveDal nutritiveDal)
        {
            _nutritiveDal = nutritiveDal;
        }

        public void Create(Nutritive entity)
        {
            _nutritiveDal.Create(entity);
        }

        public void Delete(Nutritive entity)
        {
            _nutritiveDal.Delete(entity);
        }

        public List<Nutritive> GetAll(int page, int pageSize)
        {
            return _nutritiveDal.GetAll(page, pageSize);
        }

        public IEnumerable<Nutritive> GetAll()
        {
            return _nutritiveDal.GetAll();
        }

        public int GetAllCount()
        {
            return _nutritiveDal.GetAllCount();
        }

        public Nutritive GetById(int id)
        {
            return _nutritiveDal.GetById(id);
        }

        public IEnumerable<Nutritive> GetByProductId(int id)
        {
            return _nutritiveDal.GetByProductId(id);
        }

        public void Update(Nutritive entity)
        {
            _nutritiveDal.Update(entity);
        }
    }
}
