using Microsoft.AspNetCore.Mvc;
using Tienda.Models;
using Tienda.Repositorios;
using System.Collections.Generic;

namespace Tienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresupuestosController : ControllerBase
    {
        private readonly IPresupuestosRepositorio _presupuestosRepository;
        private readonly IProductosRepositorio _productosRepositorio;

        public PresupuestosController(IPresupuestosRepositorio presupuestosRepository, IProductosRepositorio productosRepositorio)
        {
            _presupuestosRepository = presupuestosRepository;
            _productosRepositorio = productosRepositorio;
        }

        
        [HttpPost]
        public IActionResult CrearPresupuesto([FromBody] Presupuesto presupuesto)
        {
            if (presupuesto == null)
            {
                return BadRequest("El presupuesto no puede ser nulo.");
            }

            
            if (presupuesto.Detalle.Count == 0)
            {
                return BadRequest("El presupuesto debe tener al menos un detalle.");
            }

            _presupuestosRepository.CrearPresupuesto(presupuesto);
            return CreatedAtAction(nameof(ListarPresupuestos), new { id = presupuesto.IdPresupuesto }, presupuesto);
        }

        
        [HttpGet]
        public ActionResult<List<Presupuesto>> ListarPresupuestos()
        {
            var presupuestos = _presupuestosRepository.ListarPresupuestos();
            return Ok(presupuestos);
        }

       
        [HttpPost("{id}/ProductoDetalle")]
        public IActionResult AgregarProductoDetalle(int id, [FromBody] KeyValuePair<int, int> detalle)
        {
            if (detalle.Key <= 0 || detalle.Value <= 0)
            {
                return BadRequest("El id del producto y la cantidad deben ser mayores que cero.");
            }

            var presupuestoExistente = _presupuestosRepository.ObtenerPresupuestoPorId(id);
            if (presupuestoExistente == null)
            {
                return NotFound("Presupuesto no encontrado.");
            }

            
            var productoExistente = _productosRepositorio.obtenerPorID(detalle.Key);
            if (productoExistente.idProducto == 0)
            {
                return NotFound("Producto no encontrado.");
            }

            
            _presupuestosRepository.AgregarProductoAlPresupuesto(id, detalle.Key, detalle.Value);

            return NoContent();
        }

        
        [HttpPost("{id}")]
        public IActionResult AgregarProducto(int id, [FromBody] KeyValuePair<int, int> detalle)
        {
            if (detalle.Key <= 0 || detalle.Value <= 0)
            {
                return BadRequest("El id del producto y la cantidad deben ser mayores que cero.");
            }

            var presupuestoExistente = _presupuestosRepository.ObtenerPresupuestoPorId(id);
            if (presupuestoExistente == null)
            {
                return NotFound("Presupuesto no encontrado.");
            }

           
            var productoExistente = _productosRepositorio.obtenerPorID(detalle.Key);
            if (productoExistente.idProducto == 0)
            {
                return NotFound("Producto no encontrado.");
            }

            
            _presupuestosRepository.AgregarProductoAlPresupuesto(id, detalle.Key, detalle.Value);

            return NoContent();
        }
    }
}