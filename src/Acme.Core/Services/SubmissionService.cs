using Acme.Core.Data;
using Acme.Core.DTOs;
using Acme.Core.Interfaces;
using Acme.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Acme.Core.Services;

public class SubmissionService : ISubmissionService
{
    public async Task<(bool Success, string? Error)> SubmitAsync(SubmissionDto dto)
    {
        if (!IsAtLeast18(dto.DateOfBirth))
            return (false, "You must be at least 18 years old.");

        var serialExists = await _dbContext.SerialNumbers
            .AnyAsync(s => s.Code == dto.SerialNumber);

        if (!serialExists)
            return (false,"Invalid serial number.");

        var count = await _dbContext.Submissions
            .CountAsync(s => s.SerialCode == dto.SerialNumber);

        if (count >= 2)
            return (false,"Serial number already used twice.");

        var submission = new Submission
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            SerialCode = dto.SerialNumber,
            BirthDate = dto.DateOfBirth,
            SubmittedAt = DateTime.UtcNow
        };

        _dbContext.Submissions.Add(submission);
        await _dbContext.SaveChangesAsync();

        return (true,null);
    }
    
    private bool IsAtLeast18(DateTime dob)
    {
        var today = DateTime.Today;
        var age = today.Year - dob.Year;

        var birthdayThisYear = dob.AddYears(age);

        if (birthdayThisYear > today) // Check if they had birthday already.
        {
            age--;
        }
        return age >= 18;
    }

    private readonly AcmeDbContext _dbContext;

    public SubmissionService(AcmeDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}