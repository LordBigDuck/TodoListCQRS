
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TodoListAzure.Persistence
{
    public static class AddPersistenceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TodoContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TodoListAzureConnectionString")));

            return services;
        }
    }
}
