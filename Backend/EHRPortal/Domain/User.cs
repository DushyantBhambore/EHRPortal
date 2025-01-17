﻿using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class User : AuditableEntity
    {
        [Key]
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
        public int   UserTypeId { get; set; }
        public string ImageFile { get; set; } 
        public string Address { get; set; }
        public int PinCode { get; set; }
        public int StateId { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }
        public string? Qualification { get; set; } = string.Empty;
        public int? Specialisation { get; set; } = 0;
        public string? RegistrationNumber { get; set; } = string.Empty;
        public decimal? VisitingCharge { get; set; } = 0;


    }
}
