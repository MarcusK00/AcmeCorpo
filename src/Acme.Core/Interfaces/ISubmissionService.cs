using Acme.Shared.DTOs;

namespace Acme.Core.Interfaces;

public interface ISubmissionService
{
    Task<(bool Success, string? Error)> SubmitAsync(SubmissionDto dto);
}