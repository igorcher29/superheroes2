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
    public class SuperpowerController : Controller
    {
        ISuperpowerRepository _repository;

        public SuperpowerController(ISuperpowerRepository repository)
        {
            _repository = repository;
        }
        // GET: Superpower
        public ActionResult Index()
        {
            return View();
        }

        // GET: Superpower/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Superpower superPower = _repository.GetSuperpower(id);

            if (superPower == null)
            {
                return HttpNotFound();
            }
            return View(superPower);
        }

        // GET: Superpower/Create
        public ActionResult Create(int? superHeroId)
        {
            if (superHeroId != null)
            {
                ViewBag.SuperHeroId = superHeroId;
                return View();
            }
            return HttpNotFound();
        }

        // POST: Superpower/Create
        [HttpPost]
        public ActionResult Create(Superpower superpower, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    superpower.ImageMimeType = image.ContentType;
                    superpower.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(superpower.ImageData, 0, image.ContentLength);
                }
                _repository.Create(superpower);

                return RedirectToAction
                    (
                        "Edit",
                        new
                        {
                            controller = "Superhero",
                            action = "Edit",
                            id = superpower.SuperheroId
                        }
                    );
            }

            //ViewBag.SuperHeroId = new SelectList(repository.SuperHeroes, "Id", "Name", superPower.SuperHeroId);
            return View(superpower);            
        }

        // GET: Superpower/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Superpower superPower = _repository.GetSuperpower(id);
            if (superPower == null)
            {
                return HttpNotFound();
            }
            //ViewBag.SuperHeroId = new SelectList(repository.SuperHeroes, "Id", "Name", superPower.SuperHeroId);

            return View(superPower);
        }

        // POST: Superpower/Edit/5
        [HttpPost]
        public ActionResult Edit(Superpower superPower, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                //repository.SaveSuperPower(superPower, image);
                _repository.Update(superPower, image);

                return RedirectToAction
                    (
                    "Edit",
                    new
                    {
                        controller = "Superhero",
                        action = "Edit",
                        id = superPower.SuperheroId
                    }
                    );
            }
            //ViewBag.SuperHeroId = new SelectList(repository.SuperHeroes, "Id", "Name", "Rating", superPower.SuperHeroId);
            return View(superPower);
        }

        // GET: Superpower/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Superpower superPower = _repository.GetSuperpower(id);
            if (superPower == null)
            {
                return HttpNotFound();
            }
            return View(superPower);
        }

        // POST: Superpower/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed (int id)
        {
            Superpower superPower = _repository.GetSuperpower(id);
            int? superheroId = superPower.SuperheroId;
            superPower = null;            
            _repository.Delete(id);
            return RedirectToAction
            (
                "Edit",
                new
                {
                    controller = "Superhero",
                    action = "Edit",
                    id = superheroId
                }
            );
        }

        public FileContentResult GetImage(int superpowerId)
        {
            Superpower superpower = _repository.GetSuperpower(superpowerId);

            if (superpower != null)
            {
                return File(superpower.ImageData, superpower.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
