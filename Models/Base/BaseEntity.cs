using System.ComponentModel.DataAnnotations;

namespace EnergyConsumptionAPI.Models.Base
{
    /// <summary>
    /// Classe base contendo propriedades comuns para consumo de energia.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// CPF associado ao relatório.
        /// </summary>
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string CPF { get; set; }

        /// <summary>
        /// Endereço da residência.
        /// </summary>
        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public string Address { get; set; }

        /// <summary>
        /// Tipo de residência (ex: apartamento, casa).
        /// </summary>
        [Required(ErrorMessage = "O tipo de residência é obrigatório.")]
        public string ResidenceType { get; set; }

        /// <summary>
        /// Consumo mensal de energia em kWh.
        /// </summary>
        [Required(ErrorMessage = "O consumo mensal é obrigatório.")]
        public double MonthlyConsumption { get; set; }

        /// <summary>
        /// Quantidade de moradores na residência.
        /// </summary>
        [Required(ErrorMessage = "A quantidade de moradores é obrigatória.")]
        public int ResidentsCount { get; set; }
    }
}