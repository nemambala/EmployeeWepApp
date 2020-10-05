using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeApplication.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string DateOfJoin { get; set; }
        public string Department { get; set; }
        public int AddressId { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
    
}