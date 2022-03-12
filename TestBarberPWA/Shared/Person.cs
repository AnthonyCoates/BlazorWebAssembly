using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBarberPWA.Shared
{
    public class Person
    {
        public int PersonID { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Forename must contain at least 1 character")]
        public string? Forename { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Surname must contain at least 1 character")]
        public string? Surname { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public string? PhoneNo { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public bool IsEmployee { get; set; }
    }
}
