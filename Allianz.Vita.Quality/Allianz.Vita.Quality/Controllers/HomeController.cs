using System;
using System.Web.Mvc;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Services;
using Allianz.Vita.Quality.Models;

namespace Allianz.Vita.Quality.Controllers
{
	public class HomeController : Controller
	{

		IMailService Mail {
			get {
				return MailService.Instance;
			}
		}

		public ActionResult Index() {
			return View();
		}

		public ActionResult About() {
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact() {
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public ActionResult DoSomething() {
			ViewBag.Message = "Do Something.";
			
			DefectViewModel model = new DefectViewModel();

			try {
				
				model.InboxMessages = Mail.OpenInbox(pageSize: 20);
				
				model.PublicFolder = Mail.OpenFolder("Prisma Life.Quality Management.IssueVita", pageSize: 100);
				
				model.ConnectionMessage = "Connected to " + Mail.Version.ToString();

			} catch(Exception e) {
                
				model.ConnectionMessage = "Failed to retrieve messages: " + e.Message;
			}

			return View(model);
			 
		}

	}
}