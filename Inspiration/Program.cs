using Inspiration.Domain.Services;
using Inspiration.Domain.Services.Interfaces;
using Inspiration.Infrastructure;
using Inspiration.Infrastructure.FileStorage;
using Inspiration.Repository;
using Inspiration.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<INPUService, NPUService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<INPURepository, NPURepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPartRepository, PartRepository>();
builder.Services.AddScoped<ICreativityRatingRepository, CreativityRatingRepository>();
builder.Services.AddScoped<IUniquenessRatingRepository, UniquenessRatingRepository>();
builder.Services.AddScoped<IFileStorage, FileStorage>();

builder.Services.AddInfrastructure(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler("/error");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
