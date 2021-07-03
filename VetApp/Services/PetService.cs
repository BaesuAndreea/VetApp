using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetApp.Data;
using VetApp.Models;

namespace VetApp.Services
{
    public class PetService
    {
        public ApplicationDbContext _context;
        public PetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pet>> GetPets()
        {
            return await _context.Pets.ToListAsync();
        }

        public async Task<Pet> GetPet(int id)
        {
            return await _context.Pets.FindAsync(id);
        }

        public List<Pet> FilterPets(string owner)
        {
            var query = _context.Pets.Where(p => p.Owner == owner);
            return query.ToList();
        }
    }
}
