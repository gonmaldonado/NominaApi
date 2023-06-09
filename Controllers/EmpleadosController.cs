using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NominaApi.Data;
using NominaApi.Models;
using NominaApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NominaApi.Controllers
{
    
    [Route("api/v1/[controller]")]
    [ApiController]

    public class EmpleadosController : ControllerBase
    {
        private readonly NominaDbContext _dbContext;
        private readonly IMapper _mapper;
        public EmpleadosController(NominaDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IList<EmpleadoDTO>>> ObtenerEmpleados()
        {
            var resultado = await _dbContext.Empleados.ToListAsync();
            if (resultado == null) {return NotFound(); }
            var empleadoDTO = _mapper.Map<List<EmpleadoDTO>>(resultado);
            return empleadoDTO;

        }
        [HttpGet]
        [Route("{legajo}")]
        public async Task<ActionResult<EmpleadoDTO>> ObtenerEmpleado([FromRoute] int legajo)
        {
            var resultado = await _dbContext.Empleados.FirstOrDefaultAsync(x => x.Legajo == legajo);
            if (resultado == null)
            {
                return NotFound();
            }
            var empleadoDTO = _mapper.Map<EmpleadoDTO>(resultado);
            return empleadoDTO;
        }
        [HttpGet]
        [Route("Legajo/{LegajoFrom:int}/{LegajoTo:int}")]
        public async Task<ActionResult<IList<EmpleadoDTO>>> ObtenerRangoEmpleadosPorLegajo([FromRoute] int legajoFrom, int legajoTo)
        {
            var resultado = await (_dbContext.Empleados.Where(x => x.Legajo >=legajoFrom && x.Legajo <=legajoTo)).OrderBy(x => x.Legajo).ToListAsync();
            if (resultado == null)
            {
                return NotFound();
            }
            var empleadoDTO = _mapper.Map<List<EmpleadoDTO>>(resultado);
            return empleadoDTO;
        }
        [HttpGet]
        [Route("Nombre/{Nombre}")]
        public async Task<ActionResult<IList<EmpleadoDTO>>> ObtenerEmpleadoPorNombre([FromRoute] string nombre)
        {
            var resultado = await _dbContext.Empleados.Where(x => x.Nombre.Contains(nombre)).ToListAsync();
            if (resultado == null)
            {
                return NotFound();
            }
           var empleadoDTO = _mapper.Map<List<EmpleadoDTO>>(resultado);
            return empleadoDTO;
        }
        [HttpGet]
        [Route("Apellido/{Apellido}")]
        public async Task<ActionResult<IList<EmpleadoDTO>>> ObtenerEmpleadoPorApellido([FromRoute] string apellido)
        {
            var resultado = await _dbContext.Empleados.Where(x => x.Apellido.Contains(apellido)).ToListAsync();
            if (resultado == null)
            {
                return NotFound();
            }
            var empleadoDTO = _mapper.Map<List<EmpleadoDTO>>(resultado);
            return empleadoDTO;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<EmpleadoDTO>> CrearEmpleado([FromBody] EmpleadoDTO empleadoDTO)
        {
            var empleado = _mapper.Map<Empleado>(empleadoDTO);
           await _dbContext.Empleados.AddAsync(empleado);
           await _dbContext.SaveChangesAsync();
            return empleadoDTO;
        }
 
        [HttpPut]
        [Route("{legajo}")]
        public async Task<ActionResult> ModificarEmpleado([FromRoute] int legajo, [FromBody] EmpleadoDTO empleadoDTO)
        {
            if (legajo != empleadoDTO.Legajo)
            {
                return BadRequest();
            }
            var empleado = await _dbContext.Empleados.FirstOrDefaultAsync(x => x.Legajo == legajo);
            empleado.Nombre = empleadoDTO.Nombre;
            empleado.Apellido = empleadoDTO.Apellido;
            empleado.Legajo = empleadoDTO.Legajo;
            _dbContext.Entry(empleado).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        [Route("{legajo}")]
        public async Task<ActionResult<Empleado>> EliminarEmpleado([FromRoute] int legajo)
        {
            var resultado = await _dbContext.Empleados.FirstOrDefaultAsync(x => x.Legajo == legajo);
            if (resultado == null)
            {
                return NotFound();
            }
            _dbContext.Empleados.Remove(resultado);
            await _dbContext.SaveChangesAsync();
            return resultado;

        }
    }
}
