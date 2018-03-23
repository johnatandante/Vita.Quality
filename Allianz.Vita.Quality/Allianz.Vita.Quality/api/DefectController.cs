using Allianz.Vita.Quality.api.Response;
using Allianz.Vita.Quality.Attributes;
using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using Allianz.Vita.Quality.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Allianz.Vita.Quality.Extensions;

namespace Allianz.Vita.Quality.api
{
    [ApiAuthorizedOnly(typeof(IDefectService))]
    public class DefectController : ApiController
    {
        IDefectService Service
        {
            get { return ServiceFactory.Get<IDefectService>(); }
        }



        // GET api/<controller>
        public DefectResponse GetAll()
        {

            return this.HandleGetRequest( () => {
                List<IDefect> defects = Service.GetAllDefects();
                return defects.ConvertAll(item => new DefectViewModel(item)).ToArray();
            });

        }

        // GET api/<controller>/<value>
        public DefectResponse Get(string id)
        {
            return this.HandleGetRequest(() => {
                IDefect defect = Service.Get(id);
                return new DefectViewModel[] { new DefectViewModel(defect)};
            });
        }

        // PUT api/<controller>/<value>
        //[HttpPut]
        public void Put(int id, [FromBody]string value)
        {
            // generic
            throw new NotImplementedException();
        }

        [HttpPut]
        public SimpleResponse Autoassign(string id, [FromBody]string value)
        {
            return this.HandlePutRequest(() => {
                Service.Autoassign(id);
                return true;
            });
        }

        [HttpPut]
        public SimpleResponse MoveStateOn(string id, [FromBody]string value)
        {
            return this.HandlePutRequest(() => {
                Service.MoveStateOn(Service.Get(id));
                return true;
            });
        }

        // POST api/<controller>/<value>
        //[HttpPost]
        public void Post([FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<controller>/<value>
        //[HttpDelete]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}