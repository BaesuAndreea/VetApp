using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetApp.Models
{
    public enum Species
    {
        cat,
        dog,
        rodent,
        bird,
        other,
    };
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Species Species { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public string Owner { get; set; }
    }
}
