using Microsoft.Extensions.DependencyInjection;

namespace PP_BC_C_H_2.Extensions
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddCustomExtensions(this IServiceCollection services)
        {
            // Add custom services or configurations here
            return services;
        }
    }
}
