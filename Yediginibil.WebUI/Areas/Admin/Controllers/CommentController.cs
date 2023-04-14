using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yediginibil.WebUI.Areas.Admin.Models;
using Yediginibil.WebUI.Areas.Admin.Models.Comment;
using YediginiBil.Business.Abstract;

namespace Yediginibil.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        private ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult Index(int page = 1)
        {
            const int pageSize = 20;
            return View(new ListViewModel()
            {
                PageInfo = new PageInfo()
                {
                    TotalItems = _commentService.GetAllCount(),
                    CurrentPage = page,
                    ItemsPerPage = pageSize
                },
                Comments = _commentService.GetAll(page, pageSize).OrderByDescending(x => x.Id).ToList()
            });
        }
    }
}
