using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class UserProfileController : Controller
    {

        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        // GET: UserProfileController
        public ActionResult Index()
        {
            List<UserProfile> profiles = _userProfileRepository.GetAllProfiles();

            return View(profiles);
        }

        // GET: UserProfileController/Details/5
        public ActionResult Details(int id)
        {
            UserProfile profile = _userProfileRepository.GetUserProfileById(id);

            return View(profile);
        }

        // GET: UserProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserProfileController/Create
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

        // GET: UserProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserProfileController/Edit/5
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

        // GET: UserProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserProfileController/Delete/5
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

        // GET: UserProfileController/DeactivateUser/5
        [Authorize]
        public ActionResult DeactivateUser(int id)
        {
            UserProfile userProfile = _userProfileRepository.GetUserProfileById(id);

            if(userProfile.UserTypeId == 1)
            {
                return View(userProfile);
            }
           
            return NotFound();
        }

        // POST: UserProfileController/DeactivateUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeactivateUser(int id, UserProfile userProfile)
        {
            try
            {
                _userProfileRepository.DeactivateUserById(id);
                return RedirectToAction("Index");
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
