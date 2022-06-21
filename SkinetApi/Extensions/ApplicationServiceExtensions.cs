using Microsoft.AspNetCore.Mvc;
using NLog;
using SkinetApi.Contracts;
using SkinetApi.Errors;
using SkinetApi.Helpers;
using SkinetApi.LoggerService;
using SkinetApi.Repositories;

namespace SkinetApi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped<ILoggerManager, LoggerManager>(); 
            Services.AddScoped<IRepositoryManager, RepositoryManager>();
            Services.AddScoped<IGeneralRepository, GeneralRepository>();
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddAutoMapper(typeof(MappingProfile));

            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(e => e.Value.Errors)
                    .Select(e => e.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationError
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });


            return Services;
        }
    }
}
