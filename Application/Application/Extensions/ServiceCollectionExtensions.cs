using Application.Contracts.Applications;
using Application.Validators;
using Domain.Applications;
using Domain.Validators;
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
        collection.AddScoped<IValidator<SubmittedApplication>, SubmittedApplicationValidator>();
        
        return collection;
    }
}