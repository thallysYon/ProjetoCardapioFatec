using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoCardapio.Models
{
    public class Pedido
    {
        public Pedido()
        {
            Data = DateTime.Now;
            Pratos = new List<Prato>();
            PedidosPratos = new List<PedidoPrato>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessage = "Por favor, selecione um cliente.")]
        [Display(Name = "Cliente")]
        [ForeignKey("Cliente")]
        public long ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        [Display(Name = "Data do Pedido")]
        public DateTime Data { get; set; }

        [Display(Name = "Prato")]
        public ICollection<Prato> Pratos { get; set; }

        [ValidateNever]
        public ICollection<PedidoPrato> PedidosPratos { get; set; }

        [NotMapped]
        public decimal Total
        {
            get
            {
                decimal total = 0m;

                if (PedidosPratos != null)
                {
                    foreach (var itens in PedidosPratos)
                    {
                        
                        total += itens.Quantidade * itens.Prato.Preco;
                    }
                }

                return total;
            }
        }



    }
}
