namespace priority_green_wave_api.DTOs
{
    public class VeiculoRequestDTO
    {
        public string Placa { get; set; }
        public string Rfid { get; set; }
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string TipoVeiculo { get; set; }
        public bool VeiculoEmergencia { get; set; }
        public bool EstadoEmergencia { get; set; }
        public int IdUsuario { get; set; }
    }
}
