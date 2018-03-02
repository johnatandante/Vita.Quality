using System;
using System.Linq;
using System.Web.Mvc;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Models;
using Allianz.Vita.Quality.Extensions;
using Allianz.Vita.Quality.Business.Factory;
using System.Collections.Generic;

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
                if (User.Identity.IsAuthenticated)
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
            ViewBag.TfsProjectUrl = ServiceFactory.Get<IConfigurationService>().TrackingSystemUrl;
            
            return View();
		}

		public ActionResult Contact() {

			return View();
		}
        
	}
}