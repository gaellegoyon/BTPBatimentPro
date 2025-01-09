using BTPBatimentPro.API.Data;
using Microsoft.EntityFrameworkCore;
using BTPBatimentPro.API.Services;

var builder = WebApplication.CreateBuilder(args);


// Enregistrer le DbContext avec la chaîne de connexion
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Enregistrer les services
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<LeaveService>();
builder.Services.AddScoped<DistanceService>();

// Ajouter HttpClient pour le service DistanceService
builder.Services.AddHttpClient<DistanceService>();

// Enregistrer les contrôleurs
builder.Services.AddControllers();

// Ajouter la configuration Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();


app.Run();
