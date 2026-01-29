using Hangfire;
using Hangfire.Storage.SQLite;
using Scalar.AspNetCore;
using BackgroundJobs.Api.Context;
using Microsoft.EntityFrameworkCore;
using BackgroundJobs.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ApplicationDb"))
);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHangfire(config =>
{
    config.UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseSQLiteStorage(
              builder.Configuration.GetConnectionString("HangfireDb")
          );
});
builder.Services.AddHangfireServer();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUserMaintenanceService, UserMaintenanceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("API Background Jobs")
               .WithTheme(ScalarTheme.Solarized);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard("/hangfire");

RecurringJob.AddOrUpdate<UserMaintenanceService>(
  "deactivate-users-job",
  service => service.DeactivateInactiveUsersAsync(),
  Cron.Minutely
);

app.MapControllers();

app.Run();
