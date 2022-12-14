using priority_green_wave_api.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace priority_green_wave_api.Model
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Rfid { get; set; }
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string TipoVeiculo { get; set; }
        public bool VeiculoEmergencia { get; set; }
        public bool EstadoEmergencia { get; set; }  
    }
}
