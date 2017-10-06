﻿using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Allianz.Vita.Quality.Controllers
{
    public class ConvertController : Controller
    {
        // GET: Convert
        public ActionResult Index()
        {
            IMailService Mail = ServiceFactory.Get<IMailService>();

            // ViewBag.Message = "Convert Request to Defects";

            ConvertViewModel model = new ConvertViewModel();

            try
            {
                
                IFolderItem publicFolder = Mail.OpenFolder("Prisma Life.Quality Management.IssueVita", pageSize: 100, from: "SRM");

                model.PublicFolderDisplayName = publicFolder.DisplayName;

                model.PublicFolderMessages = publicFolder.Messages;

            }
            catch (Exception e)
            {
                ViewBag.Message = "Error in call " + e.Message;
                model.PublicFolderMessages = ServiceFactory.Get<IItemFactory>().GetNewMailItemList();
            }

            return View(model);

        }

    }
}