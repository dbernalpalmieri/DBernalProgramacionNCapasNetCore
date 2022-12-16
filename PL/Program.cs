
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();



//builder.Services.AddDbContext<DbernalProgramacionNcapasContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DBernalProgramacionNCapas")));
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(120);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

//HttpClientHandler handler = new HttpClientHandler();
//handler.AllowAutoRedirect= true;
//handler.UseDefaultCredentials= true;

//builder.Services.AddSingleton(new HttpClient(handler, disposeHandler:true)
//{
//    BaseAddress = new Uri("http://localhost:5058/api/")

//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}


app.UseAuthentication(); // NOTE: line is newly added
app.UseStaticFiles(); // NOTE: line is newly added

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}");

app.Run();
