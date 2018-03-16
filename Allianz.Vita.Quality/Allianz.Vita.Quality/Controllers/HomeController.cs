using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using Allianz.Vita.Quality.Extensions;
using Allianz.Vita.Quality.Models;
using Allianz.Vita.Quality.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Allianz.Vita.Quality.Controllers
{
    public class HomeController : Controller
	{        
        
        IMailService Mail {
			get {
				return ServiceFactory.Get<IMailService>();
			}
		}

        IIdentityService Auth
        {
            get
            {
                return ServiceFactory.Get<IIdentityService>();
            }
        }

        CookieAuthenticationService CookieService
        {
            get
            {
                return ServiceFactory.Get<CookieAuthenticationService>();
            }
        }

        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            if (User.Identity.IsAuthenticated)
            {
                CookieService.EnsureAuthentication(Request, User.Identity.Name);
                
            }

            base.OnAuthentication(filterContext);
        }

        public ActionResult Index()
        {

            HomeViewModel model = new HomeViewModel();

            List<string> inbox = new List<string>();
            List<string> issues = new List<string>();

            Queue<string> errors = new Queue<string>();

            try
            {
                //if (User.Identity.IsAuthenticated)
                if(Auth.IsAuthenticatedOn(Mail.GetType()))
                    inbox.AddRange( Mail.OpenInbox(pageSize: 10, read: false)
                        .Select(mail => string.Join(" ", mail.Subject, "from", mail.From)));
                
            }
            catch (Exception e)
            {
                errors.Enqueue("Failed to retrieve Inbox messages: " + e.Message);
            }

            try
            {
                if (Auth.IsAuthenticatedOn(Mail.GetType()))
                {
                    IFolderItem publicFolder = Mail.OpenFolder("Prisma Life.Quality Management.IssueVita", pageSize: 20, from: "SRM");
                    model.PublicFolderDisplayName = publicFolder.DisplayName;

                    issues.AddRange( publicFolder.Messages.Select(item => item.Subject));

                }
            }
            catch (Exception e)
            {
                errors.Enqueue("Failed to retrieve Issue Vita messages: " + e.Message);
            }

            model.InboxMessages = inbox.ToArray();
            model.PublicFolderMessages = issues.ToArray();

            ActionResult result = View(model); 
            while(errors.Count > 0)
            {
                result = result.Warning(errors.Dequeue());
            }

            return View(model);
		}

		public ActionResult About() {
            ViewBag.TfsProjectUrl = ServiceFactory.Get<IConfigurationService>().Defect.TrackingSystemUrl;
            
            return View();
		}

		public ActionResult Contact() {

			return View();
		}
        
	}
}