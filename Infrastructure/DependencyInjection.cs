using Application.Interfaces.Attention;
using Application.Interfaces.BranchOffice;
using Application.Interfaces.Company;
using Application.Interfaces.Laboratory;
using Application.Interfaces.Patient;
using Application.Interfaces.Pdf;
using Application.Interfaces.Security;
using Application.Interfaces.Service;
using Infrastructure.Repository;
using Infrastructure.Service.PatientService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //Repositories
            services.AddScoped<ISecurityRepository, SecurityRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IBranchOfficeRepository, BranchOfficeRepository>();
            services.AddScoped<IAttentionRepository, AttentionRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPdfRepository, PdfRepository>();
            services.AddScoped<ILaboratoryRepository, LaboratoryRepository>();

            //Services
            services.AddHttpClient<PatientClient>("PatientClient", config =>
            {
                config.BaseAddress = new Uri(configuration["API:PatientEndpoint"]);
                config.Timeout = TimeSpan.FromSeconds(60);
            });
            services.AddScoped<IPatientService, PatientService>();

            return services;
        }
    }
}