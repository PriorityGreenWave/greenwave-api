namespace priority_green_wave_api.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public bool MotoristaEmergencia { get; set; }

        public Usuario( string nome, string cpf, DateTime dataNascimento, string email, string telefone, string senha, bool motoristaEmergencia)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            Email = email;
            Telefone = telefone;
            Senha = senha;
            MotoristaEmergencia = motoristaEmergencia;
        }
        public Usuario()
        {

        }
    }
}
