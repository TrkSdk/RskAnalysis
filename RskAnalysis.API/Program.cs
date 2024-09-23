using RskAnalysis.API.Custom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RskAnalysis.CORE.IntRepository;
using RskAnalysis.CORE.IntServices;
using RskAnalysis.CORE.IntServices.IntBusinessesServ;
using RskAnalysis.CORE.IntServices.IntCitiesServ;
using RskAnalysis.CORE.IntServices.IntContractsServ;
using RskAnalysis.CORE.IntServices.IntPartnersServ;
using RskAnalysis.CORE.IntServices.IntRisksServ;
using RskAnalysis.CORE.IntServices.IntSectorsServ;
using RskAnalysis.CORE.IntUnitOfWork;
using RskAnalysis.DATA;
using RskAnalysis.DATA.Repository;
using RskAnalysis.DATA.UnitOfWork;
using RskAnalysis.SERVICE.Services;
using RskAnalysis.SERVICE.Services.BusinessesServ;
using RskAnalysis.SERVICE.Services.CitiesServ;
using RskAnalysis.SERVICE.Services.ContractsServ;
using RskAnalysis.SERVICE.Services.PartnersServ;
using RskAnalysis.SERVICE.Services.RisksServ;
using RskAnalysis.SERVICE.Services.SectorsServ;
using TimeSpanToStringConverter = RskAnalysis.API.Custom.TimeSpanToStringConverter;
using System.Net;
using RskAnalysis.CORE.IntRepository.IntBusinessesRepository;
using RskAnalysis.CORE.IntRepository.IntCitiesRepository;
using RskAnalysis.CORE.IntRepository.IntSectorsRepository;
using RskAnalysis.DATA.Repository.CitiesRepo;
using RskAnalysis.DATA.Repository.SectorsRepo;
using RskAnalysis.DATA.Repository.BusinessesRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(RskAnalysis.SERVICE.Service<>));

builder.Services.AddScoped<IBusinessesService, BusinessesService>();
builder.Services.AddScoped<ICitiesService, CitiesService>();
builder.Services.AddScoped<IContractsService, ContractsService>();
builder.Services.AddScoped<IPartnersService, PartnersService>();
builder.Services.AddScoped<IRisksService, RisksService>();
builder.Services.AddScoped<ISectorsService, SectorsService>();


builder.Services.AddScoped<ICitiesRepository, CitiesRepository>();
builder.Services.AddScoped<IBusinessesRepository, BusinessRepository>();
builder.Services.AddScoped<ISectorsRepository, SectorsRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Configuration.GetSection("connType").GetSection("Conn").Value == "test")
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConStr1").ToString(), sqlServerOptionsAction: o =>
        {
            o.EnableRetryOnFailure();
            o.MigrationsAssembly("MetaBoxWMS.Data");
        });
    }


    options.EnableSensitiveDataLogging();
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddControllers(o =>
{

}).AddNewtonsoftJson(o =>
{
    o.SerializerSettings.ReferenceLoopHandling =
        Newtonsoft.Json.ReferenceLoopHandling.Ignore;


}).AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter())
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var origins = builder.Configuration["AllowedHosts"].Split(",");
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(String.Join(",", origins))
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
var app = builder.Build();
app.UseHttpsRedirection();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();



app.UseAuthorization();

app.MapControllers();

app.Run();
