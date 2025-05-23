namespace LogisticsSolution.Application.Constant
{
    public class ApiResponse
    {
        public bool ResponseStatus { get; set; }
        public string ResponseMessage { get; set; }

    }

    public class ResponseModel<T> : ApiResponse
    {
        public T Result { get; set; }
    }
}
