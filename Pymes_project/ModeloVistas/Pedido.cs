using Pymes_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pymes_project.ModeloVistas
{
    public class Pedido
    {
        public Clientes Clientes { set; get; }
        public InventarioLenceria Titulos { set; get; }
        public List<InventarioLenceria> Productos { set; get; }
    }
}