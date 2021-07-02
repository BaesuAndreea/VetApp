using AutoMapper;
using VetApp.Models;
using VetApp.ViewModels.Pet;

namespace VetApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Pet, PetViewModel>().ReverseMap();
        }
    }
}
