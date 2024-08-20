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
    public class ServicesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRepository<Service> serviceRepository;
        private readonly IRepository<Specialty> specialtyRepository;

        public ServicesController(IMapper mapper, IRepository<Service> serviceRepository, IRepository<Specialty> specialtyRepository)
        {
            this.mapper = mapper;
            this.serviceRepository = serviceRepository;
            this.specialtyRepository = specialtyRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceDTO>>> GetServices()
        {
            //se obtienen todos los registros
            var services = await serviceRepository.GetAllAsync();

            //mapeo de los registros obtenidos para no devolver valores importantes
            var servicesDTO = mapper.Map<List<ServiceDTO>>(services);

            return Ok(servicesDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceDTO>> GetServiceById([FromRoute] int id)
        {
            //verificación si existe un servicio con el id enviado por el usuario
            var service = await serviceRepository.GetByIdAsync(id); 

            if(service == null)
            {
                return NotFound($"No existe ningún servicio con Id: {id}");
            }

            //mapeo del registro obtenido para no devolver valores importantes
            var serviceDTO = mapper.Map<ServiceDTO>(service);

            return Ok(serviceDTO);
        }


        [HttpPost]
        public async Task<ActionResult<CreateServiceDTO>> CreateService([FromBody] CreateServiceDTO createServiceDTO)
        {
            try
            {
                var service = mapper.Map<Service>(createServiceDTO);
                await serviceRepository.CreateAsync(service);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(createServiceDTO);
        }
    }
}
