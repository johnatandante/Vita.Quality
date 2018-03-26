using Allianz.Vita.Quality.api.Response;
using Allianz.Vita.Quality.Attributes;
using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using Allianz.Vita.Quality.Extensions;
using Allianz.Vita.Quality.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Allianz.Vita.Quality.api
{

    [ApiAuthorizedOnly(typeof(IIssueService))]
    public class IssueController : ApiController
    {

        IIssueService Service
        {
            get { return ServiceFactory.Get<IIssueService>(); }
        }

        // GET api/<controller>
        public IssueResponse Get()
        {
            return this.HandleGetRequest<IssueResponse, IssueViewModel[]>( () =>
            {
                Task<IEnumerable<IIssueItem>> task = Service.GetAll();
                task.Wait();
                List<IIssueItem> items = task.Result.ToList();
                return items.ConvertAll(item => new IssueViewModel(item)).ToArray();
            });
        }

        // GET api/<controller>/5
        public IssueResponse Get(string id)
        {
            return this.HandleGetRequest<IssueResponse, IssueViewModel[]>(() =>
            {
                Task<IIssueItem> task = Service.Get(id);
                task.Wait();

                List<IIssueItem> items = new List<IIssueItem>(new IIssueItem[] { task.Result });
                return items.ConvertAll( item => new IssueViewModel(item)).ToArray();
            });

        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}