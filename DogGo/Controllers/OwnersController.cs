﻿using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DogGo.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IDogRepository _dogRepository;
        private readonly IWalkerRepository _walkerRepository;
        private readonly INeighborhoodRepository _neighborhoodRepository;
        public OwnersController(IOwnerRepository ownerRepository, IDogRepository dogRepository, IWalkerRepository walkerRepository, INeighborhoodRepository neighborhoodRepository)
        {
            _ownerRepository = ownerRepository;
            _dogRepository = dogRepository;
            _walkerRepository = walkerRepository;
            _neighborhoodRepository = neighborhoodRepository;
        }
        // GET: OwnersController
        public ActionResult Index()
        {
            List<Owner> owners = _ownerRepository.GetAllOwners();
            return View(owners);
        }

        // GET: Owners/Details/5
        public ActionResult Details(int id)
        {
            Owner owner = _ownerRepository.GetOwnerById(id);
            List<Dog> dogs = _dogRepository.GetDogsByOwnerId(owner.Id);
            List<Walker> walkers = _walkerRepository.GetWalkersInNeighborhood(owner.NeighborhoodId);


            ProfileViewModel vm = new ProfileViewModel()
            {
                Owner = owner,
                Dogs = dogs,
                Walkers = walkers
            };

            return View(vm);
        }
        // GET: Owners/Create
        public ActionResult Create()
        {
            List<Neighborhood> neighborhoods = _neighborhoodRepository.GetAll();

            OwnerFormViewModel vm = new OwnerFormViewModel()
            {
                Owner = new Owner(),
                Neighborhoods = neighborhoods
            };

            return View(vm);
        }

        // POST: OwnersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Owner owner)
        {
            try
            {
                _ownerRepository.AddOwner(owner);

                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View(owner);
            }
        }

        // GET: Owners/Edit/5
        public ActionResult Edit(int id)
        {
            Owner owner = _ownerRepository.GetOwnerById(id);

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Owner owner)
        {
            try
            {
                _ownerRepository.UpdateOwner(owner);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(owner);
            }
        }

        // GET: Owners/Delete/5
        public ActionResult Delete(int id)
        {
            Owner owner = _ownerRepository.GetOwnerById(id);

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Owner owner)
        {
            try
            {
                _ownerRepository.DeleteOwner(id);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(owner);
            }
        }



        // GET: Owners/Login    
        public ActionResult Login()
        {
            return View();
        }

        // POST: Owners/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> Login(LoginViewModel viewModel)
        {
            Owner owner = _ownerRepository.GetOwnerByEmail(viewModel.Email);

            if (owner == null)
            {
                return Unauthorized();
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, owner.Id.ToString()),
                new Claim(ClaimTypes.Email, owner.Email),
                new Claim(ClaimTypes.Role, "DogOwner")
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Dog");
        }

        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
