using Domain.Entities;
using PIdevMVC.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PIdevMVC.Controllers
{
    public class ArchiveController : Controller
    {
        ServiceArchivage sc = null;
        ServiceDocument scd = null;

        // GET: ReservationAcc
        public ArchiveController()
        {
            sc = new ServiceArchivage();
            scd = new ServiceDocument();


        }
        // GET: Archive
        public ActionResult Index()
        {
            user user = new user();
            document doc = new document();
            List<archivage> list = new List<archivage>();
            foreach (var item in sc.GetAll())
            {
                archivage archive = new archivage();
                archive.id = item.id;
                archive.user_id = item.user_id;
                archive.document_id = item.document_id;
                archive.version = item.version;
                
                list.Add(archive);

            }
            return View(list);

        }

        // GET: Archive/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Archive/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Archive/Create
        [HttpPost]
        public ActionResult Create(int id , ArchiveModel arMVC)
        {
            arMVC.document_id = id;
            if (!ModelState.IsValid || arMVC.document_id == 0)
            {
                RedirectToAction("Create");
            }
            //document doc = new document();
            // doc = scd.GetById(id);
            int id_user = 1;
            archivage ar = new archivage();
            ar.document_id = arMVC.document_id;
            ar.user_id = id_user;
            ar.version = arMVC.version;

            sc.Add(ar);
            sc.Commit();


            // Sauvgarde de l'image


            return RedirectToAction("index");
        }

        // GET: Archive/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Archive/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ArchiveModel arMVC)
        {
            
            archivage archive = sc.GetById(id);
            archive.id = arMVC.id;
            archive.document_id = arMVC.document_id;
            archive.user_id = arMVC.user_id;
            archive.version = arMVC.version;

            if (ModelState.IsValid)
            {
                sc.Update(archive);
                sc.Commit();
                return RedirectToAction("index");
            }

            return View();
        }

        // GET: Archive/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Archive/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ArchiveModel arc)
        {
            try
            {
                archivage acc = sc.GetById(id);
                arc.document_id = acc.document_id;
                arc.user_id = acc.user_id;
                arc.version = acc.version;
                sc.Delete(acc);
                sc.Commit();
                return RedirectToAction("index");
            }
            catch
            {
                return View();
            }
        }
    }
}
