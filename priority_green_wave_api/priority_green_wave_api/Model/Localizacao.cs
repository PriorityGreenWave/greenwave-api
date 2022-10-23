using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace priority_green_wave_api.Model
{
    public class Localizacao
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Regional { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public double DistanciaSemaforo { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
