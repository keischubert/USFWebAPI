using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using USFWebAPI.Entities;

namespace USFWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public GendersController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Gender gender)
        {
            context.Genders.Add(gender);
            await context.SaveChangesAsync();

            return Ok(gender);
        }
    }
}
