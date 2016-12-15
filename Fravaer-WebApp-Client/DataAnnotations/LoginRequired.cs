using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Ast.Selectors;

namespace Fravaer_WebApp_Client.DataAnnotations
{
    public class LoginRequired : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            UrlHelper urlHelper = new UrlHelper(context.RequestContext);
            if (HttpContext.Current.Session["token"] == null)
            context.Result = new RedirectResult(urlHelper.Action("Login", "Account"));
        }
    }
}