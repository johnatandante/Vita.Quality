using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allianz.Vita.Quality.api.Response
{
    public class SimpleResponse : BaseResponse<string>
    {
        public sealed override string Result { get; set; }
    }

    public class ArryayResponse : BaseResponse<object[]>
    {
        public sealed override object[] Result { get; set; }
    }

}