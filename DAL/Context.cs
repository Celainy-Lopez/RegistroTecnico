using Microsoft.EntityFrameworkCore;
using RegistroTecnico.Models;
using SQLitePCL;

namespace RegistroTecnico.DAL;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options)
        : base(options){ }

    public DbSet<Tecnicos> Tecnicos { get; set; }
}
