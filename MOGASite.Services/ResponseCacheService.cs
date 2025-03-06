using MOGASite.Core.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MOGASite.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDatabase _database; // interface to interact with the Redis database
        public ResponseCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
            // get the database instance from the Redis connection
        }

        public async Task CacheResponseAsync(string key, object response, TimeSpan timeToLive)
        {
            if (response == null) return;

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var serializedResponse = JsonSerializer.Serialize(response, serializeOptions);

            await _database.StringSetAsync(key, serializedResponse, timeToLive);
        }

        public async Task<string?> GetCachedResponseAsync(string key)
        {
            var response =await _database.StringGetAsync(key);

            if(response.IsNullOrEmpty)
                return null;

            return response;
        }
    }
}
