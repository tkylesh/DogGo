using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DogGo.Controllers
{
    
    public class WalkersController : Controller
    {
        private readonly IWalkerRepository _walkerRepo;
        private readonly IWalksRepository _walksRepo;
        private readonly IOwnerRepository _ownerRepo;

        public WalkersController(IWalkerRepository walkerRepository, IWalksRepository walksRepo,IOwnerRepository ownerRepo)
        {
            _walkerRepo = walkerRepository;
            _walksRepo = walksRepo;
            _ownerRepo = ownerRepo;
        }
        // GET: WalkersController
        public ActionResult Index()
        {
            List<Walker> walkers = new List<Walker>();

            if (User.Identity.IsAuthenticated)
            {
                //get owner id
                int userId = GetCurrentUserId();
                //use owner repo to get owner by id
                Owner owner = _ownerRepo.GetOwnerById(userId);
                //get neighborhood id from owner
                int neighborhoodId = owner.NeighborhoodId;
                //use neighborhood id to get walkers in neighborhood
                walkers = _walkerRepo.GetWalkersInNeighborhood(neighborhoodId);
            }
            else
            {
                walkers = _walkerRepo.GetAllWalkers();
            }

            return View(walkers);
        }

        // GET: WalkersController/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);
            List<Walks> walks = _walksRepo.GetWalksByWalkerId(id);

            WalkerProfileViewModel walkerProfile = new WalkerProfileViewModel
            {
                Walker = walker,
                AllWalks = walks
            };
          
            return View(walkerProfile);
        }

        // GET: WalkersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkersController/Create
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

        // GET: WalkersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkersController/Edit/5
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

        // GET: WalkersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkersController/Delete/5
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
