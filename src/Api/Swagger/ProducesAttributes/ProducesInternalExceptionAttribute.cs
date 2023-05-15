using Api.Controllers.Views;
using Microsoft.AspNetCore.Mvc;

namespace Api.Swagger.ProducesAttributes;

public class ProducesInternalExceptionAttribute : ProducesResponseTypeAttribute
{
    public ProducesInternalExceptionAttribute() : base(typeof(BadResponseView), StatusCodes.Status500InternalServerError)
    {
        
    }
}