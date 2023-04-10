namespace EvertecApi.Helpers.Respuestas
{
    public class Generales
    {
        public string? title { get; set; }
        public int status { get; set; }
        public string? message { get; set; }
        public string? otherdata { get; set; } = null!;
    }
}
