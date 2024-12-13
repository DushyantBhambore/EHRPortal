using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Appoinment
    {
        [Key]
        public int AppoinemntId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int SpecialisationId { get; set; }
        public int UserId { get; set; }
        public DateTime AppinementTime { get; set; }
        public string Chiefcomplaint { get; set; }
        public string AppointmentStatus { get; set; }
        public decimal Fee { get; set; }
    }
}
