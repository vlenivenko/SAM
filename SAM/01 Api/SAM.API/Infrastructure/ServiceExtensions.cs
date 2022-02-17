using SAM.Core.CQRS.Handlers.Interfaces;
using SAM.Core.CQRS.Validation.Interfaces;
using SAM.Patient.Services.Queries.GetPatientList;

namespace SAM.API.Infrastructure
{
    /// <summary>
    /// Extension responsible for dependency injections
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Initializes services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<GetPatientListRequest, GetPatientListResponse>, GetPatientListHandler>();
            services.AddScoped<IRequestValidator<GetPatientListRequest, GetPatientListResponse>, GetPatientListValidator>();

            return services;
        }
    }
}