using Allianz.Vita.Quality.api.Response;
using Allianz.Vita.Quality.Models;
using System;
using System.Web.Http;

namespace Allianz.Vita.Quality.Extensions
{
    public static class ApiControllerExtensionMethods
    {

        public static T HandleGetRequest<T, W>(this ApiController controller, Func<W> action) 
            where T : BaseResponse<W>
        {
            T response = Activator.CreateInstance<T>();
            string errorMessage = string.Empty;

            try
            {
                response.Result = action();

            }
            catch (Exception e)
            {

                response.ErrorMessage = e.Message;
            }

            response.Succeded = string.IsNullOrEmpty(response.ErrorMessage);

            return response;
        }

        public static SimpleResponse HandlePutRequest(this ApiController controller, Func<bool> action)
        {
            SimpleResponse response = new SimpleResponse();
            string errorMessage = string.Empty;

            try
            {
                response.Result = action() ? "Ok" : "Ko"; ;

            }
            catch (Exception e)
            {
                response.ErrorMessage = e.Message;
            }
            
            response.Succeded = string.IsNullOrEmpty(response.ErrorMessage);

            return response;
        }


    }
}