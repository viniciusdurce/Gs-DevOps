namespace EnergyConsumptionAPI.Models
{
    public class EnergyConsumptionAnalysis : EnergyConsumption
    {
        /// <summary>
        /// Flag indicando se o consumo está acima do limite definido.
        /// </summary>
        public bool IsHighConsumption { get; set; }

        /// <summary>
        /// Mensagem sugerindo uma ação para otimizar o consumo de energia.
        /// </summary>
        public string SuggestedAction { get; set; }
    }
}