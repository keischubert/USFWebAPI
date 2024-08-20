using USFWebAPI.Entities;

namespace USFWebAPI.Services
{
    public interface ISpecialtyRepository : IRepository<Specialty>
    {
        public Task<IEnumerable<Professional>> GetProfessionalsBySpecialtyIdAsync(int specialtyId);
        public Task<IEnumerable<Service>> GetServicesBySpecialtyIdAsync(int specialtyId);
    }
}
