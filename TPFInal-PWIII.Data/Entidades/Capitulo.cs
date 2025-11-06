using System;
using System.Collections.Generic;

namespace TPFInal_PWIII.Data.Entidades;

public partial class Capitulo
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Opcion1 { get; set; } = null!;

    public string Opcion2 { get; set; } = null!;

    public int? Idopcion1 { get; set; }

    public int? Idopcion2 { get; set; }

    public int Tiempolimitesegundos { get; set; }

    public bool Esfinal { get; set; }

    public bool Esinicio { get; set; }

    public virtual Capitulo? Idopcion1Navigation { get; set; }

    public virtual Capitulo? Idopcion2Navigation { get; set; }

    public virtual ICollection<Capitulo> InverseIdopcion1Navigation { get; set; } = new List<Capitulo>();

    public virtual ICollection<Capitulo> InverseIdopcion2Navigation { get; set; } = new List<Capitulo>();

    public virtual ICollection<Partidum> Partida { get; set; } = new List<Partidum>();

    public virtual ICollection<Voto> Votos { get; set; } = new List<Voto>();
}
