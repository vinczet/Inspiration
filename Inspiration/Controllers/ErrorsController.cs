using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inspiration.Api.Controllers;

public class ErrorsController : ApiController
{
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    public IActionResult Error()
    {
        return Problem();
    }
}
