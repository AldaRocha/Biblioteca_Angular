using NetCoreAngularUdemy.Dao;
using NetCoreAngularUdemy.Modelos;
using NetCoreAngularUdemy.Modelos.ViewModels;

namespace NetCoreAngularUdemy.CQRS
{
    public class LibrosCQRS
    {
        public Resultado AgregarLibro(ProductoViewmodel pvm)
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                bool guardar = true;

                if (pvm == null) {
                    resSalida.Texto = "Los datos del libro no pueden estar vacios."; 
                    guardar = false;
                }
                if (pvm.nombre == null || pvm.nombre == "")
                {
                    resSalida.Texto = "El nombre del libro no puede estar vacio.";
                    guardar = false;
                }
                if (pvm.descripcion == null || pvm.descripcion == "")
                {
                    resSalida.Texto = "La descripcion no puede estar vacia.";
                    guardar = false;
                }
                if (pvm.precio == null || pvm.precio <= 0)
                {
                    resSalida.Texto = "El precio no puede ser 0 ni estar vacio";
                    guardar = false;
                }
                if (pvm.pdf == null || pvm.pdf == "")
                {
                    resSalida.Texto = "El pdf no puede ir vacio.";
                    guardar = false;
                }

                if (guardar)
                {
                    LibrosDAO ld = new LibrosDAO();

                    resDevuelto = ld.AgregarLibro(pvm);

                    resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                    resSalida.Error = resDevuelto.Error;
                    resSalida.Texto = resDevuelto.Texto;
                }
                else
                {
                    resSalida.ObjetoGenerico = null;
                    resSalida.Error = "No fue posible guardar el libro.";
                }
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Error al guardar el libro en el cqrs: " + ex.Message;
                resSalida.Texto = "Error al guardar el libro. Intente de nuevo.";
            }
            return resSalida;
        }

        public Resultado ActualizarLibro(ProductoViewmodel pvm, int id)
        {
            Resultado resSalida = new Resultado();
            try
            {
                Resultado resDevuelto = new Resultado();
                bool guardar = true;

                if (pvm == null)
                {
                    resSalida.Texto = "Los datos del libro no pueden estar vacios.";
                    guardar = false;
                }
                if (pvm.nombre == null || pvm.nombre == "")
                {
                    resSalida.Texto = "El nombre del libro no puede estar vacio.";
                    guardar = false;
                }
                if (pvm.descripcion == null || pvm.descripcion == "")
                {
                    resSalida.Texto = "La descripcion no puede estar vacia.";
                    guardar = false;
                }
                if (pvm.precio == null || pvm.precio <= 0)
                {
                    resSalida.Texto = "El precio no puede ser 0 ni estar vacio";
                    guardar = false;
                }
                if (pvm.pdf == null || pvm.pdf == "")
                {
                    resSalida.Texto = "El pdf no puede ir vacio.";
                    guardar = false;
                }

                if (guardar)
                {
                    LibrosDAO ld = new LibrosDAO();

                    resDevuelto = ld.ActualizarLibro(pvm, id);

                    resSalida.ObjetoGenerico = resDevuelto.ObjetoGenerico;
                    resSalida.Error = resDevuelto.Error;
                    resSalida.Texto = resDevuelto.Texto;
                }
                else
                {
                    resSalida.ObjetoGenerico = null;
                    resSalida.Error = "No fue posible actualizar el libro.";
                }
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Error al actualizar el libro en el cqrs: " + ex.Message;
                resSalida.Texto = "Error al actualizar el libro. Intente de nuevo.";
            }
            return resSalida;
        }
    }
}
