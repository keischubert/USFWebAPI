
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using USFWebAPI.DTOs;
using USFWebAPI.Entities;
using USFWebAPI.Profiles;
using USFWebAPI.Services;


namespace USFWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //registro del servicio de automapper
            builder.Services.AddScoped<IRepository<Service>, ServiceRepository>();
            builder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
            builder.Services.AddScoped<IRepository<Gender>, GenderRepository>();
            builder.Services.AddScoped<IPersonRepository<Patient>, PatientRepository>();
            builder.Services.AddScoped<IPersonRepository<Professional>, ProfessionalRepository>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
