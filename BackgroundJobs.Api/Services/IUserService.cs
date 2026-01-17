using System;

namespace BackgroundJobs.Api.Services;

public interface IUserService
{
  Task<User> CreateAsync(string name, string email);
}
