using Api.Models.Request.Student;
using AutoMapper;
using Data.Models;

namespace Services.Maps
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<CreateStudentRequestModel, Student>();

            CreateMap<UpdateStudentRequestModel, Student>()
                .ForMember(destination => destination.FirstName, option => option.Condition(source => !string.IsNullOrWhiteSpace(source.FirstName)))
                .ForMember(destination => destination.LastName, option => option.Condition(source => !string.IsNullOrWhiteSpace(source.LastName)))
                .ForMember(destination => destination.BirthDate, option => option.Condition(source => source.BirthDate != null && DateTime.TryParse(source.BirthDate.ToString(), out DateTime result)));
        }
    }
}
