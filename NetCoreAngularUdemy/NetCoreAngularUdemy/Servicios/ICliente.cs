using NetCoreAngularUdemy.Modelos;
using NetCoreAngularUdemy.Modelos.ViewModels;

namespace NetCoreAngularUdemy.Servicios
{
    public interface ICliente
    {
        public List<Cliente> DameClientes();
        public Cliente DameCliente(ClienteViewmodel c);
        public void AgregarCliente(ClienteViewmodel c);
        public void EditarCliente(ClienteViewmodel c);
        public void BorrarCliente(String Email);
        public ClienteViewmodel Login(ClienteViewmodel c);
    }
}
