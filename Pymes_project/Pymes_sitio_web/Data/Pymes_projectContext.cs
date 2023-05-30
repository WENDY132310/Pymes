using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Pymes_sitio_web.Data
{
    public class Pymes_projectContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Pymes_projectContext() : base("name=Pymes_projectContext")
        {
        }

        public System.Data.Entity.DbSet<Pymes_project.Models.Clientes> Clientes { get; set; }

        public System.Data.Entity.DbSet<Pymes_project.Models.Empleados> Empleados { get; set; }

        public System.Data.Entity.DbSet<Pymes_project.Models.InventarioLenceria> InventarioLencerias { get; set; }

        public System.Data.Entity.DbSet<Pymes_project.Models.Compras> Compras { get; set; }

        public System.Data.Entity.DbSet<Pymes_project.Models.Venta> Ventas { get; set; }

        public System.Data.Entity.DbSet<Pymes_project.Models.MovimientoLenceria> MovimientoLencerias { get; set; }
    }
}
