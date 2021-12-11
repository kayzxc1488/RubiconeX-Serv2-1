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
        /// <summary>
        /// Авторизация пользователя в системе
        /// </summary>
        /// <param name="PhoneNumberPrefix">Код страны телефона</param>
        /// <param name="PhoneNumber">Сам телефон</param>
        /// <param name="Password">Пароль</param>
        /// <returns>Объект UserInformationBlo</returns>
        public async Task<UserInformationBlo> Auth(int PhoneNumberPrefix, int PhoneNumber, string Password)
        {
          UserRto user = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumberPrefix == PhoneNumberPrefix && x.PhoneNumber == PhoneNumber && x.Password == Password);


            if (user == null)
                throw new NotFoundExeption("Неверный номер телефона, или пароль");

            return await ConvertToUserInformationBlo(user);
        }
        /// <summary>
        /// Проверяет не занят ли номер телефона 
        /// </summary>
        /// <param name="PhoneNumberPrefix">Код страны</param>
        /// <param name="PhoneNumber">Сам номер</param>
        /// <returns>true- занят, false- не занят</returns>
        public async Task<bool> DoesExist(int PhoneNumberPrefix, int PhoneNumber)
        {
            return await _context.Users.AnyAsync(x => x.PhoneNumberPrefix == PhoneNumberPrefix && x.PhoneNumber == PhoneNumber);
            
        }
       /// <summary>
       /// Возвращает информацию о пользователе
       /// </summary>
       /// <param name="UserId">Ид пользователя </param>
       /// <returns></returns>
        public async Task<UserInformationBlo> Get(int UserId)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(y => y.UserId == UserId);
            if (user == null)
                throw new NotFoundExeption("Неверный айди пользователя");
            return await ConvertToUserInformationBlo(user);
        }
        /// <summary>
        /// Создает нового пользователя в сети
        /// </summary>
        /// <param name="PhoneNumberPrefix">Код страны</param>
        /// <param name="PhoneNumber">сам номер</param>
        /// <param name="Password">пароль</param>
        /// <returns>Объект UserInformationBlo</returns>
        public async Task<UserInformationBlo> Registration(int PhoneNumberPrefix, int PhoneNumber, string Password)
        {
            UserRto newUser = new UserRto()
            { PhoneNumberPrefix = PhoneNumberPrefix, PhoneNumber = PhoneNumber, Password = Password, };

            _context.Users.Add(newUser);

            await _context.SaveChangesAsync();

            return await ConvertToUserInformationBlo(newUser);
        }
        /// <summary>
        /// обновляет уже зарегистрованного пользователя 
        /// </summary>
        /// <param name="PhoneNumberPrefix">Код страны</param>
        /// <param name="PhoneNumber">сам номер</param>
        /// <param name="Password">пароль</param>
        /// <param name="userUpdateBlo">Пакет новой инфы, которой надо заменить старую</param>
        /// <returns>Объект UserInformationBlo</returns>
        public async Task<UserInformationBlo> Update(int PhoneNumberPrefix, int PhoneNumber, string Password, UserUpdateBlo userUpdateBlo)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumberPrefix == PhoneNumberPrefix && x.PhoneNumber == PhoneNumber && x.Password == Password);

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
        /// <summary>
        /// Конвертирует из UserRto в User InformationBlo
        /// </summary>
        /// <param name="userRto"></param>
        /// <returns>объект UserInformationBlo</returns>
        private async Task<UserInformationBlo> ConvertToUserInformationBlo(UserRto userRto)
        {

            if(userRto == null)
                throw new ArgumentNullException(nameof(userRto));

          UserInformationBlo userInformationBlo = _mapper.Map<UserInformationBlo>(userRto);

          return userInformationBlo;
        }
    }
}
