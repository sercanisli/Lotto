using WebApi.Utilities.Formatters;

namespace WebApi.Extensions
{
    public static class IMvcBuilderExtensions
    {
        public static IMvcBuilder AddCustomCsvFormatter(this IMvcBuilder builder) =>
            builder.AddMvcOptions(configuration =>
                configuration.OutputFormatters
                .Add(new CsvOutputFormatter())
            );
    }
}
