using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Dto
{
    public class PractitionerDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public DateTime DOB { get; set; }
        public string BloogGroup { get; set; }
        public string Gender { get; set; }
        public int UserTypeId { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string Address { get; set; }
        public int PinCode { get; set; }
        public int StateId { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }
        public string Qualification { get; set; }
        public int Specialisation { get; set; }
        public string RegistrationNumber { get; set; }
        public decimal VisitingCharge { get; set; }
    }
}
