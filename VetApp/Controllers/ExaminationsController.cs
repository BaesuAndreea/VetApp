using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetApp.Data;
using VetApp.Models;
using VetApp.ViewModels;
using VetApp.ViewModels.AppointmentViewModels;
using VetApp.ViewModels.ExaminationViewModels;

namespace VetApp.Controllers
{
    [Authorize(AuthenticationSchemes = "Identity.Application,Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    public class ExaminationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExaminationsController(ApplicationDbContext context,  IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        
        // GET: api/Examinations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExaminationForUserResponse>>> GetExamination()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _context.Examination.Where(e => e.Doctor.Id == user.Id).Include(e => e.Appointment).Select(e => _mapper.Map<ExaminationForUserResponse>(e)).ToListAsync();
            //var resultViewModel = _mapper.Map<ExpenseForUserResponse>(result);

            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<AppointmentWitExaminationsViewModel> GetExaminationForAppointment(int id)

        {
            var query = _context.Appointment.Where(a=>a.Id==id).Include(a=>a.Examinations)
                  .Select(m => _mapper.Map<AppointmentWitExaminationsViewModel>(m));
            var s = query.ToQueryString();

            var query_v1 = _context.Appointment.Where(a => a.Id == id).Select(a => new AppointmentWitExaminationsViewModel
            {
                Id=a.Id,
                Examinations = a.Examinations.Select(e=> new ExaminationViewModel { 
                    Notes = e.Notes,
                    Doctor = new ApplicationUserViewModel
                    {
                        Name = e.Doctor.UserName
                    }
                })

            });

            return query_v1.ToList()[0];
        }

        // PUT: api/Examinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExamination(int id, PutExaminationUserRequest examinationReq)
        {
            if (id != examinationReq.Id)
            {
                return BadRequest();
            }
            var examination = _context.Examination.Find(examinationReq.Id);

            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (examination.Doctor.Id != user.Id)
            {
                return Forbid();
            }

            examination.AppointmentId = examinationReq.AppointmentId;
            examination.Doctor = user;
            examination.Notes = examinationReq.Notes;

            _context.Entry(examination).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Examination.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Examinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Examination>> PostExamination(NewExaminationRequest newRequest)
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var appointmentWithId = _context.Appointment.Find(newRequest.AppointmentId);
            if (appointmentWithId == null)
            {
                return BadRequest();
            }

            var examination = new Examination
            {
                Doctor = user,
                Appointment = appointmentWithId,
                Notes = newRequest.Notes
            };

            _context.Examination.Add(examination);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE: api/Examinations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamination(int id)
        {
            var examination = await _context.Examination.FindAsync(id);
            if (examination == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (examination.DoctorId != user.Id)
            {
                return Forbid();
            }
            _context.Examination.Remove(examination);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
