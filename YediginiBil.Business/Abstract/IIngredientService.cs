using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Business.Model;
using YediginiBil.Business.Model.Ingredient;
using YediginiBil.Entities;

namespace YediginiBil.Business.Abstract
{
    public interface IIngredientService
    {
        Ingredient GetById(int id);
        List<Ingredient> GetAll(int page, int pageSize);

        void Create(Ingredient entity);
        void Update(Ingredient entity);
        void Delete(Ingredient entity);
        IEnumerable<Ingredient> GetByProductId(int id);
        int GetAllCount();
        IEnumerable<Ingredient> GetAll();
    }
}
