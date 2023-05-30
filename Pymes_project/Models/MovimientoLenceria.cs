using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Pymes_project.Models
{
    public class MovimientoLenceria
    {
        [Key]
        public int idventa { set; get; }
        public int idnropedido { set; get; }
        public int idcodigo { set; get; }
        [Display(Name = "Cantida Vendida")]
        public int cantidadvendida { set; get; }
        [Display(Name = "Valor Venta")]
        public int valorventa { set; get; }
        public virtual Compras Compras { set; get; }
        public virtual InventarioLenceria InventarioLenceria { set; get; }
    }
}