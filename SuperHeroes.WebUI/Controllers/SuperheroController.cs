using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseModel;
using Repositories;
using Domain;
using System.Data.Entity;
using Microsoft.AspNet.Identity.Owin;
using SuperHeroes.WebUI.Models;
using Microsoft.AspNet.Identity;
using System.Net;

namespace SuperHeroes.WebUI.Controllers
{
    [Authorize]
    public class SuperheroController : Controller
    {
        ISuperHeroRepository _repository;
        public SuperheroController(ISuperHeroRepository repository)
        {
            _repository = repository;
        }
        // GET: Superhero
        public ActionResult Index()
        {
            return View(_repository.GetSuperheroesForUser(CurrentUserId()));
        }

        // GET: Superhero/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Superhero superHero = _repository.GetSuperhero(id);
            if (superHero == null)
            {
                return HttpNotFound();
            }
            return View(superHero);
        }

        // GET: Superhero/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Superhero/Create
        [HttpPost]
        public ActionResult Create(Superhero superHero, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image !=null)
                {
                    superHero.ImageMimeType = image.ContentType;
                    superHero.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(superHero.ImageData, 0, image.ContentLength);
                }
                superHero.UserId = CurrentUserId();

                _repository.Create(superHero);

                return RedirectToAction("Index");
            }

            return View(superHero);
        }

        // GET: Superhero/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Superhero superHero = _repository.GetSuperhero(id);

            if (superHero == null)
            {
                return HttpNotFound();
            }
            return View(superHero);
        }

        // POST: Superhero/Edit/5
        [HttpPost]
        public ActionResult Edit(Superhero superHero, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                superHero.UserId = CurrentUserId();
                _repository.Update(superHero, image);

                return RedirectToAction("Index");
            }

            return View(superHero);
        }

        // GET: Superhero/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Superhero superHero = _repository.GetSuperhero(id);
            if (superHero == null)
            {
                return HttpNotFound();
            }
            return View(superHero);
        }

        // POST: Superhero/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("Index");
        }

        private string CurrentUserId()
        {
            string userId = String.Empty;

            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);

            if (user != null)
            {
                userId = user.Id;
            }

            return userId;
        }

        public FileContentResult GetImage(int superheroId)
        {
            Superhero superhero = _repository.GetSuperhero(superheroId);
            if (superhero != null)
            {
                return File(superhero.ImageData, superhero.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
