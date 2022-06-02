using ComicBookStoreAPI.Domain.Exceptions;

namespace ComicBookStoreAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (ForbiddenException ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 403;

                if (string.IsNullOrEmpty(ex.Message))
                {
                    await context.Response.WriteAsync("Access forbidden");
                }
                else
                {
                    await context.Response.WriteAsync(ex.Message);
                }
            }
            catch (AccountException ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;

                if (string.IsNullOrEmpty(ex.Message))
                {
                    await context.Response.WriteAsync("A problem occurred while applying changes to the account");
                }
                else
                {
                    await context.Response.WriteAsync(ex.Message);
                }
            }
            catch (ForbidenException ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;

                if (string.IsNullOrEmpty(ex.Message))
                {
                    await context.Response.WriteAsync("A problem occurred while applying changes to the database");
                }
                else
                {
                    await context.Response.WriteAsync(ex.Message);
                }
            }
            catch (InvalidExternalAuthenticationException ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;

                if (string.IsNullOrEmpty(ex.Message))
                {
                    await context.Response.WriteAsync("External authentication is invalid");
                }
                else
                {
                    await context.Response.WriteAsync(ex.Message);
                }
            }
            catch (NotFoundException notFoundException)
            {
                _logger.LogError(notFoundException, notFoundException.Message);

                context.Response.StatusCode = 404;

                if (string.IsNullOrEmpty(notFoundException.Message))
                {
                    await context.Response.WriteAsync("Resource could not be found");
                }
                else
                {
                    await context.Response.WriteAsync(notFoundException.Message);
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Ups something went wrong");
            }
        }
    }
}
