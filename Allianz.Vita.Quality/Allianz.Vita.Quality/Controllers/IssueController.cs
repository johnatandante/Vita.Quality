using Allianz.Vita.Quality.Attributes;
using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using Allianz.Vita.Quality.Models;
using System.Web.Mvc;

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

        //// GET: Issue
        //public ActionResult Index()
        //{

        //    IIssueItem[] issues = ;

        //    return View(issues);

        //}

        public ActionResult Index(int page = 0)
        {

            IIssueItem[] issues = page > 1 ? Service.GetAllPaged( page ) : Service.GetAll();
            
            return View(issues);

        }


        public ActionResult Detail(string id)
        {

            IIssueItem issue = Service.Get(id);

            return View(new IssueViewModel(issue));
        }


    }
}