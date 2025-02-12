using AspNetFrameworkAuthorisation.Models;
using Microsoft.AspNetCore.Authentication.Negotiate;
using System.Diagnostics;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSystemWebAdapters();
builder.Services.AddHttpForwarder();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
    .AddNegotiate(); 

builder.Services.AddAuthorization(options => {
    options.AddPolicy("Admin", policy => policy.RequireClaim("Admin"));
}); 

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.Use(async (context, next) => {
    AppDbContext dbContext = new AppDbContext();

    var user = dbContext.Users.Where(data => data.Username == "Stefan").FirstOrDefault();

    if (user == null) {
        await next.Invoke();
        return;
    }

    if (user.Manager) {
        context.User.Identities.First().AddClaim(new Claim("Manager", "True"));
    }

    if (user.Admin) {
        context.User.Identities.First().AddClaim(new Claim("Admin", "True"));
    }

    Debug.WriteLine(user.Id);

    await next.Invoke();
});

app.UseAuthorization();

app.UseSystemWebAdapters();

app.MapDefaultControllerRoute();

app.Run();
