using System;
using System.Web.Mvc;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Models;
using Allianz.Vita.Quality.Business.Factory;

namespace Allianz.Vita.Quality.Controllers
{
	public class HomeController : Controller
	{

		IMailService Mail {
			get {
				return ServiceFactory.Get<IMailService>();
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
			
			WorkspaceViewModel model = new WorkspaceViewModel();

			try {

                model.ConnectionMessage = "Connected to " + Mail.Version.ToString();

                model.InboxMessages = Mail.OpenInbox(pageSize: 10);

                IFolderItem publicFolder = Mail.OpenFolder("Prisma Life.Quality Management.IssueVita", pageSize: 100, from: "SRM");

                model.PublicFolderDisplayName = publicFolder.DisplayName;

                model.PublicFolderMessages = publicFolder.Messages;
				
			} catch(Exception e) {
                
				model.ConnectionMessage = "Failed to retrieve messages: " + e.Message;

                model.InboxMessages = ServiceFactory.Get<IItemFactory>().GetNewMailItemList();

                model.PublicFolderMessages = ServiceFactory.Get<IItemFactory>().GetNewMailItemList();
            }

			return View(model);
			 
		}

	}
}