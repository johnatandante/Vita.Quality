using Allianz.Vita.Quality.Attributes;
using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Extensions;
using Allianz.Vita.Quality.Models;
using System;
using System.Web.Mvc;

namespace Allianz.Vita.Quality.Controllers
{
    [AuthorizedOnly(typeof(IMailService))]
    public class ConvertController : Controller
    {
        // GET: Convert
        public ActionResult Index()
        {
            IMailService Mail = ServiceFactory.Get<IMailService>();
            
            ConvertViewModel model = new ConvertViewModel();

            try
            {
                IConfigurationService conf = ServiceFactory.Get<IConfigurationService>();

                if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

                IFolderItem publicFolder = Mail.OpenFolder(conf.IssueFolderPath , pageSize: 100, from: conf.DefaultSender);

                model.PublicFolderDisplayName = publicFolder.DisplayName;

                model.PublicFolderMessages = publicFolder.Messages;

                return View(model).Information("Data loaded...");

            }
            catch (Exception e)
            {
                // ViewBag.Message = "Error in call " + e.Message;
                model.PublicFolderMessages = ServiceFactory.Get<IItemFactory>().GetNewMailItemList();
                
                return View(model).Error("Error in call " + e.Message);
            }
            
        }

    }
}