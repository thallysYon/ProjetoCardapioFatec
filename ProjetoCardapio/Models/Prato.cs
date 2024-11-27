using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoCardapio.Models
{
    public class Prato
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Por favor, preencha o campo {0}")]
        [MaxLength(64, ErrorMessage = "O tamanho máximo para {0} é de {1} caracteres.")]
        [Display(Name = "Nome do Prato")]
        public string NomeDoPrato { get; set; }

        [Required(ErrorMessage = "Por favor, preencha o campo {0}")]
        [Display(Name = "Categoria do Prato")]
        [ForeignKey("Categoria")]
        public long CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }

        [Required(ErrorMessage = "Por favor, preencha o campo {0}")]
        [MaxLength(150, ErrorMessage = "O tamanho máximo para {0} é de {1} caracteres.")]
        [Display(Name = "Descrição do Prato")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Por favor, preencha o campo {0}")]
        [Range(0.01, 999999.99, ErrorMessage = "O preço deve estar entre {1} e {2}")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }

        [ValidateNever]
        public ICollection<PedidoPrato> PedidosPratos { get; set; } = new List<PedidoPrato>();

        [StringLength(255)]
        public string? ImagemUrl { get; set; }
    }
}
