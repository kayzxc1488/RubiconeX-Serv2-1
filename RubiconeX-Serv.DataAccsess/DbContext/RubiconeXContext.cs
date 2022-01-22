using Microsoft.EntityFrameworkCore;
using RubiconeX_Serv.DataAccsess.Core.Interfaces.Context;
using RubiconeX_Serv.DataAccsess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RubiconeX_Serv.DataAccsess.DbContext
{
    public class RubiconeXContext : Microsoft.EntityFrameworkCore.DbContext, IRubiconeX_ServContext
    {
        public RubiconeXContext(DbContextOptions<RubiconeXContext> options) : base(options)
        {

        }
        public DbSet<UserRto> Users { get; set; }
    }
}
