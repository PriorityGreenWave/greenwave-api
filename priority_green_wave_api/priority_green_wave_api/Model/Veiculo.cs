using priority_green_wave_api.DTOs;

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
        public Usuario Usuario { get; set; }
        public Veiculo(VeiculoDTO veiculo)
        {
            Placa = veiculo.Placa;
            Rfid = veiculo.Rfid;
            Fabricante = veiculo.Fabricante;
            Modelo = veiculo.Modelo;
            Ano = veiculo.Ano;
            TipoVeiculo = veiculo.TipoVeiculo;
            VeiculoEmergencia = veiculo.VeiculoEmergencia;
            EstadoEmergencia = veiculo.EstadoEmergencia;
            Usuario.Id = veiculo.IdUsuario;
        }
        public Veiculo()
        {

        }
    }
}
