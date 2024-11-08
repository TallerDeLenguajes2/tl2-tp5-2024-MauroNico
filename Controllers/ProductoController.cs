using Microsoft.AspNetCore.Mvc;
using Tienda.Models;
using Tienda.Repositorios;
using System.Collections.Generic;

namespace Tienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductosRepositorio _productosRepositorio;

        public ProductoController(IProductosRepositorio productosRepositorio)
        {
            _productosRepositorio = productosRepositorio;
        }

        
        [HttpPost]
        public IActionResult CrearProducto([FromBody] Producto producto)
        {
            if (producto == null)
            {
                return BadRequest("El producto no puede ser nulo.");
            }

            
            _productosRepositorio.crearProducto(producto);

            
            return CreatedAtAction(nameof(CrearProducto), new { id = producto.idProducto }, producto);
        }

        
        [HttpGet]
        public ActionResult<List<Producto>> ListarProductos()
        {
            var productos = _productosRepositorio.listarProductos();
            return Ok(productos);
        }

        
        [HttpPut("{id}")]
        public IActionResult ModificarProducto(int id, [FromBody] Producto producto)
        {
            if (producto == null)
            {
                return BadRequest("El producto no puede ser nulo.");
            }

            var productoExistente = _productosRepositorio.obtenerPorID(id);
            if (productoExistente.idProducto == 0)
            {
                return NotFound("Producto no encontrado.");
            }

            _productosRepositorio.modificarProducto(id, producto);
            return NoContent();
        }
    }
}