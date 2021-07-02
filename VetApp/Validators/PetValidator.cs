using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetApp.Data;
using VetApp.ViewModels.PetViewModels;

namespace VetApp.Validator
{
    public class PetValidator : AbstractValidator<PetViewModel>
    {
        private readonly ApplicationDbContext _context;

        public PetValidator(ApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name).Custom((prop, validationContext) =>
            {
                var instance = validationContext.InstanceToValidate;
                var name = instance.Name;
                if (name[0] < 'A' || name[0] > 'Z')
                {
                    validationContext.AddFailure($"Name must statrt with an upper letter");
                }
            });
            RuleFor(x => x.Age).InclusiveBetween(0, 99);
        }
    }
}
