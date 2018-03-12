using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using Allianz.Vita.Quality.Extensions;
using Allianz.Vita.Quality.Models;
using Allianz.Vita.Quality.Services;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace Allianz.Vita.Quality.Controllers
{

    public class AccountController : Controller
    {

        CookieAuthenticationService CookieService = new CookieAuthenticationService();

        IIdentityService Service
        {
            get
            {
                return ServiceFactory.Get<IIdentityService>();
            }
        }

        [HttpGet]
        public ActionResult SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid) { }

            model.UserName = Environment.UserName;
            if (!string.IsNullOrEmpty(Environment.UserDomainName))
                model.UserName = Environment.UserDomainName + "\\" + Environment.UserName;
            model.RememberMe = true;
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Service.IsValidUser(model.UserName))
                {
                    IUserCredentials user = Service.LogOn(model.UserName);

                    CookieService.EnsureCookie(Request, Response, model.UserName, model.RememberMe);
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            return RedirectToAction("SignIn", model);
        }

        [HttpGet]
        public ActionResult Index(CredentialsViewModel model)
        {
            if (!Service.IsAuthenticated())
                RedirectToAction("SignIn");

            return View(model);

        }

        [HttpGet]
        public ActionResult Credentials(CredentialsViewModel model)
        {
            if (!Service.IsAuthenticated())
                RedirectToAction("SignIn");

            if (model == null || !model.Initialized)
            {
                CredentialsViewModel cookieCredentials = CookieService.GetData(Request, User.Identity.Name);
                model = cookieCredentials;
                model.Initialized = true;    
            }

            return View(model);

        }

        [HttpPost]
        public ActionResult UpdateCredentials(CredentialsViewModel model)
        {
            bool hasChanged = false;
            ActionResult result = RedirectToAction("Credentials", model);

            if (ModelState.IsValid)
            {
                if (Service.IsValidUser(model.TFSUserName))
                {
                    if (Service.IsValidAccount(model.TFSUserName, model.TFSPassword))
                    {
                        hasChanged = true;
                        Service.Logoff(typeof(IDefectService));
                        Service.AuthenticateOn(typeof(IDefectService), new System.Net.NetworkCredential(model.TFSUserName, model.TFSPassword, model.TFSDomainName));
                        
                        result = result.Success("TFS Account Data Saved");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }
                }

                if (Service.IsValidUser(model.ExchangeUserName))
                {
                    if (Service.IsValidAccount(model.ExchangeUserName, model.ExchangePassword))
                    {
                        hasChanged = true;
                        Service.Logoff(typeof(IMailService));
                        Service.AuthenticateOn(typeof(IMailService),
                        new System.Net.NetworkCredential(model.ExchangeUserName, model.ExchangePassword, model.ExchangeDomainName));

                        result = result.Success("Exchange Account Data Saved");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }
                }

                if (hasChanged)
                {
                    // save to cookie
                    CookieService.SetData(Request, Response, User.Identity.Name, model);
                }
            }
            else
            {
                result = RedirectToAction("SigIn", model);
            }

            return result;
        }


        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Service.Logoff();
            return RedirectToAction("Index", "Home");
        }
    }
}