using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebAPI.DTOs
{
    public class LoteDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O Nome do Lote é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O Preço do Lote é obrigatório")]
        public decimal Preco { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        [Range(2, 120000)]
        public int Quantidade { get; set; }
    }
}
