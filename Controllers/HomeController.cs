using CFarma_TemplateN.Data;
using CFarma_TemplateN.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CFarma_TemplateN.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

       /* public IActionResult Index()
        {
            
            
            return View();

        }*/


        public IActionResult Index()
        {
            int subc = 4001;

            var listaCategorias = _context.Categoria.ToList();

            var listaSubcategorias = _context.SubCategoria.ToList();

            SubCategoria sc = listaSubcategorias.Where(x => x.idsubc == subc).SingleOrDefault();

            var nomSubC = sc.NombreSubCategoria;

            var listaTipoCategorias = listaCategorias.Where(x => x.Tipo == "Nutrición" ).ToList();

            List<Producto> listarProductos = _context.Productos.ToList();

            var listaTipoProducto = listarProductos.Where(x => x.CodSubc == subc).ToList();

            ViewData["nombreSubcategoria"] = nomSubC;
            
            ViewData["listaTipoProducto"] = listaTipoProducto;

          //  ViewData["listaTipoCategorias"] = listaTipoCategorias;
            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        public IActionResult Registro2()
        {
            return View();
        }
      

        public IActionResult Logueo()
        {
            return View();
        }

        [HttpPost]
        [Route("/Home/Logueo")]
        public IActionResult Logueo(Usuario obj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var usrAdmin = _context.Administradores.Where(x => x.correo.Trim() == obj.Correo.Trim() && x.pswd == obj.Pswd).ToList();

            if (usrAdmin.Count() >0)
            {
                Usuario user = new Usuario();
                user.codUser = usrAdmin.ElementAt(0).codad;
                user.idtip = usrAdmin.ElementAt(0).idtip;
                user.Correo = usrAdmin.ElementAt(0).correo;
                user.nombre = usrAdmin.ElementAt(0).nomad;
                //user.apellido = usrAdmin.ElementAt(0).ap
                GuardarUsuarioLogeado(user);


                return RedirectToAction("Index", "Administrador"); // Modificar a una vista admin
            }
            else
            {
                var usrCliente = _context.Clientes.Where(x => x.Correo == obj.Correo && x.Pswd == obj.Pswd).ToList();

                if (usrCliente.Count() > 0)
                {

                    Usuario user = new Usuario();
                    user.codUser = usrCliente.ElementAt(0).codc;
                    user.idtip = usrCliente.ElementAt(0).Idtip;
                    user.Correo = usrCliente.ElementAt(0).Correo;
                    user.nombre = usrCliente.ElementAt(0).Nom;
                    user.apellido = usrCliente.ElementAt(0).Apellido;

                    GuardarUsuarioLogeado(user);

                    return RedirectToAction("Index", "Carrito");
                }

      

                else
                {
                    var usrRepartidor = _context.Repartidor.Where(x => x.Correo == obj.Correo && x.Pswd == obj.Pswd).ToList();

                    if (usrRepartidor.Count() > 0)
                    {
                        Usuario user = new Usuario();
                        user.codUser = usrRepartidor.ElementAt(0).codrep;
                        user.idtip = usrRepartidor.ElementAt(0).Idtip;
                        user.Correo = usrRepartidor.ElementAt(0).Correo;
                        user.nombre = usrRepartidor.ElementAt(0).Nom;

                        GuardarUsuarioLogeado(user);

                        return RedirectToAction("Index", "Repartidor", new { codRep = user.codUser });
                    }
                    else
                    {
                        TempData["error"] = "Usuario y/o contraseña incorrecto";
                        return View();
                    }

                }

            }
        }

        [HttpGet]
        [Route("/Home/logout")]
        public IActionResult CerrarSesion()
        {
            BorrarUsuarioLogeado();
            return RedirectToAction("Logueo", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void GuardarUsuarioLogeado(Usuario usuario)
        {
           
            HttpContext.Session.SetString("usuario", JsonConvert.SerializeObject(usuario));
        }

        public void BorrarUsuarioLogeado()
        {
            HttpContext.Session.Remove("usuario");
        }


    }
}
