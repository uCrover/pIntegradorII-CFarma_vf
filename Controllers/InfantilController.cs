using CFarma_TemplateN.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFarma_TemplateN.Controllers
{
    public class InfantilController : Controller
    {
        private readonly ApplicationDbContext _context;



        public InfantilController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public IActionResult ListaProducto(int idCodSub)
        {

            var listarProductos = _context.Productos.ToList();

            var listaTipo = listarProductos.Where(x => x.CodSubc == idCodSub).ToList();

            ViewData["listaTipo"] = listaTipo;
            return View(listarProductos);
        }
    }
}
