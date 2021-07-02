using AutoMapper;
using VetApp.Models;
using VetApp.ViewModels;
using VetApp.ViewModels.AppointmentViewModels;
using VetApp.ViewModels.ExaminationViewModels;
using VetApp.ViewModels.PetViewModels;

namespace VetApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Pet, PetViewModel>().ReverseMap();
            CreateMap<Examination, ExaminationForUserResponse>().ReverseMap();
            CreateMap<Appointment, AppointmentWitExaminationsViewModel>().ReverseMap();
            CreateMap<Examination, ExaminationViewModel>().ReverseMap();
            CreateMap<Appointment, AppointmentViewModel>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
        }
    }
}
