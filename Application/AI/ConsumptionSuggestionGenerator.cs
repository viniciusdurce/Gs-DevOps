using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnergyConsumptionAPI.ML;
using EnergyConsumptionAPI.Models;
using Microsoft.ML;
using Microsoft.ML.Data;
using MongoDB.Driver;

namespace EnergyConsumptionAPI.AI
{
    public class ConsumptionSuggestionGenerator
    {
        private readonly IMongoCollection<EnergyConsumption> _energyConsumptions;
        private readonly MLContext _mlContext;
        private ITransformer _model;

        public ConsumptionSuggestionGenerator(IMongoDatabase database)
        {
            _energyConsumptions = database.GetCollection<EnergyConsumption>("energyConsumptions");
            _mlContext = new MLContext();
            TrainModelAsync().Wait(); // Treinamento assíncrono ao inicializar
        }

        private async Task TrainModelAsync()
        {
            // Busca todos os dados do banco de dados
            var consumptionData = await _energyConsumptions.Find(FilterDefinition<EnergyConsumption>.Empty).ToListAsync();

            if (consumptionData.Count == 0)
            {
                Console.WriteLine("Nenhum dado encontrado para treinamento.");
                return;
            }

            // Converte os dados para uma lista de EnergyConsumptionData para treinamento
            var trainingDataList = new List<EnergyConsumptionData>();
            foreach (var data in consumptionData)
            {
                trainingDataList.Add(new EnergyConsumptionData
                {
                    ResidenceType = data.ResidenceType,
                    ResidentsCount = (float)data.ResidentsCount,
                    MonthlyConsumption = (float)data.MonthlyConsumption,
                    IsHighConsumption = data.MonthlyConsumption > 500 // Define um critério básico para treinamento
                });
            }

            // Converte a lista para IDataView
            IDataView trainingData = _mlContext.Data.LoadFromEnumerable(trainingDataList);

            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("ResidenceType")
                .Append(_mlContext.Transforms.Concatenate("Features", "ResidentsCount", "MonthlyConsumption"))
                .Append(_mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(_mlContext.BinaryClassification.Trainers.LbfgsLogisticRegression(labelColumnName: "IsHighConsumption", featureColumnName: "Features"));

            _model = pipeline.Fit(trainingData);
        }

        public async Task<(bool IsHighConsumption, string SuggestedAction)> AnalyzeConsumptionAsync(double monthlyConsumption, string residenceType, int residentsCount)
        {
            if (_model == null)
            {
                // Caso o modelo não tenha sido treinado (por exemplo, falta de dados)
                Console.WriteLine("Modelo não está disponível. Usando valores padrão para análise.");
                return (monthlyConsumption > 300, "Considere reduzir o uso de eletrodomésticos durante horários de pico.");
            }

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<EnergyConsumptionData, EnergyConsumptionPrediction>(_model);

            var input = new EnergyConsumptionData
            {
                ResidenceType = residenceType,
                ResidentsCount = (float)residentsCount,
                MonthlyConsumption = (float)monthlyConsumption
            };

            var result = predictionEngine.Predict(input);

            string suggestedAction = result.IsHighConsumption
                ? GenerateSuggestion(residenceType, residentsCount)
                : "Consumo está dentro do limite ideal.";

            return (result.IsHighConsumption, suggestedAction);
        }

        private string GenerateSuggestion(string residenceType, int residentsCount)
        {
            // Sugestões mais sofisticadas podem ser geradas aqui
            return "Considere reduzir o uso de ar-condicionado e substituir aparelhos antigos por modelos mais eficientes.";
        }
    }
}
