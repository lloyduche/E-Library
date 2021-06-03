using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBookLibrary.Presentation.Controllers.APIControllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public abstract class BaseAPIController : ControllerBase
    {
        
    }
}
