using Acme.Shared.DTOs;

namespace Acme.Web.Interfaces;

public interface ISubmissionApiService
{
    Task SubmitAsync(SubmissionDto dto);
    
    Task<List<SubmissionDto>> GetAllAsync();
}