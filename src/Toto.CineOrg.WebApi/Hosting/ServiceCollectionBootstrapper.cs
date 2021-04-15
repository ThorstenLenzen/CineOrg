using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Toto.CineOrg.ServiceModel;
using Toto.CineOrg.ServiceModel.Validation;

namespace Toto.CineOrg.WebApi.Hosting
{
    public static class ServiceCollectionBootstrapper
    {
        public static IServiceCollection AddFluentValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<MovieForCreate>, MovieForCreateValidator>();
            services.AddTransient<IValidator<MovieForUpdate>, MovieForUpdateValidator>();

            return services;
        }
    }
}