using Microsoft.EntityFrameworkCore;
using priority_green_wave_api.Model;

namespace priority_green_wave_api.Repository
{
    public class LocalizacaoSemaforoRepository
    {
        private readonly APIContext _context;
        public LocalizacaoSemaforoRepository(APIContext context)
        {
            _context = context;
        }
        public void Create(LocalizacaoSemaforo localizacaoSemaforo)
        {
            _context.localizacaoSemaforo.Add(localizacaoSemaforo);
            _context.SaveChanges();
        }
        public LocalizacaoSemaforo Read(int id)
        {

            var localizacaoSemaforo = _context.localizacaoSemaforo.AsNoTracking().Where(localizacaoSemaforo => localizacaoSemaforo.Id == id).FirstOrDefault();

            if (localizacaoSemaforo != null)
            {
                return localizacaoSemaforo;
            }
            else
            {
                return new LocalizacaoSemaforo()
                {
                    Id = -1
                };
            }

        }
        public List<LocalizacaoSemaforo> ReadListaLocalizacao(int idLocalizacao)
        {

            List<LocalizacaoSemaforo> localizacaoSemaforo = _context.localizacaoSemaforo.AsNoTracking().Where(localizacaoCatadioptrico => localizacaoCatadioptrico.IdLocalizacao == idLocalizacao).ToList();
            return localizacaoSemaforo;
        }
        public LocalizacaoSemaforo ReadSemaforo(int idSemaforo)
        {

            LocalizacaoSemaforo localizacaoSemaforo = _context.localizacaoSemaforo.AsNoTracking().Where(localizacaoCatadioptrico => localizacaoCatadioptrico.IdSemaforo == idSemaforo).FirstOrDefault();
            return localizacaoSemaforo;
        }
        public void Update(LocalizacaoSemaforo localizacaoSemaforo)
        {
            _context.localizacaoSemaforo.Update(localizacaoSemaforo);
            _context.SaveChanges();
        }
        public void Delete(LocalizacaoSemaforo localizacaoSemaforo)
        {
            _context.localizacaoSemaforo.Remove(localizacaoSemaforo);
            _context.SaveChanges();
        }
        public LocalizacaoSemaforo ReadLocalizacaoSemaforo(int idSemaforo)
        {

            var localizacaoSemaforo = _context.localizacaoSemaforo.AsNoTracking().Where(loc => loc.IdSemaforo == idSemaforo).FirstOrDefault();

            if (localizacaoSemaforo != null)
            {
                return localizacaoSemaforo;
            }
            else
            {
                return new LocalizacaoSemaforo()
                {
                    Id = -1
                };
            }

        }
    }
}
