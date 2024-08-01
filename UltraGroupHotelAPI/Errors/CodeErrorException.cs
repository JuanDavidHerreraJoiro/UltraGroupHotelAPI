namespace UltraGroupHotelAPI.API.Errors
{
    public class CodeErrorException : CodeErrorResponse
    {
        public string? Details { get; set; }
        public CodeErrorException(int statusCode, string? message = null, string? datails = null) : base(statusCode, message)
        {
            Details = datails;
        }
    }
}
