namespace AdventureWorks.Http.Filters.UrlMiddleware
{
    public class LowerCaseUrlMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public LowerCaseUrlMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;

            if (request.Path.HasValue && request.Path.Value.Any(char.IsUpper)) 
            {
                var lowerCasePath = request.Path.Value.ToLowerInvariant();
                context.Response.Redirect(lowerCasePath + request.QueryString, true);
                return;
            }

            await _requestDelegate(context);
        }
    }
}
