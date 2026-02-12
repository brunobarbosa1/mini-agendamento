using System.Text.Json.Serialization;

namespace Agendamento.Api.Configurations;

public static class ClassConfigurantion
{
    public static void ConfigureStringEnum(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
            .AddJsonOptions
                (options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new JsonStringEnumConverter());
                });
    }
}