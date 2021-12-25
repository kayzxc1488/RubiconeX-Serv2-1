using AutoMapper;
using RubiconeX_Serv.BusinessLogic.Core.Models;
using RubiconeX_Serv.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RubiconeX_Serv.AutomapperProfile
{
    public class MicroserviceProfile : Profile
    {
        public MicroserviceProfile()
        {
            CreateMap<UserInformationBlo, UserinformationDto>();
        }
    }
}
