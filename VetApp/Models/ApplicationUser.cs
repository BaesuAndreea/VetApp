using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Specialization { get; set; }
        public List<Examination> Examinations { get; set; }
    }
}
