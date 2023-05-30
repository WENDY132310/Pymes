using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Pymes_project.ModeloVistas;
using Pymes_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Pymes_project.Controllers
{
    public class UsuariosController : Controller
    {
        // Se declara la instancia bd de tipo ApplicationContext.

        private ApplicationDbContext bd = new ApplicationDbContext();

        // rol de autenticacion para usuarios Admin.
        [Authorize(Roles = "Admin")]
        // procedimiento para listar los usuarios.
        // inicio procedimiento Index
        // GET: Usuarios
        public ActionResult Index()
        {
            var usuarioAdministrador = new UserManager<ApplicationUser>
           (new UserStore<ApplicationUser>(bd));
            var usuarios = usuarioAdministrador.Users.ToList();
            var vistausuarios = new List<VistaUsuarios>();
            foreach (var usuario in usuarios)
            {
                var vistausuario = new VistaUsuarios
                {
                    Email = usuario.Email,
                    Name = usuario.UserName,
                    UserID = usuario.Id
                };
                vistausuarios.Add(vistausuario);
            }
            return View(vistausuarios);
        }
        // finaliza procedimiento Index
        // procedimiento para visualizar los roles del usuario.
        // inicio procedimiento Roles
        public ActionResult Roles(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usuarioAdministrador = new UserManager<ApplicationUser>
           (new UserStore<ApplicationUser>(bd));
            var usuarios = usuarioAdministrador.Users.ToList();
            var usuario = usuarios.Find(u => u.Id == userId);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            var rolAdministrador = new RoleManager<IdentityRole>
           (new RoleStore<IdentityRole>(bd));
            var roles = rolAdministrador.Roles.ToList();
            var vistaroles = new List<VistaRoles>();
            foreach (var elemento in usuario.Roles)
            {
                var rol = roles.Find(r => r.Id == elemento.RoleId);
                var rolvista = new VistaRoles
                {
                    RoleId = rol.Id,
                    Name = rol.Name
                };
                vistaroles.Add(rolvista);
            }
            var vistausuario = new VistaUsuarios
            {
                Email = usuario.Email,
                Name = usuario.UserName,
                UserID = usuario.Id,
                Roles = vistaroles
            };
            return View(vistausuario);
        }
        // finalizar procedimiento Roles.
        //Procedimiento para listar y seleccionar un rol.
        // inicio procedimiento GET AdicionarRol.
        [HttpGet]
        public ActionResult AdicionarRol(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usuarioAdministrador = new UserManager<ApplicationUser>
           (new UserStore<ApplicationUser>(bd));
            var usuarios = usuarioAdministrador.Users.ToList();
            var usuario = usuarios.Find(u => u.Id == userId);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            var vistausuario = new VistaUsuarios
            {
                Email = usuario.Email,
                Name = usuario.UserName,
                UserID = usuario.Id
            };
            var rolAdministrador = new RoleManager<IdentityRole>
           (new RoleStore<IdentityRole>(bd));
            var lista = rolAdministrador.Roles.ToList();
            lista.Add(new IdentityRole { Id = "", Name = "[Seleccione un Rol...]" });
            lista = lista.OrderBy(r => r.Name).ToList();
            ViewBag.RoleID = new SelectList(lista, "Id", "Name");
            return View(vistausuario);
        }
        // finaliza procedimiento GET Adicionarrol
        // procedimiento buscar y adicionar el respectivo rol
        // inicio procedimiento POST AdicionarRol
        [HttpPost]
        public ActionResult AdicionarRol(string userId, FormCollection form)
        {
            var rolId = Request["RoleID"];
            var rolAdministrador = new RoleManager<IdentityRole>
           (new RoleStore<IdentityRole>(bd));
            var usuarioAdministrador = new UserManager<ApplicationUser>
           (new UserStore<ApplicationUser>(bd));
            var usuarios = usuarioAdministrador.Users.ToList();
            var usuario = usuarios.Find(u => u.Id == userId);
            var vistausuario = new VistaUsuarios
            {
                Email = usuario.Email,
                Name = usuario.UserName,
                UserID = usuario.Id
            };
            if (string.IsNullOrEmpty(rolId))
            {
                ViewBag.Error = "Debe seleccionar un ROL....";
                var lista = rolAdministrador.Roles.ToList();
                lista.Add(new IdentityRole { Id = "", Name = "[Seleccione un Rol...]" });
                lista = lista.OrderBy(r => r.Name).ToList();
                ViewBag.RoleID = new SelectList(lista, "Id", "Name");
                return View(vistausuario);
            }
            var roles = rolAdministrador.Roles.ToList();
            var rol = roles.Find(r => r.Id == rolId);
            if (!usuarioAdministrador.IsInRole(userId, rol.Name))
            {
                usuarioAdministrador.AddToRole(userId, rol.Name);
            }
            var vistaroles = new List<VistaRoles>();
            foreach (var elemento in usuario.Roles)
            {
                rol = roles.Find(r => r.Id == elemento.RoleId);
                var rolvista = new VistaRoles
                {
                    RoleId = rol.Id,
                    Name = rol.Name
                };
                vistaroles.Add(rolvista);
            }
            vistausuario = new VistaUsuarios
            {
                Email = usuario.Email,
                Name = usuario.UserName,
                UserID = usuario.Id,
                Roles = vistaroles
            };
            return View("Roles", vistausuario);
        }
        // finaliza procedimiento POST AdicionarRol
        //procedimiento para eliminar un rol
        // inicio procedimiento Delete
        public ActionResult Delete(string userID, string roleID)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(roleID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rolAdministrador = new RoleManager<IdentityRole>
           (new RoleStore<IdentityRole>(bd));
            var usuarioAdministrador = new UserManager<ApplicationUser>
           (new UserStore<ApplicationUser>(bd));
            var usuario = usuarioAdministrador.Users.ToList().Find(u => u.Id == userID);
            var rol = rolAdministrador.Roles.ToList().Find(r => r.Id == roleID);
            if (usuarioAdministrador.IsInRole(usuario.Id, rol.Name))
            {
                usuarioAdministrador.RemoveFromRole(usuario.Id, rol.Name);
            }
            var usuarios = usuarioAdministrador.Users.ToList();
            var roles = rolAdministrador.Roles.ToList();
            var vistaroles = new List<VistaRoles>();
            foreach (var elemento in usuario.Roles)
            {
                rol = roles.Find(r => r.Id == elemento.RoleId);
                var rolvista = new VistaRoles
                {
                    RoleId = rol.Id,
                    Name = rol.Name
                };
                vistaroles.Add(rolvista);
            }
            var vistausuario = new VistaUsuarios
            {
                Email = usuario.Email,
                Name = usuario.UserName,
                UserID = usuario.Id,
                Roles = vistaroles
            };
            return View("Roles", vistausuario);
        }
        // finaliza procedimiento Delete
        //Procedimiento para liberar memoria
        // inicio procedimiento Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                bd.Dispose();
            }
            base.Dispose(disposing);
        }
    }
    // finaliza procedimiento Dispose
}