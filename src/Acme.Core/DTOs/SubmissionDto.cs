using System.ComponentModel.DataAnnotations;

namespace Acme.Core.DTOs;

public class SubmissionDto
{

    [Required, StringLength(50)]
    public string? FirstName { get; set; }
    
    [Required, StringLength(50)]
    public string? LastName { get; set; }
    
    [Required, EmailAddress]
    public string? Email { get; set; }
    
    [Required, DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
    
    [Required]
    public string? SerialNumber{ get; set; }
}