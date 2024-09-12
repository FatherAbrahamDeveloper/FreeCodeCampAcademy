namespace FreeCodeCampAcademy.Assets.AppKits;

internal static class OptionsExt
{
    public static void ConfigureServiceOptions(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;

        builder.Services.AddOptions<LookupServOptions>().BindConfiguration(LookupServOptions.SectionName).ValidateDataAnnotations();

        builder.Services.AddOptions<RegAPIServiceOptions>().BindConfiguration(RegAPIServiceOptions.SectionName).ValidateDataAnnotations();

        builder.Services.AddOptions<SerilogOptions>().BindConfiguration(SerilogOptions.SectionName).ValidateDataAnnotations();

    }
}
