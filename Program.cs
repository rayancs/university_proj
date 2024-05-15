 using Microsoft.EntityFrameworkCore;
using university_proj.DB;
using university_proj.Service;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDb>(options =>
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ShoeService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
