using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Services;
using System.Collections.Generic;

namespace Allianz.Vita.Quality.Test.Services
{
    [TestClass]
    public class DefectServiceTest
    {
        IDefectService service;

        [TestInitialize]
        public void Init()
        {

            IItemFactory itemFactory = ServiceFactory.Register<IItemFactory, ItemFactory>();

            service = ServiceFactory.Register<IDefectService, DefectService>(itemFactory);

        }


        [TestMethod]
        public void GetMyTasks()
        {

            Assert.IsInstanceOfType(service, typeof(IDefectService));
            Assert.IsInstanceOfType(service, typeof(DefectService));

            List<IDefect> collection = service.GetMyTasks();
            
            Assert.IsNotNull(collection);
            Assert.IsTrue(collection.Any());

        }

        [TestMethod]
        public void GetAllDefects()
        {
            Assert.IsInstanceOfType(service, typeof(IDefectService));
            Assert.IsInstanceOfType(service, typeof(DefectService));

            List<IDefect> collection = service.GetAllDefects();

            Assert.IsNotNull(collection);
            Assert.IsTrue(collection.Any());

        }


    }
}
