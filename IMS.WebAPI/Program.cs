using Serilog;
using Serilog.Events;
using IMS.Infrastructure.Data;
using IMS.Core.Interfaces;
using IMS.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string MyCors = "MyCors";

//Add Serilog
Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.File("C:\\IMSLogs\\log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

//Add .Net Core Cors
builder.Services.AddCors(opts =>
{
    opts.AddPolicy(name: MyCors,
                    builder =>
                    {
                        builder.SetIsOriginAllowed(x => _ = true);
                        builder.WithOrigins();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                        builder.AllowAnyOrigin();
                    });
});


//Add Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container, adding json settings for timezone, null values and reference looping.
builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    //options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                    options.UseCamelCasing(false);
                });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Connection DBContext from sql server
//builder.Services.AddDbContext<IMSDevContext>(option => 
//        option.UseSqlServer(builder.Configuration.GetConnectionString("IMSDEV")));

//Add Connection DBContext from Memory(Data inside the project) for testing purposes only
builder.Services.AddDbContext<IMSDevContext>(option =>                                                                                                                                                                                                                                                                                                                                                                                                       
        option.UseInMemoryDatabase("IMS"));

// Add services to the container.
builder.Services.AddTransient<IInventoryRepository, InventoryRepository>();

builder.Services.AddCors(opts =>
{
    opts.AddPolicy(name: MyCors,
                    builder =>
                    {
                        builder.WithOrigins("*");
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                        builder.AllowAnyOrigin();
                    });
});

var app = builder.Build();

//using cors
app.UseCors(MyCors);

var scope = app.Services.CreateScope();
var imsContext = scope.ServiceProvider.GetRequiredService<IMSDevContext>();
imsContext.Database.EnsureDeleted();
imsContext.Database.EnsureCreated();

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
