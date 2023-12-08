using NetCoreAngularUdemy.Modelos.ViewModels;

namespace NetCoreAngularUdemy.Servicios
{
    public interface IUsuarioAPI
    {
        public UsuarioAPIViewModel Autenticacion(AuthAPI authAPI);
    }
}
