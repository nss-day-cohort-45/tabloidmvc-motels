using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentRepository _commentRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public CommentsController(ICommentRepository commentRepository)
        {
            _commentRepo = commentRepository;
        }


        // GET: CommentsController
        public ActionResult Index(int id)
        {
            List<Comment> comments = _commentRepo.GetAllCommentsByPost(id);
            return View(comments);
        }

        // GET: CommentsController/Details/5
        public ActionResult Details(int id)
        {
            var comment = _commentRepo.GetCommentById(id);

            if (comment == null)
            {
                comment = _commentRepo.GetCommentById(id);
                if (comment == null)
                {
                    return NotFound();
                }
            }
            return View(comment);
        }

        // GET: CommentsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommentsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CommentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
