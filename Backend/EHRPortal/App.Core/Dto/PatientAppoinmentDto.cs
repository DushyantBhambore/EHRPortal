using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Dto
{
    public class PatientAppoinmentDto
    {
        public int AppoinemntId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int ProviderId { get; set; }
        public int PatientId { get; set; }
        public int SpecialisationId { get; set; }
        public DateTime AppinementTime { get; set; }
        public string Chiefcomplaint { get; set; }

    }
}
