using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebAPI.DTOs
{
    public class RedeSocialDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O Nome da Rede Social é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O link para a Rede Social é obrigatório")]
        public string URL { get; set; }
    }
}
