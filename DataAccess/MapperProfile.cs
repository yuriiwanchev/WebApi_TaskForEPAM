using AutoMapper;
using DataAccess.Models;
using Domain.Models;

namespace DataAccess
{
    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<StudentDb, Student>().ReverseMap();
            CreateMap<LectorDb, Lector>().ReverseMap();
            CreateMap<LectionDb, Lection>().ReverseMap();
            CreateMap<HomeworkDb, Homework>().ReverseMap();
            
            CreateMap<LectionLogDb, LectionLog>().ReverseMap();
        }
    }
}