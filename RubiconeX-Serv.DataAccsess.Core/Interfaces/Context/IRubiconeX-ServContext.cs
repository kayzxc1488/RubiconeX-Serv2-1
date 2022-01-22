using Microsoft.EntityFrameworkCore;
using RubiconeX_Serv.DataAccsess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RubiconeX_Serv.DataAccsess.Core.Interfaces.Context
{
    public interface IRubiconeX_ServContext: IDisposable, IAsyncDisposable
    {
        DbSet<UserRto> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
