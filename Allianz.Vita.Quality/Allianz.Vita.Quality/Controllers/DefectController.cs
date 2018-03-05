using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Models;
using Allianz.Vita.Quality.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Allianz.Vita.Quality.Extensions;
using Allianz.Vita.Quality.Attributes;

namespace Allianz.Vita.Quality.Controllers
{
    [AuthorizedOnly(typeof(IMailService), typeof(IDefectService))]
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

        IIdentityService Auth
        {
            get
            {
                return ServiceFactory.Get<IIdentityService>();
            }
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

            return RedirectToAction("Detail", "Defect", new { Id = id } ).Success("Assigned To Me: Done");

        }

        [HttpGet]
        public ActionResult Reply(string id)
        {
            IDefect defect = Service.Get(id);

            Service.MoveStateOn(defect);

            return View(new DefectViewModel(defect));
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(DefectViewModel model) {

			if (!ModelState.IsValid) 
			{
				return RedirectToAction("Create", new { id = model.Id, mailId = model.IMailItemUniqueId })
                    .Error("Erron in Save");
			}
            
            return RedirectToAction("Detail", "Defect", new { Id = Service.Save(model) })
                .Success("Save Ok");
            
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveNotify(DefectViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("Detail", "Defect", new { Id = Service.SaveNotify(model) })
                .Success("Notified Defect " + model.Id +( (string.IsNullOrEmpty(model.AssignedTo)) ? "" : " to " + model.AssignedTo));

        }

        [HttpGet]
        public ActionResult Create(string id, string mailId)
        {

            IMailItem model = Mail.Get(ServiceFactory.Get<IItemFactory>().GetNewMailItem(HttpUtility.UrlDecode(mailId)));
            IDefect defect = Service.LookFor(model);

            if (defect == null)
            {
                IMailItem itemRead = Mail.Get(model);
                Mail.Flag(itemRead);
                defect = ServiceFactory.Get<IItemFactory>().GetNewDefect(itemRead);
                
                return View(new DefectViewModel(defect));
            }
            else
            {
                return RedirectToAction("Notify", "Defect", new { id = defect.Id, mailId = model.UniqueId });
            }

        }

        [HttpGet]
        public ActionResult Notify(string id, string mailId)
        {
            IDefect defect = Service.Get(id);
            
            IMailItem itemRead = Mail.Get(ServiceFactory.Get<IItemFactory>().GetNewMailItem(mailId));
            Mail.Flag(itemRead);
            ServiceFactory.Get<IItemFactory>().MergeTo(itemRead, defect);
            
            return View(new DefectViewModel(defect));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Archive(MailItem model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            IMailItem itemRead = Mail.Get(model);
            Mail.Complete(itemRead);

            return RedirectToAction("Index", "Convert")
                .Success("Mail archivied: " + itemRead.Subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ArchiveDefect( DefectViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            IMailItem itemRead = Mail.Get(ServiceFactory.Get<IItemFactory>().GetNewMailItem(model.IMailItemUniqueId));
            Mail.Complete(itemRead);

            return RedirectToAction("Index", "Convert");
        }


    }
}