using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Fake.Services;
using System.Linq;
using System.Collections.Generic;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;

namespace Allianz.Vita.Quality.Business.Test
{
    [TestClass]
    public class MailServices
    {
        IMailService service;

        [TestInitialize]
        public void Init()
        {
            ServiceFactory.Register<IItemFactory, ItemFactory>();
            service = ServiceFactory.Register<IMailService, MailServiceFake>();

            Assert.IsInstanceOfType(service, typeof(IMailService));
            Assert.IsInstanceOfType(service, typeof(MailServiceFake));

        }

        [TestMethod]
        public void FromAGivenItemFactoryAndMailService_OpenInboxReturnsANonEmptyCollection()
        {

            var collection = service.OpenInbox();

            Assert.IsNotNull(collection);

            Assert.IsTrue(collection.Any());
            Assert.IsInstanceOfType(collection.First(), typeof(IMailItem));

        }

        [TestMethod]
        public void FromAGivenItemFactoryAndMailService_OpenInboxReturnsAIMailItemCollection()
        {

            var collection = service.OpenInbox();

            Assert.IsNotNull(collection);
            Assert.IsInstanceOfType(collection, typeof(IEnumerable<IMailItem>));

        }

    }
}
