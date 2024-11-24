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

        /// <summary>
        /// Coordenada de latitude associada ao placemark.
        /// Valores válidos estão entre -90 e 90.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Coordenada de longitude associada ao placemark.
        /// Valores válidos estão entre -180 e 180.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Link para a imagem associada ao placemark.
        /// O link é formatado para apontar para uma imagem PNG.
        /// </summary>
        public string MediaLink { get; set; }
    }
}
