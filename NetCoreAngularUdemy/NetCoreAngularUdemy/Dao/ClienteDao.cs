using Microsoft.AspNetCore.Mvc;
using NetCoreAngularUdemy.Modelos.ViewModels;
using NetCoreAngularUdemy.Modelos;
using System.Text;

namespace NetCoreAngularUdemy.Dao
{
    public class ClienteDao
    {
        private readonly IConfiguration configuration;

        public ClienteDao(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Resultado Login(string email)
        {
            Resultado resSalida = new Resultado();
            try
            {
                using (CursoAngularNetCoreContext basedatos = new CursoAngularNetCoreContext())
                {
                    Cliente cliente = basedatos.Clientes.Single(cli => cli.Email == email);
                    if (cliente != null)
                    {
                        resSalida.ObjetoGenerico = cliente;
                        resSalida.Error = "";
                        resSalida.Texto = "";
                    }
                    else
                    {
                        throw new Exception("Error al iniciar sesión");
                    }
                }
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = "";
                resSalida.Error = "Se produjo un error al iniciar sesión: " + ex.ToString();
                resSalida.Texto = "Usuario o password incorrecta";
            }
            return resSalida;
        }

        public Resultado AgregarCliente(ClienteViewmodel c)
        {
            Resultado resSalida = new Resultado();
            try
            {
                byte[] keyByte = new byte[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8
                };
                Util util = new Util(keyByte);

                using (CursoAngularNetCoreContext basedatos = new CursoAngularNetCoreContext())
                {
                    Cliente cliente = new Cliente();

                    cliente.Nombre = c.nombre;
                    cliente.Email = c.email;
                    cliente.Password = Encoding.ASCII.GetBytes(util.cifrar(c.pass, configuration["ClaveCifrado"]));
                    cliente.FechaAlta = DateTime.Now;
                    cliente.Rol = c.rol;
                    cliente.Estatus = (int)c.estatus;

                    basedatos.Clientes.Add(cliente);
                    basedatos.SaveChanges();

                    resSalida.ObjetoGenerico = cliente;
                    resSalida.Error = "";
                    resSalida.Texto = "";
                }
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = "";
                resSalida.Error = "Se produjo un error al agregar el cliente: " + ex.ToString();
                resSalida.Texto = "Se produjo un error al agregar el cliente Intentelo de nuevo.";
            }
            return resSalida;
        }

        public Resultado ActualizarCliente(ClienteViewmodel c)
        {
            Resultado resSalida = new Resultado();
            try
            {
                byte[] keyByte = new byte[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8
                };
                Util util = new Util(keyByte);

                using (CursoAngularNetCoreContext basedatos = new CursoAngularNetCoreContext())
                {
                    Cliente cliente = basedatos.Clientes.Single(cli => cli.Email == c.email);

                    cliente.Nombre = c.nombre;
                    cliente.Password = Encoding.ASCII.GetBytes(util.cifrar(c.pass, configuration["ClaveCifrado"])); ;

                    basedatos.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    basedatos.SaveChanges();

                    resSalida.ObjetoGenerico = cliente;
                    resSalida.Error = "";
                    resSalida.Texto = "";
                }
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = "";
                resSalida.Error = "Se produjo un error al modificar el cliente: " + ex.Message;
                resSalida.Texto = "Se produjo un error al modificar el cliente. Intente de nuevo.";
            }
            return resSalida;
        }

        public Resultado ObtenerClientes()
        {
            Resultado resSalida = new Resultado();
            try
            {
                using(CursoAngularNetCoreContext basedatos = new CursoAngularNetCoreContext())
                {
                    var lista = basedatos.Clientes.ToList();

                    resSalida.ObjetoGenerico = lista;
                    resSalida.Error = "";
                    resSalida.Texto = "";
                }
            }
            catch(Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Error al obtener los clientes: " + ex.Message;
                resSalida.Texto = "Error al obtener los clientes. Intente de nuevo";
            }
            return resSalida;
        }

        public Resultado EliminarCliente(string email)
        {
            Resultado resSalida = new Resultado();
            try
            {
                using (CursoAngularNetCoreContext basedatos = new CursoAngularNetCoreContext())
                {
                    Cliente cliente = basedatos.Clientes.Single(cli => cli.Email == email);

                    cliente.FechaBaja = DateTime.Now;
                    
                    basedatos.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    basedatos.SaveChanges();

                    resSalida.ObjetoGenerico = email;
                    resSalida.Error = "";
                    resSalida.Texto = "";
                }
            }
            catch(Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Error al eliminar el cliente: " + ex.Message;
                resSalida.Texto = "Error al eliminar el cliente. Intente de nuevo";
            }
            return resSalida;
        }
    }
}
