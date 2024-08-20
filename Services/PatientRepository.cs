using Microsoft.EntityFrameworkCore;
using USFWebAPI.Entities;

namespace USFWebAPI.Services
{
    public class PatientRepository : IPersonRepository<Patient>
    {
        private readonly ApplicationDbContext context;

        public PatientRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task CreateAsync(Patient patient)
        {
            var genderExists = await context.Genders
                .AnyAsync(gender => gender.Id == patient.GenderId);

            var patientCiExists = await context.Patients.AnyAsync(p => p.Ci.Trim() == patient.Ci.Trim());

            //validar que no exista otro paciente con ese mismo CI
            if (patientCiExists)
            {
                throw new Exception($"El CI \"{patient.Ci}\" ya esta registrado");
            }

            //validar que exista el genero
            if (!genderExists)
            {
                throw new Exception($"El Genero con Id {patient.GenderId} no existe");
            }


            //si pasa las validaciones agregar al paciente a la db
            context.Patients.Add(patient);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await context.Patients.Include(patient => patient.Gender).ToListAsync();
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            var patient = await context.Patients.FindAsync(id);

            return patient;
        }

        public async Task<Patient> GetByCiAsync(string ci)
        {
            var patient = await context.Patients
                .Include(p => p.Gender)
                .FirstOrDefaultAsync(p => string.Equals(p.Ci, ci));

            return patient;
        }

        public async Task<IEnumerable<Patient>> GetListByNameAsync(string searchName)
        {
            var patients = await context.Patients
                .Include(p => p.Gender)
                .Where(p => p.Name.Contains(searchName))
                .ToListAsync();

            return patients;

        }
    }
}
