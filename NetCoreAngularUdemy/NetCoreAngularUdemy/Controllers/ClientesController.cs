using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreAngularUdemy.AppService;
using NetCoreAngularUdemy.CQRS;
using NetCoreAngularUdemy.Dao;
using NetCoreAngularUdemy.Modelos;
using NetCoreAngularUdemy.Modelos.ViewModels;
using NetCoreAngularUdemy.Servicios;
using NetCoreAngularUdemy.ViewModel;
using System.Text;

namespace NetCoreAngularUdemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<ClientesController> log;
        private ICliente clientesServicio;

        public ClientesController(IConfiguration configuration, ILogger<ClientesController> log, ICliente clientesServicio)
        {
            this.configuration = configuration;
            this.log = log;
            this.clientesServicio = clientesServicio;
        }

        [HttpGet]
        public IActionResult DameClientes()
        {
            Resultado res = new Resultado();
            try
            {
                var lista = clientesServicio.DameClientes();
                res.ObjetoGenerico = lista;
            }
            catch (Exception ex)
            {
                res.Error = "Se produjo un error al obtener los clientes: "+ex.Message;
                res.Texto = "Se produjo un error al obtener los clientes. Intente de nuevo.";
                log.LogError("Se produjo un error al obtener los clientes: " + ex.ToString());
            }
            return Ok(res);
        }

        [HttpPost("Cliente")]
        public IActionResult DameCliente(ClienteViewmodel c)
        {
            Resultado res = new Resultado();
            try
            {
                ClienteViewmodel aux = new ClienteViewmodel();
                var cliente = clientesServicio.DameCliente(c);
                aux.email = cliente.Email;
                aux.nombre = cliente.Nombre;
                res.ObjetoGenerico = aux;
            }
            catch(Exception ex)
            {
                res.Error = "Se produjo un error al obtener los datos del cliente: " + ex.Message;
                res.Texto = "Se produjo un error al obtener los datos del cliente. Intente de nuevo.";
                log.LogError("Se produjo un error al obtener los datos del cliente: " + ex.ToString());
            }
            return Ok(res);
        }

        [HttpPost]
        public IActionResult AgregarCliente(ClienteViewmodel c)
        {
            Resultado res = new Resultado();
            try
            {
                clientesServicio.AgregarCliente(c);
            }
            catch (Exception ex)
            {
                res.Error = "Se produjo un error al agregar el cliente: " + ex.ToString();
                res.Texto = "Se produjo un error al agregar el cliente Intentelo de nuevo.";
                log.LogError("Se produjo un error al agregar el cliente: " + ex.ToString());
            }
            return Ok(res);
        }

        [HttpPut]
        public IActionResult EditarCliente(ClienteViewmodel c)
        {
            Resultado res = new Resultado();
            try
            {
                clientesServicio.EditarCliente(c);
            }
            catch (Exception ex)
            {
                res.Error = "Se produjo un error al modificar el cliente: " + ex.Message;
                res.Texto = "Se produjo un error al modificar el cliente. Intente de nuevo.";
                log.LogError("Se produjo un error al modificar el cliente: " + ex.ToString());
            }
            return Ok(res);
        }

        [HttpDelete("{Email}")]
        public IActionResult BorrarCliente(String Email)
        {
            Resultado res = new Resultado();
            try
            {
                clientesServicio.BorrarCliente(Email);
            }
            catch (Exception ex)
            {
                res.Error = "Se produjo un error al borrar el cliente: " + ex.Message;
                res.Texto = "Se produjo un error al borrar el cliente. Intente de nuevo.";
                log.LogError("Se produjo un error al borrar el cliente: " + ex.ToString());
            }
            return Ok(res);
        }

        [HttpPost("Login")]
        public IActionResult Login(ClienteViewmodel c)
        {
            Resultado res = new Resultado();
            try
            {
                byte[] keyByte = new byte[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8
                };
                Util util = new Util(keyByte);
                ClienteViewmodel cliente = clientesServicio.Login(c);
                res.ObjetoGenerico = cliente;
            }
            catch (Exception ex)
            {
                res.Error = "Se produjo un error al iniciar sesión: " + ex.ToString();
                res.Texto = "Usuario o password incorrecta";
                log.LogError("Se produjo un error al iniciar sesión: " + ex.ToString());
            }
            return Ok(res);
        }

        // --------------------------------------------------------  UNIVERSIDAD  ----------------------------------------------//

        [HttpPost("Login/Universidad")]
        public IActionResult LoginUniversidad(ClienteViewmodel c)
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                Cliente usuario = new Cliente();
                ClienteDao cd = new ClienteDao(this.configuration);

                resDevuelto = cd.Login(c.email);

                byte[] keyBbyte = new byte[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8
                };
                Util util = new Util(keyBbyte);

                if (resDevuelto != null || resDevuelto.ObjetoGenerico != "")
                {
                    usuario = (Cliente)resDevuelto.ObjetoGenerico;
                    c.rol = usuario.Rol;
                }

                if (util.desCifrar(Encoding.ASCII.GetString(usuario.Password), configuration["ClaveCifrado"]) != c.pass)
                {
                    resSalida.ObjetoGenerico = c;
                    resSalida.Texto = "Usuario o password incorrecta";
                    resSalida.Error = resDevuelto.Error;
                }
                else
                {
                    resSalida.ObjetoGenerico = c;
                    resSalida.Texto = "Inicio de sessión realizado con exito";
                    resSalida.Error = resDevuelto.Error;
                }
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = "";
                resSalida.Error = "Se produjo un error al iniciar sesión: " + ex.ToString();
                resSalida.Texto = "Se produjo un error al iniciar sesión. Intente de nuevo.";
            }
            return Ok(resSalida);
        }

        [HttpPost("AgregarClienteUniversidad")]
        public IActionResult AgregarClienteUniversidad(ClienteViewmodel c)
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                UsuariosAppService uas = new UsuariosAppService(this.configuration);

                resDevuelto = uas.AgregarCliente(c);

                resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                resSalida.Error = resDevuelto.Error;
                resSalida.Texto = resDevuelto.Texto;
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Se produjo un error al iniciar sesión: " + ex.ToString();
                resSalida.Texto = "Usuario o password incorrecta";
            }
            return Ok(resSalida);
        }

        [HttpPost("ActualizarClienteUniversidad")]
        public IActionResult ActualizarClienteUniversidad(ClienteViewmodel c)
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                UsuariosAppService uas = new UsuariosAppService(this.configuration);

                resDevuelto = uas.ActualizarCliente(c);

                resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                resSalida.Error = resDevuelto.Error;
                resSalida.Texto = resDevuelto.Texto;
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Se produjo un error al iniciar sesión: " + ex.ToString();
                resSalida.Texto = "Usuario o password incorrecta";
            }
            return Ok(resSalida);
        }

        [HttpGet("DameClientesUniversidad")]
        public IActionResult DameClientesUniversidad()
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                UsuariosAppService uas = new UsuariosAppService(this.configuration);

                resDevuelto = uas.ObtenerClientes();

                resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                resSalida.Error = resDevuelto.Error;
                resSalida.Texto = resDevuelto.Texto;
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Se produjo un error al obtener los clientes: " + ex.Message;
                resSalida.Texto = "Se produjo un error al obtener los clientes. Intente de nuevo.";
            }
            return Ok(resSalida);
        }

        [HttpPost("EliminarClienteUniversidad")]
        public IActionResult EliminarClienteUniversidad(ClienteViewmodel c)
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                ClienteCQRS cqrs = new ClienteCQRS(this.configuration);

                resDevuelto = cqrs.EliminarCliente(c);

                resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                resSalida.Error = resDevuelto.Error;
                resSalida.Texto = resDevuelto.Texto;
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Se produjo un error al borrar el cliente: " + ex.Message;
                resSalida.Texto = "Se produjo un error al borrar el cliente. Intente de nuevo.";
            }
            return Ok(resSalida);
        }

        [HttpPost("RecuperarContraseña")]
        public IActionResult RecuperarContraseña(ClienteViewmodel c)
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                UsuariosAppService uap = new UsuariosAppService(this.configuration);

                resDevuelto = uap.RecuperarContraseña(c);

                resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                resSalida.Error = resDevuelto.Error;
                resSalida.Texto = resDevuelto.Texto;
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Se produjo un error al recuperar la contraseña: " + ex.ToString();
                resSalida.Texto = "Se produjo un error al recuperar la contraseña. Intente de nuevo.";
            }
            return Ok(resSalida);
        }

        [HttpPost("BuscarPorCorreo")]
        public IActionResult BuscarPorCorreo(ClienteViewmodel c)
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                UsuariosAppService uas = new UsuariosAppService(this.configuration);

                resDevuelto = uas.BuscarPorCorreo(c.email);

                resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                resSalida.Error = resDevuelto.Error;
                resSalida.Texto = resDevuelto.Texto;
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Error al iniciar sesion: " + ex.Message;
                resSalida.Texto = "Error al iniciar sesion. Intente de nuevo.";
            }
            return Ok(resSalida);
        }

        [HttpPost("BuscarPorCorreoPublic")]
        public IActionResult BuscarPorCorreoPublic(ClienteViewmodel c)
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                UsuariosAppService uas = new UsuariosAppService(this.configuration);

                resDevuelto = uas.BuscarPorCorreoPublic(c.email);

                resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                resSalida.Error = resDevuelto.Error;
                resSalida.Texto = resDevuelto.Texto;
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Error al iniciar sesion: " + ex.Message;
                resSalida.Texto = "Error al iniciar sesion. Intente de nuevo.";
            }
            return Ok(resSalida);
        }

        //[HttpPost]
        //public IActionResult AgregarCliente(ClienteViewmodel c)
        //{
        //    Resultado res = new Resultado();
        //    try
        //    {
        //        UsuariosAppService uas = new UsuariosAppService(configuration);

        //        uas.registrarCliente(c);
        //    }
        //    catch (Exception ex)
        //    {
        //        res.Error = "Se produjo un error al agregar el cliente: " + ex.ToString();
        //        res.Texto = "Se produjo un error al agregar el cliente Intentelo de nuevo.";
        //    }

        //    return Ok(res);
        //}
    }
}
