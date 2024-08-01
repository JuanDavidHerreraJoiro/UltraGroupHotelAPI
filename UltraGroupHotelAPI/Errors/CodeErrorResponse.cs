namespace UltraGroupHotelAPI.API.Errors
{
    public class CodeErrorResponse
    {
        
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public CodeErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message??GetDafaultMessageStatusCode(statusCode);
        }

        private string GetDafaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "El request enviado tiene errores",
                401 => "No tiene authorization para este recurso",
                404 => "No se encontro el recurso solicitado",
                500 => "Se produjeron errores en el servidor",
                _ => string.Empty
            };
        }

    }
}
