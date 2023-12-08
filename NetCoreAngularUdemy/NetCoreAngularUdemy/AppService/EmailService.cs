namespace NetCoreAngularUdemy.AppService
{
    public class EmailService
    {
        public bool enviarCorreo(string correo, string contenido)
        {
            bool resSalida = false;
            try
            {
                resSalida = true;
            }
            catch(Exception ex)
            {
                throw new Exception("Se produjo un error al registrar el cliente. Intente de nuevo." + ex.Message);
            }
            return resSalida;
        }
    }
}
