using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PartiesController : Controller
    {
        private IStoreManagerRepository db;

        // if no param passed to constructor, use EF Repository & DbContext
        public PartiesController()
        {
            this.db = new EFStoreManagerRepository();
        }

        // if mock repo object passed to constructor, use Mock interface for unit testing
        public PartiesController(IStoreManagerRepository smRepo)
        {
            this.db = smRepo;
        }


        //private Comp2007Sept28_Ass2Entities db = new Comp2007Sept28_Ass2Entities();

        // GET: Parties
        public ViewResult Index()
        {
            var parties = db.Parties.Include(p => p.PLocation);
            return View(parties.ToList());
        }

        // GET: Parties/Details/5
        [Authorize]
        public ViewResult Details(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            //Party party = db.Parties.Find(id);
            Party party = db.Parties.SingleOrDefault(a => a.PartyId == id);
            if (party == null)
            {
                return View("Error");
            }
            return View(party);
        }

        // GET: Parties/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(db.Plocation, "LocationID", "City");
            return View("Create");
        }

        // POST: Parties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PartyId,Message_,Party_Type_,Address_,LocationID")] Party party)
        {
            if (ModelState.IsValid)
            {
                db.Save(party);
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(db.Plocation, "LocationID", "City", party.LocationID);
            return View("Create",party);
        }

        // GET: Parties/Edit/5
        [Authorize]
        public ViewResult Edit(int? id)
        {
            if (id == null)
            {
                 return View("Error");
            }
            //Party party = db.Parties.Find(id);
            Party party = db.Parties.SingleOrDefault(a => a.PartyId == id);
            if (party == null)
            {
                return View("Error");
            }
            ViewBag.LocationID = new SelectList(db.Plocation, "LocationID", "City", party.LocationID);
            return View("Edit",party);
        }

        // POST: Parties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PartyId,Message_,Party_Type_,Address_,LocationID")] Party party)
        {
            if (ModelState.IsValid)
            {
                db.Save(party);
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Plocation, "LocationID", "City", party.LocationID);
            return View("Edit",party);
        }

        // GET: Parties/Delete/5
        [Authorize]
        public ViewResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            //Party party = db.Parties.Find(id);
            Party party = db.Parties.SingleOrDefault(a => a.PartyId == id);
            if (party == null)
            {
                return View("Error");
            }
            return View("Delete",party);
        }

        // POST: Parties/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Party party = db.Parties.SingleOrDefault(a => a.PartyId == id);
            if (party == null)
            {
                return View("Error");
            }

            db.Delete(party);

            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

    }
}
