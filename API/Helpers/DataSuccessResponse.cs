namespace API.Helpers
{
    public class DataSuccessResponse<T> : Response
    {
        public T Data { get; }
        public DataSuccessResponse(int statusCode, T data, string message = null) : base(statusCode, message)
        {
            Data = data;
        }
    }
}