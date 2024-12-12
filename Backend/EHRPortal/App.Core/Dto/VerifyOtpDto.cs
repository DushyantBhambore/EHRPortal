using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Dto
{
    public class VerifyOtpDto
    {
        public string Username { get; set; }
        public string Otp { get; set; }
    }
}
