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

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index(int page =1)
        {
            const int pageSize = 10;
            return View(_productService.GetAll(page, pageSize).OrderByDescending(x=>x.Id).ToList());
        }
    }
}
