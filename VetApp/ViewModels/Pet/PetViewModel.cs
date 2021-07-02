using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetApp.Models;

namespace VetApp.ViewModels.Pet
{
    public class PetViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Species Species { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public string Owner { get; set; }
    }
}
