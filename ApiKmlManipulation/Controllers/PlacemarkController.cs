using System;
using Microsoft.AspNetCore.Mvc;
using ApiKMLManipulation.Services;
using System.Collections.Generic;

namespace ApiKMLManipulation.Controllers
{
    [ApiController]
    [Route("api/placemarks")]
    public class PlacemarkController : ControllerBase
    {
        private readonly PlacemarkService _placemarkService;

        public PlacemarkController(PlacemarkService placemarkService)
        {
            _placemarkService = placemarkService;
        }

        /// <summary>
        /// Obtém os valores únicos de CLIENTE, SITUAÇÃO e BAIRRO para filtragem.
        /// </summary>
        /// <returns>Objeto JSON com listas de valores únicos.</returns>
        [HttpGet("filters")]
        public IActionResult GetUniqueFilterValues()
        {
            try
            {
                var filterValues = _placemarkService.GetUniqueFilterValues();

                return Ok(filterValues);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Lista os elementos filtrados no formato JSON.
        /// </summary>
        /// <param name="cliente">Filtro de CLIENTE.</param>
        /// <param name="situacao">Filtro de SITUAÇÃO.</param>
        /// <param name="bairro">Filtro de BAIRRO.</param>
        /// <param name="referencia">Filtro parcial de REFERENCIA.</param>
        /// <param name="ruaCruzamento">Filtro parcial de RUA/CRUZAMENTO.</param>
        /// <returns>Lista de elementos filtrados no formato JSON.</returns>
        [HttpGet]
        public IActionResult GetFilteredPlacemarks(
            [FromQuery] string? cliente,
            [FromQuery] string? situacao,
            [FromQuery] string? bairro,
            [FromQuery] string? referencia,
            [FromQuery] string? ruaCruzamento)
        {
            try
            {
                var filteredPlacemarks = _placemarkService.GetFilteredPlacemarks(cliente, situacao, bairro, referencia, ruaCruzamento);
                return Ok(filteredPlacemarks);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
