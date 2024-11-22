using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EnergyConsumptionAPI.DTOs;
using EnergyConsumptionAPI.Models;
using EnergyConsumptionAPI.Services;

namespace EnergyConsumptionAPI.Controllers
{
    /// <summary>
    /// Controlador para gerenciar o consumo de energia no sistema.
    /// Este controlador fornece endpoints para criar, ler, atualizar e deletar registros de consumo de energia.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EnergyConsumptionController : ControllerBase
    {
        private readonly EnergyConsumptionService _energyConsumptionService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="EnergyConsumptionController"/>.
        /// </summary>
        /// <param name="energyConsumptionService">O serviço responsável pelas operações de consumo de energia.</param>
        public EnergyConsumptionController(EnergyConsumptionService energyConsumptionService)
        {
            _energyConsumptionService = energyConsumptionService;
        }

        /// <summary>
        /// Obtém uma lista de todos os registros de consumo de energia.
        /// </summary>
        /// <returns>Uma lista de registros de consumo de energia.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnergyConsumption>>> GetEnergyConsumptions()
        {
            var energyConsumptions = await _energyConsumptionService.GetEnergyConsumptionsAsync();
            return Ok(energyConsumptions);
        }

        /// <summary>
        /// Obtém um registro específico de consumo de energia pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do registro de consumo de energia a ser obtido.</param>
        /// <returns>O registro correspondente ao ID fornecido, ou um status 404 se não for encontrado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<EnergyConsumption?>> GetEnergyConsumptionById(string id)
        {
            var energyConsumption = await _energyConsumptionService.GetEnergyConsumptionByIdAsync(id);
            return energyConsumption is not null ? Ok(energyConsumption) : NotFound();
        }

        /// <summary>
        /// Cria um novo registro de consumo de energia.
        /// </summary>
        /// <param name="energyConsumptionDto">Os dados do novo registro de consumo de energia a ser criado.</param>
        /// <returns>Um status 201 se o registro for criado com sucesso, ou um status 400 em caso de erro.</returns>
        [HttpPost]
        public async Task<ActionResult<EnergyConsumption>> PostEnergyConsumption(EnergyConsumptionDTO energyConsumptionDto)
        {
            var energyConsumption = await _energyConsumptionService.CreateEnergyConsumptionAsync(energyConsumptionDto);
            return CreatedAtAction(nameof(GetEnergyConsumptionById), new { id = energyConsumption.Id }, energyConsumption);
        }

        /// <summary>
        /// Atualiza um registro existente de consumo de energia.
        /// </summary>
        /// <param name="id">O ID do registro a ser atualizado.</param>
        /// <param name="energyConsumptionDto">O objeto contendo os dados atualizados do registro.</param>
        /// <returns>Um status 200 se a atualização for bem-sucedida, um status 404 se o registro não for encontrado, ou um status 400 em caso de erro.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnergyConsumption(string id, EnergyConsumptionDTO energyConsumptionDto)
        {
            var updatedEnergyConsumption = await _energyConsumptionService.UpdateEnergyConsumptionAsync(id, energyConsumptionDto);
            return updatedEnergyConsumption != null ? Ok(updatedEnergyConsumption) : NotFound();
        }

        /// <summary>
        /// Deleta um registro específico de consumo de energia pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do registro a ser deletado.</param>
        /// <returns>Um status 200 se o registro for deletado com sucesso, ou um status 404 se o registro não for encontrado.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnergyConsumption(string id)
        {
            var deleted = await _energyConsumptionService.DeleteEnergyConsumptionAsync(id);
            return deleted ? Ok() : NotFound();
        }

    }
}
