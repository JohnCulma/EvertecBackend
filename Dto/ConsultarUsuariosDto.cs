namespace EvertecApi.Dto
{
    public class ConsultarUsuariosDto
    {
        public string Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public string? FotosDeUsuario { get; set; }
        public int EstadoCivil { get; set; }
        public bool TieneHermanos { get; set; }
    }
}
