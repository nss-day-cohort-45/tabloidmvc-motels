using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;
using System;
using System.Collections.Generic;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;

        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
        }

        public IActionResult Index()
        {
            var posts = _postRepository.GetAllPublishedPosts();
            return View(posts);
        }

        public IActionResult Details(int id)
        {
            var post = _postRepository.GetPublishedPostById(id);
            if (post == null)
            {
                int userId = GetCurrentUserProfileId();
                post = _postRepository.GetUserPostById(id, userId);
                if (post == null)
                {
                    return NotFound();
                }
            }
            return View(post);
        }

        public IActionResult Create()
        {
            var vm = new PostCreateViewModel();
            vm.CategoryOptions = _categoryRepository.GetAll();
            vm.Tags = _tagRepository.GetAllTags();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(PostCreateViewModel vm)
        {
            try
            {
                vm.Post.CreateDateTime = DateAndTime.Now;
                vm.Post.IsApproved = true;
                vm.Post.UserProfileId = GetCurrentUserProfileId();
                
                _postRepository.Add(vm.Post);
                
                return RedirectToAction("Details", new { id = vm.Post.Id });
            } 
            catch
            {
                vm.CategoryOptions = _categoryRepository.GetAll();
                return View(vm);
            }
        }

        [Authorize]
        public IActionResult Edit(int id)
        {

            int currentUserId = GetCurrentUserProfileId();
            Post post = _postRepository.GetUserPostById(id, currentUserId);
            List<Category> categories = _categoryRepository.GetAll();
            List<Tag> tags = _tagRepository.GetAllTags();
            PostEditViewModel vm = new PostEditViewModel()
            {
                Post = post,
                CategoryOptions = categories,
                Tags = tags
                
            };

            if(post == null)
            {
                return NotFound();
            }

            if(post.UserProfileId == currentUserId)
            {
                return View(vm);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Post post)
        {
            try
            {
                int currentUserId = GetCurrentUserProfileId();
                Post originalPost = _postRepository.GetUserPostById(id, currentUserId);
                
                post.Id = id;
                post.CreateDateTime = originalPost.CreateDateTime;
                // Setting this to always be true for easy testing, may need to set this to false later so an admin can approve edits
                post.IsApproved = true;
                post.PublishDateTime = originalPost.PublishDateTime;
                post.UserProfileId = currentUserId;

                _postRepository.UpdatePost(post);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                List<Category> categories = _categoryRepository.GetAll();

                PostEditViewModel vm = new PostEditViewModel()
                {
                    Post = post,
                    CategoryOptions = categories
                };
                return View(vm);
            }
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }



        [Authorize]
        public ActionResult Delete(int id)
        {
            Post post = _postRepository.GetPublishedPostById(id);
            int userId = GetCurrentUserId();

            if (post.UserProfileId == userId)
            {
                return View(post);
            }
            else
            {
                return NotFound();
            }
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Post post)
        {
            try
            {
                _postRepository.DeletePost(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(post);
            }
        }


        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

        public IActionResult InsertTag(Post post, Tag tag)
        {
            try
            {
                _postRepository.InsertTag(post, tag);

                return RedirectToAction("Details");
            }
            catch (Exception ex)
            {
                return View(post);
            }

        }
    }
}
