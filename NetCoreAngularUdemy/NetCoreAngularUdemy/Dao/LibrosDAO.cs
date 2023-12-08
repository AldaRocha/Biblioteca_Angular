using NetCoreAngularUdemy.Modelos;
using NetCoreAngularUdemy.Modelos.ViewModels;

namespace NetCoreAngularUdemy.Dao
{
    public class LibrosDAO
    {
        public Resultado AgregarLibro(ProductoViewmodel pvm)
        {
            Resultado resSalida = new Resultado();
            try
            {
                using(CursoAngularNetCoreContext basedatos = new CursoAngularNetCoreContext())
                {
                    Producto p = new Producto();

                    p.Nombre = pvm.nombre;
                    p.Descripcion = pvm.descripcion;
                    p.Precio = pvm.precio;
                    p.Pdf = pvm.pdf;

                    basedatos.Productos.Add(p);
                    basedatos.SaveChanges();

                    resSalida.ObjetoGenerico = p;
                    resSalida.Error = null;
                    resSalida.Texto = null;
                }
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Error al registrar el libro en el dao: " + ex.Message;
                resSalida.Texto = "Error al registrar el libro. Intente de nuevo";
            }
            return resSalida;
        }

        public Resultado ActualizarLibro(ProductoViewmodel pvm, int id)
        {
            Resultado resSalida = new Resultado();
            try
            {
                using(CursoAngularNetCoreContext basedatos = new CursoAngularNetCoreContext())
                {
                    Producto p = null;

                    p = basedatos.Productos.SingleOrDefault(p => p.Id == id);

                    if ( p != null )
                    {
                        p.Nombre = pvm.nombre;
                        p.Descripcion = pvm.descripcion;
                        p.Precio = pvm.precio;
                        p.Pdf = pvm.pdf;

                        basedatos.Entry(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        basedatos.SaveChanges();

                        resSalida.ObjetoGenerico = p;
                        resSalida.Error = null;
                        resSalida.Texto = null;
                    }
                    else
                    {
                        resSalida.ObjetoGenerico = null;
                        resSalida.Error = null;
                        resSalida.Texto = "No hay ningun registro con este id";
                    }
                }
            }
            catch(Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Error al actualizar el libro en el dao: " + ex.Message;
                resSalida.Texto = "Error al actualizar el libro. Intente de nuevo";
            }
            return resSalida;
        }

        public Resultado ObtenerLibros()
        {
            Resultado resSalida = new Resultado();
            try
            {
                using(CursoAngularNetCoreContext basedatos = new CursoAngularNetCoreContext())
                {
                    var lista = basedatos.Productos.ToList();

                    resSalida.ObjetoGenerico = lista;
                    resSalida.Error = null;
                    resSalida.Texto = null;
                }
            }
            catch (Exception ex)
            {
                resSalida.ObjetoGenerico = null;
                resSalida.Error = "Error al obtener los libros en el dao: " + ex.Message;
                resSalida.Texto = "Error al obtener los libros. Intente de nuevo.";
            }
            return resSalida;
        }
    }
}
