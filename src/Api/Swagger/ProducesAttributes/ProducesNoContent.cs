using Microsoft.AspNetCore.Mvc;

namespace Api.Swagger.ProducesAttributes;

public class ProducesNoContent : ProducesResponseTypeAttribute
{
    public ProducesNoContent() : base(StatusCodes.Status204NoContent)
    {
        
    }
}