using USFWebAPI.Entities;

namespace USFWebAPI.Services
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        public Task DeleteById(int id);
        public Task<IEnumerable<Appointment>> GetAppointmentsByDate(int specialtyId, string date);
    }
}
