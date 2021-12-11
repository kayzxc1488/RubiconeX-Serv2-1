using AutoMapper;
using RubiconeX_Serv.BusinessLogic.Core.Models;
using RubiconeX_Serv.DataAccsess.Core.Models;

namespace RubiconeX_Serv.BusinessLogic.AutoMapperProfile
{
    public class BusinessLogicProfile : Profile
    {
        public BusinessLogicProfile()
        {
            CreateMap<UserRto, UserInformationBlo>()
                .ForMember(x => x.Id, x => x.MapFrom(m => m.Id))
                .ForMember(x => x.IsBoy, x => x.MapFrom(m => m.IsBoy))
                .ForMember(x => x.FirstName, x => x.MapFrom(m => m.FirstName))
                .ForMember(x => x.LastName, x => x.MapFrom(m => m.LastName))
                .ForMember(x => x.Patronumic, x => x.MapFrom(m => m.Patronumic))
                .ForMember(x => x.AvatarUrl, x => x.MapFrom(m => m.AvatarUrl))
                .ForMember(x => x.Birtday, x => x.MapFrom(m => m.Birthday));
            CreateMap<UserRto, UserUpdateBlo>()
               .ForMember(x => x.Id, x => x.MapFrom(m => m.Id))
               .ForMember(x => x.IsBoy, x => x.MapFrom(m => m.IsBoy))
               .ForMember(x => x.FirstName, x => x.MapFrom(m => m.FirstName))
               .ForMember(x => x.LastName, x => x.MapFrom(m => m.LastName))
               .ForMember(x => x.Patronumic, x => x.MapFrom(m => m.Patronumic))
               .ForMember(x => x.AvatarUrl, x => x.MapFrom(m => m.AvatarUrl))
               .ForMember(x => x.Birthday, x => x.MapFrom(m => m.Birthday));


        }
    }
}
