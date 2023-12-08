namespace NetCoreAngularUdemy.Modelos.ViewModels
{
    public class ProductoViewmodel
    {
        public int id { get; set; }

        public string nombre { get; set; } = null!;

        public decimal precio { get; set; }

        public string descripcion { get; set; } = null!;

        public string pdf { get; set; } = null!;
    }
}
