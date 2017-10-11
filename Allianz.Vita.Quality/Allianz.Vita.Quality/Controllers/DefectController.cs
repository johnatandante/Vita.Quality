using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Models;
using Allianz.Vita.Quality.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Allianz.Vita.Quality.Controllers
{
    public class DefectController : Controller
    {
        
        IDefectService Service
        {
            get { return ServiceFactory.Get<IDefectService>(); }
        }

        IMailService Mail
        {
            get { return ServiceFactory.Get<IMailService>(); }
        }

        // GET: Defect
        public ActionResult Index() {

            List<IDefect> defects = Service.GetAllDefects();
            DefectViewModel[] collection = defects.Select(idefect => new DefectViewModel(idefect)).ToArray();

            return View(collection);
		}

        public ActionResult Detail(string id)
        {
            
            IDefect defect = Service.Get(id);

            return View(new DefectViewModel(defect));
        }

        [HttpGet]
        public ActionResult Autoassign(string id)
        {
            
            Service.Autoassign(id);

            return RedirectToAction("Detail", id );
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(DefectViewModel model) {

			if (!ModelState.IsValid) 
			{
				return View(model);
			}
            
            return RedirectToAction("Detail", Service.Save(model));
            
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(MailItem model) {

			if (!ModelState.IsValid) {
				return View(model);
			}

			IMailItem itemRead = Mail.Get(model);
			IDefect defect = ServiceFactory.Get<IItemFactory>().GetNewDefect(itemRead);

            Mail.Flag(itemRead);
			
			return View(new DefectViewModel(defect));
		}

    }
}