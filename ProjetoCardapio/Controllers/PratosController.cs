using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using ProjetoCardapio.Data;
using ProjetoCardapio.Models;

namespace ProjetoCardapio.Controllers
{
    public class PratosController : Controller
    {
        private readonly ProjetoCardapioContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PratosController(ProjetoCardapioContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Pratos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prato.ToListAsync());
        }

        // GET: Pratos/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prato = await _context.Prato
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prato == null)
            {
                return NotFound();
            }

            return View(prato);
        }

        // GET: Pratos/Create
        public IActionResult Create()
        {
            ViewBag.Categorias = new SelectList(_context.Categoria, "Id", "NomeDaCategoria");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeDoPrato,CategoriaId,Descricao,Preco,Imagem")] Prato prato, IFormFile imagem)
        {
            if (ModelState.IsValid)
            {
                
                if (imagem != null && imagem.Length > 0)
                {
                    
                    var caminhoImagem = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imagem.FileName);

                    
                    using (var stream = new FileStream(caminhoImagem, FileMode.Create))
                    {
                        await imagem.CopyToAsync(stream);
                    }

                    
                    prato.ImagemUrl = "/images/" + imagem.FileName;
                }

               
                _context.Add(prato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            
            ViewBag.Categorias = new SelectList(_context.Categoria, "Id", "NomeDaCategoria", prato.CategoriaId);
            return View(prato);
        }

        // GET: Pratos/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prato = await _context.Prato.FindAsync(id);
            if (prato == null)
            {
                return NotFound();
            }

            ViewBag.Categorias = new SelectList(_context.Categoria, "Id", "NomeDaCategoria", prato.CategoriaId);
            return View(prato);
        }

        // POST: Pratos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,NomeDoPrato,CategoriaId,Descricao,Preco,ImagemUrl")] Prato prato, IFormFile imagem)
        {
            if (id != prato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    if (imagem != null && imagem.Length > 0)
                    {
                        
                        var caminhoImagem = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imagem.FileName);

                        // Salva a imagem no diretório
                        using (var stream = new FileStream(caminhoImagem, FileMode.Create))
                        {
                            await imagem.CopyToAsync(stream);
                        }

                        // Atualiza a URL da imagem
                        prato.ImagemUrl = "/images/" + imagem.FileName;
                    }

                    _context.Update(prato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PratoExists(prato.Id))
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

            // Repassa a lista de categorias para a View
            ViewBag.Categorias = new SelectList(_context.Categoria, "Id", "NomeDaCategoria", prato.CategoriaId);
            return View(prato);
        }


        // GET: Pratos/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prato = await _context.Prato
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prato == null)
            {
                return NotFound();
            }

            return View(prato);
        }

        // POST: Pratos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var prato = await _context.Prato.FindAsync(id);
            if (prato != null)
            {
                _context.Prato.Remove(prato);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PratoExists(long id)
        {
            return _context.Prato.Any(e => e.Id == id);
        }
    }
}
