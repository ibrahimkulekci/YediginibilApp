using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yediginibil.WebUI.Models.Product;
using YediginiBil.Business.Abstract;

namespace Yediginibil.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IProductIngredientService _productIngredientService;
        private IIngredientService _ingredient;

        public ProductController(IIngredientService ingredient, IProductIngredientService productIngredientService, IProductService productService)
        {
            _ingredient = ingredient;
            _productIngredientService = productIngredientService;
            _productService = productService;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            DetailsViewModel model = new DetailsViewModel();

            //model.Product = _productService.GetById(id);

            var product = _productService.GetById(id);
            model.ImageUrl = product.ImageUrl;
            model.Title = product.Title;
            model.ShortDescription = product.ShortDescription;
            model.LongDescription = product.LongDescription;

            model.Ingredients = _ingredient.GetByProductId(id);

           
            return View(model);
        }
    }
}
