using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnergyConsumptionAPI.Data;
using MongoDB.Driver;
using EnergyConsumptionAPI.DTOs;
using EnergyConsumptionAPI.Models;

namespace EnergyConsumptionAPI.Services
{
    public class EnergyConsumptionService
    {
        private readonly IMongoCollection<EnergyConsumption> _energyConsumptions;

        public EnergyConsumptionService(MongoDbService mongoDbService)
        {
            _energyConsumptions = mongoDbService.Database?.GetCollection<EnergyConsumption>("energyConsumptions");
        }

        public async Task<IEnumerable<EnergyConsumption>> GetEnergyConsumptionsAsync()
        {
            return await _energyConsumptions.Find(FilterDefinition<EnergyConsumption>.Empty).ToListAsync();
        }

        public async Task<EnergyConsumption?> GetEnergyConsumptionByIdAsync(string id)
        {
            var filter = Builders<EnergyConsumption>.Filter.Eq(x => x.Id, id);
            return await _energyConsumptions.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<EnergyConsumption> CreateEnergyConsumptionAsync(EnergyConsumptionDTO energyConsumptionDto)
        {
            var energyConsumption = new EnergyConsumption()
            {
                Address = energyConsumptionDto.Address,
                ResidenceType = energyConsumptionDto.ResidenceType,
                MonthlyConsumption = energyConsumptionDto.MonthlyConsumption,
                ResidentsCount = energyConsumptionDto.ResidentsCount,
            };

            await _energyConsumptions.InsertOneAsync(energyConsumption);
            return energyConsumption;
        }

        public async Task<EnergyConsumption?> UpdateEnergyConsumptionAsync(string id, EnergyConsumptionDTO energyConsumptionDto)
        {
            var energyConsumption = await GetEnergyConsumptionByIdAsync(id);
            if (energyConsumption == null) return null;

            if (!string.IsNullOrEmpty(energyConsumptionDto.Address)) energyConsumption.Address = energyConsumptionDto.Address;
            if (!string.IsNullOrEmpty(energyConsumptionDto.ResidenceType)) energyConsumption.ResidenceType = energyConsumptionDto.ResidenceType;
            if (!string.IsNullOrEmpty(energyConsumptionDto.CPF)) energyConsumption.CPF = energyConsumptionDto.CPF;
            if (energyConsumptionDto.MonthlyConsumption > 0) energyConsumption.MonthlyConsumption = energyConsumptionDto.MonthlyConsumption;
            if (energyConsumptionDto.ResidentsCount > 0) energyConsumption.ResidentsCount = energyConsumptionDto.ResidentsCount;

            await _energyConsumptions.ReplaceOneAsync(c => c.Id == id, energyConsumption);
            return energyConsumption;
        }

        public async Task<bool> DeleteEnergyConsumptionAsync(string id)
        {
            var filter = Builders<EnergyConsumption>.Filter.Eq(x => x.Id, id);
            var result = await _energyConsumptions.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
    }
}
