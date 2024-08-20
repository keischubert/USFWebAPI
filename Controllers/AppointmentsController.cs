using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using USFWebAPI.DTOs;
using USFWebAPI.Entities;
using USFWebAPI.Services;

namespace USFWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IMapper mapper;

        public AppointmentsController(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            this.appointmentRepository = appointmentRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CreateAppointmentDTO>> Create(CreateAppointmentDTO createAppointment)
        {
            try
            {
                var appointment = mapper.Map<Appointment>(createAppointment);
                await appointmentRepository.CreateAsync(appointment);
                return Ok(createAppointment);
            }
            catch (MultipleErrorsException ex)
            {
                return BadRequest(ex.Exceptions.Select(e => e.Message));
            }

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAppointments()
        {
            try
            {
                var appointments = await appointmentRepository.GetAllAsync();

                var appointmentsDTO = mapper.Map<IEnumerable<AppointmentDTO>>(appointments);

                return Ok(appointmentsDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("searchDate")]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetByDate([FromQuery] int specialtyId, string date)
        {
            try
            {
                var appointments = await appointmentRepository.GetAppointmentsByDate(specialtyId, date);

                var appointmentsDTO = mapper.Map<IEnumerable<AppointmentDTO>>(appointments);

                return Ok(appointmentsDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AppointmentDTO>> GetAppointmentById([FromRoute] int id)
        {
            try
            {
                var appointment = await appointmentRepository.GetByIdAsync(id);

                var appointmentDTO = mapper.Map<AppointmentDTO>(appointment);

                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await appointmentRepository.DeleteById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}