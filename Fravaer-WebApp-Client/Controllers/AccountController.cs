using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Fravaer_WebApp_Client.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using ServiceGateways.Entities;
using ServiceGateways.Facade;
using ServiceGateways.Interfaces;

namespace Fravaer_WebApp_Client.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private IAuthorizationServiceGateway _authorizationServiceGateway = new ServiceGatewayFacade().GetAuthorisationServiceGateway();
        private IServiceGateway<User, int> _userServiceGateway = new ServiceGatewayFacade().GetUserServiceGateway();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); }
            private set { _signInManager = value; }
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = _authorizationServiceGateway.Login(model.UserName, model.Password);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Details", "Users", new {id = _userServiceGateway.ReadAll().FirstOrDefault(x => x.UserName == model.UserName)?.Id});

                }
                ModelState.AddModelError("", "Invalid login attempt!");

            }
            return View(model);
        }

    }
}