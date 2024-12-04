using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApiProyecto.Models;

// Clase de Entity Framework Core, gestiona conexión y mapeo base de datos
public partial class ApitaiContext : DbContext
{
    public ApitaiContext()
    {
    }

    // Constructor que acepta opciones de configuración
    public ApitaiContext(DbContextOptions<ApitaiContext> options) 
        : base(options)
    {
    }

    // DbSet representa las tablas en la base de datos
    public virtual DbSet<Pregunta> Preguntas { get; set; }

    public virtual DbSet<Resultado> Resultados { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }     // Método para configurar la conexión a la base de datos vacío porque se configura en Program.cs


    // Método para configurar el modelo de datos y sus relaciones
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // PREGUNTA configuración
        modelBuilder.Entity<Pregunta>(entity =>  
        {
            entity.HasKey(e => e.Id).HasName("PK__PREGUNTA__3213E83FCAA4932E"); // clave primaria

            entity.ToTable("PREGUNTAS"); // Nombre de la tabla en BBDD

            entity.HasIndex(e => e.Bloque, "IX_Preguntas_Bloque"); // Indices para rendimiento consultas
            entity.HasIndex(e => e.Tema, "IX_Preguntas_Tema");

            // Propiedades
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Bloque)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("bloque");
            entity.Property(e => e.Opcion1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("opcion1");
            entity.Property(e => e.Opcion2)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("opcion2");
            entity.Property(e => e.Opcion3)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("opcion3");
            entity.Property(e => e.Opcion4)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("opcion4");
            entity.Property(e => e.Origen)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("origen");
            entity.Property(e => e.Pregunta1)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("pregunta");
            entity.Property(e => e.Respuesta)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("respuesta");
            entity.Property(e => e.Tema)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("tema");
        });

        modelBuilder.Entity<Resultado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RESULTAD__3213E83FF8335B23");

            entity.ToTable("RESULTADOS");

            entity.HasIndex(e => e.Fecha, "IX_Resultados_Fecha");

            entity.HasIndex(e => e.PreguntaId, "IX_Resultados_PreguntaId");

            entity.HasIndex(e => e.UsuarioId, "IX_Resultados_UsuarioId");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Acierto).HasColumnName("acierto");
            entity.Property(e => e.Fecha)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("fecha");
            entity.Property(e => e.PreguntaId).HasColumnName("pregunta_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Pregunta).WithMany(p => p.Resultados)
                .HasForeignKey(d => d.PreguntaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RESULTADOS_PREGUNTAS"); //cambio por FK correcto

            entity.HasOne(d => d.Usuario).WithMany(p => p.Resultados)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RESULTADOS_USUARIOS"); //cambio por FK correcto
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USUARIOS__3213E83F7F8448E5");

            entity.ToTable("USUARIOS");

            entity.HasIndex(e => e.Email, "IX_Usuarios_Email").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__USUARIOS__AB6E6164B30FE1E9").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contraseña");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.TipoUsuario).HasColumnName("tipo_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
