using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoCardapio.Models
{
    public class PedidoPrato
    {
        [Key]
        public long Id { get; set; }


        [Required(ErrorMessage = "O ID do pedido é obrigatório.")]
        public long PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        [Required(ErrorMessage = "O ID do prato é obrigatório.")]
        public long PratoId { get; set; }
        public Prato Prato { get; set; }

        [Required(ErrorMessage = "A quantidade de pratos é obrigatória.")]
        [Range(1, 10000, ErrorMessage = "A quantidade deve estar entre {1} e {2}.")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O preço total é obrigatório.")]
        [Range(0.01, 10000, ErrorMessage = "O preço deve estar entre {1} e {2}.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecoTotal { get; set; }
    }
}
