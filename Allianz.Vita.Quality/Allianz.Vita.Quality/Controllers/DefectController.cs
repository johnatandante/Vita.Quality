using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Models;
using Allianz.Vita.Quality.Business.Services;
using Allianz.Vita.Quality.Models;

namespace Allianz.Vita.Quality.Controllers
{
    public class DefectController : Controller
    {
        // GET: Defect
		public ActionResult Index() {
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(DefectModel model) {

			if (!ModelState.IsValid) 
			{
				return View(model);
			}

			return Redirect("Index");

			// return View(model);

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(MailItem model) {

			if (!ModelState.IsValid) {
				return View(model);
			}

			IMailItem itemRead = MailService.Instance.Get(model);
			IDefect defect = ItemFactory.Instance.GetNewDefect(itemRead);
			
			return View(new DefectModel(defect));
		}

    }
}