using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using EnergyConsumptionAPI.Models.Base;

namespace EnergyConsumptionAPI.Models
{
    /// <summary>
    /// Cadastro do consumo de energia de uma residência.
    /// </summary>
    public class EnergyConsumption : BaseEntity
    {
        /// <summary>
        /// Identificador único do consumo de energia.
        /// </summary>
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        [Required(ErrorMessage = "O Id é obrigatório.")]
        public string Id { get; set; }

        
        /// <summary>
        /// Data e hora do registro do consumo de energia.
        /// </summary>
        [BsonElement("timestamp"), BsonRepresentation(BsonType.String)]
        [Required(ErrorMessage = "A data e hora do registro são obrigatórias.")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
