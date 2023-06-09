﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pymes_project.ModeloVistas
{
    public class VistaUsuarios
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public VistaRoles Rol { get; set; }
        public List<VistaRoles> Roles { get; set; }

    }
}