using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using CFarma_TemplateN.Data;
using CFarma_TemplateN.Models;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Sql;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CFarma_TemplateN.Controllers
{
    public class AdministradorController : Controller
    {

        private readonly ApplicationDbContext _context;
        private ApplicationDbContext Context { get; }

        public AdministradorController(ApplicationDbContext context)
        {
            _context = context;
            Context = context;
        }

        public IActionResult Index()
        {
            List<Pedido> lisPedidos = _context.Pedidos.ToList();
            var lisCli = _context.Clientes.ToList();
            var lisRep = _context.Repartidor.ToList();
            var lisAdmis = _context.Administradores.ToList();

            decimal sumTP=0; int sumRep = 0;
            int sumCli = 0;int sumAdmi = 0;

            foreach (Pedido x in lisPedidos)
            {
                sumTP += x.total;
            }

            foreach(Cliente x in lisCli)
            {
                sumCli++;
            }
            foreach(Repartidor x in lisRep)
            {
                sumRep++;
            }
            foreach(Administrador x in lisAdmis)
            {
                sumAdmi++;
            }

            //suma de todos los pedidos realizados
            ViewData["sumaTP"] = sumTP;
            //suma de total de clientes
            ViewData["sumCli"] = sumCli;
            //suma de total de repartidores
            ViewData["sumRep"] = sumRep;
            //suma de total de administradores
            ViewData["sumAdmi"] = sumAdmi;

            return View();
        }



        public IActionResult ListaClientes()
        {
            var lisCli = _context.Clientes.ToList();
            ViewData["lisClientes"] = lisCli;
            return View();
        }

        public IActionResult EliminarCliente(int? id)
        {
            //esta wea de _contextClientes ---> es por lo de tu DbContext
            var cliente = _context.Clientes.Find(id);
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();

            return RedirectToAction(nameof(ListaClientes));

        }

        public IActionResult ListaRepartidores()
        {
            var lisRep = _context.Repartidor.ToList();
            ViewData["lisRep"] = lisRep;
            return View();
        }


        public IActionResult EliminarRepartidor(int? id)
        {
            var repartidor = _context.Repartidor.Find(id);
            _context.Repartidor.Remove(repartidor);
            _context.SaveChanges();
            return RedirectToAction(nameof(ListaRepartidores));

        }

        public IActionResult ListaAdmis()
        {
            var lisAdmi = _context.Administradores.ToList();
            ViewData["lisAdmi"] = lisAdmi;
            return View();
        }

        public IActionResult EliminarAdmi(int? id)
        {
            var admi = _context.Administradores.Find(id);
            _context.Administradores.Remove(admi);
            _context.SaveChanges();
            return RedirectToAction(nameof(ListaAdmis));

        }

        public IActionResult ListaProductos()
        {
            var lisProd = _context.Productos.ToList();

            ViewData["lisProd"] = lisProd;
            return View();
        }

        public IActionResult ListarTipoProductos(int ?id)
        {
           var flEstado=0; 
            //solo si el id es mayor a 4k, quiere decir que es una subcategoría
            if (id > 4000)
            {
                flEstado = 1;

                var listaCategorias = _context.Categoria.ToList();//.Where(x => x.fl_estado==1);
                var listaSubcategorias = _context.SubCategoria.ToList();
                var listarProductos = _context.Productos.ToList();

                SubCategoria sc = listaSubcategorias.Where(x => x.idsubc == id).SingleOrDefault();
                var nomSubC = sc.NombreSubCategoria;
                 var idCat = sc.idcat;

                Categoria cat = listaCategorias.Where(x => x.idcat == idCat).SingleOrDefault();
                var nomCat = cat.Nombre;
                var nomSupraCategoria = cat.Tipo;

                var listaTipoCategorias = listaCategorias.Where(x => x.Tipo == "Nutrición").ToList();

                
                var listaTipoProducto = listarProductos.Where(x => x.CodSubc == id).ToList();

                //nombre de subcategoria
                ViewData["nombreSubcategoria"] = nomSubC;

                //nombre categoría
                ViewData["nombreCategoria"] = nomCat;


                //nombre supraCategoria
                ViewData["nombreSupraCat"] = nomSupraCategoria;

                ViewData["listaTipoProducto"] = listaTipoProducto;

                switch (nomSupraCategoria)
                {
                    case "Nutrición": return View("LisProdNutricion");
                    case "Infantil": return View("LisProdInfantil");
                    case "Cuidado Personal": return View("LisProdCuidPersonal");
                    case "Salud": return View("LisProdSalud");
                }

               
            }

            if (id < 5)
            {
                switch (id)
                {
                    case 4: return View("LisProdCuidPersonal");
                    case 2: return View("LisProdInfantil");
                    case 3: return View("LisProdNutricion");
                    case 1: return View("LisProdSalud");
                }
                
              }
         ViewData["flEstado"] = flEstado;

            return View();
        }


        public async Task<IActionResult> VistaEditarProducto(int? id)
        {
           
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            var imgProducto = producto.imagen;
            if(producto == null)
            {
                return NotFound();
            }
            var idSubC = producto.CodSubc;
            var listaCategorias = _context.Categoria.ToList();
            var listaSubcategorias = _context.SubCategoria.ToList();

            SubCategoria sc = listaSubcategorias.Where(x => x.idsubc == idSubC).SingleOrDefault();
            var idSubCat = sc.idsubc;
            var nomSubC = sc.NombreSubCategoria;
            var idCat = sc.idcat;

            Categoria cat = listaCategorias.Where(x => x.idcat == idCat).SingleOrDefault();
            var nomCat = cat.Nombre;
            var nomSupraCategoria = cat.Tipo;

            //nombre de subcategoria
            ViewData["nombreSubcategoria"] = nomSubC;

            //nombre categoría
            ViewData["nombreCategoria"] = nomCat;

            //nombre categoría
            ViewData["imgProducto"] = imgProducto;

            ViewData["idSubCat"] = idSubCat;

            //nombre supraCategoria
            ViewData["nombreSupraCat"] = nomSupraCategoria;
            return View(producto);

         }

        public IActionResult EliminarProducto(int? id)
        {
            /* var listaSubcategorias = _context.SubCategoria.ToList();
             SubCategoria sc = listaSubcategorias.Where(x => x.idsubc == id).SingleOrDefault();
             var idSubCat = sc.idsubc;*/

            Producto p = _context.Productos.Where(x => x.idpt == id).FirstOrDefault();
            var idSubc = p.CodSubc;

            var detailPedido = _context.Detalle_pedidos.Where(x => x.idpt== id).ToList();

            if (detailPedido.Count == 0)
            { 

                var prod = _context.Productos.Find(id);
                _context.Productos.Remove(prod);
                _context.SaveChanges();
                TempData["error"] = "success";
            }
            else
            {
                TempData["error"] = "failure";

            }



            return RedirectToAction("ListarTipoProductos", "Administrador", new { id = idSubc });
            //return RedirectToAction(nameof(LisProdCuidPersonal(id));

        }

        public async Task<IActionResult> EditarRespaldo(int idpt, [Bind("idpt, Nom, Precio, Stock")] Producto objProd)
        {
            if (idpt != objProd.idpt)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(objProd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();

                }
                //Una vez se valide, me redirigie a esta página we
                //return RedirectToAction(nameof(Listar));
                return RedirectToAction("LisProdCuidPersonal", "Administrador");
            
            }

            return View(objProd);
        }

        public IActionResult EditProduct(Producto objProducto, IFormFile? archivo)
        {
            string ruta_base = "wwwroot/img/PRODUCTOS";
            if (ModelState.IsValid)
            {
                if (archivo != null)
                {
                    var nombreImagen = new Random().Next(1, 100000) + Path.GetFileName(archivo.FileName);
                    string directorio = Path.Combine(Directory.GetCurrentDirectory(), ruta_base, nombreImagen);

                    using (var stream = new FileStream(directorio, FileMode.Create))
                    {
                        archivo.CopyTo(stream);
                        System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), ruta_base, objProducto.imagen)); // Eliminar imagen de un directorio
                    }
                    objProducto.imagen = nombreImagen;
                }

                _context.Productos.Update(objProducto);
                _context.SaveChanges();
                return RedirectToAction("ListarTipoProductos", "Administrador", new { id = objProducto.CodSubc });
            }
            return View(objProducto);
        }

        public IActionResult FormInsertProduct()
        {
            List<String> listSupraCat = new List<String>();
            listSupraCat.Add("Nutrición");
            listSupraCat.Add("Infantil");
            listSupraCat.Add("Cuidado Personal");
            listSupraCat.Add("Salud");

            //var listaCategorias = _context.Categoria.ToList();

            ViewData["listSupraCat"] = listSupraCat;

            return View();
        }

        ////[Route("/Administrador/SelectSC")]
        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var subCategorias = (from producto in Context.Productos
                                 where producto.Nom.StartsWith(prefix)
                                 select new
                                 {
                                     label = producto.Nom,
                                     val = producto.idpt
                                 }).ToList();

            return Json(subCategorias);
        }


        public IActionResult InsertarProducto(Producto objProducto)
        {

            if (ModelState.IsValid)
             {
                 _context.Add(objProducto);
                 _context.SaveChanges();
                 return RedirectToAction("ListarTipoProductos", "Administrador", new { id = objProducto.CodSubc });
             }
            
            return View("FormInsertProduct");
        }

        public IActionResult VisAddRepartidor()
        {
            return View();
        }

        public IActionResult AddRepartidor(Repartidor objRepartidor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(objRepartidor);
                _context.SaveChanges();

                return RedirectToAction("ListaRepartidores", "Administrador");
            }
            return View("VisAddRepartidor");
        }

        public IActionResult VisAddAdmi()
        {
            return View();
        }

        public IActionResult AddAdmi(Administrador objAdmi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(objAdmi);
                _context.SaveChanges();

                return RedirectToAction("ListaAdmis", "Administrador");
            
             }
            
            return View("VisAddAdmi");
        }

        public IActionResult ListaTurnos()
        {
            var turnos =  _context.sp_select_turnos.FromSqlInterpolated($"EXEC sp_select_turnos").ToList();

            ViewData["listTurnos"] = turnos;

            return View();
        }

        public IActionResult EliminarTurno(int? id)
        {
              var turno = _context.Turno.Find(id);
            _context.Turno.Remove(turno);
            _context.SaveChanges();

            return RedirectToAction("ListaTurnos", "Administrador");
        }

        public async Task<IActionResult> VisAsignarTurnos()
        {
            var lisRep = _context.Repartidor.ToList();
            var lisHorario = _context.Horario.ToList();

            ViewBag.Horario= await _context.Horario.ToListAsync();
            ViewBag.Repartidor = lisRep;

            ViewData["lisRep"] = lisRep;
            ViewData["lisHorario"] = lisHorario;
           // ViewData["lisHorario"] = lisHorario;
            return View();
        }

        public IActionResult AddTurno(Turno objTurno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(objTurno);
                _context.SaveChanges();

                return RedirectToAction("ListaTurnos", "Administrador");
            }
            
            return View("NotFound");
        }

        public IActionResult VisAddNuevProd(string? nomSubc)
        {
            SubCategoria sc = _context.SubCategoria.Where(x => x.NombreSubCategoria == nomSubc).FirstOrDefault();
            var idSubCat= sc.idsubc;
            var nomSubCat = sc.NombreSubCategoria;

            ViewData["idSubCat"] = idSubCat;
            ViewData["nomSubCat"] = nomSubCat;


            return View();
        }

        public IActionResult AddNewProduct(Producto p ,  IFormFile archivo)
        {
          
            if (ModelState.IsValid)
            {
                if (archivo !=null)
                {
                    var fileName = new Random().Next(1, 100000) + Path.GetFileName(archivo.FileName);

                    string directorio = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/PRODUCTOS", fileName);

                    using (var stream = new FileStream(directorio, FileMode.Create))
                    {
                        archivo.CopyTo(stream);
                    }

                    p.imagen = fileName;
                }
                
                _context.Add(p);
                _context.SaveChanges();

                return RedirectToAction("ListarTipoProductos", "Administrador", new { id = p.CodSubc });

            }
            var idsubc = p.CodSubc; 
            SubCategoria sc = _context.SubCategoria.Where(x => x.idsubc == idsubc).FirstOrDefault();
            var nom=sc.NombreSubCategoria;

           return RedirectToAction("VisAddNuevProd", "Administrador", new { nomSubc = nom });
           //return View("AddNewProduct");
        }

        // ----------- Métodos para editar/insertar/transformar imágenes -------

       /* public ActionResult ConvertirImagen(int? id)
        {
            var p = _context.Productos.Where(x => x.idpt == id).FirstOrDefault();
            return File(p.imgbin, "image/jpeg");
        }*/

        /*[HttpPost]
        public ActionResult Create(Producto objProducto, IFormFile Imagen)
        {
            if (Imagen != null && Imagen.Length > 0)
            {
                byte[] imageData = null;
                using (var binaryreader = new BinaryReader(Imagen.OpenReadStream()))
                {
                    imageData = binaryreader.ReadBytes(Imagen.Length);
                }
                usuario.imagen = imageData;
            }
        }*/


    }
}