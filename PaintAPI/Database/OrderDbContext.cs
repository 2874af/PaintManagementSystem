using System;
using Paint.Models;
using Microsoft.EntityFrameworkCore;

namespace PaintAPI.Database;

public class OrderDbContext: DbContext
{
    public DbSet<PaintProduct> Paints { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<User> Users { get; set; }

    public OrderDbContext(DbContextOptions<OrderDbContext> dbContextOptions) : base(dbContextOptions)
    {
        
    }
}
