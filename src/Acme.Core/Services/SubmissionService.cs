using Acme.Core.DTOs;
using Acme.Core.Interfaces;

namespace Acme.Core.Services;

public class SubmissionService : ISubmissionService
{
    public Task<(bool Success, string? Error)> SubmitAsync(SubmissionDto dto)
    {
        throw new NotImplementedException();
    }
}