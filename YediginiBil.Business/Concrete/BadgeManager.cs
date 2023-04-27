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
    public class BadgeManager : IBadgeService
    {
        private IBadgeDal _badgeDal;

        public BadgeManager(IBadgeDal badgeDal)
        {
            _badgeDal = badgeDal;
        }

        public void Create(Badge entity)
        {
            _badgeDal.Create(entity);
        }

        public void Delete(Badge entity)
        {
            _badgeDal.Delete(entity);
        }

        public List<Badge> GetAll(int page, int pageSize)
        {
            return _badgeDal.GetAll(page, pageSize);
        }

        public IEnumerable<Badge> GetAll()
        {
            return _badgeDal.GetAll();
        }

        public int GetAllCount()
        {
            return _badgeDal.GetAllCount();
        }

        public Badge GetById(int id)
        {
            return _badgeDal.GetById(id);
        }

        public IEnumerable<Badge> GetByProductId(int id)
        {
            return _badgeDal.GetByProductId(id);
        }

        public void Update(Badge entity)
        {
            _badgeDal.Update(entity);
        }
    }
}
