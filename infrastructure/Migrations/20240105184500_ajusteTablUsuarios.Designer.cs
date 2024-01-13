﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using infrastructure.Context;

#nullable disable

namespace infrastructure.Migrations
{
    [DbContext(typeof(UsuarioContext))]
    [Migration("20240105184500_ajusteTablUsuarios")]
    partial class ajusteTablUsuarios
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Business.Entities.Usuario", b =>
                {
                    b.Property<Guid>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");

                    b.Property<string>("Cargo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("fecha_creacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("fecha_modificacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
