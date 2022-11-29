﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sim.Data.Context;

#nullable disable

namespace Sim.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AmbulantePessoa", b =>
                {
                    b.Property<Guid>("AmbulanteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PessoasId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AmbulanteId", "PessoasId");

                    b.HasIndex("PessoasId");

                    b.ToTable("AmbulantePessoa");
                });

            modelBuilder.Entity("Sim.Domain.BancoPovo.Models.EContrato", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AppUser")
                        .HasColumnType("varchar(255)");

                    b.Property<Guid?>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Data")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataSituacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(255)");

                    b.Property<Guid?>("EmpresaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<int>("Pagamento")
                        .HasColumnType("int");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("EmpresaId");

                    b.ToTable("BPPContratos", (string)null);
                });

            modelBuilder.Entity("Sim.Domain.BancoPovo.Models.ERenegociacoes", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ContratoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContratoId");

                    b.ToTable("ERenegociacoes");
                });

            modelBuilder.Entity("Sim.Domain.Entity.Ambulante", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Atividade")
                        .HasColumnType("varchar(max)");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Data_Cadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("FormaAtuacao")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Local")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Protocolo")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<DateTime?>("Ultima_Alteracao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Protocolo")
                        .IsUnique();

                    b.ToTable("Ambulante", (string)null);
                });

            modelBuilder.Entity("Sim.Domain.Entity.Contador", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AppUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Modulo")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Numero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Protocolos", (string)null);
                });

            modelBuilder.Entity("Sim.Domain.Entity.DIA", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AmbulanteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Autorizacao")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<DateTime?>("DiaDesde")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Emissao")
                        .HasColumnType("datetime2");

                    b.Property<int>("InscricaoMunicipal")
                        .HasColumnType("int");

                    b.Property<string>("Processo")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Situacao")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("Validade")
                        .HasColumnType("datetime2");

                    b.Property<string>("Veiculo")
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AmbulanteId");

                    b.HasIndex("Autorizacao")
                        .IsUnique();

                    b.ToTable("DIA", (string)null);
                });

            modelBuilder.Entity("Sim.Domain.Entity.EAtendimento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Anonimo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Canal")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("Data")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataF")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(max)");

                    b.Property<Guid?>("EmpresaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Owner_AppUser_Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PessoaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Protocolo")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<Guid?>("SebraeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Servicos")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Setor")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("Ultima_Alteracao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("PessoaId");

                    b.HasIndex("Protocolo")
                        .IsUnique();

                    b.HasIndex("SebraeId");

                    b.ToTable("Atendimento", (string)null);
                });

            modelBuilder.Entity("Sim.Domain.Entity.Empregos", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Data")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("EmpresaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Experiencia")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Genero")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Inclusivo")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Ocupacao")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Pagamento")
                        .HasColumnType("varchar(50)");

                    b.Property<Guid?>("PessoaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Salario")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Status")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Vagas")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("PessoaId");

                    b.ToTable("Empregos", (string)null);
                });

            modelBuilder.Entity("Sim.Domain.Entity.Empresas", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Atividade_Principal")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Atividade_Secundarias")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Bairro")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("CEP")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("CNAE_Principal")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("varchar(18)");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("Data_Abertura")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Logradouro")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Municipio")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Nome_Empresarial")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Nome_Fantasia")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Numero")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Situacao_Cadastral")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Telefone")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("UF")
                        .HasColumnType("varchar(2)");

                    b.HasKey("Id");

                    b.HasIndex("CNPJ")
                        .IsUnique();

                    b.ToTable("Empresa", (string)null);
                });

            modelBuilder.Entity("Sim.Domain.Entity.Inscricao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AplicationUser_Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Data_Inscricao")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("EmpresaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EventoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<Guid?>("ParticipanteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Presente")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("EventoId");

                    b.HasIndex("ParticipanteId");

                    b.ToTable("Inscricao", (string)null);
                });

            modelBuilder.Entity("Sim.Domain.Entity.Pessoa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Bairro")
                        .HasColumnType("varchar(max)")
                        .UseCollation("Latin1_General_CI_AI");

                    b.Property<string>("CEP")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Cidade")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("Data_Cadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Data_Nascimento")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Deficiencia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Genero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logradouro")
                        .HasColumnType("varchar(max)")
                        .UseCollation("Latin1_General_CI_AI");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .UseCollation("Latin1_General_CI_AI");

                    b.Property<string>("Nome_Social")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Numero")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("RG")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("RG_Emissor")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("RG_Emissor_UF")
                        .HasColumnType("varchar(2)");

                    b.Property<string>("Tel_Fixo")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Tel_Movel")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("UF")
                        .HasColumnType("varchar(2)");

                    b.Property<DateTime?>("Ultima_Alteracao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CPF")
                        .IsUnique();

                    b.ToTable("Pessoa", (string)null);
                });

            modelBuilder.Entity("Sim.Domain.Entity.Planner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Anotacao")
                        .HasColumnType("varchar(max)");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataFinal")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataInicial")
                        .HasColumnType("datetime2");

                    b.Property<string>("Owner_AppUser_Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prioridades")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("ProximaSemana")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Quarta")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Quinta")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Sabado")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Segunda")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Sexta")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Terca")
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Planer", (string)null);
                });

            modelBuilder.Entity("Sim.Domain.Entity.RaeSebrae", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RAE")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RaeSebrae", (string)null);
                });

            modelBuilder.Entity("Sim.Domain.Entity.StatusAtendimento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Online")
                        .HasColumnType("bit");

                    b.Property<string>("UnserName")
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.ToTable("StatusAtendimento", (string)null);
                });

            modelBuilder.Entity("Sim.Domain.Evento.Model.EEvento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Formato")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Lotacao")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Owner")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Parceiro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Situacao")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Evento", (string)null);
                });

            modelBuilder.Entity("Sim.Domain.Evento.Model.EParceiro", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<Guid?>("DominioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DominioId");

                    b.ToTable("Parceiros", (string)null);
                });

            modelBuilder.Entity("Sim.Domain.Evento.Model.ETipo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<Guid?>("DominioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Tipo")
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DominioId");

                    b.ToTable("Tipos", (string)null);
                });

            modelBuilder.Entity("Sim.Domain.Organizacao.Model.ECanal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<Guid?>("DominioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DominioId");

                    b.ToTable("Canal", (string)null);
                });

            modelBuilder.Entity("Sim.Domain.Organizacao.Model.EOrganizacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Acronimo")
                        .HasColumnType("varchar(max)");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<Guid?>("Dominio")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Hierarquia")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Secretaria", (string)null);
                });

            modelBuilder.Entity("Sim.Domain.Organizacao.Model.EServico", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<Guid?>("DominioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DominioId");

                    b.ToTable("Servico", (string)null);
                });

            modelBuilder.Entity("AmbulantePessoa", b =>
                {
                    b.HasOne("Sim.Domain.Entity.Ambulante", null)
                        .WithMany()
                        .HasForeignKey("AmbulanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sim.Domain.Entity.Pessoa", null)
                        .WithMany()
                        .HasForeignKey("PessoasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sim.Domain.BancoPovo.Models.EContrato", b =>
                {
                    b.HasOne("Sim.Domain.Entity.Pessoa", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId");

                    b.HasOne("Sim.Domain.Entity.Empresas", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaId");

                    b.Navigation("Cliente");

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("Sim.Domain.BancoPovo.Models.ERenegociacoes", b =>
                {
                    b.HasOne("Sim.Domain.BancoPovo.Models.EContrato", "Contrato")
                        .WithMany("Renegociacaoes")
                        .HasForeignKey("ContratoId");

                    b.Navigation("Contrato");
                });

            modelBuilder.Entity("Sim.Domain.Entity.DIA", b =>
                {
                    b.HasOne("Sim.Domain.Entity.Ambulante", "Ambulante")
                        .WithMany("DIAs")
                        .HasForeignKey("AmbulanteId");

                    b.Navigation("Ambulante");
                });

            modelBuilder.Entity("Sim.Domain.Entity.EAtendimento", b =>
                {
                    b.HasOne("Sim.Domain.Entity.Empresas", "Empresa")
                        .WithMany("Atendimentos")
                        .HasForeignKey("EmpresaId");

                    b.HasOne("Sim.Domain.Entity.Pessoa", "Pessoa")
                        .WithMany("Atendimentos")
                        .HasForeignKey("PessoaId");

                    b.HasOne("Sim.Domain.Entity.RaeSebrae", "Sebrae")
                        .WithMany()
                        .HasForeignKey("SebraeId");

                    b.Navigation("Empresa");

                    b.Navigation("Pessoa");

                    b.Navigation("Sebrae");
                });

            modelBuilder.Entity("Sim.Domain.Entity.Empregos", b =>
                {
                    b.HasOne("Sim.Domain.Entity.Empresas", "Empresa")
                        .WithMany("VagasEmpregos")
                        .HasForeignKey("EmpresaId");

                    b.HasOne("Sim.Domain.Entity.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("PessoaId");

                    b.Navigation("Empresa");

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("Sim.Domain.Entity.Inscricao", b =>
                {
                    b.HasOne("Sim.Domain.Entity.Empresas", "Empresa")
                        .WithMany("Inscricoes")
                        .HasForeignKey("EmpresaId");

                    b.HasOne("Sim.Domain.Evento.Model.EEvento", "Evento")
                        .WithMany("Inscritos")
                        .HasForeignKey("EventoId");

                    b.HasOne("Sim.Domain.Entity.Pessoa", "Participante")
                        .WithMany("Inscricoes")
                        .HasForeignKey("ParticipanteId");

                    b.Navigation("Empresa");

                    b.Navigation("Evento");

                    b.Navigation("Participante");
                });

            modelBuilder.Entity("Sim.Domain.Evento.Model.EParceiro", b =>
                {
                    b.HasOne("Sim.Domain.Organizacao.Model.EOrganizacao", "Dominio")
                        .WithMany()
                        .HasForeignKey("DominioId");

                    b.Navigation("Dominio");
                });

            modelBuilder.Entity("Sim.Domain.Evento.Model.ETipo", b =>
                {
                    b.HasOne("Sim.Domain.Organizacao.Model.EOrganizacao", "Dominio")
                        .WithMany()
                        .HasForeignKey("DominioId");

                    b.Navigation("Dominio");
                });

            modelBuilder.Entity("Sim.Domain.Organizacao.Model.ECanal", b =>
                {
                    b.HasOne("Sim.Domain.Organizacao.Model.EOrganizacao", "Dominio")
                        .WithMany("Canais")
                        .HasForeignKey("DominioId");

                    b.Navigation("Dominio");
                });

            modelBuilder.Entity("Sim.Domain.Organizacao.Model.EServico", b =>
                {
                    b.HasOne("Sim.Domain.Organizacao.Model.EOrganizacao", "Dominio")
                        .WithMany("Servicos")
                        .HasForeignKey("DominioId");

                    b.Navigation("Dominio");
                });

            modelBuilder.Entity("Sim.Domain.BancoPovo.Models.EContrato", b =>
                {
                    b.Navigation("Renegociacaoes");
                });

            modelBuilder.Entity("Sim.Domain.Entity.Ambulante", b =>
                {
                    b.Navigation("DIAs");
                });

            modelBuilder.Entity("Sim.Domain.Entity.Empresas", b =>
                {
                    b.Navigation("Atendimentos");

                    b.Navigation("Inscricoes");

                    b.Navigation("VagasEmpregos");
                });

            modelBuilder.Entity("Sim.Domain.Entity.Pessoa", b =>
                {
                    b.Navigation("Atendimentos");

                    b.Navigation("Inscricoes");
                });

            modelBuilder.Entity("Sim.Domain.Evento.Model.EEvento", b =>
                {
                    b.Navigation("Inscritos");
                });

            modelBuilder.Entity("Sim.Domain.Organizacao.Model.EOrganizacao", b =>
                {
                    b.Navigation("Canais");

                    b.Navigation("Servicos");
                });
#pragma warning restore 612, 618
        }
    }
}
