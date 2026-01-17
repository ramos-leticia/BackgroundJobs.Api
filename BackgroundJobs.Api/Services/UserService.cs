using System;
using BackgroundJobs.Api.Context;

namespace BackgroundJobs.Api.Services;

public class UserService : IUserService
{
  private readonly AppDbContext _context;

  public UserService(AppDbContext context)
  {
    _context = context;
  }

  public async Task<User> CreateAsync(string name, string email)
  {
    var user = new User
    {
      Name = name,
      Email = email
    };

    _context.Users.Add(user);
    await _context.SaveChangesAsync();

    return user;
  }
}