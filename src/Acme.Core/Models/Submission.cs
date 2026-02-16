using System.ComponentModel.DataAnnotations;

namespace Acme.Core.Models;

public class Submission
{ 
     
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string? FirstName { get; set; }
        [Required, StringLength(50)]
        public string? LastName { get; set; }
        [Required, EmailAddress]
        public string? Email { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        
        public string? SerialCode { get; set; }
        
        public DateTime SubmittedAt { get; set; }
}