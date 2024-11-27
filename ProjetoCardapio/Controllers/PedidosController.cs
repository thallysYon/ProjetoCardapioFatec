using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoCardapio.Data;
using ProjetoCardapio.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoCardapio.Controllers
{
    public class PedidosController : Controller
    {
        private readonly ProjetoCardapioContext _context;

        public PedidosController(ProjetoCardapioContext context)
        {
            _context = context;
        }

        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            var pedidos = _context.Pedido.Include(p => p.Cliente);
            return View(await pedidos.ToListAsync());
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.Cliente)
                .Include(p => p.PedidosPratos)
                    .ThenInclude(pp => pp.Prato)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "NomeDoCliente");
            ViewData["Pratos"] = new SelectList(_context.Prato, "Id", "NomeDoPrato");
            return View();
        }

        // POST: Pedidos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,Data")] Pedido pedido, List<int> pratoIds, List<int> quantidades)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();

                for (int i = 0; i < pratoIds.Count; i++)
                {
                    var pedidoPrato = new PedidoPrato
                    {
                        PedidoId = pedido.Id,
                        PratoId = pratoIds[i],
                        Quantidade = quantidades[i]
                    };
                    _context.Add(pedidoPrato);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "NomeDoCliente", pedido.ClienteId);
            ViewData["Pratos"] = new SelectList(_context.Prato, "Id", "NomeDoPrato");
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.PedidosPratos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "NomeDoCliente", pedido.ClienteId);
            ViewData["Pratos"] = new SelectList(_context.Prato, "Id", "NomeDoPrato");
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ClienteId,Data")] Pedido pedido, List<int> pratoIds, List<int> quantidades)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);

                    var existingPratos = _context.PedidosPratos.Where(pp => pp.PedidoId == id).ToList();
                    _context.PedidosPratos.RemoveRange(existingPratos);

                    for (int i = 0; i < pratoIds.Count; i++)
                    {
                        var pedidoPrato = new PedidoPrato
                        {
                            PedidoId = pedido.Id,
                            PratoId = pratoIds[i],
                            Quantidade = quantidades[i]
                        };
                        _context.Add(pedidoPrato);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "NomeDoCliente", pedido.ClienteId);
            ViewData["Pratos"] = new SelectList(_context.Prato, "Id", "NomeDoPrato");
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.Cliente)
                .Include(p => p.PedidosPratos)
                    .ThenInclude(pp => pp.Prato)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var pedido = await _context.Pedido
                .Include(p => p.PedidosPratos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido != null)
            {
                _context.PedidosPratos.RemoveRange(pedido.PedidosPratos);
                _context.Pedido.Remove(pedido);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(long id)
        {
            return _context.Pedido.Any(e => e.Id == id);
        }
    }
}
