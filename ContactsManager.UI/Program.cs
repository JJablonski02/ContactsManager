using Serilog;
using crudBundle;
using crudBundle.Middleware;

var builder = WebApplication.CreateBuilder(args);


//logging
//builder.Host.ConfigureLogging(loggingProvider =>
//{
//    loggingProvider.ClearProviders();
//    loggingProvider.AddConsole();
//    loggingProvider.AddDebug();
//    loggingProvider.AddEventLog();
//});

//Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(context.Configuration).ReadFrom.Services(services); // Reading the configuration
                                                                                                   // from appsettings.json, reads out
                                                                                                   // current app's services and make them available to serilog
});

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();



if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseExceptionHandlingMiddleware();
}
app.UseSerilogRequestLogging();

app.UseHttpLogging();

//app.Logger.LogDebug("debug-message");
//app.Logger.LogInformation("information-message");
//app.Logger.LogCritical("critical-message");
//app.Logger.LogError("error-message");
//app.Logger.LogError("warning-message");

if (builder.Environment.IsEnvironment("Test") == false)
Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa" );

app.UseStaticFiles();

app.UseRouting(); //Identifying action method based route
app.UseAuthentication(); //Reading Identity cookie
app.UseAuthorization(); //Validates access permissions of the user
app.MapControllers(); //Execute the filter pipeline (action + filters)
app.MapControllers(); //Execute the filter pipeline (action + filters)


app.Run();

public partial class Program { } //make the auto-generated Program accessible programmatically