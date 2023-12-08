using NetCoreAngularUdemy.Modelos;
using NetCoreAngularUdemy.Modelos.ViewModels;

namespace NetCoreAngularUdemy.Servicios
{
    public interface IProductos
    {
        public List<Producto> DameProductos();
        public void AgregarPedido(PedidoViewModel p);

        public List<PedidoDetalleViewModel> PedidosClientes(ClienteViewmodel cliente);
    }
}
