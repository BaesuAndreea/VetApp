using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetApp.ViewModels.ExaminationViewModels
{
    public class PutExaminationUserRequest
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string Notes { get; set; }

    }
}
