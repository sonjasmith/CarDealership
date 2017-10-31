using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDealership.Models;

namespace CarDealership.Controllers
{
    public class CarBuyersController : Controller
    {
        private CarDealershipEntities db = new CarDealershipEntities();

        // GET: CarBuyers
        public ActionResult Index()
        {
            var carBuyers = db.CarBuyers.Include(c => c.Buyer).Include(c => c.Car);
            return View(carBuyers.ToList());
        }

        // GET: CarBuyers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarBuyer carBuyer = db.CarBuyers.Find(id);
            if (carBuyer == null)
            {
                return HttpNotFound();
            }
            return View(carBuyer);
        }

        // GET: CarBuyers/Create
        public ActionResult Create()
        {
            ViewBag.BuyerID = new SelectList(db.Buyers, "BuyerID", "FirstName");
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "VinNumber");
            return View();
        }

        // POST: CarBuyers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Car_BuyerID,CarID,BuyerID")] CarBuyer carBuyer)
        {
            if (ModelState.IsValid)
            {
                db.CarBuyers.Add(carBuyer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BuyerID = new SelectList(db.Buyers, "BuyerID", "FirstName", carBuyer.BuyerID);
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "VinNumber", carBuyer.CarID);
            return View(carBuyer);
        }

        // GET: CarBuyers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarBuyer carBuyer = db.CarBuyers.Find(id);
            if (carBuyer == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuyerID = new SelectList(db.Buyers, "BuyerID", "FirstName", carBuyer.BuyerID);
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "VinNumber", carBuyer.CarID);
            return View(carBuyer);
        }

        // POST: CarBuyers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Car_BuyerID,CarID,BuyerID")] CarBuyer carBuyer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carBuyer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuyerID = new SelectList(db.Buyers, "BuyerID", "FirstName", carBuyer.BuyerID);
            ViewBag.CarID = new SelectList(db.Cars, "CarID", "VinNumber", carBuyer.CarID);
            return View(carBuyer);
        }

        // GET: CarBuyers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarBuyer carBuyer = db.CarBuyers.Find(id);
            if (carBuyer == null)
            {
                return HttpNotFound();
            }
            return View(carBuyer);
        }

        // POST: CarBuyers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarBuyer carBuyer = db.CarBuyers.Find(id);
            db.CarBuyers.Remove(carBuyer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
