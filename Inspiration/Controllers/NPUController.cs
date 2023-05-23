using ErrorOr;
using Inspiration.Api.Common;
using Inspiration.Contract;
using Inspiration.Domain;
using Inspiration.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inspiration.Api.Controllers;

public class NPUController : ApiController
{
    
    private static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };
    private readonly INPUService _npuService;
    public NPUController(INPUService npuService)
    {
        _npuService = npuService;
    }

    [HttpPost("create")]    
    public async Task<IActionResult> CreateNPUAsync(IFormFile file, [FromForm] NPUCreateRequestDto request, CancellationToken cancellationToken = default)
    {
        //Get authenticated user
        var userIdGuidRequest = GetSignedInUser.GetUserId(HttpContext);
        if (userIdGuidRequest.IsError)
        {
            return Problem(userIdGuidRequest.Errors);
        }

        if (file != null)
        {
            if (ImageExtensions.Contains(Path.GetExtension(file.FileName).ToUpper()))
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms, cancellationToken);
                    ms.Position = 0;

                    var npuCreateResult = await _npuService.CreateAsync(request, userIdGuidRequest.Value, ms, Path.GetExtension(file.FileName), cancellationToken);
                    return npuCreateResult.Match(
                        npuCreateResultValue => Ok(npuCreateResultValue),
                        errors => Problem(errors));                    
                }                                
            }
            else
            {
                return Problem(new List<Error> { Errors.NPU.AttachedFileNotImage });
            }
        }
        return Problem(new List<Error> { Errors.NPU.NoFileAttached });
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetNPU(Guid id)
    {
        //Get authenticated user
        var userIdGuidRequest = GetSignedInUser.GetUserId(HttpContext);
        if (userIdGuidRequest.IsError)
        {
            return Problem(userIdGuidRequest.Errors);
        }

        var getResult = _npuService.Get(id, userIdGuidRequest.Value);

        return getResult.Match(
            getResultValue => Ok(getResultValue),
            errors => Problem(errors));
    }

    [HttpPost("{npuId:guid}/creativity/{rating:int}")]
    public IActionResult AddCreativityRating(Guid npuId, int rating)
    {
        //Get authenticated user
        var userIdGuidRequest = GetSignedInUser.GetUserId(HttpContext);
        if (userIdGuidRequest.IsError)
        {
            return Problem(userIdGuidRequest.Errors);
        }

        var createResult = _npuService.CreateCreativityRating(npuId, userIdGuidRequest.Value, rating);

        return createResult.Match(
            createResultValue => Ok(createResultValue),
            errors => Problem(errors));
    }

    [HttpPost("{npuId:guid}/uniqueness/{rating:int}")]
    public IActionResult AddUniquenessRating(Guid npuId, int rating)
    {
        //Get authenticated user
        var userIdGuidRequest = GetSignedInUser.GetUserId(HttpContext);
        if (userIdGuidRequest.IsError)
        {
            return Problem(userIdGuidRequest.Errors);
        }

        var createResult = _npuService.CreateUniquenessRating(npuId, userIdGuidRequest.Value, rating);

        return createResult.Match(
            createResultValue => Ok(createResultValue),
            errors => Problem(errors));
    }

    [HttpPost("list")]
    public IActionResult ListNPUs(NPUListRequestDto request)
    {
        //Get authenticated user
        var userIdGuidRequest = GetSignedInUser.GetUserId(HttpContext);
        if (userIdGuidRequest.IsError)
        {
            return Problem(userIdGuidRequest.Errors);
        }

        var createListResult = _npuService.List(request, userIdGuidRequest.Value) ;

        return createListResult.Match(
            createListResultValue => Ok(createListResultValue),
            errors => Problem(errors));
    }
}
