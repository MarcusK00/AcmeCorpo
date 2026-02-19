using System.ComponentModel.DataAnnotations;

namespace Acme.Shared.DTOs;

public class SubmissionDto
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
    public string FirstName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
    public string LastName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Date of birth is required")]
    public DateTime? DateOfBirth { get; set; }
    
    [Required(ErrorMessage = "Serial number is required")]
    public string SerialNumber { get; set; } = string.Empty;
}