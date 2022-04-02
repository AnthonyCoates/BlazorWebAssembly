using TestBarberPWA.Shared;

namespace TestBarberPWA.Server.Models
{
    public interface IAppointmentsRepository
    {
        Task<IEnumerable<Appointment>> Search(string? note, DateTime? dateTime);
        Task<IEnumerable<Appointment>> GetAppointments();
        Task<IEnumerable<Appointment>> GetAppointments(int personID);
        Task<IEnumerable<Appointment>> GetAppointments(DateTime dateTime);
        Task<IEnumerable<Appointment>> GetCustomerAppointments(int customerID);
        Task<IEnumerable<Appointment>> GetEmployeeAppointments(int employeeID);

        Task<Appointment> GetAppointment(int appointmentID);

        Task<Appointment> AddAppointment(Appointment appointment);
        Task<Appointment> UpdateAppointment(Appointment appointment);
        Task DeleteAppointment(int appointmentID);

    }
}
