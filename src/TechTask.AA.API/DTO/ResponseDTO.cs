using System.Net;

namespace TechTask.AA.API.DTO
{
    /// <summary>
    /// DTO for handling HTTP errors
    /// </summary>
    public class ResponseDTO
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public string? StackTrace { get; set; }
    };
}
