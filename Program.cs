using cms_pract.Data;
using cms_pract.Models;
using MailKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<CmsPractUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication()
    .AddLinkedIn(options => {
        options.ClientId = builder.Configuration["Authentication:LinkedIn:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:LinkedIn:ClientSecret"];
        options.Scope.Add("profile");
        options.Scope.Add("email");
        options.UserInformationEndpoint = "https://api.linkedin.com/v2/userinfo";

        options.Events = new Microsoft.AspNetCore.Authentication.OAuth.OAuthEvents
        {
            OnCreatingTicket = async context =>
            {
                var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

                var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                response.EnsureSuccessStatusCode();

                var userJson = await response.Content.ReadAsStringAsync();
                using (var user = JsonDocument.Parse(userJson))
                {
                    var givenName = user.RootElement.GetProperty("given_name").GetString();
                    var familyName = user.RootElement.GetProperty("family_name").GetString();
                    var email = user.RootElement.GetProperty("email").GetString();

                    if (string.IsNullOrEmpty(givenName) || string.IsNullOrEmpty(familyName) || string.IsNullOrEmpty(email))
                    {
                        throw new InvalidOperationException("One or more required user properties are missing.");
                    }


                    context.Identity.AddClaim(new Claim("FirstName", givenName));
                    context.Identity.AddClaim(new Claim("LastName", familyName));
                    context.Identity.AddClaim(new Claim(ClaimTypes.Email, email));
                }

            },

            OnRemoteFailure = context =>
            {
                context.Response.Redirect("/Account/Login?error=" + context.Failure.Message);
                context.HandleResponse();
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
