using Microsoft.EntityFrameworkCore;
using priority_green_wave_api.Model;

namespace priority_green_wave_api.Repository
{
    public class VeiculoRepository
    {
        private readonly APIContext _context;
        public VeiculoRepository(APIContext context)
        {
            _context = context;
        }
        public void Create(Veiculo veiculo)
        {
            _context.veiculo.Add(veiculo);
            _context.SaveChanges();
        }
        public Veiculo Read(int id)
        {

            var veiculo = _context.veiculo.AsNoTracking().Where(veiculo => veiculo.Id == id).FirstOrDefault();

            if (veiculo != null)
            {
                return new Veiculo()
                {
                    Id = veiculo.Id,
                    Placa = veiculo.Placa,
                    Rfid = veiculo.Rfid,
                    Fabricante = veiculo.Fabricante,
                    Modelo = veiculo.Modelo,
                    Ano = veiculo.Ano,
                    TipoVeiculo = veiculo.TipoVeiculo,
                    VeiculoEmergencia = veiculo.VeiculoEmergencia,
                    EstadoEmergencia = veiculo.EstadoEmergencia
                };
            }
            else
            {
                return new Veiculo()
                {
                    Id = -1
                };
            }

        }
        public void Update(Veiculo veiculo)
        {
            _context.veiculo.Update(veiculo);
            _context.SaveChanges();
        }
        public void Delete(Veiculo veiculo)
        {
            _context.veiculo.Remove(veiculo);
            _context.SaveChanges();
        }
        public Veiculo ReadRfid(string Rfid)
        {
            var veiculo = _context.veiculo.AsNoTracking().Where(veiculo => veiculo.Rfid == Rfid).FirstOrDefault();

            if (veiculo != null)
            {
                return new Veiculo()
                {
                    Id = veiculo.Id,
                    Placa = veiculo.Placa,
                    Rfid = veiculo.Rfid,
                    Fabricante = veiculo.Fabricante,
                    Modelo = veiculo.Modelo,
                    Ano = veiculo.Ano,
                    TipoVeiculo = veiculo.TipoVeiculo,
                    VeiculoEmergencia = veiculo.VeiculoEmergencia,
                    EstadoEmergencia = veiculo.EstadoEmergencia
                };
            }
            else
            {
                return new Veiculo()
                {
                    Id = -1
                };
            }
        }
    }
}
