using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using USFWebAPI.DTOs;
using USFWebAPI.Entities;
using USFWebAPI.Migrations;
using USFWebAPI.Services;

namespace USFWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionalsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IPersonRepository<Professional> proffesionalRepository;

        public ProfessionalsController(IMapper mapper, IPersonRepository<Professional> proffesionalRepository)
        {
            this.mapper = mapper;
            this.proffesionalRepository = proffesionalRepository;
        }

        [HttpPost]
        public async Task<ActionResult<CreateProfessionalDTO>> Post([FromBody] CreateProfessionalDTO createProfessionalDTO)
        {
            try
            {
                var professional = mapper.Map<Professional>(createProfessionalDTO);

                await proffesionalRepository.CreateAsync(professional);

                return Ok(createProfessionalDTO);

            }

            catch (MultipleErrorsException ex)
            {
                return BadRequest(ex.Exceptions.Select(e => e.Message).ToList());
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<ProfessionalDTO>>> GetProffesionals()
        {
            try
            {
                var listProffesionals = await proffesionalRepository.GetAllAsync();

                var listProfessionalsDTO = mapper.Map<List<ProfessionalDTO>>(listProffesionals);

                return Ok(listProfessionalsDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ci/{ci}")]
        public async Task<ActionResult<ProfessionalDTO>> GetById([FromRoute] string ci)
        {
            try
            {
                var professional = await proffesionalRepository.GetByCiAsync(ci);

                var professionalDTO = mapper.Map<ProfessionalDTO>(professional);

                return Ok(professionalDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProfessionalDTO>> GetById([FromRoute] int id)
        {
            try
            {
                var professional = await proffesionalRepository.GetByIdAsync(id);

                var professionalDTO = mapper.Map<ProfessionalDTO>(professional);

                return Ok(professionalDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<ProfessionalDTO>> GetListByName([FromQuery] string search)
        {
            try
            {
                var listProfessionals = await proffesionalRepository.GetListByNameAsync(search);

                var listProfessionalDTO = mapper.Map<List<ProfessionalDTO>>(listProfessionals);

                return Ok(listProfessionalDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
