CREATE DATABASE ListaTarefas;
GO

USE ListaTarefas;
GO

CREATE TABLE Tarefas (
    Id INT PRIMARY KEY IDENTITY,
    Descricao NVARCHAR(200) NOT NULL,
    Concluida BIT NOT NULL
);