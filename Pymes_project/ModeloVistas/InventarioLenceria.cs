using Pymes_project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Pymes_project.ModeloVistas
{
    public class InventarioLenceria
    {

        [Display(Name = "ID producto")]
        public int idcodigo { set; get; }
        [Display(Name = "Código")]
        public int codigo { set; get; }
        [Display(Name = "Nombre producto")]
        public string nombreproducto { set; get; }
        [Display(Name = "Valor unitario")]
        public int valorunitario { set; get; }
        [Display(Name = "Cantidad Vendida")]
        public int cantidadvendida { set; get; }

        [Display(Name = "Valor a pagar")]
        public decimal totalventaproducto
        {get{return valorunitario *cantidadvendida;}}
    }
}