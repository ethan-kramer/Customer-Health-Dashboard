using CustomerHealthDashboardWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CustomerHealthDashboardWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //set configuration sources
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets(System.Reflection.Assembly.GetExecutingAssembly())
                .Build();

            // Add services to the container.provider =>
            builder.Services.AddCors(options => options.AddPolicy(name: "DefaultPolicy",
                corsPolicyBuilder =>
                {
                    corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                }));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Add db context
            builder.Services.AddDbContext<TestimonialTreeContext>(dbOptionsBuilder =>
            {
                //Right click the project and choose manage secrets.  Set these values as json in a secrets file like this:
                //{
                //  "tt-connection-string": "Data Source=devdb.testimonialtree.com;Initial Catalog=TestimonialTree;User ID=fgcu;Password=mypasswordhere;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=True",
                //  "tt-connection-timeout":  30
                //}

                //Get connection string and connection timeout (default to 30) from a configuration source
                var connectionString = configuration.GetValue<string>("tt-connection-string");
                var connectionTimeout = configuration.GetValue<int>("tt-connection-timeout");
                dbOptionsBuilder.UseSqlServer(connectionString,
                (sqlServerOptions) =>
                {            
                    sqlServerOptions.CommandTimeout(connectionTimeout);
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("DefaultPolicy");

            app.MapControllers();

            app.Run();
        }
    }
}