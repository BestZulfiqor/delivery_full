using System.Net;

namespace Core.Responses;

public class Response<T>
{
    public bool IsSucced { get; set; }
    public int StatusCode { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }

    public Response(T? data)
    {
        IsSucced = true;
        Data = data;
        StatusCode = 200;
        Message = null;
    }

    public Response(HttpStatusCode code, string message)
    {
        IsSucced = false;
        Data = default;
        StatusCode = (int)code;
        Message = message;
    }
}