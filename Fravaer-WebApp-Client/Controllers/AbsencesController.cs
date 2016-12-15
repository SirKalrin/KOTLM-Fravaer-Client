using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BusinessLogic.Managers;
using Fravaer_WebApp_Client.Models;
using ServiceGateways.Entities;
using ServiceGateways.Facade;
using ServiceGateways.Interfaces;

namespace Fravaer_WebApp_Client.Controllers
{
    public class AbsencesController : Controller
    {
        private IServiceGateway<Absence, int> _absenceServiceGateway = new ServiceGatewayFacade().GetAbsenceServiceGateway();
        private UserManager _userManager = new UserManager();

        // GET: Absences
        public ActionResult Index()
        {
            return View(_absenceServiceGateway.ReadAll());
        }

        // GET: Absences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Absence absence = _absenceServiceGateway.Read(id.Value);
            if (absence == null)
            {
                return HttpNotFound();
            }
            return View(absence);
        }

        // GET: Absences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Absences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(int? id, DateTime? monthDate, string absenceType, DateTime? absenceDate, int? deletableAbsenceId)
        {
            
            if (absenceDate != null && absenceType != null)
            {
                _userManager.AddAbsenceToUser(id.Value, absenceDate, absenceType);
            }

            return RedirectToAction("Details", "Users", new RouteValueDictionary(new {id = id.Value, monthDate = monthDate.Value, chosenAbsence = absenceType }));

        }

        // GET: Absences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Absence absence = _absenceServiceGateway.Read(id.Value);
            if (absence == null)
            {
                return HttpNotFound();
            }
            return View(absence);
        }

        // POST: Absences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,User")] Absence absence)
        {
            if (ModelState.IsValid)
            {
                _absenceServiceGateway.Update(absence);
                return RedirectToAction("Index");
            }
            return View(absence);
        }

        // GET: Absences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Absence absence = _absenceServiceGateway.Read(id.Value);
            if (absence == null)
            {
                return HttpNotFound();
            }
            return View(absence);
        }

        // POST: Absences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id, DateTime? monthDate, string absenceType, DateTime? absenceDate, int? deletableAbsenceId)
        {
            if (absenceType.Equals("Slet") && deletableAbsenceId != null)
            {
                _absenceServiceGateway.Delete(deletableAbsenceId.Value);
            }

            return RedirectToAction("Details", "Users", new RouteValueDictionary(new { id = id.Value, monthDate = monthDate.Value, chosenAbsence = absenceType }));
        }
    }
}
