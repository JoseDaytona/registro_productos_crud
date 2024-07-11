using Microsoft.AspNetCore.Mvc;
using FacturacionAPI.Data;
using FacturacionAPI.Models;

namespace FacturacionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoData _productoData;
        public ProductoController(ProductoData productoData)
        {
            _productoData = productoData;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
           List<Producto> ListaGenera = await _productoData.Lista();
            return StatusCode(StatusCodes.Status200OK, ListaGenera);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            Producto Resultado = await _productoData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, Resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Producto objeto)
        {
            bool Resultado = await _productoData.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new { Resultado });
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Producto objeto)
        {
            bool Resultado = await _productoData.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { Resultado });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool Resultado = await _productoData.Eliminar(id);
            return StatusCode(StatusCodes.Status200OK, new { Resultado });
        }
    }
}
