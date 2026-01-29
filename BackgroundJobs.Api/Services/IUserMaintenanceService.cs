namespace BackgroundJobs.Api.Services
{
    public interface IUserMaintenanceService
    {
        Task DeactivateInactiveUsersAsync();
    }
}
