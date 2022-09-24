namespace priority_green_wave_api.DTOs
{
    public class ErrorReturnDTO
    {
        public string Error { get; set; }
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }
    }
}
