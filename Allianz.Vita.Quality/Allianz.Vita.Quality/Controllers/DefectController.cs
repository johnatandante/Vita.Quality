using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Models;
using Allianz.Vita.Quality.Business.Services;
using Allianz.Vita.Quality.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace Allianz.Vita.Quality.Controllers
{
    public class DefectController : Controller
    {
        
        // GET: Defect
        public ActionResult Index() {

            List<IDefect> defects = ServiceFactory.Get<IDefectService>().GetAllDefects();
            DefectViewModel[] collection = defects.Select(idefect => new DefectViewModel(idefect)).ToArray();

            return View(collection);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(DefectViewModel model) {

			if (!ModelState.IsValid) 
			{
				return View(model);
			}

            ServiceFactory.Get<IDefectService>().Save(model);

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
			
			return View(new DefectViewModel(defect));
		}

    }
}