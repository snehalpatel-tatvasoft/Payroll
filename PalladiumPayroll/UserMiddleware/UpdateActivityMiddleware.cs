using PalladiumPayroll.Services.User;

namespace PalladiumPayroll.Helper.Middleware.Encryption
{
    public class UpdateActivityMiddleware
    {
        private readonly RequestDelegate _next;
        public UpdateActivityMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                string path = context.Request.Path.Value?.ToLower();

                string[] excludedPaths = new[] { "/auth/checkisuserloggedin" };

                if (excludedPaths.Any(p => path != null && path.Contains(p)))
                {
                    await _next(context);
                    return;
                }

                string? userId = context.User.FindFirst("user_id")?.Value;

                if (!string.IsNullOrWhiteSpace(userId))
                {
                    IUserService? userService = context.RequestServices.GetRequiredService<IUserService>();
                    await userService.UpdateLastActivity(userId);
                }
            }
            catch (Exception)
            {
                // Optional: log error, but don’t block request
            }

            await _next(context);
        }
    }
}
