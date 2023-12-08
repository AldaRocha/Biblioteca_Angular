using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using NetCoreAngularUdemy.AppService;
using NetCoreAngularUdemy.Modelos;
using NetCoreAngularUdemy.Modelos.ViewModels;
using NetCoreAngularUdemy.Servicios;

namespace NetCoreAngularUdemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductosController : ControllerBase
    {
        private IProductos productoServicio;
        private readonly ILogger<ProductosController> log;

        public ProductosController(IProductos productoServicio, ILogger<ProductosController> log)
        {
            this.productoServicio = productoServicio;
            this.log = log;
        }

        [HttpPost("agregar")]
        public IActionResult AgregarLibro(ProductoViewmodel pvm)
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                LibrosAppService las = new LibrosAppService();

                resDevuelto = las.AgregarLibro(pvm);

                resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                resSalida.Error = resDevuelto.Error;
                resSalida.Texto = resDevuelto.Texto;
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Error al agregar el libro en el controller: " + ex.Message;
                resSalida.Texto = "Error al agregar el libro. Intente de nuevo";
            }
            return Ok(resSalida);
        }

        [HttpPut("actualizar/{id}")]
        public IActionResult ActualizarLibro(int id, [FromBody] ProductoViewmodel pvm)
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                LibrosAppService las = new LibrosAppService();

                resDevuelto = las.ActualizarLibro(pvm, id);

                resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                resSalida.Error = resDevuelto.Error;
                resSalida.Texto = resDevuelto.Texto;
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Error al actualizar el libro en el controller: " + ex.Message;
                resSalida.Texto = "Error al actualizar el libro. Intente de nuevo";
            }
            return Ok(resSalida);
        }

        [HttpPost]
        public IActionResult AgregarPedido(PedidoViewModel p)
        {
            Resultado resSalida = new Resultado();
            try
            {
                productoServicio.AgregarPedido(p);
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Se produjo un error al realizar el pedido: " + ex.Message;
                resSalida.Texto = "Se produjo un error al realizar el pedido. Intente de nuevo.";
                log.LogError("Se produjo un error al realizar el pedido: " + ex.ToString());
            }
            return Ok(resSalida);
        }

        [HttpGet]
        public IActionResult DameProductos()
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                LibrosAppService las = new LibrosAppService();

                resDevuelto = las.ObtenerLibros();

                resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                resSalida.Error = resDevuelto.Error;
                resSalida.Texto = resDevuelto.Texto;
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Se produjo un error al obtener los libros en el controller: " + ex.Message;
                resSalida.Texto = "Se produjo un error al obtener los libros. Intente de nuevo";
                log.LogError("Se produjo un error al obtener los libros: " + ex.ToString());
            }
            return Ok(resSalida);
        }

        [HttpPost("Pedidos")]
        public IActionResult PedidosClientes(ClienteViewmodel c)
        {
            Resultado res = new Resultado();
            try
            {
                var lista = productoServicio.PedidosClientes(c);

                res.ObjetoGenerico = lista;
            }
            catch(Exception ex)
            {
                res.Error = "Se produjo un error al obtener los pedidos del cliente: " + ex.Message;
                res.Texto = "Se produjo un error al obtener los pedidos del cliente. Intente de nuevo.";
                log.LogError("Se produjo un error al obtener los pedidos del cliente: " + ex.ToString());
            }
            return Ok(res);
        }
    }
}
