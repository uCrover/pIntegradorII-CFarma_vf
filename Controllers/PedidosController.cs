using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFarma_TemplateN.Data;
using CFarma_TemplateN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CFarma_TemplateN.Controllers
{
    public class PedidosController : Controller
    {
        private readonly ILogger<PedidosController> _logger;
        private readonly ApplicationDbContext _context;

        public PedidosController(ILogger<PedidosController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            Usuario user = ObtenerUsuario();
            var lista = _context.Pedidos.Where(x => x.codc == user.codUser).OrderByDescending(x => x.fecha).ToList();
     
            ViewData["listaPedidos"] = lista;

            return View();
        }

        public IActionResult Detalle(int? idPedido)
        {
            var db = _context;

            Pedido p = db.Pedidos.Find(idPedido);

            if (p !=null)
            {
                var lista = from prod in db.Productos
                            join det in db.Detalle_pedidos
                            on prod.idpt equals det.idpt
                            where det.idp == idPedido
                            select new Carrito
                            {
                                idpt = prod.idpt,
                                Nom = prod.Nom,
                                imagen = prod.imagen,
                                Precio = prod.Precio,
                                cantidad = det.cantidad,
                            };

                ViewData["pedido"] = p;
                ViewData["listaDetalle"] = lista.ToList();
                return View();
            }

            return RedirectToAction("Index", "Pedidos");
        }

        public IActionResult CancelarPedido(int? idPedido)
        {
            Pedido ped = _context.Pedidos.Find(idPedido);
            ped.idestado = 42;

            if (ped!=null)
            {
                _context.Update(ped);
                _context.SaveChanges();

                return RedirectToAction("Index", "Pedidos");
            }

            return View();
        }

        public Usuario ObtenerUsuario()
        {
            return JsonConvert.DeserializeObject<Usuario>((HttpContext.Session.GetString("usuario")));
        }
    }
}