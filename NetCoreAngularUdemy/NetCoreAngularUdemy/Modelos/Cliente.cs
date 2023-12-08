using System;
using System.Collections.Generic;

namespace NetCoreAngularUdemy.Modelos;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public DateTime FechaAlta { get; set; }

    public DateTime? FechaBaja { get; set; }

    public string Rol { get; set; } = null!;

    public int Estatus { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
