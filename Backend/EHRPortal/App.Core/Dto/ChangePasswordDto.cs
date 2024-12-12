using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Dto
{
    public class ChangePasswordDto
    {
        public string UserName { get; set; }
        public string ConfirmPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
