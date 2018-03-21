using Allianz.Vita.Quality.Attributes;
using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using Allianz.Vita.Quality.Extensions;
using Allianz.Vita.Quality.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;

namespace Allianz.Vita.Quality.Controllers
{

    [AuthorizedOnly(typeof(IIssueService))]
    public class IssueController : Controller
    {
        IIssueService Service
        {
            get { return ServiceFactory.Get<IIssueService>(); }
        }

        IIdentityService Auth
        {
            get
            {
                return ServiceFactory.Get<IIdentityService>();
            }
        }

        public async Task<ActionResult> Index()
        {

            List<IssueViewModel> issues = new List<IssueViewModel>();

            if(await Service.IsUp())
            {
                IIssueItem[] list = (await Service.GetAll()).ToArray();
                foreach(IIssueItem item in list)
                {
                    issues.Add(new IssueViewModel(item));
                }

                return View(issues)
                    .Success("Jira is Up: user " + User.Identity.Name 
                        + " can authenticate with credentials: " + Auth.GetCredentialsFor(Service).UserName);
            }
            else
            {
                return View(issues)
                    .Error("Jira Ko: user " + User.Identity.Name 
                        + " cannot authenticate with credentials: " + Auth.GetCredentialsFor(Service).UserName);
            }
            
        }
        
        public async Task<ActionResult> Detail(string id)
        {

            IIssueItem issue = await Service.Get(id);

            return View(new IssueViewModel(issue));
        }


    }
}