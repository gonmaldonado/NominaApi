using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NominaApi.Models.DTOs
{
    public class EmpleadoDTO
    {
        public int Legajo { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
    }
}
