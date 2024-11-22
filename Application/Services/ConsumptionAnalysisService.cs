using System.Threading.Tasks;
using EnergyConsumptionAPI.AI;
using EnergyConsumptionAPI.Data;
using EnergyConsumptionAPI.Models;
using MongoDB.Driver;

namespace EnergyConsumptionAPI.Services
{
    public class ConsumptionAnalysisService
    {
        private readonly IMongoCollection<EnergyConsumption> _energyConsumptions;
        private readonly ConsumptionSuggestionGenerator _suggestionGenerator;

        public ConsumptionAnalysisService(MongoDbService mongoDbService)
        {
            _energyConsumptions = mongoDbService.Database?.GetCollection<EnergyConsumption>("energyConsumptions");
            _suggestionGenerator = new ConsumptionSuggestionGenerator(mongoDbService.Database);
        }

        public async Task<EnergyConsumptionAnalysis> AnalyzeConsumptionAsync(string id)
        {
            var consumption = await _energyConsumptions.Find(e => e.Id == id).FirstOrDefaultAsync();
            if (consumption == null) return null;

            var (isHighConsumption, suggestedAction) = await _suggestionGenerator.AnalyzeConsumptionAsync(
                consumption.MonthlyConsumption,
                consumption.ResidenceType,
                consumption.ResidentsCount
            );

            return new EnergyConsumptionAnalysis
            {
                Id = consumption.Id,
                CPF = consumption.CPF,
                Address = consumption.Address,
                ResidenceType = consumption.ResidenceType,
                MonthlyConsumption = consumption.MonthlyConsumption,
                ResidentsCount = consumption.ResidentsCount,
                Timestamp = consumption.Timestamp,
                IsHighConsumption = isHighConsumption,
                SuggestedAction = suggestedAction
            };
        }
    }
}