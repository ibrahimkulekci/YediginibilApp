using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace YediginiBil.DataAccess.Abstract
{
    public interface IIngredientDal : IRepository<Ingredient>
    {
        List<Ingredient> GetAll(int page, int pageSize);
        IEnumerable<Ingredient> GetByProdcutId(int id);
        int GetAllCount();
    }
}
