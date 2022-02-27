using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBarberPWA.Shared
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int EmployeeID { get; set; }
        public int CustomerID { get; set; }
        public DateTime DateTime { get; set; }
        public string? Notes { get; set; }
    }
}
