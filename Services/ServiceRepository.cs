using AutoMapper;
using Microsoft.EntityFrameworkCore;
using USFWebAPI.DTOs;
using USFWebAPI.Entities;
using USFWebAPI.Migrations;

namespace USFWebAPI.Services
{
    public class ServiceRepository : IRepository<Service>
    {
        private readonly ApplicationDbContext context;
        public ServiceRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Service> GetByIdAsync(int id)
        {
            var service = await context.Services
                .Include(s => s.Specialty)
                .FirstOrDefaultAsync(s => s.Id == id);

            return service;
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            var services = await context.Services
                .Include(s => s.Specialty)
                .ToListAsync();

            return services;
        }

        public async Task CreateAsync(Service service)
        {
            //verificacion de la existencia de la especialidad
            var specialty = await context.Specialties.FindAsync(service.SpecialtyId);

            if (specialty == null)
            {
                throw new Exception($"La especialidad con Id: {service.SpecialtyId} no existe");
            }

            context.Services.Add(service);
            await context.SaveChangesAsync();
        }
    }
}
