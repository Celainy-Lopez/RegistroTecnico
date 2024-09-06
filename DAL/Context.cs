﻿using Microsoft.EntityFrameworkCore;
using RegistroTecnico.Models;
using SQLitePCL;

namespace RegistroTecnico.DAL;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
	public DbSet<Tecnicos> Tecnicos { get; set; }
}
