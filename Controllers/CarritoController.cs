using Microsoft.AspNetCore.Mvc;
using CFarma_TemplateN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CFarma_TemplateN.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CFarma_TemplateN.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ILogger<CarritoController> _logger;

        private readonly ApplicationDbContext _context;

        public CarritoController(ILogger<CarritoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [Route("/Carrito/Index")]
        public IActionResult Index()
        {
            List<Carrito> lista = ObtenerSessionCarrito();

            ViewData["listaCarrito"] = lista;

            return View();
        }

        [Route("/Carrito/Add")]
        public IActionResult Registrar(int id_producto)
        {
            string carpeta = "";
            Producto objProd = _context.Productos.Find(id_producto);

            if (objProd == null)
            {
                return View();
            }
            else
            {
                List<Carrito> lista = ObtenerSessionCarrito();

                int posBus = BuscarCarrito(lista , id_producto);

                if (posBus == -1)
                {
                    Carrito c = new Carrito();
                    c.idpt = objProd.idpt;
                    c.Nom = objProd.Nom;
                    c.Precio = objProd.Precio;
                    c.imagen =objProd.imagen;
                    c.cantidad = 1;
                    c.stock = objProd.Stock;
                    lista.Add(c);
                }
                else
                {
                    Carrito c = lista[posBus];
                    c.cantidad = c.cantidad + 1;

                    if (c.cantidad > objProd.Stock )
                    {
                        c.cantidad = objProd.Stock;
                    }

                    lista[posBus] = c;
                }

                GuardarSessionCarrito(lista);

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [Route("/Carrito/Update")]
        public IActionResult Actualizar(int cantidad , int indice)
        {
            List<Carrito> lista = ObtenerSessionCarrito();

            if (lista.Count() >0)
            {
                if (indice >=0 && indice < lista.Count())
                {
                    Carrito c = lista[indice];
                    c.cantidad = cantidad;
                    if (c.cantidad > c.stock)
                    {
                        c.cantidad = c.stock;
                    }
                    lista[indice] = c;
                    GuardarSessionCarrito(lista);
                }
            }

            return RedirectToAction("Index");
        }

        [Route("/Carrito/Delete")]
        public IActionResult Eliminar(int indice)
        {
            if (indice != -1)
            {
                List<Carrito> lista = ObtenerSessionCarrito();

                lista.RemoveAt(indice);

                GuardarSessionCarrito(lista);
            }

            return RedirectToAction("Index");
        }

        [Route("/Carrito/listar")]
        public IActionResult ListarCarrito()
        {

            List<Carrito> lista = ObtenerSessionCarrito();

            return new OkObjectResult(lista);
        }

        [HttpGet]
        [Route("/Carrito/confirm")]
        public IActionResult Confirmar()
        {
            return View();
        }

        [HttpPost]
        [Route("/Carrito/confirm")]
        public IActionResult Confirmar(Pedido objPedido)
        {
            Usuario user = ObtenerUsuario();
            List<Carrito> lista = ObtenerSessionCarrito();
            if (user !=null && lista.Count() >0)
            {

                objPedido.codc = user.codUser;
                objPedido.idestado = 40; 
                objPedido.fecha = DateTime.Now;
                objPedido.total = TotalCarrito(lista);

                _context.Add(objPedido);
                int res = _context.SaveChanges();

                if (res > 0)
                {
                    foreach (Carrito x in lista)
                    {
                        Detalle_pedido dp = new Detalle_pedido();
                        dp.idp = objPedido.idp;
                        dp.idpt = x.idpt;
                        dp.cantidad = x.cantidad;

                        _context.Add(dp);
                        _context.SaveChanges();

                        DisminuirStock(dp.idpt, dp.cantidad);
                    }

                    GuardarSessionCarrito(new List<Carrito>());
                }

                return RedirectToAction("Index", "Pedidos");
            }

            return RedirectToAction("Index", "Carrito");
        }

        public void DisminuirStock(int idp , int cantidad ) 
        { 
                try
                {
                    Producto p = _context.Productos.Find(idp);
                    if (p !=null)
                    {
                        p.Stock = p.Stock - cantidad;

                        _context.Entry(p).State = EntityState.Modified;
                        _context.SaveChanges();

                     //   _context.Update(p);
                      //  _context.SaveChangesAsync();
                }

                  
                }
                catch (DbUpdateConcurrencyException)
                {
                }
        }

        public List<Carrito> ObtenerSessionCarrito()
        {
            List<Carrito> lista;

            if (HttpContext.Session.GetString("carrito") == null)
            {
                lista = new List<Carrito>();
            }
            else
            {
                lista = JsonConvert.DeserializeObject<List<Carrito>>((HttpContext.Session.GetString("carrito")));
            }

            return lista;
        }

        public void GuardarSessionCarrito(List<Carrito> lista)
        {
            HttpContext.Session.SetString("carrito", JsonConvert.SerializeObject(lista));
        }

        public int BuscarCarrito(List<Carrito> lista , int idProd)
        {
            for (int i = 0; i<lista.Count;i++)
            {
                if (lista[i].idpt == idProd)
                {
                    return i;
                }
            }
            return -1;
        }
        public decimal TotalCarrito(List<Carrito> lista)
        {
            decimal total = 0;

            foreach (Carrito x in lista)
            {
                total = total + x.Total();
            }
            return total;
        }

        public Usuario ObtenerUsuario()
        {
           return JsonConvert.DeserializeObject<Usuario>((HttpContext.Session.GetString("usuario")));
        }
    }
}
