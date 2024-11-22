using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EnergyConsumptionAPI.Models;
using EnergyConsumptionAPI.Services;

namespace EnergyConsumptionAPI.Controllers
{
    /// <summary>
    /// Controlador para análise do consumo de energia.
    /// Este controlador fornece endpoints para analisar registros de consumo de energia.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EnergyConsumptionAnalysisController : ControllerBase
    {
        private readonly ConsumptionAnalysisService _consumptionAnalysisService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="EnergyConsumptionAnalysisController"/>.
        /// </summary>
        /// <param name="consumptionAnalysisService">O serviço responsável pela análise de consumo de energia.</param>
        public EnergyConsumptionAnalysisController(ConsumptionAnalysisService consumptionAnalysisService)
        {
            _consumptionAnalysisService = consumptionAnalysisService;
        }

        /// <summary>
        /// Obtém uma análise do consumo de energia específico pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do registro de consumo de energia a ser analisado.</param>
        /// <returns>A análise do consumo correspondente ao ID fornecido, ou um status 404 se não for encontrado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<EnergyConsumptionAnalysis>> GetEnergyConsumptionAnalysis(string id)
        {
            var analysis = await _consumptionAnalysisService.AnalyzeConsumptionAsync(id);
            return analysis != null ? Ok(analysis) : NotFound();
        }
    }
}