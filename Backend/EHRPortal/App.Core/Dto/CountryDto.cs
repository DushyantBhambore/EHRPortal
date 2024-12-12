using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Dto
{
    public class CountryDto
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string sortname { get; set; }
        public int phonecode { get; set; }
    }
}
