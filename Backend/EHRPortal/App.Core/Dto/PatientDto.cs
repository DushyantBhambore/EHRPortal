using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Dto
{
    public class PatientDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime DOB { get; set; }
        public string BloogGroup { get; set; }
        public string Gender { get; set; }
        public IFormFile? ImageFile { get; set; } = null;
        public string Address { get; set; }
        public int PinCode { get; set; }
        public int StateId { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }
    }
}
