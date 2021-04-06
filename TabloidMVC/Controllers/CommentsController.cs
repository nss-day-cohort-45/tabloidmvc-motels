using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IPostRepository _postRepository;

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
                comment.UserProfileId = GetCurrentUserId();
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
            Comment comment = _commentRepo.GetCommentById(id);
            int userId = GetCurrentUserId();


            if (comment == null)
            {
                return NotFound();
            }
            else
            {
                if (comment.UserProfileId == userId)
                {
                    return View(comment);
                }
                else
                {
                    return NotFound();
                }
            }

        }

        // POST: CommentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Comment comment)
        {
            comment.Id = id;
            try
            {
                _commentRepo.EditComment(comment);
              
                return RedirectToAction("Index", new { id = comment.PostId });
            }
            catch (Exception ex)
            {
                return View(comment);
            }
        }





        // GET: DogsController/Delete/5
        [Authorize]
        public ActionResult Delete(int id) //this id is the comment id 
        {
            Comment comment = _commentRepo.GetCommentById(id); //this comment does have the correct PostId

            int ownerId = GetCurrentUserId();
            if (comment.UserProfileId == ownerId)
            {
                return View(comment); //this comment does have the correct PostId
            }
            else
            {
                return NotFound();
            }
        }

        // POST: DogsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Comment comment)
        {
            var databaseComment = _commentRepo.GetCommentById(id);

            try
            {
                _commentRepo.DeleteComment(id);

                return RedirectToAction("Index", new { id = databaseComment.PostId });
            }
            catch (Exception ex)
            {
                return View(comment);
            }
        }





        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
