using Api.Extensions;
using Api.Middlewares;
using ExceptionCatcherMiddleware.Extensions;
using Infrastructure.HttpHeaderEnricher;
using Infrastructure.HttpMappers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

services.AddConfiguredExceptionCatcherMiddlewareServices();

services.AddScoped<HttpClient>();
services.AddScoped<HttpMessageInvoker, HttpClient>();
services.AddScoped<HttpForwarder>();
services.AddScoped<IHttpHeaderEnricher, CustomHttpHeaderEnricher>();
services.AddScoped<RequestBodyEnableBuffering>();
services.AddScoped<HttpRequestMapper>();
services.AddScoped<HttpResponseMapper>();

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

WebApplication app = builder.Build();

app.UseExceptionCatcherMiddleware();

app.UseMiddleware<RequestBodyEnableBuffering>();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
