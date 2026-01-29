using BackgroundJobs.Api.Context;
using Microsoft.EntityFrameworkCore;

namespace BackgroundJobs.Api.Services
{
    public class UserMaintenanceService : IUserMaintenanceService
    {
        private readonly AppDbContext _context;

        public UserMaintenanceService(AppDbContext context)
        {
            _context = context;
        }
        public async Task DeactivateInactiveUsersAsync()
        {
            var usersToDeactivate = await _context.Users
                .Where(u => u.IsActive)
                .ToListAsync();

            if (!usersToDeactivate.Any())
                return;

            foreach (var user in usersToDeactivate)
            {
                user.IsActive = false;
            }

            await _context.SaveChangesAsync();

            Console.WriteLine($"[MAINTENANCE] {usersToDeactivate.Count} usuários desativados.");
        }
    }
}
