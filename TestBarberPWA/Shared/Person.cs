using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBarberPWA.Shared
{
    public class Person
    {
        public int PersonID { get; set; }
        public string? Forename { get; set; }
        public string? Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public bool IsEmployee { get; set; }
    }
}
