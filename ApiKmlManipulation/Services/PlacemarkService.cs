using System.Linq;
using ApiKMLManipulation.Models;
using System.Collections.Generic;

namespace ApiKMLManipulation.Services
{
    public class PlacemarkService
    {
        private readonly KmlService _kmlService;

        public PlacemarkService(KmlService kmlService)
        {
            _kmlService = kmlService;
        }

        /// <summary>
        /// Obtém os valores únicos de CLIENTE, SITUAÇÃO e BAIRRO a partir dos placemarks extraídos.
        /// </summary>
        /// <returns>Um dicionário com listas de valores únicos para cada campo.</returns>
        public Dictionary<string, IEnumerable<string>> GetUniqueFilterValues()
        {
            var placemarks = _kmlService.ExtractPlacemarks();

            var clientes = placemarks
                .Select(p => p.Cliente)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct();

            var situacoes = placemarks
                .Select(p => p.Situacao)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Distinct();

            var bairros = placemarks
                .Select(p => p.Bairro)
                .Where(b => !string.IsNullOrWhiteSpace(b))
                .Distinct();


            return new Dictionary<string, IEnumerable<string>>
            {
                { "Clientes", clientes },
                { "Situacoes", situacoes },
                { "Bairros", bairros }
            };
        }
    }
}
