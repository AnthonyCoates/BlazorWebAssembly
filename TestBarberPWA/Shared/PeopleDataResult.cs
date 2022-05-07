using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBarberPWA.Shared
{
    public class PeopleDataResult //This class is for server-side paging
    {
        public IEnumerable<Person> People { get; set; }
        public int Count { get; set; }
    }
}
