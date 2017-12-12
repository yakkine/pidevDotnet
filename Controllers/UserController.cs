using Domain.Entites;
using RestSharp;
using Rotativa.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using Data;

namespace WebConsume.Controllers
{
  
    public class UserController : Controller
    {
        PidevContext ctx = new PidevContext();
        public static user currentuser = null;
        // GET: User
        public ActionResult Index()
        {
            var client = new RestClient("http://localhost:18080/pidevjee-web/rest/");
            var request = new RestRequest("users", Method.GET);
            
            request.AddHeader("Content-type", "application/json");


            IRestResponse < List <user>> u = client.Execute<List<user>> (request);

            return View(u.Data);
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
            var request = new RestRequest("users/"+email+"/"+password, Method.GET);
            request.AddHeader("Content-type", "application/json");

           
            IRestResponse<List<user>> user = client.Execute<List<user>>(request);
            currentuser = user.Data[0];

            if (currentuser.role.Equals("administrateur"))
            {
                return RedirectToAction("Create");
            }
            else
            {
                return RedirectToAction("Login");
            }

            
        }

        public ActionResult Information()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Information(HttpPostedFileBase photos)
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
                return RedirectToAction("Create");
            }
            else
            {
                return RedirectToAction("Information");
            }

        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.curentuser = currentuser;
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(user user)
        {

            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:18080/pidevjee-web/rest/");

            Client.PostAsJsonAsync<user>("users",user).ContinueWith((postTask) => postTask.Result.IsSuccessStatusCode);
            return RedirectToAction("Index");

        }
        public ActionResult ExportPdf()
        {

            return new ActionAsPdf("Index")
            {

                FileName = Server.MapPath("~/Content/List.pdf")

            };

        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:18080/");
                var deleteTask = client.DeleteAsync("pidevjee-web/rest/users/" + id);
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            var res = ctx.user.Where(x => x.id == id).FirstOrDefault();
            user u = new user();
            u.id = res.id;
            u.firstname = res.firstname;
            u.lastname = res.lastname;
            u.cin = res.cin;
            u.email = res.email;
            u.password = res.password;
            u.role = res.sexe;
            u.salaire = res.salaire;
            u.sexe = res.sexe;
           
            return View(u);
        }

        [HttpPost]
        public ActionResult Edit(user item)
        {
            var u = ctx.user.Where(x => x.id == item.id).FirstOrDefault();

            if (ModelState.IsValid)
            {
                u.firstname = item.firstname;
                u.lastname = item.lastname;
                u.cin = item.cin;
                u.email = item.email;
                u.password = item.password;
                u.role = item.role;
                u.salaire = item.salaire;
                u.sexe = item.sexe;
                

                ctx.SaveChanges();

                return RedirectToAction("Index");
            }
            else

                return View(u);
        }

        public ActionResult Statistique()
        {

            new Chart(width: 800, height: 200).AddSeries(chartType: "Column", xValue: new[] { "Actif", "NonActif" }, yValues: new[] { 1, 5}).Write("png");
            return View("chart");
        }

        public ActionResult Chart()
        {
            var context = new PidevContext();
            var CountN = context.user.SqlQuery("Select * from user where isValid=1").Count();
            var CountT = context.user.SqlQuery("Select * from user where isValid=0").Count();

            new Chart(width: 800, height: 200).AddSeries(chartType: "pie", xValue: new[] { "Utilisateur Actif", "User non Acif" }, yValues: new[] { CountT, CountN }).Write("png");
            return View("chart");
        }
    }
}