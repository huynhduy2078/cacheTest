using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace TestConnectDB.Database.Redis
{
    public class RedisCache
    {
       public static async Task SetObjectAsync<T>(IDistributedCache cache, string  key,  T value)
        {
            await cache.SetStringAsync(key, JsonConvert.SerializeObject(value));
        }

        public static async Task<T> GetObjectAsync<T>(IDistributedCache cache, string key)
        {

            var value = await cache.GetStringAsync(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static async Task RemoveObjectAsync<T>(IDistributedCache cache, string key)
        {
            await cache.RemoveAsync(key);
        }

        //verify if an object exists
        public static async Task<bool> ExistObjetcAsync<T>(IDistributedCache cache, string key)
        {
            var value = await cache.GetStringAsync(key);
            return value == null ? false : true;
        }
    }
}
