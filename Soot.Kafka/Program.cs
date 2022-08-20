using Confluent.Kafka;
using QueDuler.Helpers;
using Soot.Common;
using Soot.Common.Models;

namespace Soot.Kafka
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var Configuration = builder.Configuration;
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            services.AddEdgeLayerCommonServices(builder.Configuration);

            var kafkaSetting = Configuration.GetSection("KafkaSetting");
            services.Configure<SootKafkaConfig>(kafkaSetting);
            var kafkaConfig = kafkaSetting?.Get<SootKafkaConfig>();

            services.AddQueduler(a =>
            {
                a.AddKafkaBroker(services, new ConsumerConfig
                {
                    BootstrapServers = kafkaConfig.Server,
                    GroupId = kafkaConfig.GroupId,
                    AutoOffsetReset = AutoOffsetReset.Earliest
                }, topics: kafkaConfig.Topics)
                .AddJobAssemblies(typeof(Program));
            });


            var app = builder.Build();
            var dispatcher = builder.Services.BuildServiceProvider().GetRequiredService<Dispatcher>();
            dispatcher.Start(new CancellationToken());

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
        }
    }
}