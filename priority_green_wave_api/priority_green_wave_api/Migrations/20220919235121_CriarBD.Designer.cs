﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using priority_green_wave_api.Model;

#nullable disable

namespace priority_green_wave_api.Migrations
{
    [DbContext(typeof(APIContext))]
    [Migration("20220919235121_CriarBD")]
    partial class CriarBD
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("priority_green_wave_api.Model.Catadioptrico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("Id");

                    b.ToTable("catadioptricoModel");
                });

            modelBuilder.Entity("priority_green_wave_api.Model.GrupoSemaforo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("Id");

                    b.ToTable("grupoSemaforoModel");
                });

            modelBuilder.Entity("priority_green_wave_api.Model.Localizacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("DistanciaSemaforo")
                        .HasColumnType("float");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("Regional")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("catadioptricoId")
                        .HasColumnType("int");

                    b.Property<int>("semaforoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("catadioptricoId");

                    b.HasIndex("semaforoId");

                    b.ToTable("localizacaoModel");
                });

            modelBuilder.Entity("priority_green_wave_api.Model.Semaforo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("GrupoSemaforoId")
                        .HasColumnType("int");

                    b.Property<int>("Orientacao")
                        .HasColumnType("int");

                    b.Property<int>("Sentido")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GrupoSemaforoId");

                    b.ToTable("semaforoModel");
                });

            modelBuilder.Entity("priority_green_wave_api.Model.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("MotoristaEmergencia")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("usuarioModel");
                });

            modelBuilder.Entity("priority_green_wave_api.Model.Veiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Ano")
                        .HasColumnType("int");

                    b.Property<bool>("EstadoEmergencia")
                        .HasColumnType("bit");

                    b.Property<string>("Fabricante")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoVeiculo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<bool>("VeiculoEmergencia")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("veiculoModel");
                });

            modelBuilder.Entity("priority_green_wave_api.Model.Localizacao", b =>
                {
                    b.HasOne("priority_green_wave_api.Model.Catadioptrico", "catadioptrico")
                        .WithMany()
                        .HasForeignKey("catadioptricoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("priority_green_wave_api.Model.Semaforo", "semaforo")
                        .WithMany()
                        .HasForeignKey("semaforoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("catadioptrico");

                    b.Navigation("semaforo");
                });

            modelBuilder.Entity("priority_green_wave_api.Model.Semaforo", b =>
                {
                    b.HasOne("priority_green_wave_api.Model.GrupoSemaforo", null)
                        .WithMany("semaforos")
                        .HasForeignKey("GrupoSemaforoId");
                });

            modelBuilder.Entity("priority_green_wave_api.Model.Veiculo", b =>
                {
                    b.HasOne("priority_green_wave_api.Model.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("priority_green_wave_api.Model.GrupoSemaforo", b =>
                {
                    b.Navigation("semaforos");
                });
#pragma warning restore 612, 618
        }
    }
}
