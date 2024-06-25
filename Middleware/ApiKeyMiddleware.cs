namespace ProcessUploadedFile.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<string> _apiKeys;

        public ApiKeyMiddleware(RequestDelegate next, List<string> apiKeys)
        {
            _next = next;
            _apiKeys = apiKeys;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("X-API-KEY", out var extractedApiKey) || !_apiKeys.Contains(extractedApiKey))
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Invalid API Key.");
                return;
            }

            await _next(context);
        }
    }
}
