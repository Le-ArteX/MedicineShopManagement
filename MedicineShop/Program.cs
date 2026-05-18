
using BLL.Services;
using DAL.EF;
using DAL.EF.Table;
using DAL.Repos;


using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddScoped<UserRepo>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CategoryRepo>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CustomerRepo>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<MedicineRepo>();
builder.Services.AddScoped<MedicineService>();
builder.Services.AddScoped<SupplierRepo>();
builder.Services.AddScoped<SupplierService>();
builder.Services.AddScoped<PurchaseRepo>();
builder.Services.AddScoped<PurchaseService>();
builder.Services.AddScoped<SaleRepo>();
builder.Services.AddScoped<SaleService>();
builder.Services.AddScoped<PurchaseItemRepo>();
builder.Services.AddScoped<PurchaseItemService>();
builder.Services.AddScoped<ReportRepo>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<AdminService>();

builder.Services.AddDbContext<MedicineShopDbContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConn"));
});


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication("CookieAuth")
 .AddCookie("CookieAuth", options =>
 {
     options.Cookie.Name = "UserLoginCookie";
     options.LoginPath = "/Account/Login";
     options.AccessDeniedPath = "/Account/AccessDenied";
     options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
 });
builder.Services.AddControllersWithViews();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
