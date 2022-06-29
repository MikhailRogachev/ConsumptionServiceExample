using System.Net;
using System.Text.Json;

namespace ConsumptionService.Handlers
{
    /// <summary>
    /// This class spesifies an error handling.
    /// </summary>
    public class GlobalErrorHandler
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var result = JsonSerializer.Serialize(new { message = ex.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
