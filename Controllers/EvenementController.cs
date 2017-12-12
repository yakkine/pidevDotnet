using Data;
using Domain.Entites;
using pidev.service.Repositories;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace WebConsume.Controllers
{
    public class EvenementController : Controller
    {
        EvenementService ise = null;
        public static user currentuser = null;
        public EvenementController()
        {
            ise = new EvenementService();
        }


        // GET: Evenement
        public ActionResult Index()
        {
            return View(ise.getAllEvenement());
        }

        // GET: Evenement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Evenement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Evenement/Create
        [HttpPost] 
        public ActionResult Create(evenement e)
        {
          
            ise.createEvenement(e);
            TempData.Clear();
          
            return RedirectToAction("Index", "Evenement");

        }

        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Login(HttpPostedFileBase photo)
        {

            var email = Request.Form["email"];
            var password = Request.Form["password"];
            var client = new RestClient("http://localhost:18080/pidevjee-web/rest/");
            var request = new RestRequest("users/" + email + "/" + password, Method.GET);
            request.AddHeader("Content-type", "application/json");


            IRestResponse<List<user>> user = client.Execute<List<user>>(request);
            currentuser = user.Data[0];

            if (currentuser.role.Equals("administrateur"))
            {
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Index");
            }


        }

        // GET: Evenement/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            evenement c = ise.getEvenementById(id);
            if (c == null)
            {
                return HttpNotFound();
            }

            return View(c);
        }

        // POST: Evenement/Edit/5
        [HttpPost]
        public ActionResult Edit(evenement e)
        {
            if (ModelState.IsValid)
            {
                ise.updateEvenement(e);
                TempData.Clear();
                TempData["updated"] = e.titre;
                return RedirectToAction("Index");
            }
            return View(e);
        }

        // GET: Evenement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Evenement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            ise.deleteEvenementById(id);
            var hs = ise.getAllEvenement();
            return RedirectToAction("Index", hs);
        }

        public ActionResult Chart()
        {
            var context = new PidevContext();
            var CountN = context.evenement.SqlQuery("Select * from evenement where nbrmaxpart > 10").Count();
            var CountT = context.evenement.SqlQuery("Select * from evenement where nbrmaxpart < 10").Count();

            new Chart(width: 800, height: 200).AddSeries(chartType: "pie", xValue: new[] { "Capacité > 10", "Capacité < 10" }, yValues: new[] { CountT, CountN }).Write("png");
            return View("Chart");
        }
    }
}
