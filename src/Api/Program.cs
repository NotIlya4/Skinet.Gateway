using Api.Extensions;
using Api.Middlewares;
using Api.Properties;
using ExceptionCatcherMiddleware.Extensions;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ParametersProvider parameters = new(builder.Configuration);

services.AddConfiguredExceptionCatcherMiddleware();
services.AddConfiguredCors();
services.AddCorrelationId();
services.AddForwardInfoOptions(parameters);
services.AddConfiguredSwagger();
services.AddConfiguredSerilog(builder.Configuration, parameters.Seq);
services.AddAuther(parameters.AccountServiceUrlProvider);
services.AddYarp(parameters.Yarp);

services.AddControllers();
services.AddEndpointsApiExplorer();

WebApplication app = builder.Build();

app.UseMiddleware<CorrelationIdGeneratorMiddleware>();
app.UseSerilogRequestLogging();
app.UseExceptionCatcherMiddleware();
app.UseMiddleware<ServiceBadResponseExceptionCatcherMiddleware>();
app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<YarpExceptionRethrowerMiddleware>();
app.MapReverseProxy();

app.Run();
