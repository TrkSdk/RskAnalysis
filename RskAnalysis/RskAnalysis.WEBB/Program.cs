using RskAnalysis.CORE.IntRepository;
using RskAnalysis.CORE.IntServices;
using RskAnalysis.DATA.Repository;
using RskAnalysis.WEBB.Services.BusinessesSer;
using RskAnalysis.WEBB.Services.CitiesSer;
using RskAnalysis.WEBB.Services.PartnerRiskSer;
using RskAnalysis.WEBB.Services.PartnersSer;
using RskAnalysis.WEBB.Services.SectorsSer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(RskAnalysis.SERVICE.Service<>));

//builder.Services.AddScoped<IBusinessesService, BusinessesService>();
//builder.Services.AddTransient<ICitiesService, CitiesService>();
//builder.Services.AddScoped<IContractsService, ContractsService>();
//builder.Services.AddScoped<IPartnersService, PartnersService>();
//builder.Services.AddScoped<IRisksService, RisksService>();
//builder.Services.AddScoped<ISectorsService, SectorsService>();
//builder.Services.AddScoped<IPartnerRequestService, PartnerRequestService>();

builder.Services.AddHttpClient<CitiesWServices>();
builder.Services.AddHttpClient<BusinessesWServices>();
builder.Services.AddHttpClient<SectorsWServices>();
builder.Services.AddHttpClient<PartnersWServices>();
builder.Services.AddHttpClient<PartnerRequestWServices>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
