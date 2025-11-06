using System;
using System.Collections.Generic;

namespace TPFInal_PWIII.Data.Entidades;

public partial class Voto
{
    public int Id { get; set; }

    public string IdJugador { get; set; } = null!;

    public int IdPartida { get; set; }

    public int IdCapitulo { get; set; }

    public string Opcionelegida { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public virtual Capitulo IdCapituloNavigation { get; set; } = null!;

    public virtual Jugador IdJugadorNavigation { get; set; } = null!;

    public virtual Partidum IdPartidaNavigation { get; set; } = null!;
}
