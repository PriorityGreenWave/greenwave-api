namespace priority_green_wave_api.DTOs
{
    public class UsuarioRequestDTO
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public bool MotoristaEmergencia { get; set; }
    }
}
