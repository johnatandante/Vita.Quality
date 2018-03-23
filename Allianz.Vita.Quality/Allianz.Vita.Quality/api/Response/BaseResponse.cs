namespace Allianz.Vita.Quality.api.Response
{
    public abstract class BaseResponse<T>
    {

        public string ErrorMessage { get; set; }

        public bool Succeded { get; set; }

        public abstract T Result { get; set; }

    }
}