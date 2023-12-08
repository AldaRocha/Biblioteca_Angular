using NetCoreAngularUdemy.CQRS;
using NetCoreAngularUdemy.Dao;
using NetCoreAngularUdemy.Modelos;
using NetCoreAngularUdemy.Modelos.ViewModels;

namespace NetCoreAngularUdemy.AppService
{
    public class LibrosAppService
    {
        public Resultado AgregarLibro(ProductoViewmodel pvm)
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                LibrosCQRS lcqrs = new LibrosCQRS();

                resDevuelto = lcqrs.AgregarLibro(pvm);

                resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                resSalida.Error = resDevuelto.Error;
                resSalida.Texto = resDevuelto.Texto;
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Error al guardar el libro en el app service: " + ex.Message;
                resSalida.Texto = "Error al guardar el libro. Intente de nuevo";
            }
            return resSalida;
        }

        public Resultado ActualizarLibro(ProductoViewmodel pvm, int id)
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                LibrosCQRS lcqrs = new LibrosCQRS();

                resDevuelto = lcqrs.ActualizarLibro(pvm, id);

                resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                resSalida.Error = resDevuelto.Error;
                resSalida.Texto = resDevuelto.Texto;
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Error al actualizar el libro en el app service: " + ex.Message;
                resSalida.Texto = "Error al actualizar el libro. Intente de nuevo";
            }
            return resSalida;
        }
    
        public Resultado ObtenerLibros()
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                LibrosDAO ld = new LibrosDAO();

                resDevuelto = ld.ObtenerLibros();

                resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                resSalida.Error = resDevuelto.Error;
                resSalida.Texto = resDevuelto.Texto;
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Error al guardar el libro en el app service: " + ex.Message;
                resSalida.Texto = "Error al guardar el libro. Intente de nuevo";
            }
            return resSalida;
        }
    }
}
