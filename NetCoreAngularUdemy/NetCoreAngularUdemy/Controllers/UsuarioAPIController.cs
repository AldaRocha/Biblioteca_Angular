using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreAngularUdemy.Modelos;
using NetCoreAngularUdemy.Modelos.ViewModels;
using NetCoreAngularUdemy.Servicios;
using System.Text;

namespace NetCoreAngularUdemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioAPIController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private IUsuarioAPI usuarioAPIServicio;
        private readonly ILogger<UsuarioAPIController> log;
        public UsuarioAPIController(IConfiguration configuration, IUsuarioAPI usuarioAPIServicio, ILogger<UsuarioAPIController> log)
        {
            this.configuration = configuration;
            this.usuarioAPIServicio = usuarioAPIServicio;
            this.log = log;
        }

        //UTILIZADA DE FORMA TERMPORAL PARA EL ALTA
        //[HttpPost("Alta")]
        //public IActionResult AltaUsuario(AuthAPI usuarioAPI)
        //{
        //    Resultado res = new Resultado();
        //    try
        //    {
        //        byte[] keyBbyte = new byte[]
        //        {
        //            1, 2, 3, 4, 5, 6, 7, 8
        //        };
        //        Util util = new Util(keyBbyte);
        //        using(CursoAngularNetCoreContext basedatos = new CursoAngularNetCoreContext())
        //        {
        //            UsuariosApi api = new UsuariosApi();

        //            api.Email = usuarioAPI.email;
        //            api.Password = Encoding.ASCII.GetBytes(util.cifrar(usuarioAPI.password, configuration["ClaveCifrado"]));
        //            api.FechaAlta = DateTime.Now;

        //            basedatos.UsuariosApis.Add(api);
        //            basedatos.SaveChanges();
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        res.Error = "Se produjo un error al dar el alta el usuario de API: " + ex.ToString();
        //        res.Texto = "Se produjo un error al dar de alta. Intente de nuevo.";
        //    }
        //    return Ok(res);
        //}

        [HttpPost]
        public IActionResult DameUsuarioAPI(AuthAPI auth)
        {
            Resultado res = new Resultado();
            try
            {
                res.ObjetoGenerico = usuarioAPIServicio.Autenticacion(auth);
            }
            catch(Exception ex)
            {
                res.Error = "Se produjo un error al obtener el usuario de api: " + ex.Message;
                log.LogError("Error al obtener nuestro usuario de API: " + ex.ToString());
            }
            return Ok(res);
        }
    }
}
