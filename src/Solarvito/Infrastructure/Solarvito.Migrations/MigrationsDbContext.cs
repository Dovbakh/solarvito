using Microsoft.EntityFrameworkCore;
using Solarvito.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.Migrations
{
    public class MigrationsDbContext : SolarvitoContext
    {
        public MigrationsDbContext(DbContextOptions<MigrationsDbContext> options) : base(options)
        {
        }
    }
}
