-- =============================================================================
-- Desafio ViaCEP: Script DDL de Criação da Estrutura do Banco de Dados
-- Sistema Gerenciador de Endereços com Integração ViaCEP e Autenticação
-- Banco de Dados: Microsoft SQL Server / LocalDB
-- =============================================================================

-- 1. Criação do Banco de Dados
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ViaCepDb')
BEGIN
    CREATE DATABASE [ViaCepDb];
END
GO

USE [ViaCepDb];
GO

-- 2. Tabela de Usuários (Autenticação e Cadastro)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuarios]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Usuarios] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [Nome] NVARCHAR(150) NOT NULL,
        [NomeUsuario] NVARCHAR(50) NOT NULL,
        [SenhaHash] NVARCHAR(MAX) NOT NULL,
        CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [UQ_Usuarios_NomeUsuario] UNIQUE NONCLUSTERED ([NomeUsuario] ASC)
    );
END
GO

-- 3. Tabela de Endereços (CRUD de Endereços e Integração ViaCEP)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Enderecos]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Enderecos] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [Cep] NVARCHAR(9) NOT NULL,
        [Logradouro] NVARCHAR(200) NOT NULL,
        [Complemento] NVARCHAR(100) NULL,
        [Bairro] NVARCHAR(100) NOT NULL,
        [Cidade] NVARCHAR(100) NOT NULL,
        [Uf] NVARCHAR(2) NOT NULL,
        [Numero] NVARCHAR(20) NOT NULL,
        [UsuarioId] INT NOT NULL,
        CONSTRAINT [PK_Enderecos] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_Enderecos_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId]) 
            REFERENCES [dbo].[Usuarios] ([Id]) ON DELETE CASCADE
    );
END
GO

-- 4. Índice para Otimização de Consultas de Endereços por Usuário caso queira aplicar para testes com BD com muitos registros
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Enderecos_UsuarioId' AND object_id = OBJECT_ID(N'[dbo].[Enderecos]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Enderecos_UsuarioId] ON [dbo].[Enderecos] ([UsuarioId] ASC);
END
GO
