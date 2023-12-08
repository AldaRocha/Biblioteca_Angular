using NetCoreAngularUdemy.CQRS;
using NetCoreAngularUdemy.Dao;
using NetCoreAngularUdemy.Modelos;
using NetCoreAngularUdemy.Modelos.ViewModels;
using NetCoreAngularUdemy.ViewModel;

namespace NetCoreAngularUdemy.AppService
{
    public class UsuariosAppService
    {
        private readonly IConfiguration configuration;

        public UsuariosAppService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Resultado AgregarCliente(ClienteViewmodel c)
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                ClienteCQRS ccqrs = new ClienteCQRS(configuration);
                EmailService es = new EmailService();

                resDevuelto = ccqrs.AgregarCliente(c);

                if(resDevuelto.ObjetoGenerico != null || resDevuelto.ObjetoGenerico != "")
                {
                    es.enviarCorreo(c.email, "Confirma tu cuenta");
                }

                resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                resSalida.Error = resDevuelto.Error;
                resSalida.Texto = resDevuelto.Texto;
            }
            catch(Exception ex)
            {
                resSalida.ObjetoGenerico = "";
                resSalida.Error = "Se produjo un error al registrar el cliente: " + ex.Message;
                resSalida.Texto = "Se produjo un error al registrar el cliente. Intente de nuevo.";
            }
            return resSalida;
        }

        public Resultado ActualizarCliente(ClienteViewmodel c)
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                ClienteCQRS ccqrs = new ClienteCQRS(configuration);
                EmailService es = new EmailService();

                resDevuelto = ccqrs.ActualizarCliente(c);

                if (resDevuelto.ObjetoGenerico != null || resDevuelto.ObjetoGenerico != "")
                {
                    es.enviarCorreo(c.email, "Confirma tu informacion");
                }

                resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                resSalida.Error = resDevuelto.Error;
                resSalida.Texto = resDevuelto.Texto;
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = "";
                resSalida.Error = "Se produjo un error al registrar el cliente: " + ex.Message;
                resSalida.Texto = "Se produjo un error al registrar el cliente. Intente de nuevo.";
            }
            return resSalida;
        }

        public Resultado ObtenerClientes()
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                ClienteDao cd = new ClienteDao(this.configuration);

                resDevuelto = cd.ObtenerClientes();

                resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                resSalida.Error = resDevuelto.Error;
                resSalida.Texto = resDevuelto.Texto;
            }
            catch(Exception ex)
            {
                resSalida.ObjetoGenerico = "";
                resSalida.Error = "Se produjo un error al obtener los cliente: " + ex.Message;
                resSalida.Texto = "Se produjo un error al obtener los cliente. Intente de nuevo.";
            }
            return resSalida;
        }

        public Resultado BuscarPorCorreoPublic(string email)
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                Cliente usuario = new Cliente();
                ClienteDao cd = new ClienteDao(this.configuration);

                resDevuelto = cd.Login(email);

                if (resDevuelto.ObjetoGenerico != null || resDevuelto.ObjetoGenerico != "")
                {
                    usuario = (Cliente)resDevuelto.ObjetoGenerico;
                }

                UsuarioPublic up = new UsuarioPublic(usuario.Id, usuario.Email);

                resSalida.ObjetoGenerico = up;
                resSalida.Error = resDevuelto.Error;
                resSalida.Texto = resDevuelto.Texto;
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = "";
                resSalida.Error = "Error al buscar por correo: " + ex.Message;
                resSalida.Texto = "Error al buscar por correo. Intente de nuevo";
            }
            return resSalida;
        }

        public bool RecuperarCuenta(string correo)
        {
            bool resultado = false;
            try
            {
                ClienteDao cd = new ClienteDao(this.configuration);
                EmailService es = new EmailService();
                Resultado resDevuelto = cd.Login(correo);
                
                if(resDevuelto.ObjetoGenerico != null || resDevuelto.ObjetoGenerico != "") 
                {
                    resultado = es.enviarCorreo(correo, "Estas apunto de recuperar tu cuenta, solo sigue los siguientes paso: ...");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Se produjo un error al registrar el cliente. Intente de nuevo." + ex.Message);
            }
            return resultado;
        }

        public Resultado RecuperarContraseña(ClienteViewmodel c)
        {
            Resultado resSalida = new Resultado();
            try
            {
                bool enviado = false;
                Resultado resDevuelto = new Resultado();
                ClienteCQRS cqrs = new ClienteCQRS(configuration);
                EmailService es = new EmailService();

                resDevuelto = cqrs.ActualizarCliente(c);
                if (resDevuelto.ObjetoGenerico != null || resDevuelto.ObjetoGenerico != "")
                {
                    enviado = es.enviarCorreo(c.email, ": ...");
                }

                resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                resSalida.Error = resDevuelto.Error;
                resSalida.Texto = enviado ? resDevuelto.Texto + "/Correo enviado con exito" : resDevuelto.Texto;
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = "";
                resSalida.Error = "Error al recuperar la contraseña: " + ex.Message;
                resSalida.Texto = "Error al recuperar la contraseña, Intente de nuevo.";
            }
            return resSalida;
        }

        public Resultado BuscarPorCorreo(string email)
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                Cliente usuario = new Cliente();
                ClienteDao cd = new ClienteDao(this.configuration);

                resDevuelto = cd.Login(email);

                if (resDevuelto.ObjetoGenerico != null || resDevuelto.ObjetoGenerico != "")
                {
                    usuario = (Cliente)resDevuelto.ObjetoGenerico;
                }

                resSalida.ObjetoGenerico = usuario;
                resSalida.Error = resDevuelto.Error;
                resSalida.Texto = resDevuelto.Texto;
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = "";
                resSalida.Error = "Error al buscar por correo: " + ex.Message;
                resSalida.Texto = "Error al buscar por correo. Intente de nuevo";
            }
            return resSalida;
        }
    }
}
