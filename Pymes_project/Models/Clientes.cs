using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Pymes_project.Models
{
    public class Clientes
    {
        [Key]
        [Display(Name = "ID NIT")]
        public int idnit { set; get; }
        [Display(Name = "Nit")]
        public int nit { set; get; }
        [Display(Name = "Cliente")]
        public string empresa { set; get; }
        [Display(Name = "Dirección")]
        public string direccion { set; get; }
        [Display(Name = "Teléfono")]
        public string telefono { set; get; }
        [Display(Name = "Ciudad")]
        public string ciudad { set; get; }
        public virtual ICollection<Venta> Ventas { set; get; }
    }
}