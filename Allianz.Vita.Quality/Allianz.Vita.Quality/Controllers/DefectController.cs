using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Allianz.Vita.Quality.Business.Models;
using Allianz.Vita.Quality.Models;

namespace Allianz.Vita.Quality.Controllers
{
    public class DefectController : Controller
    {
        // GET: Defect
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Create(DefectModel model) {

			if (!ModelState.IsValid) 
			{
				return View(model);
			}

			return View(model);

		}

    }
}