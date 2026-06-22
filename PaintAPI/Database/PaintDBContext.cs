using System;
using Microsoft.EntityFrameworkCore;
using Paint.Models;

namespace PaintAPI.Database;

public class PaintDBContext: DbContext
{
    public DbSet<PaintProduct> Paints { get; set; }

    public PaintDBContext(DbContextOptions<PaintDBContext> dbContextOptions) : base(dbContextOptions)
    {
        
    }
}
