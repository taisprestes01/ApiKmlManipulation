namespace ApiKMLManipulation.Models
{
    /// <summary>
    /// Representa os dados de um elemento Placemark extraído de um arquivo KML.
    /// </summary>
    public class PlacemarkModel
    {
        /// <summary>
        /// Nome ou identificação do cliente associado ao placemark.
        /// </summary>
        public string Cliente { get; set; }

        /// <summary>
        /// Situação ou status do placemark.
        /// </summary>
        public string Situacao { get; set; }

        /// <summary>
        /// Bairro onde o placemark está localizado.
        /// </summary>
        public string Bairro { get; set; }

        /// <summary>
        /// Referência textual adicional para identificar o placemark.
        /// Deve conter pelo menos 3 caracteres.
        /// </summary>
        public string Referencia { get; set; }

        /// <summary>
        /// Nome da rua ou cruzamento associado ao placemark.
        /// Deve conter pelo menos 3 caracteres.
        /// </summary>
        public string RuaCruzamento { get; set; }
    }
}
