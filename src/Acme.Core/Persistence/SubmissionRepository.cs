using Acme.Core.Data;
using Acme.Core.Interfaces;
using Acme.Core.Models;

namespace Acme.Core.Persistence;

public class SubmissionRepository : ISubmissionRepository
{
    private readonly AcmeDbContext _dbContext;
    
    public SubmissionRepository(AcmeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task AddSubmissionAsync(Submission submission)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetSubmissionsCountAsync(string serial)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SubmissionExistsAsync(string serial)
    {
        throw new NotImplementedException();
    }

    public Task<Submission> GetSubmissionAsync(int id)
    {
        throw new NotImplementedException();
    }
}