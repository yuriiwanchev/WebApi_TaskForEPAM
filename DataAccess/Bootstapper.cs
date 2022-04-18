using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DataAccess.Repositories;
using Domain.Models;
using Domain.Repositories;

namespace DataAccess
{
    public static class Bootstapper
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
        {
            return services
                .AddAutoMapper(typeof(MapperProfile))
                .AddDbContext<StudentsDbContext>(options => options.UseNpgsql(connectionString))
                .AddScoped<ICrudRepository<Student>, StudentsRepository>()
                .AddScoped<ICrudRepository<Lector>, LectorsRepository>()
                .AddScoped<ICrudRepository<Lection>, LectionRepository>()
                .AddScoped<ICrudRepository<Homework>, HomeworkRepository>()
                .AddScoped<ILectionLogRepository, LectionLogsRepository>();
        }
    }
}