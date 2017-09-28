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
            
            Assert.IsInstanceOfType(service, typeof(IDefectService));
            Assert.IsInstanceOfType(service, typeof(DefectService));

        }
        
        [TestMethod]
        public void GetMyTasks()
        {
            
            List<IDefect> collection = service.GetMyTasks();
            
            Assert.IsNotNull(collection);

            CollectionAssert.AllItemsAreInstancesOfType(collection, typeof(IDefect));

        }

        [TestMethod]
        public void GetAllDefects()
        {

            var collection = service.GetAllDefects();

            Assert.IsNotNull(collection);

            CollectionAssert.AllItemsAreInstancesOfType(collection, typeof(IDefect));

        }

        [TestMethod]
        public void InsertDefect()
        {
            IMailItem mailItem = ServiceFactory.Get<IItemFactory>().GetNewMailItem();
            IDefect defect = ServiceFactory.Get<IItemFactory>().GetNewDefect(mailItem);
            service.Save(defect);

            var collection = service.GetAllDefects();

            Assert.IsNotNull(collection);
            CollectionAssert.AllItemsAreInstancesOfType(collection, typeof(IDefect));
            Assert.IsTrue(collection.Any( item => item.DefectID.Equals(defect.DefectID)));

        }

    }
}
