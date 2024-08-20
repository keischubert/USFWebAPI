using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using USFWebAPI.DTOs;
using USFWebAPI.Entities;
using USFWebAPI.Profiles;
using USFWebAPI.Services;

namespace USFWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IPersonRepository<Patient> patientRepository;

        public PatientsController(ApplicationDbContext context, IMapper mapper, IPersonRepository<Patient> patientRepository)
        {
            this.context = context;
            this.mapper = mapper;
            this.patientRepository = patientRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreatePatient(CreatePatientDTO createPatientDTO)
        {
            try
            {
                //Mapeo de los datos enviados por el usuario
                var patient = mapper.Map<Patient>(createPatientDTO);

                //Ejecucion del metodo del repositorio para realizar las validaciones necesarias
                await patientRepository.CreateAsync(patient);

                return Ok(createPatientDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<PatientDTO>>> GetPatients()
        {
            var patients = await patientRepository.GetAllAsync();

            var patientsDTO = mapper.Map<List<PatientDTO>>(patients);

            return Ok(patientsDTO);
        }

        [HttpGet("ci/{ci}")]
        public async Task<ActionResult<PatientDTO>> GetPatientByCI([FromRoute] string ci)
        {
            var patient = await patientRepository.GetByCiAsync(ci);

            if (patient == null)
            {
                return NotFound($"No se ha encontrado el paciente con CI: {ci}");
            }

            var patientDTO = mapper.Map<PatientDTO>(patient);

            return Ok(patientDTO);
        }

        [HttpGet("name/{searchName}")]
        public async Task<ActionResult<List<PatientDTO>>> GetPatientsByName([FromRoute] string searchName)
        {
            var patients = await patientRepository.GetListByNameAsync(searchName); 

            return mapper.Map<List<PatientDTO>>(patients);
        }
    }
}
