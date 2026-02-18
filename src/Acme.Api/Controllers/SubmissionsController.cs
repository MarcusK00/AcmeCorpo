using Acme.Shared.DTOs;
using Acme.Core.Interfaces;
using Acme.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Acme.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubmissionsController : ControllerBase
{
    private readonly ISubmissionService _service;

    public SubmissionsController(ISubmissionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Submit([FromBody] SubmissionDto dto)
    {
        Console.WriteLine($"Received: FirstName={dto.FirstName}, LastName={dto.LastName}, Email={dto.Email}, DOB={dto.DateOfBirth}, Serial={dto.SerialNumber}");
    
        if (!ModelState.IsValid)
        {
            Console.WriteLine("ModelState is invalid:");
            foreach (var error in ModelState)
            {
                Console.WriteLine($"  {error.Key}: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
            }
            return BadRequest(ModelState);
        }

        var result = await _service.SubmitAsync(dto);

        if (!result.Success)
        {
            Console.WriteLine($"Service returned error: {result.Error}");
            return BadRequest(result.Error);
        }

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<SubmissionDto>>> GetAllAsync()
    {
        var submissions = await _service.GetAllSubmissionsAsync();
        
        if (submissions.Count == 0)
            return Ok(new List<SubmissionDto>());  
        
  
        var dtos = submissions.Select(s => new SubmissionDto
        {
            FirstName = s.FirstName,
            LastName = s.LastName,
            Email = s.Email,
            DateOfBirth = s.BirthDate,
            SerialNumber = s.SerialCode
        }).ToList();
        
        return Ok(dtos);
    }
}