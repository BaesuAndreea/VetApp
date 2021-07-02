using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetApp.ViewModels.ExaminationViewModels
{
    public class NewExaminationRequest
    {
        public int AppointmentId { get; set; }
        public string Notes { get; set; }
    }
}
