using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using Allianz.Vita.Quality.Extensions;
using Allianz.Vita.Quality.Models;
using Allianz.Vita.Quality.Services;
using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Allianz.Vita.Quality.Controllers
{

    public class AccountController : Controller
    {

        CookieAuthenticationService CookieService = new CookieAuthenticationService();

        IConfigurationService Conf
        {
            get
            {
                return ServiceFactory.Get<IConfigurationService>();
            }
        }

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
            //if (ModelState.IsValid) { }

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
                return RedirectToAction("SignIn")
                    .Warning("Please Sign In");

            return RedirectToAction("Credentials", model);

        }

        [HttpGet]
        public ActionResult Credentials(CredentialsViewModel model)
        {
            if (!Service.IsAuthenticated())
                return RedirectToAction("SignIn")
                    .Warning("Please Sign In");

            if (model == null || !model.Initialized)
            {
                CredentialsViewModel cookieCredentials = CookieService.GetData(Request, User.Identity.Name);
                model = cookieCredentials;
                model.Initialized = true;
            }

            return View(model);

        }

        [HttpGet]
        public ActionResult Issue(IssueCredentialsViewModel model)
        {
            if (!Service.IsAuthenticated())
                return RedirectToAction("SignIn")
                    .Warning("Please Sign In");

            if (model == null || model.ServiceName == null)
            {
                IConfigurationService conf = Store.GetConfiguration();
                model = new IssueCredentialsViewModel(conf.Issue);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateIssueSettings(IssueCredentialsViewModel model)
        {

            ActionResult result = View("Issue", model);

            return HandleResult("Issue", model, () =>
               {
                   if (Store.Store(model))
                   {
                       return result
                           .Success("Issue data successfully saved.");
                   }
                   else
                   {
                       return result
                           .Error("Issue data can't be saved.");
                   }
               });
        }

        private ActionResult HandleResult(string view, object model, Func<ActionResult> action)
        {
            ActionResult result = View(view, model)
                .Warning("Data model is not valid...");

            if (ModelState.IsValid)
            {
                try
                {
                    result = action();
                }
                catch (Exception e)
                {
                    result = result
                            .Error("Error on handling data: " + e.Message);
                }
            }

            return result;

        }

        [HttpGet]
        public ActionResult Defect(DefectCredentialsViewModel model)
        {
            if (!Service.IsAuthenticated())
                return RedirectToAction("SignIn")
                    .Warning("Please Sign In");

            if (model == null || model.ServiceName == null)
            {
                IConfigurationService conf = Store.GetConfiguration();
                model = new DefectCredentialsViewModel(conf.Defect);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateDefectSettings(DefectCredentialsViewModel model)
        {
            ActionResult result = View("Defect", model);

            return HandleResult("Defect", model, () =>
            {
                if (Store.Store(model))
                {
                    return result
                        .Success("Defect data successfully saved.");
                }
                else
                {
                    return result
                        .Error("Defect data can't be saved.");
                }
            });
        }

        [HttpGet]
        public ActionResult Mail(MailCredentialsViewModel model)
        {
            if (!Service.IsAuthenticated())
                return RedirectToAction("SignIn")
                    .Warning("Please Sign In");

            if (model == null || model.ServiceName == null)
            {
                IConfigurationService conf = Store.GetConfiguration();
                model = new MailCredentialsViewModel(conf.Mail);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateMailSettings(MailCredentialsViewModel model)
        {
            ActionResult result = View("Mail", model);

            return HandleResult("Mail", model, () =>
            {
                if (Store.Store(model))
                {
                    return result
                        .Success("Mail data successfully saved.");
                }
                else
                {
                    return result
                        .Error("Mail data can't be saved.");
                }

            });

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

        public JsonResult GetConfigs()
        {
            JsonResult result = new JsonResult();
            try
            {
                result.Data = Store.GetDataToExport();
            }
            catch (Exception e)
            {
                result.Data = Store.GetErrorDataToExport(e);
            }

            return result;

        }

        static string configFileDownloadFileName = "settings.json";

        public async Task<ActionResult> ImportSettings(HttpPostedFileBase file)
        {
            ActionResult result = View("Credentials");
            try
            {
                if (file == null)
                {
                    return result.Warning("Error on importing configs");
                }

                await Store.ImportSettings(file.InputStream);

                return result
                    .Success("Configs imported successfully");
            }
            catch (Exception e)
            {
                return result
                    .Error("Error on import settings: " + e.Message);
            }
        }

        public FileContentResult Export()
        {
            try
            {
                JsonResult jsonresult = GetConfigs();
                return File(Store.GetDownloadableTextData(jsonresult.Data), MediaTypeNames.Text.Plain, configFileDownloadFileName);
            }
            catch (Exception)
            {
                return new FileContentResult(new byte[] { }, MediaTypeNames.Text.Html);
            }
        }


        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Service.Logoff();
            return RedirectToAction("Index", "Home");
        }
    }
}