using priority_green_wave_api.Model;
using priority_green_wave_api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace priority_green_wave_api.Repository
{
    public class CatadioptricoRepository
    {
        private readonly APIContext _context;
        public CatadioptricoRepository(APIContext context)
        {
            _context = context;
        }
        public void Create(Catadioptrico catadioptrico)
        {
            _context.catadioptrico.Add(catadioptrico);
            _context.SaveChanges();
        }
        public Catadioptrico Read(int id)
        {

            var catadioptrico = _context.catadioptrico.AsNoTracking().Where(catadioptrico => catadioptrico.Id == id).FirstOrDefault();

            if (catadioptrico != null)
            {
                return new Catadioptrico()
                {
                    Id = catadioptrico.Id,
                    
                };
            }
            else
            {
                return new Catadioptrico()
                {
                    Id = -1
                };
            }

        }
        public void Update(Catadioptrico catadioptrico)
        {
            _context.catadioptrico.Update(catadioptrico);
            _context.SaveChanges();
        }
        public void Delete(Catadioptrico catadioptrico)
        {
            _context.catadioptrico.Remove(catadioptrico);
            _context.SaveChanges();
        }
    }
}
