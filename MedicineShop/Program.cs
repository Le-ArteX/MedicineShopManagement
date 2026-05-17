//using BLL.Services;
using BLL.Services;
using DAL.EF;
using DAL.EF.Table;
using DAL.Repos;

//using DAL.Repos;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
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

builder.Services.AddDbContext<MedicineShopDbContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConn"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Category}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
