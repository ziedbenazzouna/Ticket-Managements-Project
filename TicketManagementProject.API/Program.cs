using TicketManagementProject.API.Extensions;
using TicketManagementProject.API.Repository;
using TicketManagementProject.API.Repository.Interfaces;
using TicketManagementProject.API.Services;
using TicketManagementProject.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppConfig(builder.Configuration);

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ITicketRepository,TicketRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITicketService, TicketService>();

builder.Services.ConfigureCors(builder.Configuration).AddAuth(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.AddCors().AddAuthMiddlewares();

app.MapControllers();

app.Run();

public partial class Program { }
