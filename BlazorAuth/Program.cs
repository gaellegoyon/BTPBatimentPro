using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Ajouter HttpClient pour les requêtes API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5148/api/") });
builder.Services.AddScoped<AuthenticationService>();

builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<LeavesService>();
builder.Services.AddScoped<DistanceService>();

var app = builder.Build();

// Configurer le pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();