namespace OpenAPI.Modesl.Responses
{
    public class ApiResponse<T>
    {
        public bool status { get; set; }
        public string? message { get; set; }
        public T? data { get; set; }
        public ApiResponse()
        {
            status = true;
        }
        public ApiResponse(T data, string message)
        {
            status = true;
            this.data = data;
            this.message = message;
        }
        public ApiResponse(bool status, string message)
        {
            this.status = status;
            this.message = message;
        }
    }
    public class LoginResponse
    {
        public string userName { get; set; }
        public string token { get; set; }
        public bool status { get; set; }
        public string message { get; set; }

    }
}
