using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetApp.Models;

namespace VetApp.ViewModels.ExaminationViewModels
{
    public class ExaminationForUserResponse
    {
        public ApplicationUser Doctor { get; set; }
        public Appointment Appointment { get; set; }
        public string Notes { get; set; }
    }
}
