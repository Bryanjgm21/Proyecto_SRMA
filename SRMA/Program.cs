using SRMA.Interfaces;
using SRMA.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IUserModel, UserModel>();
builder.Services.AddSingleton<IProductModel, ProductModel>();
builder.Services.AddSingleton<ISupplierModel, SupplierModel>();
builder.Services.AddSingleton<IReservationModel, ReservationModel>();
builder.Services.AddSingleton<IUtilities, Utilities>();
builder.Services.AddSingleton<IFidelityProModel,FidelityProModel>();
builder.Services.AddSingleton<IEmployeeInfoModel, EmployeeInfoModel>();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=LogIn}/{id?}");

app.Run();
