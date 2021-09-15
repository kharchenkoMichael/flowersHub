using FlowersHub.Data;
using Microsoft.Extensions.DependencyInjection;

namespace FlowersHub.Services
{
    public static class DependencyConfigurationExtensions
    {
        public static IServiceCollection AddFlowersHubServiceProviders(this IServiceCollection services)
        {
            services.AddDbContext<FlowersHubContext>();
            return services;
        }
    }
}
