using priority_green_wave_api.Model;
using priority_green_wave_api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace priority_green_wave_api.Repository
{
    public class UsuarioRepository
    {
        private readonly APIContext _context;
        public UsuarioRepository (APIContext context)
        {
            _context = context;
        }
        public int Create(Usuario usuario)
        {
            _context.usuario.Add(usuario);
            _context.SaveChanges();
            return usuario.Id;
        }
        public Usuario Read(int id)
        {
            
            var usuario = _context.usuario.AsNoTracking().Where(usuario => usuario.Id == id).FirstOrDefault();

            if (usuario != null)
            {
                return new Usuario()
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Cpf = usuario.Cpf,
                    DataNascimento = usuario.DataNascimento,
                    Email = usuario.Email,
                    Telefone = usuario.Telefone,
                    Senha = usuario.Senha,
                    MotoristaEmergencia = usuario.MotoristaEmergencia
                };
            }
            else
            {
                return new Usuario()
                {
                    Id = -1
                };
            }

        }
        public void Update(Usuario usuario)
        {
            _context.usuario.Update(usuario);
            _context.SaveChanges();
        }
        public void Delete(Usuario usuario)
        {
            _context.usuario.Remove(usuario);
            _context.SaveChanges();
        }
        public bool CheckEmail(string email) => (_context.usuario.Any(user => user.Email == email));
        public Usuario CheckLogin(LoginRequestDTO request)
        {
            var usuario = _context.usuario.Where(usuario => usuario.Email == request.login && usuario.Senha == request.password).FirstOrDefault();

            if (usuario != null)
            {
                return new Usuario()
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    MotoristaEmergencia = usuario.MotoristaEmergencia
                };
            }
            else
            {
                return new Usuario()
                {
                    Id = -1
                };
            }
        }     
    }
}
