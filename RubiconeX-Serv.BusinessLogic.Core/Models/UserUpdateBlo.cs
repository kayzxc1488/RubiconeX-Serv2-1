using System;
using System.Collections.Generic;
using System.Text;

namespace RubiconeX_Serv.BusinessLogic.Core.Models
{
    public class UserUpdateBlo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronumic { get; set; }
        public string AvatarUrl { get; set; }
        public string Introdution { get; set; }
        public bool IsBoy { get; set; }
        public DateTimeOffset Birthday { get; set; }
        public string Password { get; set; }
    }
}
