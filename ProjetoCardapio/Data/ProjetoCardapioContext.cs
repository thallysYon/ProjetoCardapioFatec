using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoCardapio.Models;

namespace ProjetoCardapio.Data
{
    public class ProjetoCardapioContext : DbContext
    {
        public ProjetoCardapioContext (DbContextOptions<ProjetoCardapioContext> options)
            : base(options)
        {
        }

        public DbSet<ProjetoCardapio.Models.Categoria> Categoria { get; set; } = default!;
        public DbSet<ProjetoCardapio.Models.Cliente> Cliente { get; set; } = default!;
        public DbSet<ProjetoCardapio.Models.Pedido> Pedido { get; set; } = default!;
        public DbSet<ProjetoCardapio.Models.Prato> Prato { get; set; } = default!;
        public DbSet<ProjetoCardapio.Models.PedidoPrato> PedidosPratos { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prato>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Pratos)
                .HasForeignKey(p => p.CategoriaId);

            modelBuilder.Entity<PedidoPrato>()
                .HasKey(pp => new { pp.PedidoId, pp.PratoId });

            modelBuilder.Entity<PedidoPrato>()
                .HasOne(pp => pp.Pedido)
                .WithMany(p => p.PedidosPratos)
                .HasForeignKey(pp => pp.PedidoId);

            modelBuilder.Entity<PedidoPrato>()
                .HasOne(pp => pp.Prato)
                .WithMany(p => p.PedidosPratos)
                .HasForeignKey(pp => pp.PratoId);
        }
    }
}
