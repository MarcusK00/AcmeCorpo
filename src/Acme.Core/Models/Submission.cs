using System.ComponentModel.DataAnnotations;

namespace Acme.Core.Models;

public class Submission
{ 
     
        public int Id { get; set; }

        public string? FirstName { get; set; }
       
        public string? LastName { get; set; }
      
        public string? Email { get; set; }
     
        public DateTime BirthDate { get; set; }
        
        public string? SerialCode { get; set; }
        
        public DateTime SubmittedAt { get; set; }
}