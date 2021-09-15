using FlowersHub.Data;
using FlowersHub.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FlowersHub.Services
{
    public static class DependencyConfigurationExtensions
    {
        public static IServiceCollection AddFlowersHubServiceProviders(this IServiceCollection services)
        {
            services.AddDbContext<FlowersHubContext>();
            services.AddScoped<IFlowerService, FlowerService>();
            return services;
        }
    }
}
