using Microsoft.AspNetCore.Mvc;
using CFarma_TemplateN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CFarma_TemplateN.Data;
using Microsoft.EntityFrameworkCore;

namespace CarritoPrueba1.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ILogger<ClienteController> _logger;

        private readonly ApplicationDbContext _context;

        public ClienteController(ILogger<ClienteController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index2()
        {
            return View();
        }

        public IActionResult Listar()
        {
            var listarContactos = _context.Clientes.ToList();
            return View(listarContactos);
        }

        [HttpPost]
        public IActionResult Registro(Cliente objCliente)
        {
            if (ModelState.IsValid)
            {
                //guardar
                _context.Add(objCliente);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
           return View();
        }


        public async Task<IActionResult> Editar(int ? id)
        {
            if(id == null)
            {
                return NotFound();
            }else
            {
                var contacto = await _context.Clientes.FindAsync(id);
                if(contacto == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(contacto);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Editar2 (int codc, [Bind("codc, Nom, Apellido, Nrodoc, Sexo, Movil, Correo, Pswd")] Cliente objCli)
        {
        if(codc != objCli.codc)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(objCli);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                    
                }
                //Una vez se valide, me redirigie a esta página we
                //return RedirectToAction(nameof(Listar));
                return RedirectToAction("Index","Home");
                //return PartialView("~/Views/Home/Index.cshtml");
            }

            return View(objCli);
        }

        public IActionResult Eliminar (int ? id)
        {
            //esta wea de _contextClientes ---> es por lo de tu DbContext
            var cliente = _context.Clientes.Find(id);
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();

            return RedirectToAction(nameof(Listar));

        }


    }
}
