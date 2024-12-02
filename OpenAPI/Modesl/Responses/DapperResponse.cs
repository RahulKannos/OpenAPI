namespace OpenAPI.Modesl.Responses
{
    public class DapperResponse<T>
    {
        public T? data { get; set; }
        public string? message { get; set; }
    }
}
