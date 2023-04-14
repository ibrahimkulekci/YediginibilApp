using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yediginibil.WebUI.Areas.Admin.Models;
using Yediginibil.WebUI.Models.Product;
using YediginiBil.Business.Abstract;

namespace Yediginibil.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IProductIngredientService _productIngredientService;
        private IIngredientService _ingredient;
        private ICommentService _commentService;

        public ProductController(ICommentService commentService,IIngredientService ingredient, IProductIngredientService productIngredientService, IProductService productService)
        {
            _ingredient = ingredient;
            _productIngredientService = productIngredientService;
            _productService = productService;
            _commentService = commentService;
        }


        public IActionResult Index(int page = 1)
        {
            const int pageSize = 3;
            return View(new ListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _productService.GetAllCount(),
                    CurrentPage = page,
                    ItemsPerPage = pageSize
                },
                Products = _productService.GetAll(page, pageSize).Where(x=>x.Status==true).OrderByDescending(x=>x.Id).ToList()
            });
        }
        public IActionResult Details(int id)
        {
            DetailsViewModel model = new DetailsViewModel();

            //model.Product = _productService.GetById(id);

            var product = _productService.GetById(id);
            model.Id = product.Id;
            model.ImageUrl = product.ImageUrl;
            model.Title = product.Title;
            model.SeoUrl = product.SeoUrl;
            model.ShortDescription = product.ShortDescription;
            model.LongDescription = product.LongDescription;

            model.Ingredients = _ingredient.GetByProductId(id);
            model.Comments = _commentService.GetAll().Where(x => x.ProductId == id && x.Status == true).ToList();
            model.CommentCount = _commentService.GetAll().Where(x => x.ProductId == id && x.Status == true).Count();
            model.CommentStarAvg = Convert.ToInt32(_commentService.GetAll().Where(y => y.ProductId == id && y.Status == true).Select(x => x.Star).DefaultIfEmpty().Average());



            return View(model);
        }
        [HttpPost]
        public IActionResult CommentAdd(CommentAddViewModel model)
        {
            YediginiBil.Entities.Comment record = new YediginiBil.Entities.Comment();

            record.BlogId = model.BlogId;
            record.Content = model.Content;
            record.CreatingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            record.UpdatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            record.Email = model.Email;
            record.NameSurname = model.NameSurname;
            record.ProductId = model.ProductId;
            record.Star = model.Star;
            record.Status = model.Status;
            record.Type = model.Type;

            _commentService.Create(record);

            TempData["Message"] = "Success";
            TempData["Message_Detail"] = "Yorumunuz başarıyla eklenmiştir. Onaylandıktan sonra yorumunuz gözükecektir.";
            return Redirect("~/Product/Details/" + model.ProductId + "/" + model.ProductSeoUrl);
        }
    }
}
