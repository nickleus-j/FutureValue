using FutureValue.Persistence.EfImplementation.Shared;
using FutureValue.Persistence.Shared;
using FutureValue.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;

InitiatorHelperSingleton singleton= InitiatorHelperSingleton.Instance;
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FutureValueContext>(options =>
{
    options.UseSqlServer(singleton.GetConnectionString(builder));
}); 

builder.Services.AddTransient<IFutureValueContext, FutureValueContext>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<FutureValueContext>();
    context.Database.EnsureCreated();
    context.SeedData();
}
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(x => x.AllowAnyHeader()
      .AllowAnyMethod()
      .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost"));
app.MapControllers();

app.Run();
