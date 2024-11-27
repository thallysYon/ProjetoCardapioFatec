using System.ComponentModel.DataAnnotations;

namespace ProjetoCardapio.Models
{
    public class Cliente
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Por favor preencha o campo {0}")]
        [MaxLength(50, ErrorMessage = "O tamanho máximo para {0} é de {1} caracteres.")]
        [Display(Name = "Nome do cliente")]
        public string NomeDoCliente { get; set; }

        [Required(ErrorMessage = "Por favor preencha o campo {0}")]
        [Display(Name = "Número da mesa")]
        public int NumeroDaMesa { get; set; }

        public ICollection<Pedido>? Pedidos { get; set; }
    }
}
