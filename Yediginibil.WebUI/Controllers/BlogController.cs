using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yediginibil.WebUI.Models.Blog;
using YediginiBil.Business.Abstract;

namespace Yediginibil.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            DetailsViewModel model = new DetailsViewModel();
            var blog = _blogService.GetById(id);
            model.CreatingDate = blog.CreatingDate;
            model.Description = blog.Description;
            model.Id = blog.Id;
            model.Image = blog.Image;
            model.SeoDescription = blog.SeoDescription;
            model.SeoTitle = blog.SeoTitle;
            model.SeoUrl = blog.SeoUrl;
            model.Status = blog.Status;
            model.Title = blog.Title;
            model.UpdatedDate = blog.UpdatedDate;


            return View(model);
        }
    }
}
