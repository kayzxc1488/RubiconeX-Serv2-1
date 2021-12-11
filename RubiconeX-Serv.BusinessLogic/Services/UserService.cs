using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RubiconeX_Serv.BusinessLogic.Core.Interfaces;
using RubiconeX_Serv.BusinessLogic.Core.Models;
using RubiconeX_Serv.DataAccsess.Core.Interfaces.Context;
using RubiconeX_Serv.DataAccsess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using RubiconeX_Serv.Shared.Exception;

namespace RubiconeX_Serv.BusinessLogic.Services
{
    public class UserService : IUseerServece
    {
        private readonly IMapper _mapper;
        private readonly IRubiconeX_ServContext _context;

        public UserService(IMapper mapper, IRubiconeX_ServContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserInformationBlo> Auth(int PhoneNumberPrefix, int PhoneNumber, string password)
        {
          UserRto user = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumberPrefix == PhoneNumberPrefix && x.PhoneNumber == PhoneNumber && x.Password == password);


            if (user == null)
                throw new NotFoundExeption("Неверный номер телефона, или пароль");

            return await ConvertToUserInformationBlo(user);
        }

        public Task<bool> DoesExist(int phoneNumberPreFix, int phoneNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<UserInformationBlo> Get(int UserId)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(y => y.UserId == UserId);
            if (user == null)
                throw new NotFoundExeption("Неверный айди пользователя");
            return await ConvertToUserInformationBlo(user);
        }

        public async Task<UserInformationBlo> Registration(int phoneNumberPrefix, int phoneNumber, string password)
        {
            UserRto newUser = new UserRto()
            { PhoneNumberPrefix = phoneNumberPrefix, PhoneNumber = phoneNumber, Password = password, };

            _context.Users.Add(newUser);

            await _context.SaveChangesAsync();

            return await ConvertToUserInformationBlo(newUser);
        }

        public async Task<UserInformationBlo> Update(int phoneNumberPrefix, int phoneNumber, string password, UserUpdateBlo userUpdateBlo)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumberPrefix == phoneNumberPrefix && x.PhoneNumber == phoneNumber && x.Password == password);

            if (user == null)
                throw new NotFoundExeption("Неверный номер телефона, или пароль");

            if (userUpdateBlo.IsBoy != null) user.IsBoy = userUpdateBlo.IsBoy;
            if (userUpdateBlo.Password != null) user.Password = userUpdateBlo.Password;
            if (userUpdateBlo.FirstName != null) user.FirstName = userUpdateBlo.FirstName;
            if (userUpdateBlo.LastName != null) user.LastName = userUpdateBlo.LastName;
            if (userUpdateBlo.Patronumic != null) user.Patronumic = userUpdateBlo.Patronumic;
            if (userUpdateBlo.AvatarUrl != null) user.AvatarUrl = userUpdateBlo.AvatarUrl;
            if (userUpdateBlo.Birthday != null) user.Birthday = userUpdateBlo.Birthday;

            await _context.SaveChangesAsync();

            return await ConvertToUserInformationBlo(user);
        }

        private async Task<UserInformationBlo> ConvertToUserInformationBlo(UserRto userRto)
        {

            if(userRto == null)
                throw new ArgumentNullException(nameof(userRto));

          UserInformationBlo userInformationBlo = _mapper.Map<UserInformationBlo>(userRto);

          return userInformationBlo;
        }
    }
}
