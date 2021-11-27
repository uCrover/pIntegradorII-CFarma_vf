using CFarma_TemplateN.Data;
using CFarma_TemplateN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CFarma_TemplateN.Controllers
{
    public class NutricionController : Controller
    {
        private readonly ILogger<NutricionController> _logger;

        private readonly ApplicationDbContext _context;
        public NutricionController(ILogger<NutricionController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListaProducto(int idCodSub)
        {

            var listarProductos = _context.Productos.ToList();

            var listaTipo = listarProductos.Where(x => x.CodSubc == idCodSub).ToList();

            ViewData["listaTipo"] = listaTipo;
            return View(listarProductos);
        }

        public IActionResult Hierro()
        {

            var listarProductos = _context.Productos.ToList();

            var listaTipo = listarProductos.Where(x => x.CodSubc == 4007).ToList();

            ViewData["listaTipo"] = listaTipo;
            
            return View(listarProductos);
        }
        public IActionResult Magnesio()
        {
            var listarProductos = _context.Productos.ToList();

            var listaTipo = listarProductos.Where(x => x.CodSubc == 4005).ToList();


            ViewData["listaTipo"] = listaTipo;
            return View(listarProductos);
        }
        public IActionResult Zinc()
        {
            var listarProductos = _context.Productos.ToList();

            var listaTipo = listarProductos.Where(x => x.CodSubc == 4006).ToList();


            ViewData["listaTipo"] = listaTipo;
            return View(listarProductos);
        }


        [Route("/Nutricion/Eliminar")]
        public IActionResult Eliminar(int id_producto)
        {
    
            return View("Carrito");

        }



        /*
          [HttpPost]
         [Route("/Nutricion/Agregar2")]
         public IActionResult Agregar([FromQuery] string cliente, [FromQuery] string producto, [FromQuery] int cantidad)
         {
             Carrito objCarrito = new Carrito();

             objCarrito.id_cliente = cliente;
             objCarrito.id_producto = producto;
             objCarrito.cantidad = cantidad;
            // objCarrito.identificador = identificador;
             try
             {
                 //guardar
                 _context.Add(objCarrito);
                 _context.SaveChanges();

             }catch(Exception e)
             {
                 return Ok("ERROR");
             }
             RedirectToAction("Carrito", "Carrito");

             return Ok(objCarrito);
             //return View("Carrito");

         }

              */
        public IActionResult Carrito()
        {
            /*
             var usuario = "user";
            var listarCarrito = _context.Carritos.Where(x => x.id_cliente.Equals(usuario)).ToList();


            ViewData["listaCarrito"] = listarCarrito;
             */

            return View();
        }
    }
}
