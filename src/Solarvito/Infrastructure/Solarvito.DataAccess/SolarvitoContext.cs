using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Solarvito.DataAccess;
/// <summary>
/// Контекст БД
/// </summary>
public class SolarvitoContext : DbContext
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="ShoppingCartContext"/>.
    /// </summary>
    public SolarvitoContext(DbContextOptions options)
        : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), t => t.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

        base.OnModelCreating(modelBuilder);
    }
}