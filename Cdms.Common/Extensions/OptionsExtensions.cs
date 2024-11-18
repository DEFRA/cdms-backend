
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Cdms.Common.Extensions;

public interface IValidatingOptions
{
    public bool Validate();
}

public static class OptionsExtensions
{
    
    private static OptionsBuilder<TOptions> CdmsValidation<TOptions>(this OptionsBuilder<TOptions>  options) where TOptions : class, IValidatingOptions
    {
        return options
            .ValidateDataAnnotations()
            .Validate(o => o.Validate())
            .ValidateOnStart();
    }

    public static OptionsBuilder<TOptions> CdmsAddOptions<TOptions, TValidator>(this IServiceCollection services,
        IConfiguration configuration, string section)
        where TOptions : class where TValidator : class, IValidateOptions<TOptions>
    {
        return services
            .AddSingleton<IValidateOptions<TOptions>, TValidator>()
            .CdmsAddOptions<TOptions>(configuration, section);
    }

    public static OptionsBuilder<TOptions> CdmsAddOptions<TOptions>(this IServiceCollection services, IConfiguration configuration, string section)
        where TOptions : class 
    {
        
        var s = services
            .AddOptions<TOptions>()
            .Bind(configuration.GetSection(section))
            .ValidateDataAnnotations();

        // var members = typeof(TOptions).GetMembers();
        //
        // // if (typeof(TOptions).GetMembers())
        //     // if (typeof(IValidatingOptions).IsAssignableFrom(typeof(TOptions)))
        //     // {
        //     //     s = s.Validate(o => ((IValidatingOptions)o).Validate())
        //     //         .ValidateOnStart();
        //     // }
        //
        // var nested = members.Where(m =>
        //     m.MemberType == MemberTypes.NestedType);
        //     // && ((RuntimeType)m);
        //
        //     var validators = nested
        //         .Select(n => (Type)n)
        //         .Where(n => typeof(IValidateOptions<TOptions>).IsAssignableFrom(n));
        //
        // if (validators.Count() > 1)
        // {
        //     throw new Exception("Not expecting more than one Validator for an options class at the moment");
        // }
        // else if (validators.Count() == 1)
        // {
        //     var t = validators.First() as Type;
        //     
        //     services.AddSingleton<IValidateOptions<TOptions>, t>();
        // }
            
    
        return s;
    }
    
    // public static OptionsBuilder<TOptions> CdmsAddOptionsWithValidation<TOptions>(this IServiceCollection services, IConfiguration configuration, string section) where TOptions : class, IValidatingOptions
    // {
    //     return services
    //         .AddOptions<TOptions>()
    //         .Bind(configuration.GetSection(section))
    //         .ValidateDataAnnotations()  
    //         .CdmsValidation();
    // }
    
}