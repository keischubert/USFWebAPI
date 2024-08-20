using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using USFWebAPI.DTOs;
using USFWebAPI.Entities;
using USFWebAPI.Services;

namespace USFWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ISpecialtyRepository specialtyRepository;
        private readonly IMapper mapper;

        public SpecialtiesController(ISpecialtyRepository specialtyRepository, IMapper mapper)
        {
            this.specialtyRepository = specialtyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecialtyDTO>>> GetSpecialties()
        {
            try
            {
                var specialties = await specialtyRepository.GetAllAsync();

                return Ok(mapper.Map<IEnumerable<SpecialtyDTO>>(specialties));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<CreateSpecialtyDTO>> CreateSpecialty([FromBody] CreateSpecialtyDTO createSpecialtyDTO)
        {
            try
            {
                var specialty = mapper.Map<Specialty>(createSpecialtyDTO);

                await specialtyRepository.CreateAsync(specialty);

                return Ok(createSpecialtyDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SpecialtyDTO>> GetSpecialtyById([FromRoute] int id)
        {
            try
            {
                var specialty = await specialtyRepository.GetByIdAsync(id);

                return Ok(mapper.Map<SpecialtyDTO>(specialty));

            }

            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id:int}/professionals")]
        public async Task<ActionResult<IEnumerable<ProfessionalDTO>>> GetProfessionalsBySpecialtyId(int id)
        {
            try
            {
                var listProfessionals = await specialtyRepository.GetProfessionalsBySpecialtyIdAsync(id);

                return Ok(mapper.Map<IEnumerable<ProfessionalDTO>>(listProfessionals));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}/services")]
        public async Task<ActionResult<IEnumerable<ServiceDTO>>> GetServicesBySpecialtyId(int id)
        {
            try
            {
                var listServices = await specialtyRepository.GetServicesBySpecialtyIdAsync(id);

                return Ok(mapper.Map<IEnumerable<ServiceDTO>>(listServices));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}