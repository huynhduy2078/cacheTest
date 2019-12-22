using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace TestConnectDB.Database.Redis
{
    public static class Extensions
    {
        private static readonly string SectionName = "Redis";
        public static IServiceCollection Addredis(this IServiceCollection service)
        {
            IConfiguration configuration;

            using (var serviceProvider = service.BuildServiceProvider()) //Create serviceprovider
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            service.Configure<RedisOptions>(configuration.GetSection(SectionName)); // 


            //var option = configuration.getOptions<RedisOptions>(SectionName);

            service.AddDistributedRedisCache(o =>
            {
                o.Configuration = "redis_image:6379,abortConnect=False";
                o.InstanceName = "SampleInstance";

            });

            return service;

        }
    }
}
