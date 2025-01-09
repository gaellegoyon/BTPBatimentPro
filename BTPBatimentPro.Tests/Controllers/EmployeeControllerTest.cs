using BTPBatimentPro.API.Controllers;
using BTPBatimentPro.API.Models;
using BTPBatimentPro.API.Services;
using BTPBatimentPro.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BTPBatimentPro.Tests.Controllers
{
    public class EmployeeControllerTest
    {
        private readonly EmployeesController _controller;
        private readonly EmployeeService _service;

        public EmployeeControllerTest()
        {
            // Configuration du DbContext pour utiliser une base de données en mémoire
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")  // Base de données en mémoire
                .Options;

            var context = new ApplicationDbContext(options);  // Crée une instance du DbContext avec ces options
            _service = new EmployeeService(context);  // Crée une instance de votre service avec ce DbContext
            _controller = new EmployeesController(_service);  // Crée le contrôleur
        }

        [Fact]
        public async Task GetEmployees_ReturnsOkResult_WithListOfEmployees()
        {
            // Arrange : préparation des données
            var employees = new List<Employee>
            {
                new Employee { Id = 1, FirstName = "John", LastName = "Doe", Role = "Manager" },
                new Employee { Id = 2, FirstName = "Jane", LastName = "Doe", Role = "Developer" }
            };

            // Ajout des employés dans la base de données en mémoire
            using (var context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("TestDb").Options))
            {
                await context.AddRangeAsync(employees);
                await context.SaveChangesAsync();
            }

            // Act : appel de la méthode GetEmployees dans le contrôleur
            var result = await _controller.GetEmployees();

            // Assert : vérification du résultat
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Vérifie que le résultat est un OkObjectResult
            var returnValue = Assert.IsType<List<Employee>>(okResult.Value); // Vérifie que la valeur retournée est une liste d'Employee
            Assert.Equal(2, returnValue.Count); // Vérifie que le nombre d'employés retournés est 2
        }

    }
}
