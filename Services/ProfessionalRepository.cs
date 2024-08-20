using Microsoft.EntityFrameworkCore;
using USFWebAPI.Entities;

namespace USFWebAPI.Services
{
    public class ProfessionalRepository : IPersonRepository<Professional>
    {
        private readonly ApplicationDbContext context;

        public ProfessionalRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(Professional professional)
        {
            var multipleExceptions = new List<Exception>();

            //validar que no exista ese mismo CI
            var ciExists = await context.Professionals
                .AnyAsync(p => p.Ci == professional.Ci) || await context.Patients
                .AnyAsync(p => p.Ci == professional.Ci);

            if (ciExists)
            {
                multipleExceptions.Add(new Exception($"El CI: {professional.Ci} ya esta registrado"));
                //throw new Exception($"El CI \"{professional.Ci}\" ya esta registrado");
            }

            //validar que exista el genero
            var genderExists = await context.Genders
                .AnyAsync(g => g.Id == professional.GenderId);

            if (!genderExists)
            {
                multipleExceptions.Add(new Exception($"El Genero con Id: {professional.GenderId} no existe"));
                //throw new Exception($"El Genero con Id \"{professional.GenderId}\" no existe");
            }

            //validar que exista la especialidad
            var specialtyExists = await context.Specialties
                .AnyAsync(s => s.Id == professional.SpecialtyId);

            if (!specialtyExists)
            {
                multipleExceptions.Add(new Exception($"La especialidad con Id: {professional.SpecialtyId} no existe"));
                //throw new Exception($"La especialidad con Id \"{professional.SpecialtyId}\" no existe");
            }

            if (multipleExceptions.Count > 0)
            {
                throw new MultipleErrorsException(multipleExceptions);
            } 

            //agregar el paciente si pasa las validaciones
            context.Professionals.Add(professional);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Professional>> GetAllAsync()
        {
            var listProffesionals = await context.Professionals
                .ToListAsync();

            if(listProffesionals.Count == 0)
            {
                throw new Exception("No se econtraron recursos");
            }

            return listProffesionals;

        }

        public async Task<Professional> GetByCiAsync(string ci)
        {
            var proffesional = await context.Professionals
                .FirstOrDefaultAsync(p => p.Ci == ci);
            
            if(proffesional == null)
            {
                throw new Exception("Ha ocurrido un error al solicitar el recurso");
            }

            return proffesional;
        }

        public async Task<Professional> GetByIdAsync(int id)
        {
            var professional = await context.Professionals.FindAsync(id);

            if(professional == null)
            {
                throw new Exception("Ha ocurrido un error al solicitar el recurso");
            }

            return professional;
        }

        public async Task<IEnumerable<Professional>> GetListByNameAsync(string name)
        {
            var listProfessionals = await context.Professionals
                .Where(p => p.Name.Contains(name))
                .ToListAsync();

            if (listProfessionals.Count == 0)
            {
                throw new Exception("No existen profesionales que coincidan con esa busqueda");
            }

            return listProfessionals;
            
        }
    }
}
