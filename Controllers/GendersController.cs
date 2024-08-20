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
    public class GendersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRepository<Gender> genderRepository;

        public GendersController(IMapper mapper, IRepository<Gender> genderRepository)
        {
            this.mapper = mapper;
            this.genderRepository = genderRepository;
        }

        [HttpPost]
        public async Task<ActionResult<CreateGenderDTO>> CreateGender(CreateGenderDTO createGenderDTO)
        {
            try
            {
                var gender = mapper.Map<Gender>(createGenderDTO);

                await genderRepository.CreateAsync(gender);

                return Ok(createGenderDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }       
        }

        [HttpGet]
        public async Task<ActionResult<List<Gender>>> GetGenders()
        {
            var genders = await genderRepository.GetAllAsync();

            var gendersDTO = mapper.Map<List<GenderDTO>>(genders);

            return Ok(gendersDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GenderDTO>> GetGender(int id)
        {
            var gender = await genderRepository.GetByIdAsync(id);

            if(gender == null)
            {
                return BadRequest($"No existe ningun genero con Id \"{id}\"");
            }

            var genderDTO = mapper.Map<GenderDTO>(gender);

            return Ok(genderDTO);
        }
    }
}
