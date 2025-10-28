﻿using System;
using System.Collections.Generic;

namespace TPFInal_PWIII.Data.Entidades;

public partial class Partida
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public DateTime Fechacreacion { get; set; }

    public string Hashprimerbloque { get; set; } = null!;

    public virtual ICollection<Voto> Votos { get; set; } = new List<Voto>();

    public virtual ICollection<Jugador> Idjugadors { get; set; } = new List<Jugador>();
}
