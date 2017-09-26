using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Models;
using Allianz.Vita.Quality.Business.Services;
using Allianz.Vita.Quality.Models;
using System.Web.Mvc;

namespace Allianz.Vita.Quality.Controllers
{
    public class DefectController : Controller
    {
        // GET: Defect
		public ActionResult Index() {

            var model = new DefectModel[] { };
            
            return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(DefectModel model) {

			if (!ModelState.IsValid) 
			{
				return View(model);
			}

			return Redirect("Index");
            
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(MailItem model) {

			if (!ModelState.IsValid) {
				return View(model);
			}

			IMailItem itemRead = ServiceFactory.Get<IMailService>().Get(model);
			IDefect defect = ServiceFactory.Get<IItemFactory>().GetNewDefect(itemRead);
			
			return View(new DefectModel(defect));
		}

    }
}