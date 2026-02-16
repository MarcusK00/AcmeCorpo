using Acme.Core.DTOs;
using Acme.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Acme.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubmissionsController : Controller
{
    private readonly ISubmissionService _service;

    public SubmissionsController(ISubmissionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Submit([FromBody] SubmissionDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _service.SubmitAsync(dto);

        if (!result.Success)
            return BadRequest(result.Error);

        return Ok();
    }
}