using System.Threading.Tasks;
using EnergyConsumptionAPI.Controllers;
using EnergyConsumptionAPI.Models;
using EnergyConsumptionAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EnergyConsumptionAPI.Tests
{
    public class EnergyConsumptionAnalysisControllerTests
    {
        private readonly Mock<ConsumptionAnalysisService> _mockService;
        private readonly EnergyConsumptionAnalysisController _controller;

        public EnergyConsumptionAnalysisControllerTests()
        {
            _mockService = new Mock<ConsumptionAnalysisService>();
            _controller = new EnergyConsumptionAnalysisController(_mockService.Object);
        }

        [Fact]
        public async Task GetEnergyConsumptionAnalysis_ReturnsOkResult_WithEnergyConsumptionAnalysis()
        {
            // Arrange
            var mockAnalysis = new EnergyConsumptionAnalysis
            {
                Id = "1",
                CPF = "12345678901",
                Address = "Rua A",
                ResidenceType = "casa",
                MonthlyConsumption = 600,
                ResidentsCount = 4,
                IsHighConsumption = true,
                SuggestedAction = "Reduzir o uso de eletrodomésticos."
            };
            _mockService.Setup(service => service.AnalyzeConsumptionAsync("1")).ReturnsAsync(mockAnalysis);

            // Act
            var result = await _controller.GetEnergyConsumptionAnalysis("1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<EnergyConsumptionAnalysis>(okResult.Value);
            Assert.True(returnValue.IsHighConsumption);
        }

        [Fact]
        public async Task GetEnergyConsumptionAnalysis_ReturnsNotFound_WhenIdDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.AnalyzeConsumptionAsync("nonexistent")).ReturnsAsync((EnergyConsumptionAnalysis)null);

            // Act
            var result = await _controller.GetEnergyConsumptionAnalysis("nonexistent");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
