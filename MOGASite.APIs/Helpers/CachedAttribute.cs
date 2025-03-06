using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MOGASite.Core.Services;
using System.Text;

namespace MOGASite.APIs.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSeconds;

        public CachedAttribute(int timeToLiveInSeconds)
        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var responseCacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            // Ask CLR to creating an instance of IResponseCacheService Explicitly 

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var response = await responseCacheService.GetCachedResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(response))
            {
                var result = new ContentResult
                {
                    Content = response,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = result;

                return;
            }

            var actionExecutedContext = await next.Invoke(); // Will Execute the next Action Filter or the Action Method itself

            if (actionExecutedContext.Result is OkObjectResult okObjectResult && okObjectResult.Value is not null)
            {
                await responseCacheService.CacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveInSeconds));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append(request.Path); // api/AllBlogs

            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");

                // api/AllBlogs|pageIndex-1
                // api/AllBlogs|pageIndex-1|pageSize-10
                // api/AllBlogs|pageIndex-1|pageSize-10|category-software
            }

            return keyBuilder.ToString();
        }
    }
}
