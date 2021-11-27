using Microsoft.AspNetCore.Mvc;
using CFarma_TemplateN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CFarma_TemplateN.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CarritoPrueba1.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ILogger<ProductoController> _logger;

        private readonly ApplicationDbContext _context;

        public ProductoController(ILogger<ProductoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index2()
        {
            return View();
        }

        public IActionResult Listar(int id)
        {
            var listarProductos = _context.Productos.ToList();


            ArrayList listaTipo = new ArrayList();

            foreach (var item in listarProductos)
            {
                var Producto = _context.Productos.Where(x => x.CodSubc == id);
                listaTipo.Add(Producto);
            }

            return View(listarProductos);
        }

        public IActionResult ListarTipoProducto(int? id)
        {
            var listaCategorias = _context.Categoria.ToList();
            var listaSubcategorias = _context.SubCategoria.ToList();
            var listarProductos = _context.Productos.ToList();

          
            SubCategoria sc = listaSubcategorias.Where(x => x.idsubc == id).SingleOrDefault();
            var nomSubC = sc.NombreSubCategoria; 
            var idCat = sc.idcat;
        

            //var listaTipoCategorias = listaCategorias.Where(x => x.Tipo == "Nutrición").ToList();
            Categoria cat = listaCategorias.Where(x => x.idcat == idCat).SingleOrDefault();
            var nomCat = cat.Nombre;
            var nomSupraCategoria = cat.Tipo;

            var listaTipoProducto = listarProductos.Where(x => x.CodSubc == id).ToList();

            //nombre de subcategoria
            ViewData["nombreSubcategoria"] = nomSubC;

            //nombre categoría
            ViewData["nombreCategoria"] = nomCat;

            //nombre supraCategoria
            ViewData["nombreSupraCat"] = nomSupraCategoria;

            ViewData["listaTipoProducto"] = listaTipoProducto;

            //ViewData["listaTipoCategorias"] = listaTipoCategorias;

            return View();
        }

        public IActionResult DetalleProducto(int? id)
        {
            Producto producto = _context.Productos.Find(id);
                        

            var nombreProducto = producto.Nom;
            var imagenProducto = producto.imagen;
            var precioProducto = producto.Precio;
            var descripcion1Producto = producto.Descripcion1;
            int stock = producto.Stock;
            

            ViewData["nomProdc"] = nombreProducto;
            ViewData["imaProdc"] = imagenProducto;
            ViewData["precProdc"] = precioProducto;
            ViewData["desc1Prodc"] = descripcion1Producto;
            ViewData["stockProdc"] = stock;
            ViewData["idProdc"] = id;

            return View();
        }


    }
}
