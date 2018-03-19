using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using Allianz.Vita.Quality.Extensions;
using Allianz.Vita.Quality.Models;
using Allianz.Vita.Quality.Services;
using System;
using System.Net;
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

        IStorageService Store
        {
            get
            {
                return ServiceFactory.Get<IStorageService>();
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

        [HttpGet]
        public ActionResult Issue()
        {
            IConfigurationService conf = Store.GetConfiguration();
            
            return View(new IssueCredentialsViewModel(conf.Issue));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateIssueSettings(IssueCredentialsViewModel model)
        {
            ActionResult result = RedirectToAction("Issue", model)
                .Warning("Data model is not valid...");

            if (ModelState.IsValid)
            {
                if (Store.Store(model))
                {
                    result = RedirectToAction("Issue", model)
                        .Success("Issue data successfully saved.");
                }
                else
                {
                    result = result
                        .Error("Issue data can't be saved.");
                }

            }

            return result;

        }

        [HttpGet]
        public ActionResult Defect()
        {
            IConfigurationService conf = Store.GetConfiguration();

            return View(new DefectCredentialsViewModel(conf.Defect));
        }

        [HttpGet]
        public ActionResult Mail()
        {
            IConfigurationService conf = Store.GetConfiguration();

            return View(new MailCredentialsViewModel(conf.Mail));
        }


        [HttpPost]
        public ActionResult UpdateCredentials(CredentialsViewModel model)
        {
            bool hasChanged = false;
            ActionResult result = RedirectToAction("Credentials", model);

            if (ModelState.IsValid)
            {
                if (model.UpdateTfsAccount)
                {
                    if (Service.IsValidAccount(model.TFSUserName, model.TFSPassword))
                    {
                        hasChanged = true;
                        Service.Logoff(typeof(IDefectService));
                        Service.AuthenticateOn(typeof(IDefectService), 
                            new NetworkCredential(model.TFSUserName, model.TFSPassword, model.TFSDomainName));
                        
                        result = result.Success("TFS Account Data Saved");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }
                }

                if (model.UpdateExchangeAccount)
                {
                    if (Service.IsValidAccount(model.ExchangeUserName, model.ExchangePassword))
                    {
                        hasChanged = true;
                        Service.Logoff(typeof(IMailService));
                        Service.AuthenticateOn(typeof(IMailService),
                            new NetworkCredential(model.ExchangeUserName, model.ExchangePassword, model.ExchangeDomainName));

                        result = result.Success("Exchange Account Data Saved");
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }
                }

                if (model.UpdateJiraAccount)
                {
                    if (Service.IsValidAccount(model.JiraUserName, model.JiraPassword))
                    {
                        hasChanged = true;
                        Service.Logoff(typeof(IIssueService));
                        Service.AuthenticateOn(typeof(IIssueService),
                            new NetworkCredential(model.JiraUserName, model.JiraPassword));

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