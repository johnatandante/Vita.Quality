using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Fake.Services;
using Allianz.Vita.Quality.Business.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Allianz.Vita.Quality.Test.Services
{
    [TestClass]
    public class DefectServiceTest
    {
        IDefectService service;

        [TestInitialize]
        public void Init()
        {
            IConfigurationService conf = ServiceFactory.Register<IConfigurationService, ConfigurationServiceFake>();
            IStorageService stor = ServiceFactory.Register<IStorageService, StorageServiceFake>();

            IItemFactory itemFactory = ServiceFactory.Register<IItemFactory, ItemFactory>(stor, conf);

            service = ServiceFactory.Register<IDefectService, DefectServiceFake>();
            
            Assert.IsInstanceOfType(service, typeof(IDefectService));
            Assert.IsInstanceOfType(service, typeof(DefectServiceFake));

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
