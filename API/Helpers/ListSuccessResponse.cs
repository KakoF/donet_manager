using System.Collections.Generic;

namespace API.Helpers
{
    public class ListSuccessResponse<T> : Response
    {
        public IEnumerable<T> Data { get; }
        public ListSuccessResponse(int statusCode, IEnumerable<T> data, string message = null) : base(statusCode, message)
        {
            Data = data;
        }

    }
}