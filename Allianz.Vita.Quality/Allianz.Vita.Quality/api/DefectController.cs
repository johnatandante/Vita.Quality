using Allianz.Vita.Quality.api.Response;
using Allianz.Vita.Quality.Attributes;
using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using Allianz.Vita.Quality.Extensions;
using Allianz.Vita.Quality.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

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

            return this.HandleGetRequest<DefectResponse, DefectViewModel[]>(() =>
            {
                List<IDefect> defects = Service.GetAllDefects();
                return defects.ConvertAll(item => new DefectViewModel(item)).ToArray();
            });

        }

        // GET api/<controller>/<value>
        public DefectResponse Get(string id)
        {
            return this.HandleGetRequest<DefectResponse, DefectViewModel[]>(() =>
            {
                IDefect defect = Service.Get(id);
                return new DefectViewModel[] { new DefectViewModel(defect) };
            });
        }

        [HttpGet]
        public DefectResponse GetMyDefects()
        {
            return this.HandleGetRequest<DefectResponse, DefectViewModel[]>(() =>
            {
                List<IDefect> defects = Service.GetAllDefects();
                return defects.ConvertAll(item => new DefectViewModel(item) ).ToArray();
            });
        }
        
        [HttpGet]
        public DefectResponse GetByTitle(string title)
        {
            return this.HandleGetRequest<DefectResponse, DefectViewModel[]>(() =>
            {
                IDefect defect = Service.LookFor(title);
                return new DefectViewModel[] { new DefectViewModel(defect) };
            });
        }

        [HttpGet]
        ArryayResponse GetAllowedValues(string key)
        {
            return this.HandleGetRequest<ArryayResponse, object[]>( () => {
                return Service.GetAllowedValues(key);
            }) ;
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
            return this.HandlePutRequest(() =>
            {
                Service.Autoassign(id);
                return true;
            });
        }

        [HttpPut]
        public SimpleResponse MoveStateOn(string id, [FromBody]string value)
        {
            return this.HandlePutRequest(() =>
            {
                Service.MoveStateOn(Service.Get(id));
                return true;
            });
        }

        [HttpPut]
        public SimpleResponse NotifyReopened(DefectViewModel model)
        {
            return this.HandlePutRequest(() =>
            {
                string result = Service.NotifyReopened(model);
                return result.Equals(model.DefectID);
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