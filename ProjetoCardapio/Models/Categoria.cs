using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace ProjetoCardapio.Models
{
    public class Categoria
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Por favor preencha o campo {0}")]
        [MaxLength(50, ErrorMessage = "O tamanho máximo para {0} é de {1} caracteres.")]
        [Display(Name = "Nome da Categoria")]
        public string NomeDaCategoria { get; set; }

        [Required(ErrorMessage = "Por favor preencha o campo {0}")]
        [MaxLength(50, ErrorMessage = "O tamanho máximo para {0} é de {1} caracteres.")]
        [Display(Name = "Descrição da Categoria")]
        public string DescricaoDaCategoria { get; set; }
        public ICollection<Prato>? Pratos { get; set; }
    }
}
