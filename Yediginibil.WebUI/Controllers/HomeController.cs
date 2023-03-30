using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Yediginibil.WebUI.Models;
using YediginiBil.Business.Abstract;

namespace Yediginibil.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;
        private IBlogService _blogService;

        public HomeController(IBlogService blogService,IProductService productService)
        {
            _blogService = blogService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            Yediginibil.WebUI.Models.Home.ListViewModel model = new Models.Home.ListViewModel();

            model.Products = _productService.GetAll(1, 10).OrderByDescending(x => x.Id).ToList();
            model.Blogs = _blogService.GetAll(1, 3).OrderByDescending(x => x.Id).ToList();

            return View(model);
        }
    }
}
