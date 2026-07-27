using PaintAPI.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddDbContext<PaintDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PaintDb")));
builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("UserDb")));
builder.Services.AddDbContext<OrderDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDb")));



var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
