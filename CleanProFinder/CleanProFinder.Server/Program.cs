using CleanProFinder.Db;
using CleanProFinder.Server.BuildExtensions;
using CleanProFinder.Server.Hubs;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSetSwagger();
builder.Services.AddMediatR(cfg =>
     cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddLogging();
builder.Services.AddServices();
builder.Services.AddSetSecurity(builder.Configuration);
builder.Services.AddSetCors();
builder.Services.AddDbSetup(builder.Configuration);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddValidators();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors(CorsInjection.PolicyName);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationHub>("/notifications");

app.Run();
