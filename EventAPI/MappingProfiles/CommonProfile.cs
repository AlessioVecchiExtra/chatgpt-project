using AutoMapper;
using EventAPI.Dto;
using EventAPI.Models;

namespace MyEventApi.MappingProfiles
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<Vote, VoteDto>().ReverseMap();
            CreateMap<Meeting, MeetingDto>().ReverseMap();
            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<QuestionAnswer, QuestionAnswerDto>().ReverseMap();
        }
    }

}