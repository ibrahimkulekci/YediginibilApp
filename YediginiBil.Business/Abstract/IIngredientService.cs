using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Entities;

namespace YediginiBil.Business.Abstract
{
    public interface IIngredientService
    {
        Ingredient GetById(int id);
        List<Ingredient> GetAll();

        void Create(Ingredient entity);
        void Update(Ingredient entity);
        void Delete(Ingredient entity);
    }
}
