using System.Collections.Generic;
using System.Threading.Tasks;
using EnergyConsumptionAPI.Controllers;
using EnergyConsumptionAPI.DTOs;
using EnergyConsumptionAPI.Models;
using EnergyConsumptionAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EnergyConsumptionAPI.Tests
{
    public class EnergyConsumptionControllerTests
    {
        private readonly Mock<EnergyConsumptionService> _mockService;
        private readonly EnergyConsumptionController _controller;

        public EnergyConsumptionControllerTests()
        {
            _mockService = new Mock<EnergyConsumptionService>();
            _controller = new EnergyConsumptionController(_mockService.Object);
        }

        [Fact]
        public async Task GetEnergyConsumptions_ReturnsOkResult_WithListOfEnergyConsumptions()
        {
            // Arrange
            var mockData = new List<EnergyConsumption>
            {
                new EnergyConsumption { Id = "1", CPF = "12345678901", Address = "Rua A", ResidenceType = "casa", MonthlyConsumption = 500, ResidentsCount = 3 },
                new EnergyConsumption { Id = "2", CPF = "98765432100", Address = "Rua B", ResidenceType = "apartamento", MonthlyConsumption = 300, ResidentsCount = 2 }
            };
            _mockService.Setup(service => service.GetEnergyConsumptionsAsync()).ReturnsAsync(mockData);

            // Act
            var result = await _controller.GetEnergyConsumptions();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<EnergyConsumption>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetEnergyConsumptionById_ReturnsNotFound_WhenIdDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.GetEnergyConsumptionByIdAsync("nonexistent")).ReturnsAsync((EnergyConsumption)null);

            // Act
            var result = await _controller.GetEnergyConsumptionById("nonexistent");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostEnergyConsumption_ReturnsCreatedAtAction_WithNewEnergyConsumption()
        {
            // Arrange
            var newConsumptionDto = new EnergyConsumptionDTO
            {
                CPF = "12345678901",
                Address = "Rua A",
                ResidenceType = "casa",
                MonthlyConsumption = 600,
                ResidentsCount = 4
            };

            var createdConsumption = new EnergyConsumption
            {
                Id = "3",
                CPF = "12345678901",
                Address = "Rua A",
                ResidenceType = "casa",
                MonthlyConsumption = 600,
                ResidentsCount = 4
            };

            _mockService.Setup(service => service.CreateEnergyConsumptionAsync(newConsumptionDto)).ReturnsAsync(createdConsumption);

            // Act
            var result = await _controller.PostEnergyConsumption(newConsumptionDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<EnergyConsumption>(createdResult.Value);
            Assert.Equal("3", returnValue.Id);
        }
    }
}
