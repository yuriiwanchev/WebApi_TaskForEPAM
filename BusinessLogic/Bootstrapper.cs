using BusinessLogic.Functional;
using BusinessLogic.Services;
using Domain.Models;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            return services
                .AddScoped<ICrudService<Student>, CrudService<Student>>()
                .AddScoped<ICrudService<Lector>, CrudService<Lector>>()
                .AddScoped<ICrudService<Lection>, CrudService<Lection>>()
                .AddScoped<ICrudService<Homework>, CrudService<Homework>>()
                .AddScoped<ILectionLogService, LectionLogService>()
                .AddTransient<IStudentNotificationService, StudentNotification>()
                .AddTransient<IAttendanceReport, AttendanceReport>();
        }
    }
}