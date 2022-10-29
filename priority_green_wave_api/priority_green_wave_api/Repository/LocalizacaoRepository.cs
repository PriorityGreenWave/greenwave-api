using Microsoft.EntityFrameworkCore;
using priority_green_wave_api.Model;

namespace priority_green_wave_api.Repository
{
    public class LocalizacaoRepository
    {
        private readonly APIContext _context;
        public LocalizacaoRepository(APIContext context)
        {
            _context = context;
        }
        public int Create(Localizacao localizacao)
        {
            _context.localizacao.Add(localizacao);
            _context.SaveChanges();
            return localizacao.Id;
        }
        public Localizacao Read(int id)
        {

            var localizacao = _context.localizacao.AsNoTracking().Where(localizacao => localizacao.Id == id).FirstOrDefault();

            if (localizacao != null)
            {
                return localizacao;
            }
            else
            {
                return new Localizacao()
                {
                    Id = -1
                };
            }

        }
        public void Update(Localizacao localizacao)
        {
            _context.localizacao.Update(localizacao);
            _context.SaveChanges();
        }
        public void Delete(Localizacao localizacao)
        {
            _context.localizacao.Remove(localizacao);
            _context.SaveChanges();
        }
        
    } 
}
