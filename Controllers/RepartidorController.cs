using CFarma_TemplateN.Data;
using CFarma_TemplateN.Models;
using CFarma_TemplateN.Models.CustomTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CFarma_TemplateN.Controllers
{
    public class RepartidorController : Controller
    {
        
        private readonly ApplicationDbContext _context;

        public RepartidorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<sp_select_pedido_estados> lisTipoPedidos(int? codRep, int? tipoPed)
        {
            List<sp_select_pedido_estados> lisTP = _context.sp_select_pedido_estados.FromSqlRaw("sp_select_pedido_estados @p0,@p1", codRep, tipoPed).ToList();

            return lisTP;
        }

        public IActionResult Index(int? codRep)
        {
            int tipoPedPendiente = 40; int tipoPedEntregado = 41; int tipoPedCancelado = 42;

            int sumPedPendiente = 0; int sumPedEntregado = 0; int sumPedCancelado = 0;

            var lisPedPendientes = lisTipoPedidos(codRep, tipoPedPendiente);
            var lisPedEntregados = lisTipoPedidos(codRep, tipoPedEntregado);
            var lisPedCancelado = lisTipoPedidos(codRep, tipoPedCancelado);


            foreach (sp_select_pedido_estados x in lisPedPendientes)
            {
                sumPedPendiente ++;
            }
            foreach (sp_select_pedido_estados x in lisPedEntregados)
            {
                sumPedEntregado++;
            }
            foreach (sp_select_pedido_estados x in lisPedCancelado)
            {
                sumPedCancelado++;
            }


            ViewData["sumPedPendiente"] = sumPedPendiente;
            ViewData["sumPedEntregado"] = sumPedEntregado;
            ViewData["sumPedCancelado"] = sumPedCancelado;
            return View();
        }

        public IActionResult VisLisTipoPedidos(int? codRep, int? tipoPed)
        {
            var lisTipPed = lisTipoPedidos(codRep, tipoPed);
            EstadoPedido estadoPedido = _context.EstadoPedido.Where(x => x.idEstadoPedido == tipoPed).FirstOrDefault();
            string estado = estadoPedido.nom;
          
            ViewData["lisTipPed"] = lisTipPed;
            ViewData["idEstadoPed"] = tipoPed;
            ViewData["nomEP"] = estado;
            ViewData["codRep"] = codRep;
            
            return View();
        }

        public IActionResult VisLisTipoPedidos2(List<Int32> valores )
        {
            var lisTipPed = lisTipoPedidos(valores[0], valores[1]);
            EstadoPedido estadoPedido = _context.EstadoPedido.Where(x => x.idEstadoPedido == valores[1]).FirstOrDefault();
            string estado = estadoPedido.nom;

            ViewData["lisTipPed"] = lisTipPed;
            ViewData["idEstadoPed"] = valores[1];
            ViewData["nomEP"] = estado;
            ViewData["codRep"] = valores[0];

            return View("VisLisTipoPedidos");
        }

        public IActionResult UpdatePedido(int? idp,int? estadoP, int? codRep)
        {
            Pedido ped = _context.Pedidos.Find(idp);
            ped.idestado = 41;

            List<Int32> val = new List<Int32>();
            val.Add((Int32)codRep);
            val.Add((Int32)estadoP);

            if (ped != null)
            {
              _context.Pedidos.Update(ped);
              _context.SaveChanges();            
             return RedirectToAction("VisLisTipoPedidos2", "Repartidor", new { valores= val });
            }

           return View();
          
        }

        public IActionResult DetallePedido(int? idp)
        {
            var db = _context;

            Pedido p = db.Pedidos.Find(idp);

            if (p != null)
            {
                var lista = from prod in db.Productos
                            join det in db.Detalle_pedidos
                            on prod.idpt equals det.idpt
                            where det.idp == idp
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




    }
}
