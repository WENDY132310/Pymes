using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Pymes_project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Pymes_project
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<Pymes_sitio_web.Data.Pymes_projectContext,
                Migrations.Configuration>());
            ApplicationDbContext bd = new ApplicationDbContext();
            CrearRoles(bd);
            CrearSuperUsuario(bd);
            AdicionarPermisosSuperUsuario(bd);
            bd.Dispose();


        }

        private void CrearRoles(ApplicationDbContext bd)
        {
            var rolAdministrador = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(bd));
            if (!rolAdministrador.RoleExists("View"))
            {
                rolAdministrador.Create(new IdentityRole("View"));
            }
            if (!rolAdministrador.RoleExists("Details"))
            {
                rolAdministrador.Create(new IdentityRole("Details"));
            }
            if (!rolAdministrador.RoleExists("Edit"))
            {
                rolAdministrador.Create(new IdentityRole("Edit"));
            }
            if (!rolAdministrador.RoleExists("Create"))
            {
                rolAdministrador.Create(new IdentityRole("Create"));
            }
            if (!rolAdministrador.RoleExists("Delete"))
            {
                rolAdministrador.Create(new IdentityRole("Delete"));
            }
            if (!rolAdministrador.RoleExists("Admin"))
            {
                rolAdministrador.Create(new IdentityRole("Admin"));
            }
            if (!rolAdministrador.RoleExists("Usuario"))
            {
                rolAdministrador.Create(new IdentityRole("Usuario"));
            }

        }
        private void CrearSuperUsuario(ApplicationDbContext bd)
        {
            var usuarioAdministrador = new UserManager<ApplicationUser>(new
           UserStore<ApplicationUser>(bd));
            var usuario = usuarioAdministrador.FindByName("admin@gmail.com");
            if (usuario == null)
            {
                usuario = new ApplicationUser
                {   
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com"
                };
                usuarioAdministrador.Create(usuario, "Administrador");
            }
        }
        private void AdicionarPermisosSuperUsuario(ApplicationDbContext bd)
        {
            var usuarioAdministrador = new UserManager<ApplicationUser>(new
           UserStore<ApplicationUser>(bd));
            var rolAdministrador = new RoleManager<IdentityRole>(new
           RoleStore<IdentityRole>(bd));
            var usuario =
           usuarioAdministrador.FindByName("admin@gmail.com");
            if (!usuarioAdministrador.IsInRole(usuario.Id, "View"))
            {
                usuarioAdministrador.AddToRole(usuario.Id, "View");
            }
            if (!usuarioAdministrador.IsInRole(usuario.Id, "Detalis"))
            {
                usuarioAdministrador.AddToRole(usuario.Id, "Details");
            }
            if (!usuarioAdministrador.IsInRole(usuario.Id, "Create"))
            {
                usuarioAdministrador.AddToRole(usuario.Id, "Create");
            }
            if (!usuarioAdministrador.IsInRole(usuario.Id, "Edit"))
            {
                usuarioAdministrador.AddToRole(usuario.Id, "Edit");
            }
            if (!usuarioAdministrador.IsInRole(usuario.Id, "Delete"))
            {
                usuarioAdministrador.AddToRole(usuario.Id, "Delete");
            }
            if (!usuarioAdministrador.IsInRole(usuario.Id, "Admin"))
            {
                usuarioAdministrador.AddToRole(usuario.Id, "Admin");
            }
            if (!usuarioAdministrador.IsInRole(usuario.Id, "Usuario"))
            {
                usuarioAdministrador.AddToRole(usuario.Id, "Usuario");
            }
        }

    }
}
