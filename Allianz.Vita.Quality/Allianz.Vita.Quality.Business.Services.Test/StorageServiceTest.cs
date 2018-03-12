using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Fake.Services;
using Allianz.Vita.Quality.Business.Interfaces.Service;

namespace Allianz.Vita.Quality.Test.Services
{
    [TestClass]
    public class StorageServiceTest
    {
        IStorageService service;

        [TestInitialize]
        public void Init()
        {
            service = ServiceFactory.Register<IStorageService, StorageServiceFake>();

            Assert.IsInstanceOfType(service, typeof(IStorageService));
            Assert.IsInstanceOfType(service, typeof(StorageServiceFake));

        }

        [TestMethod]
        public void StoreSomething()
        {
            Assert.Fail("Not Implemented");

        }
    }
}
