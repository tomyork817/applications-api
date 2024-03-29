using Application.Contracts.Applications;
using Application.Validators;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddMediatR(typeof(IAssemblyMarker));
        collection.AddScoped<IValidator<CreateApplication.Command>, CreateApplicationValidator>();
        collection.AddScoped<IValidator<UpdateApplication.Command>, UpdateApplicationValidator>();
        
        return collection;
    }
}