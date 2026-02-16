using Acme.Core.Models;

namespace Acme.Core.Interfaces;

public interface ISubmissionRepository
{
    Task AddSubmissionAsync(Submission submission);
    Task<int> GetSubmissionsCountAsync(string serial);
    Task<bool> SubmissionExistsAsync(string serial);
    Task<Submission> GetSubmissionAsync(int id);
}