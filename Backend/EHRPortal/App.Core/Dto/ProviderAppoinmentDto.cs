using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Dto
{
    public class ProviderAppoinmentDto
    {
        public int AppoinemntId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int SpecialisationId { get; set; }
        public int UserId { get; set; }
        public DateTime AppinementTime { get; set; }
        public string Chiefcomplaint { get; set; }
        public string AppointmentStatus { get; set; }
    }
}
