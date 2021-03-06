﻿using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using Allianz.Vita.Quality.Business.Services.Authentication;
using Allianz.Vita.Quality.Business.Services.Storage;
using Allianz.Vita.Quality.Services;
using Owin;
#if FAKEENV
using Allianz.Vita.Quality.Business.Fake.Services;
#else
using Allianz.Vita.Quality.Business.Services.Defect;
using Allianz.Vita.Quality.Business.Services.Issues;
using Allianz.Vita.Quality.Business.Services.Mail;
#endif

namespace Allianz.Vita.Quality
{
    public partial class Startup
    {
        public void ConfigureServices(IAppBuilder app)
        {
            ServiceFactory.Register<IConfigurationService, ConfigurationService>();
            ServiceFactory.Register<IItemFactory, ItemFactory>();
            ServiceFactory.Register<IIdentityService, IdentityService>();
            ServiceFactory.Register<IStorageService, StorageService>();

            ServiceFactory.Register<CookieAuthenticationService, CookieAuthenticationService>();

#if FAKEENV
            ServiceFactory.Register<IMailService, MailServiceFake>();
            ServiceFactory.Register<IDefectService, DefectServiceFake>();
            ServiceFactory.Register<IDefectService, IssueServiceFake>();
#else
            ServiceFactory.Register<IMailService, ExchangeMailService>();
            ServiceFactory.Register<IDefectService, TfsDefectService>();
            ServiceFactory.Register<IIssueService, JiraIssueService>();
#endif

        }
    }
}