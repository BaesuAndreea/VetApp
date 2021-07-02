using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetApp.Models;

namespace VetApp.ViewModels.AppointmentViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public Pet Pet { get; set; }
        public DateTime Date { get; set; }
        public string Details { get; set; }
        public bool Finished { get; set; }
    }
}
