﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sim.Data.Migrations
{
    public partial class ApStart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ambulante",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Protocolo = table.Column<string>(type: "varchar(256)", nullable: false),
                    FormaAtuacao = table.Column<string>(type: "varchar(max)", nullable: true),
                    Local = table.Column<string>(type: "varchar(max)", nullable: true),
                    Atividade = table.Column<string>(type: "varchar(max)", nullable: true),
                    Data_Cadastro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ultima_Alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ambulante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CNPJ = table.Column<string>(type: "varchar(18)", nullable: false),
                    Data_Abertura = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nome_Empresarial = table.Column<string>(type: "varchar(max)", nullable: true),
                    Nome_Fantasia = table.Column<string>(type: "varchar(max)", nullable: true),
                    CNAE_Principal = table.Column<string>(type: "varchar(max)", nullable: true),
                    Atividade_Principal = table.Column<string>(type: "varchar(max)", nullable: true),
                    Atividade_Secundarias = table.Column<string>(type: "varchar(max)", nullable: true),
                    CEP = table.Column<string>(type: "varchar(10)", nullable: true),
                    Logradouro = table.Column<string>(type: "varchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "varchar(max)", nullable: true),
                    Complemento = table.Column<string>(type: "varchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "varchar(max)", nullable: true),
                    Municipio = table.Column<string>(type: "varchar(max)", nullable: true),
                    UF = table.Column<string>(type: "varchar(2)", nullable: true),
                    Email = table.Column<string>(type: "varchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "varchar(max)", nullable: true),
                    Situacao_Cadastral = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Formato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parceiro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lotacao = table.Column<int>(type: "int", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(max)", nullable: false, collation: "Latin1_General_CI_AI"),
                    Nome_Social = table.Column<string>(type: "varchar(max)", nullable: true),
                    Data_Nascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CPF = table.Column<string>(type: "varchar(14)", nullable: false),
                    RG = table.Column<string>(type: "varchar(max)", nullable: true),
                    RG_Emissor = table.Column<string>(type: "varchar(max)", nullable: true),
                    RG_Emissor_UF = table.Column<string>(type: "varchar(2)", nullable: true),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deficiencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEP = table.Column<string>(type: "varchar(max)", nullable: true),
                    Logradouro = table.Column<string>(type: "varchar(max)", nullable: true, collation: "Latin1_General_CI_AI"),
                    Numero = table.Column<string>(type: "varchar(max)", nullable: true),
                    Complemento = table.Column<string>(type: "varchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "varchar(max)", nullable: true, collation: "Latin1_General_CI_AI"),
                    Cidade = table.Column<string>(type: "varchar(max)", nullable: true),
                    UF = table.Column<string>(type: "varchar(2)", nullable: true),
                    Tel_Movel = table.Column<string>(type: "varchar(max)", nullable: true),
                    Tel_Fixo = table.Column<string>(type: "varchar(max)", nullable: true),
                    Email = table.Column<string>(type: "varchar(max)", nullable: true),
                    Data_Cadastro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ultima_Alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Segunda = table.Column<string>(type: "varchar(max)", nullable: true),
                    Terca = table.Column<string>(type: "varchar(max)", nullable: true),
                    Quarta = table.Column<string>(type: "varchar(max)", nullable: true),
                    Quinta = table.Column<string>(type: "varchar(max)", nullable: true),
                    Sexta = table.Column<string>(type: "varchar(max)", nullable: true),
                    Sabado = table.Column<string>(type: "varchar(max)", nullable: true),
                    ProximaSemana = table.Column<string>(type: "varchar(max)", nullable: true),
                    Prioridades = table.Column<string>(type: "varchar(max)", nullable: true),
                    Anotacao = table.Column<string>(type: "varchar(max)", nullable: true),
                    DataInicial = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataFinal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Owner_AppUser_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Protocolos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Numero = table.Column<string>(type: "varchar(max)", nullable: true),
                    Modulo = table.Column<string>(type: "varchar(max)", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Protocolos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RaeSebrae",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RAE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaeSebrae", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Secretaria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(max)", nullable: true),
                    Owner = table.Column<string>(type: "varchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secretaria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusAtendimento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnserName = table.Column<string>(type: "varchar(256)", nullable: true),
                    Online = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusAtendimento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tipos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(max)", nullable: true),
                    Owner = table.Column<string>(type: "varchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DIA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InscricaoMunicipal = table.Column<int>(type: "int", nullable: false),
                    Autorizacao = table.Column<string>(type: "varchar(256)", nullable: false),
                    Veiculo = table.Column<string>(type: "varchar(max)", nullable: true),
                    Emissao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Validade = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Processo = table.Column<string>(type: "varchar(max)", nullable: true),
                    Situacao = table.Column<string>(type: "varchar(max)", nullable: true),
                    DiaDesde = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AmbulanteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DIA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DIA_Ambulante_AmbulanteId",
                        column: x => x.AmbulanteId,
                        principalTable: "Ambulante",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Empregos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ocupacao = table.Column<string>(type: "varchar(max)", nullable: true),
                    Experiencia = table.Column<bool>(type: "bit", nullable: false),
                    Salario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Pagamento = table.Column<string>(type: "varchar(50)", nullable: true),
                    Vagas = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: true),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empregos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empregos_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AmbulantePessoa",
                columns: table => new
                {
                    AmbulanteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PessoasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmbulantePessoa", x => new { x.AmbulanteId, x.PessoasId });
                    table.ForeignKey(
                        name: "FK_AmbulantePessoa_Ambulante_AmbulanteId",
                        column: x => x.AmbulanteId,
                        principalTable: "Ambulante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmbulantePessoa_Pessoa_PessoasId",
                        column: x => x.PessoasId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inscricao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    AplicationUser_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data_Inscricao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Presente = table.Column<bool>(type: "bit", nullable: false),
                    ParticipanteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EventoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscricao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inscricao_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inscricao_Evento_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Evento",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inscricao_Pessoa_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Pessoa",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Atendimento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Protocolo = table.Column<string>(type: "varchar(256)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataF = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Setor = table.Column<string>(type: "varchar(max)", nullable: true),
                    Canal = table.Column<string>(type: "varchar(max)", nullable: true),
                    Servicos = table.Column<string>(type: "varchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(max)", nullable: true),
                    Status = table.Column<string>(type: "varchar(max)", nullable: true),
                    Ultima_Alteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Owner_AppUser_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PessoaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SebraeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atendimento_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Atendimento_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Atendimento_RaeSebrae_SebraeId",
                        column: x => x.SebraeId,
                        principalTable: "RaeSebrae",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Parceiros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(max)", nullable: true),
                    SecretariaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parceiros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parceiros_Secretaria_SecretariaId",
                        column: x => x.SecretariaId,
                        principalTable: "Secretaria",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Setor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(max)", nullable: true),
                    SecretariaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Setor_Secretaria_SecretariaId",
                        column: x => x.SecretariaId,
                        principalTable: "Secretaria",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Canal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(max)", nullable: true),
                    SecretariaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SetorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Canal_Secretaria_SecretariaId",
                        column: x => x.SecretariaId,
                        principalTable: "Secretaria",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Canal_Setor_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Setor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Servico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(max)", nullable: true),
                    SecretariaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SetorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servico_Secretaria_SecretariaId",
                        column: x => x.SecretariaId,
                        principalTable: "Secretaria",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Servico_Setor_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Setor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ambulante_Protocolo",
                table: "Ambulante",
                column: "Protocolo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AmbulantePessoa_PessoasId",
                table: "AmbulantePessoa",
                column: "PessoasId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_EmpresaId",
                table: "Atendimento",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_PessoaId",
                table: "Atendimento",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_Protocolo",
                table: "Atendimento",
                column: "Protocolo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Atendimento_SebraeId",
                table: "Atendimento",
                column: "SebraeId");

            migrationBuilder.CreateIndex(
                name: "IX_Canal_SecretariaId",
                table: "Canal",
                column: "SecretariaId");

            migrationBuilder.CreateIndex(
                name: "IX_Canal_SetorId",
                table: "Canal",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_DIA_AmbulanteId",
                table: "DIA",
                column: "AmbulanteId");

            migrationBuilder.CreateIndex(
                name: "IX_DIA_Autorizacao",
                table: "DIA",
                column: "Autorizacao",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empregos_EmpresaId",
                table: "Empregos",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_CNPJ",
                table: "Empresa",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inscricao_EmpresaId",
                table: "Inscricao",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscricao_EventoId",
                table: "Inscricao",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscricao_ParticipanteId",
                table: "Inscricao",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Parceiros_SecretariaId",
                table: "Parceiros",
                column: "SecretariaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_CPF",
                table: "Pessoa",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servico_SecretariaId",
                table: "Servico",
                column: "SecretariaId");

            migrationBuilder.CreateIndex(
                name: "IX_Servico_SetorId",
                table: "Servico",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Setor_SecretariaId",
                table: "Setor",
                column: "SecretariaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmbulantePessoa");

            migrationBuilder.DropTable(
                name: "Atendimento");

            migrationBuilder.DropTable(
                name: "Canal");

            migrationBuilder.DropTable(
                name: "DIA");

            migrationBuilder.DropTable(
                name: "Empregos");

            migrationBuilder.DropTable(
                name: "Inscricao");

            migrationBuilder.DropTable(
                name: "Parceiros");

            migrationBuilder.DropTable(
                name: "Planer");

            migrationBuilder.DropTable(
                name: "Protocolos");

            migrationBuilder.DropTable(
                name: "Servico");

            migrationBuilder.DropTable(
                name: "StatusAtendimento");

            migrationBuilder.DropTable(
                name: "Tipos");

            migrationBuilder.DropTable(
                name: "RaeSebrae");

            migrationBuilder.DropTable(
                name: "Ambulante");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "Evento");

            migrationBuilder.DropTable(
                name: "Pessoa");

            migrationBuilder.DropTable(
                name: "Setor");

            migrationBuilder.DropTable(
                name: "Secretaria");
        }
    }
}
