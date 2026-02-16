using Acme.Core.Data;
using Acme.Core.DTOs;
using Acme.Core.Interfaces;
using Acme.Core.Models;
using Acme.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Acme.Core.Services;

public class SubmissionService : ISubmissionService
{
    private readonly ISubmissionRepository _submissionRepository;

    public SubmissionService(ISubmissionRepository submissionRepository)
    {
        _submissionRepository = submissionRepository;
    }
    public async Task<(bool Success, string? Error)> SubmitAsync(SubmissionDto dto)
    {
        if (!IsAtLeast18(dto.DateOfBirth))
            return (false, "You must be at least 18 years old.");

        var serialExists = await _submissionRepository.SerialExistsAsync(dto.SerialNumber!);

        if (!serialExists)
            return (false,"Invalid serial number.");

        var count = await _submissionRepository.GetSubmissionsCountAsync(dto.SerialNumber!);

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

        await _submissionRepository.AddSubmissionAsync(submission);

        return (true,null);
    }
    
    private static bool IsAtLeast18(DateTime dob)
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
}