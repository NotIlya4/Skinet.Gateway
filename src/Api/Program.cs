using Api.Extensions;
using Api.Middlewares;
using Api.Middlewares.JwtParserMiddleware;
using Api.Properties;
using ExceptionCatcherMiddleware.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ParametersProvider parametersProvider = new(builder.Configuration);

services.AddConfiguredExceptionCatcherMiddleware();
services.AddForwarder();
services.AddScoped<RequestBodyEnableBuffering>();
services.AddJwtParserMiddleware();
services.AddConfiguredCors();
services.AddServiceHttpClient();
services.AddAccountService(parametersProvider.GetAccountServiceUrlProvider());
services.AddProductService(parametersProvider.GetProductServiceUrlProvider());

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

WebApplication app = builder.Build();

app.UseExceptionCatcherMiddleware();
app.UseCors();
app.UseMiddleware<RequestBodyEnableBuffering>();
app.UseMiddleware<JwtParser>();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
