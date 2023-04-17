using Api.Extensions;
using Api.Middlewares;
using Api.Properties;
using ExceptionCatcherMiddleware.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ParametersProvider parametersProvider = new(builder.Configuration);

services.AddSingleton(parametersProvider.GetLinkProvider());
services.AddConfiguredExceptionCatcherMiddlewareServices();
services.AddForwarder();
services.AddScoped<RequestBodyEnableBuffering>();

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
