IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Ambulante] (
    [Id] uniqueidentifier NOT NULL,
    [Protocolo] varchar(256) NOT NULL,
    [FormaAtuacao] varchar(max) NULL,
    [Local] varchar(max) NULL,
    [Atividade] varchar(max) NULL,
    [Data_Cadastro] datetime2 NULL,
    [Ultima_Alteracao] datetime2 NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Ambulante] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Empresa] (
    [Id] uniqueidentifier NOT NULL,
    [CNPJ] varchar(18) NOT NULL,
    [Data_Abertura] datetime2 NULL,
    [Nome_Empresarial] varchar(max) NULL,
    [Nome_Fantasia] varchar(max) NULL,
    [CNAE_Principal] varchar(max) NULL,
    [Atividade_Principal] varchar(max) NULL,
    [Atividade_Secundarias] varchar(max) NULL,
    [CEP] varchar(10) NULL,
    [Logradouro] varchar(max) NULL,
    [Numero] varchar(max) NULL,
    [Complemento] varchar(max) NULL,
    [Bairro] varchar(max) NULL,
    [Municipio] varchar(max) NULL,
    [UF] varchar(2) NULL,
    [Email] varchar(max) NULL,
    [Telefone] varchar(max) NULL,
    [Situacao_Cadastral] varchar(max) NULL,
    CONSTRAINT [PK_Empresa] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Evento] (
    [Id] uniqueidentifier NOT NULL,
    [Codigo] int NOT NULL,
    [Tipo] nvarchar(max) NULL,
    [Nome] nvarchar(max) NULL,
    [Formato] nvarchar(max) NULL,
    [Data] datetime2 NULL,
    [Descricao] nvarchar(max) NULL,
    [Owner] nvarchar(max) NULL,
    [Parceiro] nvarchar(max) NULL,
    [Lotacao] int NOT NULL,
    [Situacao] int NULL,
    CONSTRAINT [PK_Evento] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Pessoa] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(max) COLLATE Latin1_General_CI_AI NOT NULL,
    [Nome_Social] varchar(max) NULL,
    [Data_Nascimento] datetime2 NOT NULL,
    [CPF] varchar(14) NOT NULL,
    [RG] varchar(max) NULL,
    [RG_Emissor] varchar(max) NULL,
    [RG_Emissor_UF] varchar(2) NULL,
    [Genero] nvarchar(max) NULL,
    [Deficiencia] nvarchar(max) NULL,
    [CEP] varchar(max) NULL,
    [Logradouro] varchar(max) COLLATE Latin1_General_CI_AI NULL,
    [Numero] varchar(max) NULL,
    [Complemento] varchar(max) NULL,
    [Bairro] varchar(max) COLLATE Latin1_General_CI_AI NULL,
    [Cidade] varchar(max) NULL,
    [UF] varchar(2) NULL,
    [Tel_Movel] varchar(max) NULL,
    [Tel_Fixo] varchar(max) NULL,
    [Email] varchar(max) NULL,
    [Data_Cadastro] datetime2 NULL,
    [Ultima_Alteracao] datetime2 NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Pessoa] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Planer] (
    [Id] uniqueidentifier NOT NULL,
    [Segunda] varchar(max) NULL,
    [Terca] varchar(max) NULL,
    [Quarta] varchar(max) NULL,
    [Quinta] varchar(max) NULL,
    [Sexta] varchar(max) NULL,
    [Sabado] varchar(max) NULL,
    [ProximaSemana] varchar(max) NULL,
    [Prioridades] varchar(max) NULL,
    [Anotacao] varchar(max) NULL,
    [DataInicial] datetime2 NULL,
    [DataFinal] datetime2 NULL,
    [Owner_AppUser_Id] nvarchar(max) NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Planer] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Protocolos] (
    [Id] uniqueidentifier NOT NULL,
    [Numero] varchar(max) NULL,
    [Modulo] varchar(max) NULL,
    [AppUserId] nvarchar(max) NULL,
    [Data] datetime2 NULL,
    CONSTRAINT [PK_Protocolos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [RaeSebrae] (
    [Id] uniqueidentifier NOT NULL,
    [RAE] nvarchar(max) NULL,
    CONSTRAINT [PK_RaeSebrae] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Secretaria] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(max) NULL,
    [Owner] varchar(max) NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Secretaria] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [StatusAtendimento] (
    [Id] uniqueidentifier NOT NULL,
    [UnserName] varchar(256) NULL,
    [Online] bit NOT NULL,
    CONSTRAINT [PK_StatusAtendimento] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Tipos] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(max) NULL,
    [Owner] varchar(max) NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Tipos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [DIA] (
    [Id] uniqueidentifier NOT NULL,
    [InscricaoMunicipal] int NOT NULL,
    [Autorizacao] varchar(256) NOT NULL,
    [Veiculo] varchar(max) NULL,
    [Emissao] datetime2 NULL,
    [Validade] datetime2 NULL,
    [Processo] varchar(max) NULL,
    [Situacao] varchar(max) NULL,
    [DiaDesde] datetime2 NULL,
    [AmbulanteId] uniqueidentifier NULL,
    CONSTRAINT [PK_DIA] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DIA_Ambulante_AmbulanteId] FOREIGN KEY ([AmbulanteId]) REFERENCES [Ambulante] ([Id])
);
GO

CREATE TABLE [Empregos] (
    [Id] uniqueidentifier NOT NULL,
    [Data] datetime2 NULL,
    [Ocupacao] varchar(max) NULL,
    [Experiencia] bit NOT NULL,
    [Salario] decimal(18,2) NOT NULL,
    [Pagamento] varchar(50) NULL,
    [Vagas] int NOT NULL,
    [Status] varchar(50) NULL,
    [EmpresaId] uniqueidentifier NULL,
    CONSTRAINT [PK_Empregos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Empregos_Empresa_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresa] ([Id])
);
GO

CREATE TABLE [AmbulantePessoa] (
    [AmbulanteId] uniqueidentifier NOT NULL,
    [PessoasId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AmbulantePessoa] PRIMARY KEY ([AmbulanteId], [PessoasId]),
    CONSTRAINT [FK_AmbulantePessoa_Ambulante_AmbulanteId] FOREIGN KEY ([AmbulanteId]) REFERENCES [Ambulante] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AmbulantePessoa_Pessoa_PessoasId] FOREIGN KEY ([PessoasId]) REFERENCES [Pessoa] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Inscricao] (
    [Id] uniqueidentifier NOT NULL,
    [Numero] int NOT NULL,
    [AplicationUser_Id] nvarchar(max) NULL,
    [Data_Inscricao] datetime2 NULL,
    [Presente] bit NOT NULL,
    [ParticipanteId] uniqueidentifier NULL,
    [EmpresaId] uniqueidentifier NULL,
    [EventoId] uniqueidentifier NULL,
    CONSTRAINT [PK_Inscricao] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Inscricao_Empresa_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresa] ([Id]),
    CONSTRAINT [FK_Inscricao_Evento_EventoId] FOREIGN KEY ([EventoId]) REFERENCES [Evento] ([Id]),
    CONSTRAINT [FK_Inscricao_Pessoa_ParticipanteId] FOREIGN KEY ([ParticipanteId]) REFERENCES [Pessoa] ([Id])
);
GO

CREATE TABLE [Atendimento] (
    [Id] uniqueidentifier NOT NULL,
    [Protocolo] varchar(256) NOT NULL,
    [Data] datetime2 NULL,
    [DataF] datetime2 NULL,
    [Setor] varchar(max) NULL,
    [Canal] varchar(max) NULL,
    [Servicos] varchar(max) NULL,
    [Descricao] varchar(max) NULL,
    [Status] varchar(max) NULL,
    [Ultima_Alteracao] datetime2 NULL,
    [Ativo] bit NOT NULL,
    [Owner_AppUser_Id] nvarchar(max) NULL,
    [PessoaId] uniqueidentifier NULL,
    [EmpresaId] uniqueidentifier NULL,
    [SebraeId] uniqueidentifier NULL,
    CONSTRAINT [PK_Atendimento] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Atendimento_Empresa_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresa] ([Id]),
    CONSTRAINT [FK_Atendimento_Pessoa_PessoaId] FOREIGN KEY ([PessoaId]) REFERENCES [Pessoa] ([Id]),
    CONSTRAINT [FK_Atendimento_RaeSebrae_SebraeId] FOREIGN KEY ([SebraeId]) REFERENCES [RaeSebrae] ([Id])
);
GO

CREATE TABLE [Parceiros] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(max) NULL,
    [SecretariaId] uniqueidentifier NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Parceiros] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Parceiros_Secretaria_SecretariaId] FOREIGN KEY ([SecretariaId]) REFERENCES [Secretaria] ([Id])
);
GO

CREATE TABLE [Setor] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(max) NULL,
    [SecretariaId] uniqueidentifier NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Setor] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Setor_Secretaria_SecretariaId] FOREIGN KEY ([SecretariaId]) REFERENCES [Secretaria] ([Id])
);
GO

CREATE TABLE [Canal] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(max) NULL,
    [SecretariaId] uniqueidentifier NULL,
    [SetorId] uniqueidentifier NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Canal] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Canal_Secretaria_SecretariaId] FOREIGN KEY ([SecretariaId]) REFERENCES [Secretaria] ([Id]),
    CONSTRAINT [FK_Canal_Setor_SetorId] FOREIGN KEY ([SetorId]) REFERENCES [Setor] ([Id])
);
GO

CREATE TABLE [Servico] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(max) NULL,
    [SecretariaId] uniqueidentifier NULL,
    [SetorId] uniqueidentifier NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Servico] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Servico_Secretaria_SecretariaId] FOREIGN KEY ([SecretariaId]) REFERENCES [Secretaria] ([Id]),
    CONSTRAINT [FK_Servico_Setor_SetorId] FOREIGN KEY ([SetorId]) REFERENCES [Setor] ([Id])
);
GO

CREATE UNIQUE INDEX [IX_Ambulante_Protocolo] ON [Ambulante] ([Protocolo]);
GO

CREATE INDEX [IX_AmbulantePessoa_PessoasId] ON [AmbulantePessoa] ([PessoasId]);
GO

CREATE INDEX [IX_Atendimento_EmpresaId] ON [Atendimento] ([EmpresaId]);
GO

CREATE INDEX [IX_Atendimento_PessoaId] ON [Atendimento] ([PessoaId]);
GO

CREATE UNIQUE INDEX [IX_Atendimento_Protocolo] ON [Atendimento] ([Protocolo]);
GO

CREATE INDEX [IX_Atendimento_SebraeId] ON [Atendimento] ([SebraeId]);
GO

CREATE INDEX [IX_Canal_SecretariaId] ON [Canal] ([SecretariaId]);
GO

CREATE INDEX [IX_Canal_SetorId] ON [Canal] ([SetorId]);
GO

CREATE INDEX [IX_DIA_AmbulanteId] ON [DIA] ([AmbulanteId]);
GO

CREATE UNIQUE INDEX [IX_DIA_Autorizacao] ON [DIA] ([Autorizacao]);
GO

CREATE INDEX [IX_Empregos_EmpresaId] ON [Empregos] ([EmpresaId]);
GO

CREATE UNIQUE INDEX [IX_Empresa_CNPJ] ON [Empresa] ([CNPJ]);
GO

CREATE INDEX [IX_Inscricao_EmpresaId] ON [Inscricao] ([EmpresaId]);
GO

CREATE INDEX [IX_Inscricao_EventoId] ON [Inscricao] ([EventoId]);
GO

CREATE INDEX [IX_Inscricao_ParticipanteId] ON [Inscricao] ([ParticipanteId]);
GO

CREATE INDEX [IX_Parceiros_SecretariaId] ON [Parceiros] ([SecretariaId]);
GO

CREATE UNIQUE INDEX [IX_Pessoa_CPF] ON [Pessoa] ([CPF]);
GO

CREATE INDEX [IX_Servico_SecretariaId] ON [Servico] ([SecretariaId]);
GO

CREATE INDEX [IX_Servico_SetorId] ON [Servico] ([SetorId]);
GO

CREATE INDEX [IX_Setor_SecretariaId] ON [Setor] ([SecretariaId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220617002046_ApStart', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Evento]') AND [c].[name] = N'Tipo');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Evento] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Evento] ALTER COLUMN [Tipo] varchar(max) NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Evento]') AND [c].[name] = N'Owner');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Evento] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Evento] ALTER COLUMN [Owner] varchar(max) NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Evento]') AND [c].[name] = N'Nome');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Evento] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Evento] ALTER COLUMN [Nome] varchar(max) NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Evento]') AND [c].[name] = N'Formato');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Evento] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Evento] ALTER COLUMN [Formato] varchar(255) NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Evento]') AND [c].[name] = N'Descricao');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Evento] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Evento] ALTER COLUMN [Descricao] varchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220704222428_AddEventConfig', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Atendimento] ADD [Anonimo] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220807015305_Add_A_Anonimo', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Empregos]') AND [c].[name] = N'Experiencia');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Empregos] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Empregos] ALTER COLUMN [Experiencia] varchar(50) NULL;
GO

ALTER TABLE [Empregos] ADD [Genero] varchar(50) NULL;
GO

ALTER TABLE [Empregos] ADD [Inclusivo] varchar(50) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220807163730_UpdateEmpregos', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Empregos] ADD [PessoaId] uniqueidentifier NULL;
GO

CREATE INDEX [IX_Empregos_PessoaId] ON [Empregos] ([PessoaId]);
GO

ALTER TABLE [Empregos] ADD CONSTRAINT [FK_Empregos_Pessoa_PessoaId] FOREIGN KEY ([PessoaId]) REFERENCES [Pessoa] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220822224953_Add-Pessoa-Empregos', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Tipos].[Owner]', N'Tipo', N'COLUMN';
GO

EXEC sp_rename N'[Secretaria].[Owner]', N'Acronimo', N'COLUMN';
GO

ALTER TABLE [Tipos] ADD [OwnerId] uniqueidentifier NULL;
GO

ALTER TABLE [Secretaria] ADD [OwnerId] uniqueidentifier NULL;
GO

CREATE TABLE [Prefeitura] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(max) NULL,
    [Cidade] varchar(max) NULL,
    [UF] varchar(2) NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Prefeitura] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_Tipos_OwnerId] ON [Tipos] ([OwnerId]);
GO

CREATE INDEX [IX_Secretaria_OwnerId] ON [Secretaria] ([OwnerId]);
GO

ALTER TABLE [Secretaria] ADD CONSTRAINT [FK_Secretaria_Prefeitura_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Prefeitura] ([Id]);
GO

ALTER TABLE [Tipos] ADD CONSTRAINT [FK_Tipos_Secretaria_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Secretaria] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221002141327_Add-Prefeitura', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Canal] DROP CONSTRAINT [FK_Canal_Secretaria_SecretariaId];
GO

ALTER TABLE [Canal] DROP CONSTRAINT [FK_Canal_Setor_SetorId];
GO

ALTER TABLE [Parceiros] DROP CONSTRAINT [FK_Parceiros_Secretaria_SecretariaId];
GO

ALTER TABLE [Secretaria] DROP CONSTRAINT [FK_Secretaria_Prefeitura_OwnerId];
GO

ALTER TABLE [Servico] DROP CONSTRAINT [FK_Servico_Secretaria_SecretariaId];
GO

ALTER TABLE [Servico] DROP CONSTRAINT [FK_Servico_Setor_SetorId];
GO

ALTER TABLE [Tipos] DROP CONSTRAINT [FK_Tipos_Secretaria_OwnerId];
GO

DROP TABLE [Prefeitura];
GO

DROP TABLE [Setor];
GO

DROP INDEX [IX_Servico_SecretariaId] ON [Servico];
GO

DROP INDEX [IX_Secretaria_OwnerId] ON [Secretaria];
GO

DROP INDEX [IX_Canal_SecretariaId] ON [Canal];
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Servico]') AND [c].[name] = N'SecretariaId');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Servico] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Servico] DROP COLUMN [SecretariaId];
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Secretaria]') AND [c].[name] = N'OwnerId');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Secretaria] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Secretaria] DROP COLUMN [OwnerId];
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Canal]') AND [c].[name] = N'SecretariaId');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Canal] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Canal] DROP COLUMN [SecretariaId];
GO

EXEC sp_rename N'[Tipos].[OwnerId]', N'DominioId', N'COLUMN';
GO

EXEC sp_rename N'[Tipos].[IX_Tipos_OwnerId]', N'IX_Tipos_DominioId', N'INDEX';
GO

EXEC sp_rename N'[Servico].[SetorId]', N'DominioId', N'COLUMN';
GO

EXEC sp_rename N'[Servico].[IX_Servico_SetorId]', N'IX_Servico_DominioId', N'INDEX';
GO

EXEC sp_rename N'[Parceiros].[SecretariaId]', N'DominioId', N'COLUMN';
GO

EXEC sp_rename N'[Parceiros].[IX_Parceiros_SecretariaId]', N'IX_Parceiros_DominioId', N'INDEX';
GO

EXEC sp_rename N'[Canal].[SetorId]', N'DominioId', N'COLUMN';
GO

EXEC sp_rename N'[Canal].[IX_Canal_SetorId]', N'IX_Canal_DominioId', N'INDEX';
GO

ALTER TABLE [Secretaria] ADD [Dominio] nvarchar(max) NULL;
GO

ALTER TABLE [Secretaria] ADD [Hierarquia] int NULL;
GO

ALTER TABLE [Canal] ADD CONSTRAINT [FK_Canal_Secretaria_DominioId] FOREIGN KEY ([DominioId]) REFERENCES [Secretaria] ([Id]);
GO

ALTER TABLE [Parceiros] ADD CONSTRAINT [FK_Parceiros_Secretaria_DominioId] FOREIGN KEY ([DominioId]) REFERENCES [Secretaria] ([Id]);
GO

ALTER TABLE [Servico] ADD CONSTRAINT [FK_Servico_Secretaria_DominioId] FOREIGN KEY ([DominioId]) REFERENCES [Secretaria] ([Id]);
GO

ALTER TABLE [Tipos] ADD CONSTRAINT [FK_Tipos_Secretaria_DominioId] FOREIGN KEY ([DominioId]) REFERENCES [Secretaria] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221004230530_AddOrg-RemSetorPref', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221005001745_start-update-domain', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Secretaria]') AND [c].[name] = N'Dominio');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Secretaria] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Secretaria] ALTER COLUMN [Dominio] uniqueidentifier NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221005161947_model-organizacao-dominio-as-guid', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [BPPContratos] (
    [Id] uniqueidentifier NOT NULL,
    [Numero] int NOT NULL,
    [Data] datetime2 NULL,
    [Valor] decimal(18,2) NOT NULL,
    [Situacao] int NOT NULL,
    [DataSituacao] datetime2 NULL,
    [ClienteId] uniqueidentifier NULL,
    [EmpresaId] uniqueidentifier NULL,
    [Pagamento] int NOT NULL,
    [Descricao] varchar(255) NULL,
    [AppUser] varchar(255) NULL,
    CONSTRAINT [PK_BPPContratos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BPPContratos_Empresa_EmpresaId] FOREIGN KEY ([EmpresaId]) REFERENCES [Empresa] ([Id]),
    CONSTRAINT [FK_BPPContratos_Pessoa_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Pessoa] ([Id])
);
GO

CREATE TABLE [ERenegociacoes] (
    [Id] uniqueidentifier NOT NULL,
    [ContratoId] uniqueidentifier NULL,
    [Data] datetime2 NULL,
    [Situacao] int NOT NULL,
    CONSTRAINT [PK_ERenegociacoes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ERenegociacoes_BPPContratos_ContratoId] FOREIGN KEY ([ContratoId]) REFERENCES [BPPContratos] ([Id])
);
GO

CREATE INDEX [IX_BPPContratos_ClienteId] ON [BPPContratos] ([ClienteId]);
GO

CREATE INDEX [IX_BPPContratos_EmpresaId] ON [BPPContratos] ([EmpresaId]);
GO

CREATE INDEX [IX_ERenegociacoes_ContratoId] ON [ERenegociacoes] ([ContratoId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221129033629_migrationBPP', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [ERenegociacoes] DROP CONSTRAINT [FK_ERenegociacoes_BPPContratos_ContratoId];
GO

ALTER TABLE [ERenegociacoes] DROP CONSTRAINT [PK_ERenegociacoes];
GO

EXEC sp_rename N'[ERenegociacoes]', N'BPPRenegociacoes';
GO

EXEC sp_rename N'[BPPRenegociacoes].[IX_ERenegociacoes_ContratoId]', N'IX_BPPRenegociacoes_ContratoId', N'INDEX';
GO

ALTER TABLE [BPPRenegociacoes] ADD CONSTRAINT [PK_BPPRenegociacoes] PRIMARY KEY ([Id]);
GO

ALTER TABLE [BPPRenegociacoes] ADD CONSTRAINT [FK_BPPRenegociacoes_BPPContratos_ContratoId] FOREIGN KEY ([ContratoId]) REFERENCES [BPPContratos] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221129044343_migrationBPP01', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [BPPContratos] ADD [UltimaAlteracao] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221204055805_migrationsBPP02', N'6.0.8');
GO

COMMIT;
GO

