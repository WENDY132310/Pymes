using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Pymes_project.Models
{
    public class Empleados
    {
        [Key]
        [Display(Name = "ID ")]
        public int id { set; get; }

        [Display(Name = "Empleado")]
        public string empleado { set; get; }
        [Display(Name = "Cargo")]
        public string cargo { set; get; }
        [Display(Name = "Teléfono")]
        public string telefono { set; get; }
        [Display(Name = "Ciudad")]
        public string ciudad { set; get; }
        public virtual ICollection<Venta> Ventas { set; get; }
    }
}