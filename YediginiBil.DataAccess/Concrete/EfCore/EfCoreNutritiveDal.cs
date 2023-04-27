using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.DataAccess.Abstract;
using YediginiBil.Entities;

namespace YediginiBil.DataAccess.Concrete.EfCore
{
    public class EfCoreNutritiveDal : EfCoreGenericRepository<Nutritive, YediginibilDbContext>, INutritiveDal
    {
        public List<Nutritive> GetAll(int page, int pageSize)
        {
            List<Nutritive> resultList = new List<Nutritive>();

            using (var context = new YediginibilDbContext())
            {
                var nutritives = context.Nutritives.AsQueryable().ToList();

                return nutritives.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public int GetAllCount()
        {
            using (var context = new YediginibilDbContext())
            {
                var nutritives = context.Nutritives.AsQueryable();

                return nutritives.Count();
            }
        }

        public IEnumerable<Nutritive> GetByProductId(int id)
        {
            var context = new YediginibilDbContext();

            var record = from i in context.Nutritives
                         where i.ProductId == id
                         select i;

            return record.ToList();
        }

        public Nutritive GetOne()
        {
            throw new NotImplementedException();
        }
    }
}
