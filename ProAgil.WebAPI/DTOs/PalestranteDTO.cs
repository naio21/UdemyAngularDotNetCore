using System.Collections.Generic;

namespace ProAgil.WebAPI.DTOs
{
    public class PalestranteDTO
    {
        public string Nome { get; set; }
        public string MiniCurriculo { get; set; }
        public string ImageURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public List<RedeSocialDTO> RedesSociais { get; set; }
        public List<EventoDTO> Eventos { get; set; }
    }
}
