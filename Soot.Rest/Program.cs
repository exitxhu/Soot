using Microsoft.EntityFrameworkCore;
using Soot.Application.Command;
using Soot.Application.Email;
using Soot.Application.Helpers;
using Soot.Application.Sms;
using Soot.Db.Ef;
using Soot.Db.Ef.Helpers;
using Soot.Db.Ef.RepoImples;
using Soot.Domain.Repositories;
using Soot.Mail.Mailkit;
using Soot.Sms.Kavenegar;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
var services = builder.Services;

// Add services to the container.
builder.Services.AddControllers();

// Add configs
var smtp = Configuration.GetSection("SmtpConfig");
services.Configure<MailConfiguration>(smtp);
var m = smtp.Get<MailConfiguration>();

services.AddScoped<IEmailModule, MailkitEmailModule>();
services.AddScoped<ISmsModule, KavenegarSmsModule>();

services.AddSootDbEF(a => a.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
services.AddSootApplication();
services.AddSootSmsKavenegar(Configuration.GetSection("KavenegarConfig"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
