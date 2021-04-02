using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IPostRepository _postRepository;


        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public CommentsController(ICommentRepository commentRepository, IPostRepository postRepository)
        {
            _commentRepo = commentRepository;
            _postRepository = postRepository;
        }


        // GET: CommentsController
        public ActionResult Index(int id)
        {
            Post post = _postRepository.GetPublishedPostById(id);
            List<Comment> comments = _commentRepo.GetAllCommentsByPost(id);
            PostCommentsViewModel vm = new PostCommentsViewModel()
            {
                Post = post,
                Comments = comments
            };
            return View(vm);
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
       

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Comment comment)
        {
            try
            {
                   // comment.PostId(int id)
                // update the dogs OwnerId to the current user's Id 
                comment.UserProfileId = GetCurrentUserId();
                //comment.PostId = id
                DateTime dt = DateTime.Now;
                comment.CreateDateTime = dt;
                _commentRepo.AddComment(comment);
                        

                        return RedirectToAction("Index", new { id = comment.PostId });
                    }  
            catch (Exception ex)
            {
                return View(comment);
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

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
