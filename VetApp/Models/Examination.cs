using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetApp.Models
{
    public class Examination
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public ApplicationUser Doctor { get; set; }
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public string Notes { get; set; }
         

    }
}
