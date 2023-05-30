using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Pymes_project.ModeloVistas
{
    public class Clientes
    {
        [Display(Name = "Nit")]
        public int idnit { set; get; }
        [Display(Name = "Cliente")]
        public string empresa { set; get; }
        [Display(Name = "Fecha de Compra")]
        [DataType(DataType.Date)]
        public DateTime fechaventa { set; get; }
        [Display(Name = "Pedido Nro.")]
        public int nropedido { set; get; }
        [Display(Name = "Observación")]
        public string comentario { set; get; }
    }
}