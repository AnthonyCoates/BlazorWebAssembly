using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBarberPWA.Shared
{
    public class Address
    {
        public int AddressID { get; set; }
        public int PersonID { get; set; }
        public string? LineOne { get; set; }
        public string? LineTwo { get; set; }
        public string? Town { get; set; }
        public string? PostCode { get; set; }
    }
}
