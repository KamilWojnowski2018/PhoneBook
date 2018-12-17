using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhoneBook.Models;

namespace PhoneBook.Controllers
{
    public class PersonController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            SourceManager sourceManager = new SourceManager();
            var show = sourceManager.GetAll();
            return View(show);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(PersonModel personModel)
        {

            if (ModelState.IsValid)
            {
                SourceManager sourceManager = new SourceManager();
                int id = sourceManager.Add(personModel);
                TempData["ID"] = "Dodałeś osobę o numerze identyfikacyjnym ID: " + id;
                return RedirectToAction("Index");
            }
            return View(personModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SourceManager sourceManager = new SourceManager();

            return View(sourceManager.GetByID(id));
        }

        [HttpPost]
        public ActionResult Edit(PersonModel personModel)
        {
            if (ModelState.IsValid)
            {
                SourceManager sourceManager = new SourceManager();
                sourceManager.Update(personModel);
                TempData["Edit"] = "Uaktualniłeś osobę: " + personModel.FirstName + " " + personModel.LastName;
                return RedirectToAction("Index");
            }
            return View(personModel);
        }

        [HttpGet]
        public ActionResult Remove(int id)
        {
            SourceManager sourceManager = new SourceManager();

            return View(sourceManager.GetByID(id));
        }

        [HttpPost]
        public ActionResult RemoveConfirm(int id)
        {
            SourceManager sourceManager = new SourceManager();
            var osoba = sourceManager.GetByID(id);
            sourceManager.Remove(id);
            TempData["Remove"] = "Usunąłeś następującą osobę: " + osoba.FirstName + " " + osoba.LastName;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string TextBox)
        {
            SourceManager sourceManager = new SourceManager();
            var result = sourceManager.Search(TextBox);
            return View(result);
        }
    }
}