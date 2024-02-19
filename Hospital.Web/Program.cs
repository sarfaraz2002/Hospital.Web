using Hospital.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Hospital.Utilities;
using Hospital.Repositories.Repository;
using Hospital.Repositories.Implementations;
using Microsoft.AspNetCore.Identity.UI.Services;
using Hospital.Services;
using Hospital.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IDbInitializer,DbInitializer>();
builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
builder.Services.AddTransient<IHospitalInfoService,HospitalInfoService>();
builder.Services.AddTransient<IRoomService,RoomService>();
builder.Services.AddTransient<IContactService, ContactService>();
builder.Services.AddScoped<IEmailSender,EmailSender>();
builder.Services.AddRazorPages();
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
DataSeeding();
app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{Area=Admin}/{controller=Hospital}/{action=Index}/{id?}");

app.Run();

void DataSeeding()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}