using System.Linq;
using ApiKMLManipulation.Models;
using System.Collections.Generic;
using System;
using System.IO;
using SharpKml.Dom;
using SharpKml.Engine;

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

        public IEnumerable<PlacemarkModel> GetFilteredPlacemarks(
            string? cliente,
            string? situacao,
            string? bairro,
            string? referencia,
            string? ruaCruzamento)
        {
            var placemarks = _kmlService.ExtractPlacemarks();

            ValidateFilters(cliente, situacao, bairro, referencia, ruaCruzamento);

            if (!string.IsNullOrWhiteSpace(cliente))
            {
                placemarks = placemarks.Where(p => p.Cliente.Equals(cliente, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(situacao))
            {
                placemarks = placemarks.Where(p => p.Situacao?.Equals(situacao, StringComparison.OrdinalIgnoreCase) == true).ToList();
            }

            if (!string.IsNullOrWhiteSpace(bairro))
            {
                placemarks = placemarks.Where(p => p.Bairro?.Equals(bairro, StringComparison.OrdinalIgnoreCase) == true).ToList();
            }

            if (!string.IsNullOrWhiteSpace(referencia))
            {
                placemarks = placemarks.Where(p => p.Referencia?.Contains(referencia, StringComparison.OrdinalIgnoreCase) == true).ToList();
            }

            if (!string.IsNullOrWhiteSpace(ruaCruzamento))
            {
                placemarks = placemarks.Where(p => p.RuaCruzamento?.Contains(ruaCruzamento, StringComparison.OrdinalIgnoreCase) == true).ToList();
            }

            return placemarks;
        }

        /// <summary>
        /// Valida os filtros fornecidos.
        /// </summary>
        private void ValidateFilters(
            string? cliente,
            string? situacao,
            string? bairro,
            string? referencia,
            string? ruaCruzamento)
        {
            var placemarks = _kmlService.ExtractPlacemarks();

            var validClients = placemarks.Select(p => p.Cliente).Distinct().ToList();
            var validSituacoes = placemarks.Select(p => p.Situacao).Distinct().ToList();
            var validBairros = placemarks.Select(p => p.Bairro).Distinct().ToList();

            ValidateFilter(cliente, validClients, "cliente");
            ValidateFilter(situacao, validSituacoes, "situação");
            ValidateFilter(bairro, validBairros, "bairro");
            ValidateMinimumLength(referencia, "referencia", 3);
            ValidateMinimumLength(ruaCruzamento, "ruaCruzamento", 3);
        }

        /// <summary>
        /// Valida se um parâmetro está dentro da lista de valores válidos.
        /// </summary>
        /// <param name="value">O valor a ser validado.</param>
        /// <param name="validValues">A lista de valores válidos.</param>
        /// <param name="fieldName">O nome do campo, para mensagens de erro.</param>
        /// <exception cref="ArgumentException">Lançada se o valor não for válido.</exception>
        private void ValidateFilter(string? value, List<string> validValues, string fieldName)
        {
            if (!string.IsNullOrWhiteSpace(value) && !validValues.Contains(value, StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException($"{fieldName} '{value}' não é válido. Verifique os valores disponíveis.");
            }
        }

        /// <summary>
        /// Valida se um parâmetro tem pelo menos uma quantidade mínima de caracteres, se fornecido.
        /// </summary>
        /// <param name="value">O valor a ser validado.</param>
        /// <param name="fieldName">O nome do campo, para mensagens de erro.</param>
        /// <param name="minLength">Quantidade mínima de caracteres.</param>
        /// <exception cref="ArgumentException">Lançada se o valor não atender ao critério.</exception>
        private void ValidateMinimumLength(string? value, string fieldName, int minLength)
        {
            if (!string.IsNullOrWhiteSpace(value) && value.Length < minLength)
            {
                throw new ArgumentException($"O campo '{fieldName}' deve ter pelo menos {minLength} caracteres.");
            }
        }

        /// <summary>
        /// Exporta os placemarks filtrados para um arquivo KML.
        /// </summary>
        /// <param name="filters">Filtros a serem aplicados.</param>
        /// <returns>Stream do arquivo KML gerado.</returns>
        public Stream ExportFilteredKml(string? cliente, string? situacao, string? bairro, string? referencia, string? ruaCruzamento)
        {
            var filteredPlacemarks = GetFilteredPlacemarks(cliente, situacao, bairro, referencia, ruaCruzamento);

            var kml = new Kml();
            var document = new Document();

            foreach (var placemark in filteredPlacemarks)
            {
                var kmlPlacemark = new Placemark
                {
                    Name = placemark.Cliente,
                    Description = new Description
                    {
                        Text = $"Situação: {placemark.Situacao}, Bairro: {placemark.Bairro}, Referência: {placemark.Referencia}, Rua/Cruzamento: {placemark.RuaCruzamento}"
                    },
                    Geometry = new Point()
                    {
                        Coordinate = new SharpKml.Base.Vector(placemark.Latitude, placemark.Longitude)  
                    }
                };

                document.AddFeature(kmlPlacemark);
            }

            kml = new Kml
            {
                Feature = document
            };

            var stream = new MemoryStream();
            KmlFile.Create(kml, false).Save(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
    }
}
