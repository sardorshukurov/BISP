 using Welisten.API;
using Welisten.API.Configuration;
using Welisten.Common.Security;
using Welisten.Common.Settings;
using Welisten.Context;
using Welisten.Context.Seeder;
using Welisten.Context.Setup;
using Welisten.Services.Logger.Logger;
using Welisten.Services.Posts;
using Welisten.Services.Settings.AppSettings;

var mainSettings = CommonSettings.Load<MainSettings>("Main");
var logSettings = CommonSettings.Load<LogSettings>("Log");
var swaggerSettings = CommonSettings.Load<SwaggerSettings>("Swagger");
var identitySettings = CommonSettings.Load<IdentitySettings>("Identity");

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

builder.AddAppLogger(mainSettings, logSettings);

services.AddHttpContextAccessor();

services.AddAppDbContext(builder.Configuration);

services.AddAppCors();

services.AddAppAuth(builder.Configuration);

services.AddAppHealthChecks();

services.AddAppVersioning();

services.AddAppSwagger(mainSettings, swaggerSettings);

services.AddAppAutoMappers();

services.AddAppValidator();

services.AddAppControllerAndViews();

services.RegisterServices(builder.Configuration);

var app = builder.Build();

var logger = app.Services.GetRequiredService<IAppLogger>();

app.UseAppCors();

app.UseAppHealthChecks();

app.UseAppSwagger();

app.UseAppAuth();

app.UseAppControllerAndViews();

DbInitializer.Execute(app.Services);

DbSeeder.Execute(app.Services);

logger.Information("The Welisten.API has started");

app.Run();

logger.Information("The Welisten.API has stopped");