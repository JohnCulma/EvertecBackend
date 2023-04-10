using System.ComponentModel.DataAnnotations;

namespace EvertecApi.Dto
{
    public class EditarUsuariosDto
    {
        public string Id { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apeliidos { get; set; } = null!;
        public DateTime FechaDeNacimiento { get; set; }
        public string? FotosDeUsuario { get; set; }
        public int EstadoCivil { get; set; }
        public bool TieneHermanos { get; set; }
    }
}
