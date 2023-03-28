using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YediginiBil.Business.Abstract;
using YediginiBil.Business.Model;
using YediginiBil.Business.Model.Ingredient;
using YediginiBil.DataAccess.Abstract;
using YediginiBil.Entities;

namespace YediginiBil.Business.Concrete
{
    public class IngredientManager : IIngredientService
    {
        private IIngredientDal _ingredientDal;

        public IngredientManager(IIngredientDal ingredientDal)
        {
            _ingredientDal = ingredientDal;
        }

        public void Create(Ingredient entity)
        {
            _ingredientDal.Create(entity);
        }

        public void Delete(Ingredient entity)
        {
            _ingredientDal.Delete(entity);
        }

        public List<Ingredient> GetAll(int page, int pageSize)
        {
            return _ingredientDal.GetAll(page, pageSize).ToList();
        }

        public IEnumerable<Ingredient> GetAll()
        {
            return _ingredientDal.GetAll();
        }

        public int GetAllCount()
        {
            return _ingredientDal.GetAllCount();
        }

        public Ingredient GetById(int id)
        {
            return _ingredientDal.GetById(id);
        }

        public IEnumerable<Ingredient> GetByProductId(int id)
        {
            return _ingredientDal.GetByProdcutId(id);
        }

        public void Update(Ingredient entity)
        {
            _ingredientDal.Update(entity);
        }
    }
}
