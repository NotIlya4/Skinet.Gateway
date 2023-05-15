using Microsoft.AspNetCore.Mvc;

namespace Api.Swagger.ProducesAttributes;

public class ProducesOkAttribute : ProducesResponseTypeAttribute
{
    public ProducesOkAttribute() : base(StatusCodes.Status200OK)
    {
        
    }
}