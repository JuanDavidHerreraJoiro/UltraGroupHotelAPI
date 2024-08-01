using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraGroupHotelAPI.Application.Models.Identity
{
    public class Response
    {
        public int? StatusCode { get; set; }
        public string? Message { get; set; } = string.Empty;
        public object? Data { get; set; }
        public List<string>? Errors { get; set; }

        public Response(int statusCode, string message, object data = null, List<string> errors = null)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
            Errors = errors ?? new List<string>();
        }

        public Response(int statusCode, string message, List<string> errors)
        {
            StatusCode = statusCode;
            Message = message;
            Errors = errors;
        }

        public Response(object data)
        {
            StatusCode = 200;
            Message = "OK";
            Data = data;
        }
    }
}
