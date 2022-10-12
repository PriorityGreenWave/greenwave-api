using System.ComponentModel.DataAnnotations.Schema;

namespace priority_green_wave_api.Model
{
    public class VeiculoUsuario
    {
        public int Id { get; set; }

        [ForeignKey("IdUsuario")]
        public int IdUsuario { get; set; }
        [ForeignKey("IdVeiculo")]
        public int IdVeiculo { get; set; }
    }
}
