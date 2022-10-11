using priority_green_wave_api.DTOs;

namespace priority_green_wave_api.Model
{
    public class Catadioptrico
    {
        public int Id { get; set; }
        public Localizacao Localizacao { get; set; }

        public Catadioptrico(CatadioptricoDTO catadioptrico)
        {
            Localizacao.Id = catadioptrico.IdLocalizacao;
        }
        public Catadioptrico()
        {

        }
    }
}
