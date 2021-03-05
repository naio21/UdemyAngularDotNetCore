using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebAPI.DTOs
{
    public class EventoDTO
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "Local obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Mínimo de 3 e máximo de 100 caracteres")]
        public string Local { get; set; }
        [Required (ErrorMessage = "Data do evento obrigatória")]
        public string DataEvento { get; set; }
        [Required (ErrorMessage = "Tema obrigatório")]
        public string Tema { get; set; }
        [Range(1, 120000, ErrorMessage = "Mínimo de 2 e máximo de 120.000 pessoas")]
        public int QtdPessoas { get; set; }
        public string ImagemURL { get; set; }
        public string Telefone { get; set; }
        [EmailAddress (ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }
        public List<LoteDTO> Lotes { get; set; }
        public List<RedeSocialDTO> RedesSociais { get; set; }
        public List<PalestranteDTO> Palestrantes { get; set; }
    }
}
