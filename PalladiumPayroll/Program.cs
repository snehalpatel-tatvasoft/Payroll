using AutoMapper;
using PalladiumPayroll.DataContext;
using PalladiumPayroll.Helper;
using PalladiumPayroll.Helper.Middleware.Encryption;
using PalladiumPayroll.Helper.Middleware.Exceptions;
using PalladiumPayroll.JWT;
using PalladiumPayroll.Repositories;
using PalladiumPayroll.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DI for Repositories, services, common and JWT
builder.Services.AddTransient<DapperContext>();
builder.Services.AddServiceRepositories();
builder.Services.AddServices();
builder.Services.AddHelpers(builder.Configuration);
builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Mapper));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<EncryptionMiddleware>();
app.UseMiddleware<UpdateActivityMiddleware>();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

