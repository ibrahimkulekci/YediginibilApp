using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yediginibil.WebUI.Areas.Admin.Models;
using Yediginibil.WebUI.Models.Blog;
using YediginiBil.Business.Abstract;
using YediginiBil.DataAccess.Concrete.EfCore;

namespace Yediginibil.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private IBlogService _blogService;
        private IBlogCategoryService _blogCategoryService;
        private ICommentService _commentService;

        YediginibilDbContext context = new YediginibilDbContext();

        public BlogController(IBlogService blogService, IBlogCategoryService blogCategoryService, ICommentService commentService)
        {
            _blogService = blogService;
            _blogCategoryService = blogCategoryService;
            _commentService = commentService;
        }

        public IActionResult Index(int page = 1)
        {
            const int pageSize = 10;
            return View(new ListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _blogService.GetAllCount(),
                    CurrentPage = page,
                    ItemsPerPage = pageSize
                },
                Blogs = _blogService.GetAll(page, pageSize).OrderByDescending(x => x.Id).ToList()
            });
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
            model.Category = _blogCategoryService.GetById(blog.CategoryId);

            model.Comments = _commentService.GetAll().Where(x => x.BlogId == id && x.Status==true).ToList();
            model.CommentCount = _commentService.GetAll().Where(x => x.BlogId == id && x.Status==true).Count();


            return View(model);
        }
        [HttpPost]
        public IActionResult Search(string inputSearch)
        {
            SearchViewModel model = new SearchViewModel();
            model.Blogs = (from x in context.Blogs
                           where x.Title.Contains(inputSearch)
                           select x).ToList();
            model.inputSearch = inputSearch;

            return View(model);
        }
        [HttpPost]
        public IActionResult CommentAdd(CommendAddViewModel model)
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
            return Redirect("~/Blog/Details/"+model.BlogId+"/"+model.BlogSeoUrl);
        }
    }
}
