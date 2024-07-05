WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

WebApplication app = builder.Build();

await app.BootUmbracoAsync();


app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    // Other configurations...

    // Ensure the directory exists
    string mediaPath = Path.Combine(env.WebRootPath, "media");
    if (!Directory.Exists(mediaPath))
    {
        Directory.CreateDirectory(mediaPath);
    }

    // Continue with the rest of the configuration...
}
