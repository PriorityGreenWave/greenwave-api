using Microsoft.EntityFrameworkCore;


// C:\Users\arthu\source\repos\priority_green_wave_api\priority_green_wave_api dotnet-ef migrations add CreateUserTable
// C:\Users\arthu\source\repos\priority_green_wave_api\priority_green_wave_api dotnet-ef migrations remove
// dotnet-ef database update

namespace priority_green_wave_api.Model
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {

        }

        public DbSet<Catadioptrico> catadioptrico { get; set; }
        public DbSet<Localizacao> localizacao { get; set; }
        public DbSet<Semaforo> semaforo { get; set; }
        public DbSet<Usuario> usuario { get; set; }
        public DbSet<Veiculo> veiculo { get; set; }
        public DbSet<Area> area { get; set; }
    }
}
