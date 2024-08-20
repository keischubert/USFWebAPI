using Microsoft.EntityFrameworkCore;
using USFWebAPI.Entities;

namespace USFWebAPI.Services
{
    public class SpecialtyRepository : ISpecialtyRepository
    {
        private readonly ApplicationDbContext context;

        public SpecialtyRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(Specialty specialty)
        {
            var nameExists = await context.Specialties.AnyAsync(x => x.Name.Equals(specialty.Name));

            if (nameExists)
            {
                throw new Exception($"{specialty.Name} ya existe");
            }

            context.Specialties.Add(specialty);

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Specialty>> GetAllAsync()
        {
            var specialties = await context.Specialties
                .ToListAsync();

            if (specialties.Count == 0)
            {
                throw new Exception("No se han econtrado recursos");
            }

            return specialties;
        }

        public async Task<Specialty> GetByIdAsync(int id)
        {
            var specialty = await context.Specialties
                .FindAsync(id);

            if (specialty == null)
            {
                throw new Exception($"No se ha encontrado el recurso con Id {id}");
            }

            return specialty;
        }

        public async Task<IEnumerable<Professional>> GetProfessionalsBySpecialtyIdAsync(int specialtyId)
        {
            var listProfessionals = await context.Professionals
                .Include(p => p.Gender)
                .Include(p => p.Specialty)
                .Where(p => p.SpecialtyId == specialtyId)
                .ToListAsync();

            if (listProfessionals.Count == 0)
            {
                throw new Exception($"No se han encontrado recursos con el Id {specialtyId}");
            }

            return listProfessionals;
        }

        public async Task<IEnumerable<Service>> GetServicesBySpecialtyIdAsync(int specialtyId)
        {
            var listServices = await context.Services
                .Include(s => s.Specialty)
                .Where(s => s.SpecialtyId == specialtyId)
                .ToListAsync();

            if (listServices.Count == 0)
            {
                throw new Exception($"No se han encontrado recursos con el Id {specialtyId}");
            }

            return listServices;
        }
    }
}
