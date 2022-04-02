using Microsoft.EntityFrameworkCore;
using TestBarberPWA.Shared;

namespace TestBarberPWA.Server.Models
{
    public class AppointmentsRepository : IAppointmentsRepository
    {
        private readonly AppDBContext appDBContext;

        public AppointmentsRepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public async Task<Appointment> GetAppointment(int appointmentID)
        {
            return await appDBContext.Appointments
                .FirstOrDefaultAsync(a => a.AppointmentID == appointmentID);
        }

        public async Task<IEnumerable<Appointment>> GetAppointments()
        {
            return await appDBContext.Appointments.ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointments(int personID)
        {
            IQueryable<Appointment> query = appDBContext.Appointments;

            return await query.Where(a => a.EmployeeID == personID || a.CustomerID == personID).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointments(DateTime dateTime)
        {
            IQueryable<Appointment> query = appDBContext.Appointments;

            return await query.Where(a => a.DateTime == dateTime).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetCustomerAppointments(int customerID)
        {
            IQueryable<Appointment> query = appDBContext.Appointments;

            return await query.Where(a =>a.CustomerID == customerID).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetEmployeeAppointments(int employeeID)
        {
            IQueryable<Appointment> query = appDBContext.Appointments;

            return await query.Where(a => a.EmployeeID == employeeID).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> Search(string? note, DateTime? dateTime)
        {
            IQueryable<Appointment> query = appDBContext.Appointments;

            if (!string.IsNullOrEmpty(note))
            {
                query = query.Where(a => a.Notes.Contains(note));
            }

            if (!dateTime.HasValue)
            {
                query = query.Where(a => a.DateTime == dateTime);
            }

            return await query.ToListAsync();
        }

        public async Task<Appointment> AddAppointment(Appointment appointment)
        {
            var result = await appDBContext.Appointments.AddAsync(appointment);
            await appDBContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Appointment> UpdateAppointment(Appointment appointment)
        {
            var result = await appDBContext.Appointments.FirstOrDefaultAsync(a => appointment.AppointmentID == appointment.AppointmentID);

            if (result != null)
            {
                result.EmployeeID = appointment.EmployeeID;
                result.CustomerID = appointment.CustomerID;
                result.DateTime = appointment.DateTime;
                result.Notes = appointment.Notes;

                await appDBContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task DeleteAppointment(int appointmentID)
        {
            var result = await appDBContext.Appointments.FirstOrDefaultAsync(a => a.AppointmentID == appointmentID);

            if (result != null)
            {
                appDBContext.Appointments.Remove(result);
                await appDBContext.SaveChangesAsync();
            }
        }
    }
}
