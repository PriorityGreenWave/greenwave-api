using Microsoft.EntityFrameworkCore;
using priority_green_wave_api.Model;

namespace priority_green_wave_api.Repository
{
    public class SemaforoRepository
    {
        private readonly APIContext _context;
        public SemaforoRepository(APIContext context)
        {
            _context = context;
        }
        public int Create(Semaforo semaforo)
        {
            _context.semaforo.Add(semaforo);
            _context.SaveChanges();
            return semaforo.Id;
        }
        public Semaforo Read(int id)
        {

            var semaforo = _context.semaforo.AsNoTracking().Where(semaforo => semaforo.Id == id).FirstOrDefault();

            if (semaforo != null)
            {
                return semaforo;
            }
            else
            {
                return new Semaforo()
                {
                    Id = -1
                };
            }

        }
        public void Update(Semaforo semaforo)
        {
            _context.semaforo.Update(semaforo);
            _context.SaveChanges();
        }
        public void Delete(Semaforo semaforo)
        {
            _context.semaforo.Remove(semaforo);
            _context.SaveChanges();
        }
    }
}
