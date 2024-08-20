using Microsoft.EntityFrameworkCore;
using USFWebAPI.Entities;

namespace USFWebAPI.Services
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task CreateAsync(Appointment appointment)
        {
            var multipleErrors = new List<Exception>();

            //validar si el paciente existe
            var patient = await context.Patients.FirstOrDefaultAsync(p => p.Id == appointment.PatientId);
            if (patient == null)
            {
                multipleErrors.Add(new Exception($"El paciente con Id {appointment.PatientId} no exciste"));
            }

            //validar que la especialidad existe
            var specialty = await context.Specialties.FirstOrDefaultAsync(s => s.Id == appointment.SpecialtyId);
            if (specialty == null)
            {
                multipleErrors.Add(new Exception($"La especialidad con Id {appointment.SpecialtyId} no existe"));
            }

            //validar que el profesional exista
            var professional = await context.Professionals.FirstOrDefaultAsync(p => p.Id == appointment.ProfessionalId);
            if (professional == null)
            {
                multipleErrors.Add(new Exception($"El profesional con Id {appointment.ProfessionalId} no existe"));
            }

            //validar que el profesional tenga la especialidad necesaria
            if (specialty != null && professional != null)
            {
                if(specialty.Id != professional.SpecialtyId)
                {
                    multipleErrors.Add(new Exception($"La especialidad del profesional no coincide con la especialidad necesaria para la cita"));
                }
            }

            

            //obtener
            var appointments = await context.Appointments
                .Where(a => a.SpecialtyId == appointment.SpecialtyId && a.Date == DateOnly.FromDateTime(DateTime.Now))
                .ToListAsync();

            //validar que un paciente no pueda pedir 2 turnos para una especialidad
            var patientHasAppointment = appointments
                .Where(a => a.PatientId == appointment.PatientId)
                .ToList();
            if (patientHasAppointment.Count >= 1) 
            {
                multipleErrors.Add(new Exception($"El paciente con Id {appointment.PatientId} ya tiene registrado una cita con la especialidad {appointment.SpecialtyId}"));
            }

            if (appointments.Count >= 10)
            {
                multipleErrors.Add(new Exception($"Solo se pueden agendar 10 turnos por dia"));
            }
            else
            {
                appointment.QueueNumber = appointments.Count + 1;
            }

            //lanzar los errores si se encuentra alguno
            if (multipleErrors.Count > 0)
            {
                throw new MultipleErrorsException(multipleErrors);
            }

            //se procede a guardar la cita
            context.Appointments.Add(appointment);
            await context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            //validar que exista la cita
            var appointment = await context.Appointments
                .FindAsync(id);

            if (appointment == null)
            {
                throw new Exception($"El AppointmentId {id} no existe");
            }

            //actualizar los turnos de los demas pacientes
            var appointments = await context.Appointments
                .Where(a => a.SpecialtyId == appointment.SpecialtyId && a.Date == appointment.Date && a.QueueNumber > appointment.QueueNumber)
                .ToListAsync();
            
            foreach(var a in appointments)
            {
                a.QueueNumber--; //actualiza la propiedad del recurso
            }

            //se procede a eliminar el recurso
            context.Appointments.Remove(appointment);
            
            //se procede a guardar todos los cambios
            await context.SaveChangesAsync();

            
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            var appointments = await context.Appointments
                .ToListAsync();

            if(appointments.Count == 0)
            {
                throw new Exception("No se han encontrado recursos");
            }

            return appointments;
        }

        /*
         *Este metodo obtiene recursos de Appointments segun la fecha. 
         *Puede obtener citas segun la fecha y especialidad.
         *Si no existen recursos con la especialidad segun la fecha, se obtendran los recursos de la fecha
         *de todas las especialidades.
         *Si no se le pasa un valor a date, este tomara el valor del dia actual
        */
        public async Task<IEnumerable<Appointment>> GetAppointmentsByDate(int specialtyId, string date)
        {
            if(date == null)
            {
                date = DateOnly.FromDateTime(DateTime.Now).ToString();
            }

            if (!DateOnly.TryParse(date, out DateOnly dateParsed))
            {
                throw new FormatException("La fecha no es valida yyyy-mm-dd");
            }

            var appointments = await context.Appointments
                  .Where(a => (a.SpecialtyId == specialtyId && a.Date == dateParsed) || (a.Date == dateParsed))
                  .ToListAsync();




            if (appointments.Count == 0)
            {
                throw new Exception("No se han encontrado recursos");
            }

            return appointments;
        }

        public async Task<Appointment> GetByIdAsync(int id)
        {
            var appointment = await context.Appointments.FindAsync(id);

            if(appointment == null)
            {
                throw new Exception("No se ha encontrado el recurso");
            }

            return appointment;


        }
    }
}
