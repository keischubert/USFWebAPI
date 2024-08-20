using Microsoft.EntityFrameworkCore;
using USFWebAPI.Entities;

namespace USFWebAPI.Services
{
    public class GenderRepository : IRepository<Gender>
    {
        private readonly ApplicationDbContext context;

        public GenderRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task CreateAsync(Gender gender)
        {
            var existsName = await context.Genders.AnyAsync(g => g.Name.ToLower().Trim() == gender.Name.ToLower().Trim());

            if (existsName)
            {
                throw new Exception($"El Genero \"{gender.Name.Trim()}\" ya existe");
            }

            context.Genders.Add(gender);
            await context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Gender>> GetAllAsync()
        {
            var genders = await context.Genders.ToListAsync();

            return genders;
        }

        public async Task<Gender> GetByIdAsync(int id)
        {
            return await context.Genders.FindAsync(id);
        }
    }
}
