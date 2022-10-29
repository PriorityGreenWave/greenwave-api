using Microsoft.EntityFrameworkCore;
using priority_green_wave_api.Model;

namespace priority_green_wave_api.Repository
{
    public class LocalizacaoCatadioptricoRepository
    {
        private readonly APIContext _context;
        public LocalizacaoCatadioptricoRepository(APIContext context)
        {
            _context = context;
        }
        public void Create(LocalizacaoCatadioptrico localizacaoCatadioptrico)
        {
            _context.localizacaoCatadioptrico.Add(localizacaoCatadioptrico);
            _context.SaveChanges();
        }
        public LocalizacaoCatadioptrico Read(int id)
        {

            var localizacaoCatadioptrico = _context.localizacaoCatadioptrico.AsNoTracking().Where(localizacaoCatadioptrico => localizacaoCatadioptrico.Id == id).FirstOrDefault();

            if (localizacaoCatadioptrico != null)
            {
                return localizacaoCatadioptrico;
            }
            else
            {
                return new LocalizacaoCatadioptrico()
                {
                    Id = -1
                };
            }

        }
        public List<LocalizacaoCatadioptrico> ReadListaLocalizacao(int idLocalizacao)
        {

            List<LocalizacaoCatadioptrico> localizacaoCatadioptrico = _context.localizacaoCatadioptrico.AsNoTracking().Where(localizacaoCatadioptrico => localizacaoCatadioptrico.IdLocalizacao == idLocalizacao).ToList(); 
            return localizacaoCatadioptrico;
        }
        public LocalizacaoCatadioptrico ReadCatadioptrico(int idCatadioptrico)
        {

            LocalizacaoCatadioptrico localizacaoCatadioptrico = _context.localizacaoCatadioptrico.AsNoTracking().Where(localizacaoCatadioptrico => localizacaoCatadioptrico.IdCatadioptrico == idCatadioptrico).FirstOrDefault();
            return localizacaoCatadioptrico;
        }
        public void Update(LocalizacaoCatadioptrico localizacaoCatadioptrico)
        {
            _context.localizacaoCatadioptrico.Update(localizacaoCatadioptrico);
            _context.SaveChanges();
        }
        public void Delete(LocalizacaoCatadioptrico localizacaoCatadioptrico)
        {
            _context.localizacaoCatadioptrico.Remove(localizacaoCatadioptrico);
            _context.SaveChanges();
        }
        public LocalizacaoCatadioptrico ReadLocalizacaoCatadioptrico(int idCatadioptrico) { 
        
            var localizacaoCatadioptrico = _context.localizacaoCatadioptrico.AsNoTracking().Where(loc => loc.IdCatadioptrico == idCatadioptrico).FirstOrDefault();
           
            if (localizacaoCatadioptrico != null)
            {
                return localizacaoCatadioptrico;
            }
            else
            {
                return new LocalizacaoCatadioptrico()
                {
                    Id = -1
                };
            }

        } 
    }
}
