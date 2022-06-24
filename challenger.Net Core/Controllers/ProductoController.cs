using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using challenger.Net_Core.Models;

namespace challenger.Net_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly DBGESTIONSTOCKContext _dbcontex;

        public ProductoController(DBGESTIONSTOCKContext context)
        {
            _dbcontex = context;
        }

        //La lista de productos se deberá filtrar en base a su precio.

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()

        {

            List<Producto> lista = _dbcontex.Productos.OrderByDescending(p => p.IdProducto).ThenBy(p => p.FechaCarga).ToList();

            return StatusCode(StatusCodes.Status200OK, lista);

        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] Producto request)
        {
            await _dbcontex.Productos.AddAsync(request);
            await _dbcontex.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "ok");

        }
        [HttpDelete]
        [Route("Cerrar/{id:int}")]
        public async Task<IActionResult> Cerrar(int id)
        {
            Producto producto = _dbcontex.Productos.Find(id);

            _dbcontex.Productos.Remove(producto);
            await _dbcontex.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, "ok");

        }
        //Se deberá filtrar un único producto de cada categoría
        [HttpGet]
        [Route("Filtrar")]
        public async Task<IActionResult> Filtrar()
        {
            List<Producto> lista = _dbcontex.Productos.Where(p => p.Categoria == "PRODDOS" && p.Categoria == "PRODUNO").ToList();
            return StatusCode(StatusCodes.Status200OK, lista);
        }
        //Se deberá ofrecer el de mayor precio.
        [HttpGet]
        [Route("MayorPrecio")]
        public async Task<IActionResult> MayorPrecio()
        {
            Producto producto = _dbcontex.Productos.OrderByDescending(p => p.Precio).FirstOrDefault();
            return StatusCode(StatusCodes.Status200OK, producto);
        }
        //El precio de ambos productos sumados debe ser menor o igual al presupuesto del cliente
        [HttpGet]
        [Route("Precio")]
        public async Task<IActionResult> Precio()
        {
            List<Producto> lista = _dbcontex.Productos.Where(p => p.Precio <= 100).ToList();
            return StatusCode(StatusCodes.Status200OK, lista);
        }
        //El filtro solo debe aceptar valores enteros.
        [HttpGet]
        [Route("Filtro")]
        public async Task<IActionResult> Filtro()
        {
            List<Producto> lista = _dbcontex.Productos.Where(p => p.Precio % 2 == 0).ToList();
            return StatusCode(StatusCodes.Status200OK, lista);
        }
        //Filtro debe permitir valores entre 0 y 1.000.000
        [HttpGet]
        [Route("Filtro2")]
        public async Task<IActionResult> Filtro2()
        {
            List<Producto> lista = _dbcontex.Productos.Where(p => p.Precio >= 0 && p.Precio <= 1000000).ToList();
            return StatusCode(StatusCodes.Status200OK, lista);
        }
        // ofrecer al cliente el resultado debe ser vacío.
        [HttpGet]
        [Route("Filtro3")]
        public async Task<IActionResult> Filtro3()
        {
            List<Producto> lista = _dbcontex.Productos.Where(p => p.Precio == 0).ToList();
            return StatusCode(StatusCodes.Status200OK, lista);
        }


        //El monto total de la venta debe ser siempre el que menor diferencia tenga con el presupuesto del cliente.
        [HttpGet]
        [Route("Filtro4")]
        public async Task<IActionResult> Filtro6()
        {
            List<Producto> lista = _dbcontex.Productos.Where(p => p.Precio == _dbcontex.Productos.Min(p => p.Precio)).ToList();
            return StatusCode(StatusCodes.Status200OK, lista);
        }



    }
}
