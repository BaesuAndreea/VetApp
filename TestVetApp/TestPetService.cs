using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetApp.Data;
using VetApp.Models;
using VetApp.Services;

namespace TestVetApp
{
    public class TestPetService
    {
        private ApplicationDbContext _context;
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("In setup.");
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            _context = new ApplicationDbContext(options, new OperationalStoreOptionsForTests());

            _context.Pets.Add(new Pet { Name = "Mau", Species = Species.cat, Breed = "maidanez", Age = 14, Owner = "Andreea" });
            _context.Pets.Add(new Pet { Name = "Masha", Species = Species.dog, Breed = "maidanez", Age = 14, Owner = "Monica" });
            _context.Pets.Add(new Pet { Name = "Dora", Species = Species.rodent, Breed = "guinea pig", Age = 14, Owner = "Andreea" });
            _context.SaveChanges();
        }

        [TearDown]
        public void Teardown()
        {
            Console.WriteLine("In teardown");

            foreach (var product in _context.Pets)
            {
                _context.Remove(product);
            }
            _context.SaveChanges();
        }

        public async Task<List<Pet>> GetAllPets()
        {
            var service = new PetService(_context);
            var getPetsTask = await service.GetPets();
            return getPetsTask;
        }

        [Test]
        public void TestGetAll()
        {
            var getPetsTask = GetAllPets();
            getPetsTask.Wait();
            var pets = getPetsTask.Result;
            Assert.AreEqual(3, pets.Count);
        }

        [Test]
        public void TestGetByOwner()
        {
            var service = new PetService(_context);
            Assert.AreEqual(2, service.FilterPets("Andreea").Count);
            Assert.AreEqual(1, service.FilterPets("Monica").Count);
            Assert.AreEqual(0, service.FilterPets("Constantin").Count);
        }


        public async Task<Pet> GetPetById(int id)
        {
            var service = new PetService(_context);
            var getPetTask = await service.GetPet(id);
            return getPetTask;
        }
        [Test]
        public void TestGetPetById()
        {
            var getPetTask = GetPetById(1);
            getPetTask.Wait();
            var pet = getPetTask.Result;
            Console.WriteLine(pet.ToString());

            Assert.AreEqual("Mau", pet.Name);
            Assert.AreEqual(Species.cat, pet.Species);
            Assert.AreEqual("maidanez", pet.Breed);
            Assert.AreEqual(14, pet.Age);
            Assert.AreEqual("Andreea", pet.Owner);
        }

    }
}