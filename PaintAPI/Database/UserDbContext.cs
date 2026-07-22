using System;
using Microsoft.EntityFrameworkCore;
using Paint.Models;

namespace PaintAPI.Database;

public class UserDbContext: DbContext
{
    public DbSet<PaintProduct> Paints { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<User> Users { get; set; }

    public UserDbContext(DbContextOptions<UserDbContext> dbContextOptions) : base(dbContextOptions)
    {
        
    }

}
