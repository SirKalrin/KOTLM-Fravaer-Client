using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using BusinessLogic.Managers;
using DateTimeExtensions;
using Fravaer_WebApp_Client.DataAnnotations;
using Fravaer_WebApp_Client.Models;
using Microsoft.AspNet.Identity.Owin;
using ServiceGateways.Entities;
using ServiceGateways.Facade;
using ServiceGateways.Interfaces;
using ServiceGateways.ServiceGateways;

namespace Fravaer_WebApp_Client.Controllers
{
    [LoginRequired]
    public class UsersController : Controller
    {
        private IServiceGateway<User, int> _userServiceGateway = new ServiceGatewayFacade().GetUserServiceGateway();

        private IServiceGateway<Department, int> _departmentServiceGateway =
            new ServiceGatewayFacade().GetDepartmentServiceGateway();
        private IServiceGateway<Absence, int> _absenceServiceGateway = new ServiceGatewayFacade().GetAbsenceServiceGateway();
        private UserManager _userManager = new UserManager();

        private IAuthorizationServiceGateway _authorizationServiceGateway = new ServiceGatewayFacade().GetAuthorisationServiceGateway();

        private string _deleteType = "Slet";
        // GET: User
        public ActionResult Index(DateTime? monthTime)
        {
            decimal averageDays = Decimal.Divide(100, DateTime.Now.LastDayOfTheMonth().Day);
            var month = DateTime.Now;
            if (monthTime != null)
            {
                month = monthTime.Value;
            }
            var ViewModel = new UserIndexViewModel()
            {
                Departments = _departmentServiceGateway.ReadAll(),
                MonthDateTime = month,
                AverageDaysInt = averageDays
            };
            return View(ViewModel);
        }

        // GET: User/Details/5
        /* Return the DetailsView, which visualizes the user with the given id, 
         and show its absences in the given month, 
         and select the chosenAbsence type if one is given*/
        public ActionResult Details(int? id, DateTime? monthDate, string chosenAbsence)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //read and checks the user
            User user = _userServiceGateway.Read(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }

            //Setting the dateTime
            DateTime monthShow = DateTime.Now;
            if (monthDate != null)
            {
                monthShow = monthDate.Value;
            }
            //Getting the index to start the month from
            var initIndex = _userManager.GetInitIndex(monthShow);
            //Getting the different types of absences + description
            var absenceTypes = _userManager.GetAbsenceTypes();



            //Creating the ViewModel
            var viewModel = new UserDetailsViewModel()
            {
                User = user,
                DateTime = monthShow,
                InitIndex = initIndex,
                AbsenceTypes = absenceTypes,
                ChosenAbsence = chosenAbsence,
            };

            return View(viewModel);
        }

        /* This POST method deleted an absence if an absence Id is given with the absence type of delete, 
         or creates an absence if the above criterias isnt met an absence DateTime and absence type is given, 
         where it redirect to DetailsView afterwards*/
        [HttpPost]
        public ActionResult Details(int? id, DateTime? monthDate, string absenceType, DateTime? absenceDate, int? deletableAbsenceId)
        {
            //User user = _userServiceGateway.Read(id.Value);

            ////Either adds or deleted an absence
            //if (absenceType.Equals("Slet") && deletableAbsenceId != null)
            //{
            //    _userManager.DeleteAbsenceFromUser(deletableAbsenceId.Value);
            //}
            //else if(absenceType.Equals("Slet"))
            //{
            //    //Do nothing
            //}
            //else if(absenceDate != null && absenceType != null)
            //{
            //    _userManager.AddAbsenceToUser(user, absenceDate, absenceType);
            //}

            return RedirectToAction("Details", "Users", new RouteValueDictionary(new {id = id.Value, monthDate = monthDate.Value, chosenAbsence = absenceType}));
        }

        // GET: Medarbejder/Create
        public ActionResult Create()
        {
            return View(new CreateUserViewModel()
            {
                User = new User(),
                Departments = _departmentServiceGateway.ReadAll()
            });
        }

        // POST: Medarbejder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "Id,FirstName,LastName,UserName,Email,Password,ConfirmPassword,Department,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                user = _userServiceGateway.Create(user);
                _authorizationServiceGateway.Register(user);
                return RedirectToAction("Index", "Users");
            }

            return View(new CreateUserViewModel()
            {
                User = user,
                Departments = _departmentServiceGateway.ReadAll()
            });
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _userServiceGateway.Read(id.Value);
            user.ConfirmPassword = user.Password;
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(new CreateUserViewModel() { User = user, Departments = _departmentServiceGateway.ReadAll() });
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "Id,FirstName,LastName,UserName,Email,Password,ConfirmPassword,Department,Role")] User user)
        {
            if (ModelState.IsValid)
            {
                _userServiceGateway.Update(user);
                return RedirectToAction("Index");
            }
            return View(new CreateUserViewModel() { User = user, Departments = _departmentServiceGateway.ReadAll() });
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _userServiceGateway.Read(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _userServiceGateway.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddGrayDaysToUser(int? id, DateTime dateFrom, DateTime dateEnd, List<string> chosenDays)
        {
            for (DateTime i = dateFrom; i <= dateEnd;)
            {
                foreach (var dayType in chosenDays)
                {
                    if (i.DayOfWeek.ToString().Equals(dayType))
                    {
                        _userManager.AddAbsenceToUser(id.Value, i, Statuses.GRAY.ToString());
                    }
                }
                i = i.AddDays(1);
            }
            return RedirectToAction("Details", new RouteValueDictionary(new { id = id.Value, monthDate = dateFrom }));
        }

        // POST: Absences/Delete/5
        //Deletes the absence with the given deletableAbsenceId and redirects to the details view.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAbsence(int? id, DateTime? monthDate, string absenceType, int? deletableAbsenceId)
        {
            if (absenceType.Equals(_deleteType) && deletableAbsenceId != null)
            {
                _absenceServiceGateway.Delete(deletableAbsenceId.Value);
            }

            return RedirectToAction("Details", "Users", new RouteValueDictionary(new { id = id.Value, monthDate = monthDate.Value, chosenAbsence = absenceType }));
        }

        // POST: Absences/Create
        // Calls the userManager to create an absence with the given variables and redirects to the detailsView.
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAbsence(int? id, DateTime? monthDate, string absenceType, DateTime? absenceDate)
        {

            if (absenceDate != null && absenceType != null && absenceType != _deleteType)
            {
                _userManager.AddAbsenceToUser(id.Value, absenceDate, absenceType);
            }

            return RedirectToAction("Details", "Users", new RouteValueDictionary(new { id = id.Value, monthDate = monthDate.Value, chosenAbsence = absenceType }));

        }

        /*
         * Sends a hardcoded notification-email message to all users in the database.
         */
        public async Task<ActionResult> EmailNotification()
        {
            var printString = "";
            foreach (var user in _userServiceGateway.ReadAll())
            {             
                    var body = "<p>Hej {0} {1}</p><p></p><p>{2}</p>";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress(user.Email));
                    message.Subject = "Registrering af fravær";
                    message.Body = string.Format(body, user.FirstName, user.LastName,
                        "Fristen for godkendelse af fravær nærmer sig. Godkend venligst dit fravær hurtigst muligt via ´Min Side´ på hjemmesiden, hvis du endnu ikke har gjort det.");
                    message.IsBodyHtml = true;
                    using (var smtp = new SmtpClient())
                    {
                        await smtp.SendMailAsync(message);
                        printString = printString + user.FirstName + " " + user.LastName + "\n";
                }                
            }

            System.Windows.Forms.MessageBox.Show($"Email notifikationer blev sendt til:\n{printString}");
            return RedirectToAction("Index");
        }
    }
}