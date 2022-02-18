using Microsoft.EntityFrameworkCore;
using SAM.Core.CQRS.Handlers.Interfaces;
using SAM.Core.CQRS.Validation.Interfaces;
using SAM.Patient.MappingProfiles;
using SAM.Patient.Services.Commands.CreatePatient;
using SAM.Patient.Services.Queries.GetPatientList;
using SAM.Repository.Contexts;
using SAM.Repository.Repositories.Interfaces;

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

            services.AddScoped<IRequestHandler<CreatePatientRequest, CreatePatientResponse>, CreatePatientHandler>();
            services.AddScoped<IRequestValidator<CreatePatientRequest, CreatePatientResponse>, CreatePatientValidator>();

            return services;
        }

        /// <summary>
        /// Initializes mapper
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection InitializeMapper(this IServiceCollection services)
        {
            // add mapping profiles here
            services.AddAutoMapper(typeof(PatientMappingProfile).Assembly);

            return services;
        }

        /// <summary>
        /// Initializes database and its data
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection InitializeDatabase(this IServiceCollection services)
        {
            services.AddDbContext<Context>(opt => opt.UseInMemoryDatabase("SAM.DB"));

            var context = new Context(services.BuildServiceProvider().GetRequiredService<DbContextOptions<Context>>());
            services.AddScoped<IRepository>(a => new SAM.Repository.Repositories.Repository(context));

            // seed data just for testing purposes
            SeedData(services, context);

            return services;
        }

        #region Data Seeding

        private static IServiceCollection SeedData(IServiceCollection services, Context context)
        {
            context.Patients.Add(new SAM.Repository.Models.Patient()
            {
                Id = 1,
                FirstName = "Vlad",
                LastName = "Lenivenko",
                DateOfBirth = new DateOnly(1989, 08, 10),
                DiseaseType = "Allergy",
            });

            context.SaveChanges();

            return services;
        }

        #endregion
    }
}