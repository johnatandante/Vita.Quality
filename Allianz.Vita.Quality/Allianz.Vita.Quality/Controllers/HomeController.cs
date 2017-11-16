using System;
using System.Linq;
using System.Web.Mvc;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Models;
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

		public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            
            // ConnectionMessages.Add("Mail server version: " + Mail.Version.ToString());

            try
            {
                model.InboxMessages = Mail.OpenInbox(pageSize: 10, read: false)
                    .Select(mail => string.Join(" ", mail.Subject, "from", mail.From)).ToArray();
                
            }
            catch (Exception)
            {
                //ConnectionMessages.Add("Failed to retrieve Inbox messages: " + e.Message);
                model.InboxMessages = new string[] { };

            }

            try
            {
                IFolderItem publicFolder = Mail.OpenFolder("Prisma Life.Quality Management.IssueVita", pageSize: 20, from: "SRM");
                model.PublicFolderDisplayName = publicFolder.DisplayName;
                model.PublicFolderMessages = publicFolder.Messages.Select( item => item.Subject).ToArray();
            }
            catch (Exception)
            {
                //ConnectionMessages.Add("Failed to retrieve Inbox messages: " + e.Message);
                model.PublicFolderMessages = new string[] { };
            }

            //model.ConnectionMessages = ConnectionMessages;

            return View(model);
		}

		public ActionResult About() {
           //  ViewBag.Message = "About this application";
            ViewBag.TfsProjectUrl = ServiceFactory.Get<IConfigurationService>().TrackingSystemUrl;
            
            return View();
		}

		public ActionResult Contact() {
			// ViewBag.Message = "My contact page.";

			return View();
		}
        
	}
}