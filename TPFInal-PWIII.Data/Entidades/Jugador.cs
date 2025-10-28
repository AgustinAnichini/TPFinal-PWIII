using System;
using System.Collections.Generic;

namespace TPFInal_PWIII.Data.Entidades;

public partial class Jugador
{
    public string Id { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contrasenahash { get; set; } = null!;

    public DateTime Fecharegistro { get; set; }

    public virtual ICollection<Voto> Votos { get; set; } = new List<Voto>();

    public virtual ICollection<Partida> Idpartida { get; set; } = new List<Partida>();
}
