-- Database.sql


CREATE DATABASE InventarioDB;
GO

USE InventarioDB;
GO

CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Usuario NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash CHAR(64) NOT NULL -- SHA256 hex (64 chars)
);

CREATE TABLE Productos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(200) NOT NULL,
    Codigo NVARCHAR(100) NOT NULL,
    Categoria NVARCHAR(100) NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    Cantidad INT NOT NULL,
    StockMinimo INT NOT NULL
);