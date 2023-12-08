using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NetCoreAngularUdemy.Dao;
using NetCoreAngularUdemy.Modelos;
using NetCoreAngularUdemy.Modelos.ViewModels;
using System.Text;

namespace NetCoreAngularUdemy.CQRS
{
    public class ClienteCQRS
    {
        private readonly IConfiguration configuration;

        public ClienteCQRS(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Resultado AgregarCliente(ClienteViewmodel c)
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                bool guardar = true;

                if(c == null)
                {
                    resSalida.Texto = "Los campos no pueden ir vacios";
                    guardar = false;
                }
                if(c.nombre == null || c.nombre == "")
                {
                    resSalida.Texto = "El nombre esta vacio";
                    guardar = false;
                }
                if(c.email == null || c.email == "")
                {
                    resSalida.Texto = "El correo esta vacio";
                    guardar = false;
                }
                if (c.pass == null || c.pass == "")
                {
                    resSalida.Texto = "La contraseña esta vacia";
                    guardar = false;
                }
                if( c.rol == null || c.rol == "")
                {
                    resSalida.Texto = "El rol esta vacio";
                    guardar = false;
                }

                c.rol = "Cliente";
                c.estatus = 0;
                ClienteDao cd = new ClienteDao(this.configuration);
                if (guardar)
                {
                    resDevuelto = cd.AgregarCliente(c);

                    resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                    resSalida.Error = resDevuelto.Error;
                    resSalida.Texto = resDevuelto.Texto;
                }
            }
            catch(Exception ex)
            {
                resSalida.ObjetoGenerico = "";
                resSalida.Error = "Se produjo un error al iniciar sesión: " + ex.ToString();
                resSalida.Texto = "Usuario o password incorrecta";
            }
            return resSalida;
        }

        public Resultado ActualizarCliente(ClienteViewmodel c)
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                bool actualizar = true;

                if (c == null)
                {
                    resSalida.Texto = "Los campos no pueden ir vacios";
                    actualizar = false;
                }
                if (c.nombre == null || c.nombre == "")
                {
                    resSalida.Texto = "El nombre esta vacio";
                    actualizar = false;
                }
                if (c.email == null || c.email == "")
                {
                    resSalida.Texto = "El correo esta vacio";
                    actualizar = false;
                }
                if (c.pass == null || c.pass == "")
                {
                    resSalida.Texto = "La contraseña esta vacia";
                    actualizar = false;
                }
                if (c.rol == null || c.rol == "")
                {
                    resSalida.Texto = "El rol esta vacio";
                    actualizar = false;
                }

                ClienteDao cd = new ClienteDao(this.configuration);
                if (actualizar)
                {
                    resDevuelto = cd.ActualizarCliente(c);

                    resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                    resSalida.Error = resDevuelto.Error;
                    resSalida.Texto = resDevuelto.Texto;
                }
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = "";
                resSalida.Error = "Error al actualizar los datos del cliente: " + ex.Message;
                resSalida.Texto = "Error al actualizar los datos del cliente. Intente de nuevo.";
            }
            return resSalida;
        }

        public Resultado EliminarCliente(ClienteViewmodel c)
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                bool eliminar = true;

                if (c == null)
                {
                    resSalida.Texto = "Los campos no pueden ir vacios";
                    eliminar = false;
                }
                if (c.email == null || c.email == "")
                {
                    resSalida.Texto = "El nombre esta vacio";
                    eliminar = false;
                }

                ClienteDao cd = new ClienteDao(this.configuration);
                if (eliminar)
                {
                    resDevuelto = cd.EliminarCliente(c.email);

                    resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                    resSalida.Error = resDevuelto.Error;
                    resSalida.Texto = resDevuelto.Texto;
                }
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = "";
                resSalida.Error = "Error al eliminar el cliente: " + ex.Message;
                resSalida.Texto = "Error al eliminar el cliente. Intente de nuevo.";
            }
            return resSalida;
        }
    }
}
