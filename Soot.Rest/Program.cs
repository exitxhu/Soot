using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using Soot.Application.Helpers;
using Soot.Application.Module.Email;
using Soot.Application.Module.Sms;
using Soot.Db.Ef;
using Soot.Db.Ef.Helpers;
using Soot.Mail.Mailkit;
using Soot.Sms.Kavenegar;



var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;
    var services = builder.Services;

    // Add services to the container.
    builder.Services.AddControllers();

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add configs
    var smtp = configuration.GetSection("SmtpConfig");
    services.Configure<MailConfiguration>(smtp);
    smtp.Get<MailConfiguration>();
    services.AddScoped<IEmailModule, MailkitEmailModule>();
    services.AddScoped<ISmsModule, KavenegarSmsModule>();
    //services.AddSootDbEF(a => a.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
    services.AddSootDbEf(a => a.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    //services.AddSootDbEF(a => a.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
    //services.AddSootDbEF(a => a.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
    services.AddSootApplication();
    services.AddSootSmsKavenegar(configuration.GetSection("KavenegarConfig"));
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    var app = builder.Build();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {

    }
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<SootContext>();
        db.Database.Migrate();
    }
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}
