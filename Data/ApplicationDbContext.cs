using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFarma_TemplateN.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CFarma_TemplateN.Models.Cliente> Clientes { get; set; }
        public DbSet<CFarma_TemplateN.Models.Producto> Productos { get; set; }
        public DbSet<CFarma_TemplateN.Models.Administrador> Administradores { get; set; }
        public DbSet<CFarma_TemplateN.Models.Pedido> Pedidos { get; set; }
        public DbSet<CFarma_TemplateN.Models.Detalle_pedido> Detalle_pedidos { get; set; }
        public DbSet<CFarma_TemplateN.Models.EstadoPedido> EstadoPedidos { get; set; }
        public DbSet<CFarma_TemplateN.Models.Categoria> Categoria { get; set; }
        public DbSet<CFarma_TemplateN.Models.SubCategoria> SubCategoria { get; set; }
        public DbSet<CFarma_TemplateN.Models.Repartidor> Repartidor { get; set; }
        public DbSet<CFarma_TemplateN.Models.Turno> Turno { get; set; }
        public DbSet<CFarma_TemplateN.Models.Horario> Horario { get; set; }
        public DbSet<CFarma_TemplateN.Models.EstadoPedido> EstadoPedido { get; set; }



        public DbSet<Models.CustomTypes.sp_select_turnos> sp_select_turnos { get; set; }

        public DbSet<Models.CustomTypes.sp_select_pedido_estados> sp_select_pedido_estados { get; set; }

    }
}
