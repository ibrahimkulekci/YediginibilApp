using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YediginiBil.Business.Abstract;

namespace Yediginibil.WebUI.ViewComponents
{
    public class BlogCategoriesViewComponent:ViewComponent
    {
        private IBlogCategoryService _blogCategoryService;

        public BlogCategoriesViewComponent(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }

        public IViewComponentResult Invoke()
        {
            var record = _blogCategoryService.GetAll();
            return View(record);
        }
    }
}
