﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public string sortname { get; set; }
        public int phonecode { get; set; }
    }
}
