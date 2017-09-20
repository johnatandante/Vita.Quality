using System.Web.Mvc;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Models;
using Allianz.Vita.Quality.Business.Services;
using Allianz.Vita.Quality.Models;

namespace Allianz.Vita.Quality.Controllers
{
    public class ManageFolderController : Controller
    {
        // GET: ManageFolder
        public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult OpenFolder(FolderItem model) {

			if (!ModelState.IsValid) {
				return View(model);
			}
			
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Convert(MailItem model) {

			if (!ModelState.IsValid) {
				return View(model);
			}

			IMailItem itemRead = MailService.Instance.Get(model);
			IDefect defect = ItemFactory.Instance.GetNewDefect(itemRead);

			MailItemToDefectViewModel newModel = new MailItemToDefectViewModel() {
				Defect = defect
			};
			
			return View(newModel);
		}

    }
}