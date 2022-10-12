using Microsoft.EntityFrameworkCore;
using priority_green_wave_api.Model;

namespace priority_green_wave_api.Repository
{
    public class VeiculoUsuarioRepository
    {
        private readonly APIContext _context;
        public VeiculoUsuarioRepository(APIContext context)
        {
            _context = context;
        }
        public void Create(VeiculoUsuario veiculoUsuario)
        {
            _context.veiculoUsuario.Add(veiculoUsuario);
            _context.SaveChanges();
        }
        public VeiculoUsuario Read(int id)
        {

            var veiculoUsuario = _context.veiculoUsuario.AsNoTracking().Where(veiculoUsuario => veiculoUsuario.Id == id).FirstOrDefault();

            if (veiculoUsuario != null)
            {
                return new VeiculoUsuario()
                {
                   Id = veiculoUsuario.Id,
                   IdUsuario = veiculoUsuario.IdUsuario,
                   IdVeiculo = veiculoUsuario.IdVeiculo
                };
            }
            else
            {
                return new VeiculoUsuario()
                {
                    Id = -1
                };
            }

        }
        public VeiculoUsuario ReadIdVeiculo(int id)
        {

            var veiculoUsuario = _context.veiculoUsuario.AsNoTracking().Where(veiculoUsuario => veiculoUsuario.IdVeiculo == id).FirstOrDefault();

            if (veiculoUsuario != null)
            {
                return new VeiculoUsuario()
                {
                    Id = veiculoUsuario.Id,
                    IdUsuario = veiculoUsuario.IdUsuario,
                    IdVeiculo = veiculoUsuario.IdVeiculo
                };
            }
            else
            {
                return new VeiculoUsuario()
                {
                    Id = -1
                };
            }

        }
        public List<VeiculoUsuario> ReadIdUsuario(int id)
        {
            return _context.veiculoUsuario.AsNoTracking().Where(veiculoUsuario => veiculoUsuario.IdUsuario == id).ToList<VeiculoUsuario>(); 
        }
        public void Update(VeiculoUsuario veiculoUsuario)
        {
            _context.veiculoUsuario.Update(veiculoUsuario);
            _context.SaveChanges();
        }
        public void Delete(VeiculoUsuario veiculoUsuario)
        {
            _context.veiculoUsuario.Remove(veiculoUsuario);
            _context.SaveChanges();
        }
    }
}
