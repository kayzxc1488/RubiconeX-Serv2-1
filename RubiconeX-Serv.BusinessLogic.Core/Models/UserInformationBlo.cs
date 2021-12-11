using System;
using System.Collections.Generic;
using System.Text;

namespace RubiconeX_Serv.BusinessLogic.Core.Models
{
    public class UserInformationBlo
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronumic { get; set; }
        public string AvatarUrl { get; set; }
        public DateTimeOffset Birtday { get; set; }
        public bool IsBoy { get; set; }
    }
}
