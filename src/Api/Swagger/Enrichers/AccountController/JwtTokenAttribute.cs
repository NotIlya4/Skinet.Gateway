using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using SwaggerEnrichers.CreateCustomEnrichers;

namespace Api.Swagger.Enrichers.AccountController;

public class JwtTokenAttribute : Attribute, ISchemaEnricher
{
    public void Enrich(OpenApiSchema schema)
    {
        schema.Example = new OpenApiString("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkVhc3RlciBlZ2chISEiLCJpYXQiOjE1MTYyMzkwMjJ9.0ZyUjvhUaxbOCsZhnoG6rrfNrtUO2y-t5dGv-BhEZOg");
    }
}