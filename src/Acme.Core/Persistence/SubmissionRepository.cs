using Acme.Core.Data;
using Acme.Core.Interfaces;
using Acme.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Acme.Core.Persistence;

public class SubmissionRepository : ISubmissionRepository
{
    private readonly AcmeDbContext _dbContext;
    
    public SubmissionRepository(AcmeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddSubmissionAsync(Submission submission)
    {
            _dbContext.Submissions!.Add(submission);

            await _dbContext.SaveChangesAsync();
    }

    public async Task<int> GetSubmissionsCountAsync(string serial)
    {
        return await _dbContext.Submissions!.CountAsync(s => s.SerialCode == serial);
    }

    public async Task<bool> SerialExistsAsync(string serial)
    {
        if (await _dbContext.Submissions!.AnyAsync(s => s.SerialCode == serial))
            return await Task.FromResult(true);
        
        return await Task.FromResult(false);
    }
}