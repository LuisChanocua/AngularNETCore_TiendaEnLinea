﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tiendaenlinea.DTO;

namespace Tiendaenlinea.BLL.Servicios.Contrato
{
    public interface IRolService
    {
        Task<List<RolDTO>> ListarRol();
    }
}