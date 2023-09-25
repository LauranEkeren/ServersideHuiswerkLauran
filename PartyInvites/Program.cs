
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using PartyInvites.Models;

const string AuthScheme = "cookie";
const string AuthScheme2 = "cookie2";

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container -->
builder.Services.AddControllersWithViews();
builder.Services
    .AddScoped<IRepository, Repository>()


    // EF DB stuff -->
    .AddDbContext<InviteDbContext>(opts =>
    {
        opts
            .UseSqlServer(builder.Configuration["ConnectionStrings:Default"])
            .EnableSensitiveDataLogging(true);
    })
    // <-- end EF DB stuff
    // Identity stuff -->
    .AddDbContext<SecurityDbContext>(opts =>
    {
        opts
            .UseSqlServer(builder.Configuration.GetConnectionString("Security"))
            .EnableSensitiveDataLogging(true);
    })
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<SecurityDbContext>()
    .AddDefaultTokenProviders();

// Enable authentication
builder.Services.AddAuthentication(AuthScheme)
    .AddCookie(AuthScheme)
    .AddCookie(AuthScheme2);
// Configure the policies -->

builder.Services.AddAuthorization(policyBuilder =>
{
    //policyBuilder.AddPolicy("tt", p => p.Requirements.Add(new ))
    //opts.AddPolicy("LoggedInAdminUser", policy => policy.RequireClaim("Admin"));
    //opts.AddPolicy("IsUser", policy => policy.RequireClaim("User"));
    policyBuilder.AddPolicy("EventOwner", policy =>
    {
        policy.RequireAuthenticatedUser()
              .RequireClaim("UserType", "eventowner", "admin");
    });
    policyBuilder.AddPolicy("LoggedInUser", policy =>
    {
        policy.RequireAuthenticatedUser()
              .RequireClaim("UserType", "admin", "user", "eventowner", "limiteduser");
    });
    policyBuilder.AddPolicy("PrivilegedUser", policy =>
    {
        policy.RequireAuthenticatedUser()
              .RequireClaim("UserType", "admin", "user", "eventowner");
    });

});
// <-- end identity stuff



// add antiforgery for javascript clients -->
builder.Services
    .Configure<AntiforgeryOptions>(opts =>
    {
        opts.HeaderName = "X-XSRF-TOKEN";
    })
    .Configure<CookiePolicyOptions>(options =>
    {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.IsEssential = false;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
else
{
    // Only in dev env seed with dummy data -->

}


app.UseHttpsRedirection();
app.UseStaticFiles();

// cookie policy -->
app.UseCookiePolicy();

// sessie activeren -->
app.UseSession();

IAntiforgery antiforgery = app.Services.GetRequiredService<IAntiforgery>();
app.Use(async (context, next) =>
{
    if (!context.Request.Path.StartsWithSegments("/api"))
    {
        string? token = antiforgery.GetAndStoreTokens(context).RequestToken;
        if (token != null)
        {
            context.Response.Cookies.Append("XSRF-TOKEN", token, new CookieOptions { HttpOnly = false });
        }
    }
    await next();
});

//app.UseRouting();

// Enable Identity authentication / authorization -->
app.UseAuthentication();
app.UseAuthorization();


// vb custom mappings -->
app.MapControllerRoute(
    name: "pagination",
    pattern: "Home/ListResponses{page}",
    defaults: new { Controller = "Home", action = "ListResponses" }
);
//app.MapControllerRoute(
//    name: "reponses",
//    pattern: "responses/{*response}",
//    defaults: new { controller = "Home", action = "responses" }
// );

app.MapDefaultControllerRoute();

app.Run();









/*using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PartyInvites.Models;


var builder = WebApplication.CreateBuilder(args);

// Database
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<InviteDbContext>(options =>options.UseSqlServer(connectionString));

// Security
builder.Services.AddDbContext<SecurityDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("Security")))
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddRoles<IdentityRole>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<SecurityDbContext>().AddDefaultTokenProviders();

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRepository, Repository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "responses",
    pattern: "responses/{id?}",
    defaults: new {controller = "Home", action = "ListResponses" }
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
*/