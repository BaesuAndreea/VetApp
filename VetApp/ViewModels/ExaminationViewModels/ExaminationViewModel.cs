using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetApp.Models;
using VetApp.ViewModels.AppointmentViewModels;

namespace VetApp.ViewModels.ExaminationViewModels
{
    public class ExaminationViewModel
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public ApplicationUserViewModel Doctor { get; set; }
        public int AppointmentId { get; set; }
        public AppointmentViewModel Appointment { get; set; }
        public string Notes { get; set; }
    }
}
