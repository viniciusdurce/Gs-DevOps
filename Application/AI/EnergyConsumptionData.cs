using Microsoft.ML.Data;

namespace EnergyConsumptionAPI.ML
{
    public class EnergyConsumptionData
    {
        [LoadColumn(0)]
        public string ResidenceType { get; set; }

        [LoadColumn(1)]
        public float ResidentsCount { get; set; }

        [LoadColumn(2)]
        public float MonthlyConsumption { get; set; }

        [LoadColumn(3)]
        public bool IsHighConsumption { get; set; }  // Label para treinamento
    }

    public class EnergyConsumptionPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool IsHighConsumption { get; set; }

        public float Probability { get; set; }
    }
}