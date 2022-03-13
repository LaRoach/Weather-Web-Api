using WeatherWebApi.Models.Users;
using AutoMapper;

namespace WeatherWebApi.Models
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            // User -> UserLoginResponse
            CreateMap<User, UserLoginResponse>();

            // UserRegisterRequest -> User
            CreateMap<UserRegisterRequest, User>();

            // User -> UserProfileResponse
            CreateMap<User, UserProfileResponse>();
        }
    }
}