using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Pymes_project.Models
{
    public class Venta
    {
        [Key]
        [Display(Name = "ID pedido")]
        public int idnropedido { set; get; }
        [Display(Name = "Número de pedido")]
        public int nropedido { set; get; }
        [Display(Name = "ID Nit")]
        public int idnit { set; get; }
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}",
       ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de venta")]
        [DataType(DataType.Date)]
        public DateTime fechaventa { set; get; }
        [Display(Name = "Observación")]
        public string comentario { set; get; }
        public virtual Clientes Clientes { set; get; }
        public virtual ICollection<Compras> Compras { set; get; }
    }
}