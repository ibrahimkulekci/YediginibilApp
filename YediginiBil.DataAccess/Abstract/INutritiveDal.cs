using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace YediginiBil.DataAccess.Abstract
{
    public interface INutritiveDal:IRepository<Nutritive>
    {
        Nutritive GetOne();
        List<Nutritive> GetAll(int page, int pageSize);
        int GetAllCount();
        IEnumerable<Nutritive> GetByProductId(int id);
    }
}
