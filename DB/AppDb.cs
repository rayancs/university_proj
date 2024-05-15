using Microsoft.EntityFrameworkCore;
using System;
using university_proj.DataModels;

namespace university_proj.DB;

public class AppDb : DbContext
{
    public AppDb(DbContextOptions<AppDb> options) : base(options) { }

    public DbSet<ShoeModel> Shoe { get; set; }
}
