using Acme.Shared.DTOs;
using Acme.Core.Models;

namespace Acme.Core.Interfaces;

public interface ISubmissionService
{
    Task<(bool Success, string? Error)> SubmitAsync(SubmissionDto dto);
    
    Task<List<Submission>> GetAllSubmissionsAsync();
}