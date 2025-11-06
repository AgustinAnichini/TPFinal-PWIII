using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TPFInal_PWIII.Data.Entidades;

namespace TPFInal_PWIII.Data.Data;

public partial class AventuraBlockchainDbContext : DbContext
{
    public AventuraBlockchainDbContext()
    {
    }

    public AventuraBlockchainDbContext(DbContextOptions<AventuraBlockchainDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Capitulo> Capitulos { get; set; }

    public virtual DbSet<Jugador> Jugadors { get; set; }

    public virtual DbSet<Partidum> Partida { get; set; }

    public virtual DbSet<Voto> Votos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=dpg-d40chkripnbc73cjd5bg-a.oregon-postgres.render.com;Port=5432;Database=aventura_blockchain_db;Username=aventura_blockchain_db_user;Password=AS6xdxfUTBH9xiKUIh23mJScixZGRvGt");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Capitulo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("capitulo_pkey");

            entity.ToTable("capitulo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Esfinal)
                .HasDefaultValue(false)
                .HasColumnName("esfinal");
            entity.Property(e => e.Esinicio)
                .HasDefaultValue(false)
                .HasColumnName("esinicio");
            entity.Property(e => e.Idopcion1).HasColumnName("idopcion1");
            entity.Property(e => e.Idopcion2).HasColumnName("idopcion2");
            entity.Property(e => e.Opcion1)
                .HasMaxLength(500)
                .HasColumnName("opcion1");
            entity.Property(e => e.Opcion2)
                .HasMaxLength(500)
                .HasColumnName("opcion2");
            entity.Property(e => e.Tiempolimitesegundos).HasColumnName("tiempolimitesegundos");

            entity.HasOne(d => d.Idopcion1Navigation).WithMany(p => p.InverseIdopcion1Navigation)
                .HasForeignKey(d => d.Idopcion1)
                .HasConstraintName("fk_capitulo_opcion1");

            entity.HasOne(d => d.Idopcion2Navigation).WithMany(p => p.InverseIdopcion2Navigation)
                .HasForeignKey(d => d.Idopcion2)
                .HasConstraintName("fk_capitulo_opcion2");
        });

        modelBuilder.Entity<Jugador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("jugador_pkey");

            entity.ToTable("jugador");

            entity.HasIndex(e => e.Correo, "jugador_correo_key").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(128)
                .HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            entity.Property(e => e.Contrasenahash).HasColumnName("contrasenahash");
            entity.Property(e => e.Correo)
                .HasMaxLength(256)
                .HasColumnName("correo");
            entity.Property(e => e.Fecharegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecharegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Partidum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("partida_pkey");

            entity.ToTable("partida");

            entity.HasIndex(e => e.Hashprimerbloque, "partida_hashprimerbloque_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");
            entity.Property(e => e.Hashprimerbloque)
                .HasMaxLength(64)
                .HasColumnName("hashprimerbloque");
            entity.Property(e => e.Puntoactualid)
                .HasDefaultValue(1)
                .HasColumnName("puntoactualid");
            entity.Property(e => e.Titulo)
                .HasMaxLength(200)
                .HasColumnName("titulo");

            entity.HasOne(d => d.Puntoactual).WithMany(p => p.Partida)
                .HasForeignKey(d => d.Puntoactualid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_partida_puntoactualid");

            entity.HasMany(d => d.Idjugadors).WithMany(p => p.Idpartida)
                .UsingEntity<Dictionary<string, object>>(
                    "Partidajugador",
                    r => r.HasOne<Jugador>().WithMany()
                        .HasForeignKey("Idjugador")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_partidajugador_jugador"),
                    l => l.HasOne<Partidum>().WithMany()
                        .HasForeignKey("Idpartida")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_partidajugador_partida"),
                    j =>
                    {
                        j.HasKey("Idpartida", "Idjugador").HasName("pk_partidajugador");
                        j.ToTable("partidajugador");
                        j.IndexerProperty<int>("Idpartida").HasColumnName("idpartida");
                        j.IndexerProperty<string>("Idjugador")
                            .HasMaxLength(128)
                            .HasColumnName("idjugador");
                    });
        });

        modelBuilder.Entity<Voto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("voto_pkey");

            entity.ToTable("voto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCapitulo).HasColumnName("id_capitulo");
            entity.Property(e => e.IdJugador)
                .HasMaxLength(128)
                .HasColumnName("id_jugador");
            entity.Property(e => e.IdPartida).HasColumnName("id_partida");
            entity.Property(e => e.Opcionelegida)
                .HasMaxLength(10)
                .HasColumnName("opcionelegida");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timestamp");

            entity.HasOne(d => d.IdCapituloNavigation).WithMany(p => p.Votos)
                .HasForeignKey(d => d.IdCapitulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_voto_capitulo");

            entity.HasOne(d => d.IdJugadorNavigation).WithMany(p => p.Votos)
                .HasForeignKey(d => d.IdJugador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_voto_jugador");

            entity.HasOne(d => d.IdPartidaNavigation).WithMany(p => p.Votos)
                .HasForeignKey(d => d.IdPartida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_voto_partida");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
